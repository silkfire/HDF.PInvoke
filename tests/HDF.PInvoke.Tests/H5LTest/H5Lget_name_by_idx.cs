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
using System.Text;

public partial class H5LTest
{
    [Fact]
    public void H5Lget_name_by_idxTest1()
    {
        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, "/", m_v0_test_file, "A", H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, "/", m_v0_test_file, "AB", H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, "/", m_v0_test_file, "ABC", H5LFixture.m_lcpl) >= 0);

        size_t buf_size = ssize_t.Zero;
        ssize_t size = H5L.get_name_by_idx(m_v0_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, null, buf_size);
        Assert.True(size.ToInt32() == 2);
        buf_size = new ssize_t(size.ToInt32() + 1);
        StringBuilder nameBuilder = new StringBuilder(buf_size.ToInt32());
        size = H5L.get_name_by_idx(m_v0_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, nameBuilder, buf_size);
        Assert.True(nameBuilder.ToString() == "AB");

        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, "/", m_v2_test_file, "A", H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, "/", m_v2_test_file, "AB", H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, "/", m_v2_test_file, "ABC", H5LFixture.m_lcpl) >= 0);

        buf_size = ssize_t.Zero;
        size = H5L.get_name_by_idx(m_v2_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, null, buf_size);
        Assert.True(size.ToInt32() == 2);
        buf_size = new ssize_t(size.ToInt32() + 1);
        nameBuilder = new StringBuilder(buf_size.ToInt32());
        size = H5L.get_name_by_idx(m_v2_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, nameBuilder, buf_size);
        Assert.True(nameBuilder.ToString() == "AB");
    }

    [Fact]
    public void H5Lget_name_by_idxTest2()
    {
        hid_t lcpl = H5P.copy(H5LFixture.m_lcpl);
        Assert.True(lcpl >= 0);
        Assert.True(H5P.set_char_encoding(lcpl, H5T.cset_t.UTF8) >= 0);

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, "/", H5LFixture.m_v0_class_file, H5LFixture.m_utf8strings[i], lcpl) >= 0);
        }

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            size_t buf_size = ssize_t.Zero;
            ssize_t size = H5L.get_name_by_idx(m_v0_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, (hsize_t)i, null, buf_size);
            buf_size = new ssize_t(size.ToInt32() + 1);
            StringBuilder nameBuilder = new StringBuilder(buf_size.ToInt32());
            size = H5L.get_name_by_idx(m_v0_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, (hsize_t)i, nameBuilder, buf_size);
        }

        Assert.True(H5P.close(lcpl) >= 0);
    }
}
