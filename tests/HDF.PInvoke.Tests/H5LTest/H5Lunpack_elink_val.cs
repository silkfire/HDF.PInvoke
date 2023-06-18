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
        // v0 file format

        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, "/", m_v0_test_file, "A/B/C", H5LFixture.m_lcpl) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, "A/B/C", ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        IntPtr size = new IntPtr(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        IntPtr buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != IntPtr.Zero);
        Assert.True(H5L.get_val(m_v0_test_file, "A/B/C", buf, size) >= 0);

        uint flags = 0;
        IntPtr filename = new IntPtr();
        IntPtr obj_path = new IntPtr();
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, ref filename, ref obj_path) >= 0);

        Assert.True(Marshal.PtrToStringAnsi(filename) == H5LFixture.m_v0_class_file_name);
        Assert.True(Marshal.PtrToStringAnsi(obj_path) == "/");

        Marshal.FreeHGlobal(buf);

        // v2 file format

        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, "/", m_v2_test_file, "A/B/C", H5LFixture.m_lcpl) >= 0);

        info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v2_test_file, "A/B/C", ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        size = new IntPtr(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != IntPtr.Zero);
        Assert.True(H5L.get_val(m_v2_test_file, "A/B/C", buf, size) >= 0);

        flags = 0;
        filename = new IntPtr();
        obj_path = new IntPtr();
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, ref filename, ref obj_path) >= 0);

        Assert.True(Marshal.PtrToStringAnsi(filename) == H5LFixture.m_v2_class_file_name);
        Assert.True(Marshal.PtrToStringAnsi(obj_path) == "/");

        Marshal.FreeHGlobal(buf);
    }

    [Fact]
    public void H5Lunpack_elink_valTest2()
    {
        // v0 file format

        byte[] bytes = Encoding.UTF8.GetBytes(H5LFixture.m_utf8strings[0]);

        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, Encoding.ASCII.GetBytes("/"), m_v0_test_file, bytes, H5LFixture.m_lcpl_utf8) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, bytes, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        IntPtr size = new IntPtr(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        IntPtr buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != IntPtr.Zero);
        Assert.True(H5L.get_val(m_v0_test_file, bytes, buf, size) >= 0);

        uint flags = 0;
        IntPtr filename = new IntPtr();
        IntPtr obj_path = new IntPtr();
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, ref filename, ref obj_path) >= 0);

        Assert.True(Marshal.PtrToStringAnsi(filename) == H5LFixture.m_v0_class_file_name);
        Assert.True(Marshal.PtrToStringAnsi(obj_path) == "/");

        Marshal.FreeHGlobal(buf);

        // v2 file format

        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, Encoding.ASCII.GetBytes("/"), m_v2_test_file, bytes, H5LFixture.m_lcpl_utf8) >= 0);

        info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v2_test_file, bytes, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        size = new IntPtr(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != IntPtr.Zero);
        Assert.True(H5L.get_val(m_v2_test_file, bytes, buf, size) >= 0);

        flags = 0;
        filename = new IntPtr();
        obj_path = new IntPtr();
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, ref filename, ref obj_path) >= 0);

        Assert.True(Marshal.PtrToStringAnsi(filename) == H5LFixture.m_v2_class_file_name);
        Assert.True(Marshal.PtrToStringAnsi(obj_path) == "/");

        Marshal.FreeHGlobal(buf);
    }

    [Fact]
    public void H5Lunpack_elink_valTest3()
    {
        // v0 file format

        byte[] bytes = Encoding.UTF8.GetBytes(H5LFixture.m_utf8strings[0]);

        Assert.True(H5G.close(H5G.create(H5LFixture.m_v0_class_file, bytes, H5LFixture.m_lcpl_utf8)) >= 0);

        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, bytes, m_v0_test_file, bytes, H5LFixture.m_lcpl_utf8) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, bytes, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        IntPtr size = new IntPtr(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        IntPtr buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != IntPtr.Zero);
        Assert.True(H5L.get_val(m_v0_test_file, bytes, buf, size) >= 0);

        uint flags = 0;
        IntPtr filename = new IntPtr();
        IntPtr obj_path = new IntPtr();
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, ref filename, ref obj_path) >= 0);

        Assert.True(Marshal.PtrToStringAnsi(filename) == H5LFixture.m_v0_class_file_name);

        // the elink value is packed like this:
        // <file name>\0<object path>\0
        // the whole thing is of info.u.val_size 

        int count = size.ToInt32() - (int)(obj_path.ToInt64() + 1 - filename.ToInt64()) - 1;
        byte[] obj_path_buf = new byte[count];
        Marshal.Copy(obj_path, obj_path_buf, 0, count);

        Assert.True(Encoding.UTF8.GetString(obj_path_buf) == H5LFixture.m_utf8strings[0], $"{Encoding.UTF8.GetString(obj_path_buf)}");

        Marshal.FreeHGlobal(buf);

        // v2 file format

        Assert.True(H5G.close(H5G.create(H5LFixture.m_v2_class_file, bytes, H5LFixture.m_lcpl_utf8)) >= 0);

        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, bytes, m_v2_test_file, bytes, H5LFixture.m_lcpl_utf8) >= 0);

        info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v2_test_file, bytes, ref info) >= 0);

        Assert.True(info.type == H5L.type_t.EXTERNAL);
        size = new IntPtr(info.u.val_size.ToInt32());
        Assert.True(size.ToInt32() > 0);

        buf = Marshal.AllocHGlobal(size.ToInt32());
        Assert.True(buf != IntPtr.Zero);
        Assert.True(H5L.get_val(m_v2_test_file, bytes, buf, size) >= 0);

        flags = 0;
        filename = new IntPtr();
        obj_path = new IntPtr();
        Assert.True(H5L.unpack_elink_val(buf, size, ref flags, ref filename, ref obj_path) >= 0);

        Assert.True(Marshal.PtrToStringAnsi(filename) == H5LFixture.m_v2_class_file_name);

        // the elink value is packed like this:
        // <file name>\0<object path>\0
        // the whole thing is of info.u.val_size 

        count = size.ToInt32() - (int)(obj_path.ToInt64() + 1 - filename.ToInt64()) - 1;
        obj_path_buf = new byte[count];
        Marshal.Copy(obj_path, obj_path_buf, 0, count);

        Assert.True(Encoding.UTF8.GetString(obj_path_buf) == H5LFixture.m_utf8strings[0], $"{Encoding.UTF8.GetString(obj_path_buf)}");

        Marshal.FreeHGlobal(buf);
    }
}
