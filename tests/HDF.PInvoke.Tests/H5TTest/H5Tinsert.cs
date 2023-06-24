/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 * Copyright by The HDF Group.                                               *
 * Copyright by the Board of Trustees of the University of Illinois.         *
 * All rights reserved.                                                      *
 *                                                                           *
 * This file is part of HDF5.  The full HDF5 copyright notice, including     *
 * terms governing use, modification, and redistribution, is contained in    *
 * the files COPYING and Copyright.html.  COPYING can be found at the root   *
 * of the source code distribution tree; Copyright.html can be found at the  *
 * root level of an installed copy of the electronic HDF5 document set and   *
 * is linked from the top-level documents page.  It can also be found at     *
 * http://hdfgroup.org/HDF5/doc/Copyright.html.  If you do not have          *
 * access to either file, you may request a copy from help@hdfgroup.org.     *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

namespace HDF.PInvoke.Tests;

using hsize_t = System.UInt64;
using hid_t = System.Int64;

using HDF5;

using Xunit;

using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public partial class H5TTest
{
    [Fact]
    public void H5TinsertTest1()
    {
        var keyStringPtr = Marshal.StringToHGlobalAnsi("key");
        var valStringPtr = Marshal.StringToHGlobalAnsi("value");
        var keyValStringPtr = Marshal.StringToHGlobalAnsi("KeyVal");

        // a fixed-length string type
        hid_t fls = H5T.create(H5T.class_t.STRING, new nint(16));
        Assert.True(fls >= 0);
        Assert.True(H5T.is_variable_str(fls) == 0);

        // a variable-length string type
        hid_t vls = H5T.create(H5T.class_t.STRING, H5T.VARIABLE);
        Assert.True(vls >= 0);
        Assert.True(H5T.is_variable_str(vls) > 0);

        // a key-value compound
        nint size = new nint(16 + nint.Size);
        hid_t kvt = H5T.create(H5T.class_t.COMPOUND, size);
        Assert.True(H5T.insert(kvt, keyStringPtr, nint.Zero, fls) >= 0);
        Assert.True(H5T.insert(kvt, valStringPtr, new nint(16), vls) >= 0);
        Assert.True(H5T.close(vls) >= 0);
        Assert.True(H5T.close(fls) >= 0);

        // create a key-value dataset (3 elements)

        hid_t fsp = H5S.create_simple(1, new hsize_t[] { 3 }, null);
        Assert.True(fsp >= 0);

        hid_t dset = H5D.create(H5TFixture.m_v2_class_file, keyValStringPtr, kvt, fsp);
        Assert.True(dset >= 0);
        Assert.True(H5S.close(fsp) >= 0);

        // write a 3 elements

        string[] keys = {
                            "Key0123456789ABC", "Key0123456789DEF", "Key0123456789GHI"
                        };

        nint[] values = new nint[3];
        values[0] = Marshal.StringToHGlobalAnsi("I am a managed String!");
        values[1] = Marshal.StringToHGlobalAnsi("I am also a managed String!");
        values[2] = Marshal.StringToHGlobalAnsi("I am another managed String!");

        MemoryStream ms = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(ms);

        for (int i = 0; i < 3; ++i)
        {
            writer.Write(Encoding.ASCII.GetBytes(keys[i]));
            if (nint.Size == 8)
            {
                writer.Write(values[i].ToInt64());
            }
            else
            {
                writer.Write(values[i].ToInt32());
            }
        }

        byte[] wdata = ms.ToArray();
        GCHandle hnd = GCHandle.Alloc(wdata, GCHandleType.Pinned);

        Assert.True(H5D.write(dset, kvt, H5S.ALL, H5S.ALL, H5P.DEFAULT, hnd.AddrOfPinnedObject()) >= 0);

        hnd.Free();

        // now read it back

        byte[] rdata = new byte[3 * size.ToInt32()];
        hnd = GCHandle.Alloc(rdata, GCHandleType.Pinned);

        Assert.True(H5D.read(dset, kvt, H5S.ALL, H5S.ALL, H5P.DEFAULT, hnd.AddrOfPinnedObject()) >= 0);

        hnd.Free();

        // check it out

        MemoryStream ms1 = new MemoryStream(rdata);
        BinaryReader reader = new BinaryReader(ms1);

        for (int i = 0; i < 3; ++i)
        {
            string k = Encoding.ASCII.GetString(reader.ReadBytes(16));
            Assert.Equal(k, keys[i]);
            nint ptr = nint.Zero;
            if (nint.Size == 8)
            {
                ptr = new nint(reader.ReadInt64());
            }
            else
            {
                ptr = new nint(reader.ReadInt32());
            }

            string v = Marshal.PtrToStringAnsi(ptr);
            Assert.Equal(v, Marshal.PtrToStringAnsi(values[i]));
            Marshal.FreeHGlobal(ptr);
            Marshal.FreeHGlobal(values[i]);
        }

        Assert.True(H5D.close(dset) >= 0);
        Assert.True(H5T.close(kvt) >= 0);

        Marshal.FreeHGlobal(keyStringPtr);
        Marshal.FreeHGlobal(valStringPtr);
        Marshal.FreeHGlobal(keyValStringPtr);
    }
}
