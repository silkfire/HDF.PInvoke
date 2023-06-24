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
    public void H5Lunpack_elink_valTest1()
    {
        var v0ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v0_class_file_name);
        var v2ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v2_class_file_name);
        var pathSeparatorStringPtr = Marshal.StringToHGlobalAnsi("/");
        var abcStringPtr = Marshal.StringToHGlobalAnsi("A/B/C");

        // v0 file format

        Assert.True(H5L.create_external(v0ClassFileNameStringPtr, pathSeparatorStringPtr, m_v0_test_file, abcStringPtr, H5LFixture.m_lcpl) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, abcStringPtr, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        nint size = new nint(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        nint buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != nint.Zero);
        Assert.True(H5L.get_val(m_v0_test_file, abcStringPtr, buf, size) >= 0);

        uint flags = 0;
        nint filenamePtr = nint.Zero, objPathPtr = nint.Zero;
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, filenamePtr, objPathPtr) >= 0);

        Assert.Equal(H5LFixture.m_v0_class_file_name, Marshal.PtrToStringAnsi(filenamePtr));
        Assert.Equal("/", Marshal.PtrToStringAnsi(objPathPtr));

        Marshal.FreeHGlobal(buf);

        // v2 file format

        Assert.True(H5L.create_external(v2ClassFileNameStringPtr, pathSeparatorStringPtr, m_v2_test_file, abcStringPtr, H5LFixture.m_lcpl) >= 0);

        info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v2_test_file, abcStringPtr, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        size = new nint(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != nint.Zero);
        Assert.True(H5L.get_val(m_v2_test_file, abcStringPtr, buf, size) >= 0);

        flags = 0;
        filenamePtr = nint.Zero;
        objPathPtr = nint.Zero;
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, filenamePtr, objPathPtr) >= 0);

        Assert.Equal(H5LFixture.m_v2_class_file_name, Marshal.PtrToStringAnsi(filenamePtr));
        Assert.Equal("/", Marshal.PtrToStringAnsi(objPathPtr));

        Marshal.FreeHGlobal(buf);

        Marshal.FreeHGlobal(v0ClassFileNameStringPtr);
        Marshal.FreeHGlobal(v2ClassFileNameStringPtr);
        Marshal.FreeHGlobal(pathSeparatorStringPtr);
        Marshal.FreeHGlobal(abcStringPtr);
    }

    [Fact]
    public void H5Lunpack_elink_valTest2()
    {
        var firstUtf8StringPtr = Marshal.StringToCoTaskMemUTF8(H5LFixture.m_utf8strings[0]);
        var v0ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v0_class_file_name);
        var v2ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v2_class_file_name);
        var pathSeparatorStringPtr = Marshal.StringToHGlobalAnsi("/");

        // v0 file format

        Assert.True(H5L.create_external(v0ClassFileNameStringPtr, pathSeparatorStringPtr, m_v0_test_file, firstUtf8StringPtr, H5LFixture.m_lcpl_utf8) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, firstUtf8StringPtr, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        nint size = new nint(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        nint buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != nint.Zero);
        Assert.True(H5L.get_val(m_v0_test_file, firstUtf8StringPtr, buf, size) >= 0);

        uint flags = 0;
        nint filenamePtr = nint.Zero, objPathPtr = nint.Zero;
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, filenamePtr, objPathPtr) >= 0);

        Assert.Equal(H5LFixture.m_v0_class_file_name, Marshal.PtrToStringAnsi(filenamePtr));
        Assert.Equal("/", Marshal.PtrToStringAnsi(objPathPtr));

        Marshal.FreeHGlobal(buf);

        // v2 file format

        Assert.True(H5L.create_external(v2ClassFileNameStringPtr, pathSeparatorStringPtr, m_v2_test_file, firstUtf8StringPtr, H5LFixture.m_lcpl_utf8) >= 0);

        info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v2_test_file, firstUtf8StringPtr, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        size = new nint(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != nint.Zero);
        Assert.True(H5L.get_val(m_v2_test_file, firstUtf8StringPtr, buf, size) >= 0);

        flags = 0;
        filenamePtr = nint.Zero;
        objPathPtr = nint.Zero;
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, filenamePtr, filenamePtr) >= 0);

        Assert.Equal(H5LFixture.m_v2_class_file_name, Marshal.PtrToStringAnsi(filenamePtr));
        Assert.Equal("/", Marshal.PtrToStringAnsi(objPathPtr));

        Marshal.FreeHGlobal(buf);

        Marshal.FreeCoTaskMem(firstUtf8StringPtr);
        Marshal.FreeHGlobal(v0ClassFileNameStringPtr);
        Marshal.FreeHGlobal(v2ClassFileNameStringPtr);
        Marshal.FreeHGlobal(pathSeparatorStringPtr);
    }

    [Fact]
    public void H5Lunpack_elink_valTest3()
    {
        var firstUtf8StringPtr = Marshal.StringToCoTaskMemUTF8(H5LFixture.m_utf8strings[0]);
        var v0ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v0_class_file_name);
        var v2ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v2_class_file_name);
        var pathSeparatorStringPtr = Marshal.StringToHGlobalAnsi("/");
        
        // v0 file format

        Assert.True(H5G.close(H5G.create(H5LFixture.m_v0_class_file, firstUtf8StringPtr, H5LFixture.m_lcpl_utf8)) >= 0);

        Assert.True(H5L.create_external(v0ClassFileNameStringPtr, firstUtf8StringPtr, m_v0_test_file, firstUtf8StringPtr, H5LFixture.m_lcpl_utf8) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, firstUtf8StringPtr, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        nint size = new nint(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        nint buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != nint.Zero);
        Assert.True(H5L.get_val(m_v0_test_file, firstUtf8StringPtr, buf, size) >= 0);

        uint flags = 0;
        nint filenamePtr = nint.Zero, objPathPtr = nint.Zero;
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, filenamePtr, objPathPtr) >= 0);

        Assert.Equal(H5LFixture.m_v0_class_file_name, Marshal.PtrToStringAnsi(filenamePtr));

        // the elink value is packed like this:
        // <file name>\0<object path>\0
        // the whole thing is of info.u.val_size 

        int count = size.ToInt32() - (int)(objPathPtr.ToInt64() + 1 - filenamePtr.ToInt64()) - 1;
        byte[] obj_path_buf = new byte[count];
        Marshal.Copy(objPathPtr, obj_path_buf, 0, count);

        Assert.True(Encoding.UTF8.GetString(obj_path_buf) == H5LFixture.m_utf8strings[0], $"{Encoding.UTF8.GetString(obj_path_buf)}");

        Marshal.FreeHGlobal(buf);

        // v2 file format

        Assert.True(H5G.close(H5G.create(H5LFixture.m_v2_class_file, firstUtf8StringPtr, H5LFixture.m_lcpl_utf8)) >= 0);

        Assert.True(H5L.create_external(v2ClassFileNameStringPtr, firstUtf8StringPtr, m_v2_test_file, firstUtf8StringPtr, H5LFixture.m_lcpl_utf8) >= 0);

        info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v2_test_file, firstUtf8StringPtr, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        size = new nint(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != nint.Zero);
        Assert.True(H5L.get_val(m_v2_test_file, firstUtf8StringPtr, buf, size) >= 0);

        flags = 0;
        filenamePtr = nint.Zero;
        objPathPtr = nint.Zero;
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, filenamePtr, objPathPtr) >= 0);

        Assert.Equal(H5LFixture.m_v2_class_file_name, Marshal.PtrToStringAnsi(filenamePtr));

        // the elink value is packed like this:
        // <file name>\0<object path>\0
        // the whole thing is of info.u.val_size 

        count = size.ToInt32() - (int)(objPathPtr.ToInt64() + 1 - filenamePtr.ToInt64()) - 1;
        obj_path_buf = new byte[count];
        Marshal.Copy(objPathPtr, obj_path_buf, 0, count);

        Assert.True(Encoding.UTF8.GetString(obj_path_buf) == H5LFixture.m_utf8strings[0], $"{Encoding.UTF8.GetString(obj_path_buf)}");

        Marshal.FreeHGlobal(buf);

        Marshal.FreeCoTaskMem(firstUtf8StringPtr);
        Marshal.FreeHGlobal(v0ClassFileNameStringPtr);
        Marshal.FreeHGlobal(v2ClassFileNameStringPtr);
        Marshal.FreeHGlobal(pathSeparatorStringPtr);
    }
}
