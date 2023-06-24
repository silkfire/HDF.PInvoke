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
using size_t = nint;
using ssize_t = nint;
using hid_t = System.Int64;

using HDF5;

using Xunit;

using System.Runtime.InteropServices;

public partial class H5LTest
{
    [Fact]
    public void H5Lget_name_by_idxTest1()
    {
        var v0ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v0_class_file_name);
        var slashStringPtr = Marshal.StringToHGlobalAnsi("/");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var abStringPtr = Marshal.StringToHGlobalAnsi("AB");
        var abcStringPtr = Marshal.StringToHGlobalAnsi("ABC");
        var dotStringPtr = Marshal.StringToHGlobalAnsi(".");
        var v2ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v2_class_file_name);

        Assert.True(H5L.create_external(v0ClassFileNameStringPtr, slashStringPtr, m_v0_test_file, aStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(v0ClassFileNameStringPtr, slashStringPtr, m_v0_test_file, abStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(v0ClassFileNameStringPtr, slashStringPtr, m_v0_test_file, abcStringPtr, H5LFixture.m_lcpl) >= 0);

        size_t buf_size = ssize_t.Zero;
        ssize_t size = H5L.get_name_by_idx(m_v0_test_file, dotStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, nint.Zero, buf_size);
        Assert.True(size.ToInt32() == 2);
        buf_size = new ssize_t(size.ToInt32() + 1);
        var buf = Marshal.AllocHGlobal(buf_size);
        size = H5L.get_name_by_idx(m_v0_test_file, dotStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, buf, buf_size);
        Assert.Equal("AB", Marshal.PtrToStringAnsi(buf));
        Marshal.FreeHGlobal(buf);

        Assert.True(H5L.create_external(v2ClassFileNameStringPtr, slashStringPtr, m_v2_test_file, aStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(v2ClassFileNameStringPtr, slashStringPtr, m_v2_test_file, abStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(v2ClassFileNameStringPtr, slashStringPtr, m_v2_test_file, abcStringPtr, H5LFixture.m_lcpl) >= 0);

        buf_size = ssize_t.Zero;
        size = H5L.get_name_by_idx(m_v2_test_file, dotStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, nint.Zero, buf_size);
        Assert.True(size.ToInt32() == 2);
        buf_size = new ssize_t(size.ToInt32() + 1);

        buf = Marshal.AllocHGlobal(buf_size);
        size = H5L.get_name_by_idx(m_v2_test_file, dotStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, buf, buf_size);
        Assert.Equal("AB", Marshal.PtrToStringAnsi(buf));
        Marshal.FreeHGlobal(buf);

        Marshal.FreeHGlobal(v0ClassFileNameStringPtr);
        Marshal.FreeHGlobal(slashStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(abStringPtr);
        Marshal.FreeHGlobal(abcStringPtr);
        Marshal.FreeHGlobal(dotStringPtr);
        Marshal.FreeHGlobal(v2ClassFileNameStringPtr);
    }

    [Fact]
    public void H5Lget_name_by_idxTest2()
    {
        var v0ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v0_class_file_name);
        var slashStringPtr = Marshal.StringToHGlobalAnsi("/");
        var dotStringPtr = Marshal.StringToHGlobalAnsi(".");

        hid_t lcpl = H5P.copy(H5LFixture.m_lcpl);
        Assert.True(lcpl >= 0);
        Assert.True(H5P.set_char_encoding(lcpl, H5T.cset_t.UTF8) >= 0);

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            var utf8StringPtr = Marshal.StringToCoTaskMemUTF8(H5LFixture.m_utf8strings[i]);

            Assert.True(H5L.create_external(v0ClassFileNameStringPtr, slashStringPtr, H5LFixture.m_v0_class_file, utf8StringPtr, lcpl) >= 0);

            Marshal.FreeCoTaskMem(utf8StringPtr);
        }

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            size_t buf_size = ssize_t.Zero;
            ssize_t size = H5L.get_name_by_idx(m_v0_test_file, dotStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, (hsize_t)i, nint.Zero, buf_size);
            buf_size = new ssize_t(size.ToInt32() + 1);
            
            var buf = Marshal.AllocHGlobal(buf_size);
            size = H5L.get_name_by_idx(m_v0_test_file, dotStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, (hsize_t)i, buf, buf_size);
            Marshal.FreeHGlobal(buf);
        }

        Assert.True(H5P.close(lcpl) >= 0);

        Marshal.FreeHGlobal(v0ClassFileNameStringPtr);
        Marshal.FreeHGlobal(slashStringPtr);
        Marshal.FreeHGlobal(dotStringPtr);
    }
}
