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
    public void H5Lget_valTest()
    {
        var symPath = string.Join("/", H5LFixture.m_utf8strings);
        var symPathStringPtr = Marshal.StringToCoTaskMemUTF8(symPath);
        var abcdStringPtr = Marshal.StringToHGlobalAnsi("/A/B/C/D");

        Assert.True(H5L.create_soft(symPathStringPtr, m_v0_test_file, abcdStringPtr, H5LFixture.m_lcpl) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, abcdStringPtr, ref info) >= 0);
        Assert.True(info.type == H5L.type_t.SOFT);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        int size = info.u.val_size.ToInt32();
        Assert.Equal(70, size);

        // the library appends a null terminator (weired!)
        Assert.Equal(symPath.Length + 1, size);

        byte[] buf = new byte[size];

        GCHandle hnd = GCHandle.Alloc(buf, GCHandleType.Pinned);
        Assert.True(H5L.get_val(m_v0_test_file, abcdStringPtr, hnd.AddrOfPinnedObject(), new nint(buf.Length)) >= 0);
        hnd.Free();

        var bufString = Encoding.UTF8.GetString(buf);
        Assert.Equal(symPath, bufString);

        Assert.True(H5L.create_soft(symPathStringPtr, m_v2_test_file, abcdStringPtr, H5LFixture.m_lcpl) >= 0);

        info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v2_test_file, abcdStringPtr, ref info) >= 0);
        Assert.True(info.type == H5L.type_t.SOFT);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        size = info.u.val_size.ToInt32();
        Assert.Equal(70, size);

        // the library appends a null terminator
        Assert.Equal(symPath.Length + 1, size);

        buf = new byte[size - 1];

        hnd = GCHandle.Alloc(buf, GCHandleType.Pinned);
        Assert.True(H5L.get_val(m_v2_test_file, abcdStringPtr, hnd.AddrOfPinnedObject(), new nint(buf.Length)) >= 0);
        hnd.Free();

        bufString = Encoding.UTF8.GetString(buf);
        Assert.Equal(symPath, bufString);

        Marshal.FreeCoTaskMem(symPathStringPtr);
        Marshal.FreeHGlobal(abcdStringPtr);
    }
}
