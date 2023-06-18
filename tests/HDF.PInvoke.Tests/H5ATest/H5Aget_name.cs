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
    public void H5Aget_nameTest1()
    {
        size_t buf_size = ssize_t.Zero;
        ssize_t size = ssize_t.Zero;
        hid_t att = H5A.create(m_v2_test_file, "H5Aget_name", H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);

        // pretend we don't know the size
        size = H5A.get_name(att, buf_size, (StringBuilder)null);
        Assert.Equal(11, size.ToInt32());
        buf_size = new ssize_t(size.ToInt32() + 1);
        StringBuilder nameBuilder = new StringBuilder(buf_size.ToInt32());
        size = H5A.get_name(att, buf_size, nameBuilder);
        Assert.Equal(11, size.ToInt32());
        string name = nameBuilder.ToString();
        // names should match
        Assert.Equal("H5Aget_name", name);

        // read a truncated version
        buf_size = new ssize_t(3);
        nameBuilder = new StringBuilder(3);
        size = H5A.get_name(att, buf_size, nameBuilder);
        Assert.Equal(11, size.ToInt32());
        name = nameBuilder.ToString();
        // names won't match
        Assert.NotEqual("H5Aget_name", name);
        Assert.Equal("H5", name);

        Assert.True(H5A.close(att) >= 0);
    }

    [Fact]
    public void H5Aget_nameTest2()
    {
        Assert.False(H5A.get_name(Utilities.RandomInvalidHandle(), ssize_t.Zero, (StringBuilder)null).ToInt32() >= 0);

        Assert.False(H5A.get_name(Utilities.RandomInvalidHandle(), ssize_t.Zero, (byte[])null).ToInt32() >= 0);
    }

    [Fact]
    public void H5Aget_nameTest3()
    {
        byte[] name = Encoding.UTF8.GetBytes(string.Join(":", H5AFixture.m_utf8strings));
        byte[] name_buf = new byte[name.Length + 1];
        Array.Copy(name, name_buf, name.Length);
        hid_t att = H5A.create(m_v0_test_file, name_buf, H5T.IEEE_F64BE, H5AFixture.m_space_scalar, H5AFixture.m_acpl);
        Assert.True(att >= 0);

        ssize_t buf_size = H5A.get_name(att, ssize_t.Zero, (byte[])null) + 1;
        Assert.True(buf_size.ToInt32() > 1);
        byte[] buf = new byte[buf_size.ToInt32()];
        Assert.True(H5A.get_name(att, buf_size, buf).ToInt32() >= 0);

        for (int i = 0; i < buf.Length; ++i)
        {
            Assert.Equal(name_buf[i], buf[i]);
        }

        Assert.True(H5A.close(att) >= 0);
    }

    [Fact]
    public void H5Aget_nameTest4()
    {
        byte[] name = Encoding.UTF8.GetBytes(string.Join(":", H5AFixture.m_utf8strings));
        byte[] name_buf = new byte[name.Length + 1];
        Array.Copy(name, name_buf, name.Length);
        hid_t att = H5A.create(m_v2_test_file, name_buf, H5T.IEEE_F64BE, H5AFixture.m_space_scalar, H5AFixture.m_acpl);
        Assert.True(att >= 0);

        ssize_t buf_size = H5A.get_name(att, ssize_t.Zero, (byte[])null) + 1;
        Assert.True(buf_size.ToInt32() > 1);
        byte[] buf = new byte[buf_size.ToInt32()];
        Assert.True(H5A.get_name(att, buf_size, buf).ToInt32() >= 0);

        for (int i = 0; i < buf.Length; ++i)
        {
            Assert.Equal(name_buf[i], buf[i]);
        }

        Assert.True(H5A.close(att) >= 0);
    }
}
