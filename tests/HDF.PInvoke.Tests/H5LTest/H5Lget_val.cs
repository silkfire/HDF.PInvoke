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

using HDF5;
using Xunit;
using System;
using System.Runtime.InteropServices;
using System.Text;

public partial class H5LTest
{
    [Fact]
    public void H5Lget_valTest1()
    {
        string sym_path = string.Join("/", H5LFixture.m_utf8strings);
        byte[] bytes = Encoding.UTF8.GetBytes(sym_path);

        Assert.True(H5L.create_soft(bytes, m_v0_test_file, Encoding.ASCII.GetBytes("/A/B/C/D"), H5LFixture.m_lcpl) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, "/A/B/C/D", ref info) >= 0);
        Assert.True(info.type == H5L.type_t.SOFT);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        int size = info.u.val_size.ToInt32();
        Assert.True(size == 70);

        // the library appends a null terminator (weired!)
        Assert.True(size == bytes.Length + 1);

        byte[] buf = new byte[size];

        GCHandle hnd = GCHandle.Alloc(buf, GCHandleType.Pinned);
        Assert.True(H5L.get_val(m_v0_test_file, "/A/B/C/D", hnd.AddrOfPinnedObject(), new IntPtr(buf.Length)) >= 0);
        hnd.Free();

        for (int i = 0; i < buf.Length - 1; ++i)
        {
            Assert.True(buf[i] == bytes[i]);
        }

        Assert.True(H5L.create_soft(bytes, m_v2_test_file, Encoding.ASCII.GetBytes("/A/B/C/D"), H5LFixture.m_lcpl) >= 0);

        info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v2_test_file, "/A/B/C/D", ref info) >= 0);
        Assert.True(info.type == H5L.type_t.SOFT);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        size = info.u.val_size.ToInt32();
        Assert.True(size == 70);

        // the library appends a null terminator
        Assert.True(size == bytes.Length + 1);

        buf = new byte[size - 1];

        hnd = GCHandle.Alloc(buf, GCHandleType.Pinned);
        Assert.True(H5L.get_val(m_v2_test_file, "/A/B/C/D", hnd.AddrOfPinnedObject(), new IntPtr(buf.Length)) >= 0);
        hnd.Free();

        for (int i = 0; i < buf.Length - 1; ++i)
        {
            Assert.True(buf[i] == bytes[i]);
        }
    }
}
