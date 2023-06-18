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

using size_t = nint;
using ssize_t = nint;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System;
using System.Text;

public partial class H5ATest
{
    [Fact]
    public void H5Aget_name_by_idxTest1()
    {
        hid_t att = H5A.create(m_v2_test_file, "H5Aget_name", H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);
        att = H5A.create(m_v2_test_file, "H5Aget_name_by_idx", H5T.STD_I16LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        size_t buf_size = ssize_t.Zero;
        ssize_t size = ssize_t.Zero;
        StringBuilder nameBuilder = new StringBuilder(19);
        buf_size = new ssize_t(19);
        size = H5A.get_name_by_idx(m_v2_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 0, nameBuilder, buf_size);
        Assert.Equal(11, size.ToInt32());
        string name = nameBuilder.ToString();
        // names should match
        Assert.Equal("H5Aget_name", name);

        nameBuilder.Clear();
        size = H5A.get_name_by_idx(m_v2_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, nameBuilder, buf_size);
        Assert.Equal(18, size.ToInt32());
        name = nameBuilder.ToString();
        // names should match
        Assert.Equal("H5Aget_name_by_idx", name);

        // read a truncated version
        buf_size = new ssize_t(3);
        nameBuilder = new StringBuilder(3);
        size = H5A.get_name_by_idx(m_v2_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, nameBuilder, buf_size);
        Assert.Equal(18, size.ToInt32());
        name = nameBuilder.ToString();
        // names won't match
        Assert.NotEqual("H5Aget_name_by_idx", name);
        Assert.Equal("H5", name);
    }

    [Fact]
    public void H5Aget_name_by_idxTest2()
    {
        Assert.False(H5A.get_name_by_idx(Utilities.RandomInvalidHandle(), ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 0, null, ssize_t.Zero).ToInt32() >= 0);
    }

    [Fact]
    public void H5Aget_name_by_idxTest3()
    {
        hid_t att = H5A.create(m_v0_test_file, "H5Aget_name", H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);
        att = H5A.create(m_v0_test_file, "H5Aget_name_by_idx", H5T.STD_I16LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        byte[] name = Encoding.UTF8.GetBytes(string.Join(":", H5AFixture.m_utf8strings));
        byte[] name_buf = new byte[name.Length + 1];
        Array.Copy(name, name_buf, name.Length);
        att = H5A.create(m_v0_test_file, name_buf, H5T.IEEE_F64BE, H5AFixture.m_space_scalar, H5AFixture.m_acpl);
        Assert.True(att >= 0);

        ssize_t buf_size = H5A.get_name(att, ssize_t.Zero, (byte[])null) + 1;
        Assert.True(buf_size.ToInt32() > 1);
        byte[] buf = new byte[buf_size.ToInt32()];
        Assert.True(H5A.get_name_by_idx(m_v0_test_file, Encoding.ASCII.GetBytes("."), H5.index_t.NAME, H5.iter_order_t.NATIVE, 2, buf, buf_size).ToInt32() >= 0);

        for (int i = 0; i < buf.Length; ++i)
        {
            Assert.Equal(name_buf[i], buf[i]);
        }

        Assert.True(H5A.close(att) >= 0);
    }

    [Fact]
    public void H5Aget_name_by_idxTest4()
    {
        hid_t att = H5A.create(m_v2_test_file, "H5Aget_name", H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);
        att = H5A.create(m_v2_test_file, "H5Aget_name_by_idx", H5T.STD_I16LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        byte[] name = Encoding.UTF8.GetBytes(string.Join(":", H5AFixture.m_utf8strings));
        byte[] name_buf = new byte[name.Length + 1];
        Array.Copy(name, name_buf, name.Length);
        att = H5A.create(m_v2_test_file, name_buf, H5T.IEEE_F64BE, H5AFixture.m_space_scalar, H5AFixture.m_acpl);
        Assert.True(att >= 0);

        ssize_t buf_size = H5A.get_name(att, ssize_t.Zero, (byte[])null) + 1;
        Assert.True(buf_size.ToInt32() > 1);
        byte[] buf = new byte[buf_size.ToInt32()];
        Assert.True(H5A.get_name_by_idx(m_v2_test_file, Encoding.ASCII.GetBytes("."), H5.index_t.NAME, H5.iter_order_t.NATIVE, 2, buf, buf_size).ToInt32() >= 0);

        for (int i = 0; i < buf.Length; ++i)
        {
            Assert.Equal(name_buf[i], buf[i]);
        }

        Assert.True(H5A.close(att) >= 0);
    }
}
