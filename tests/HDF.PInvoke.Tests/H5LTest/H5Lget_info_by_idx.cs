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

public partial class H5LTest
{
    [Fact]
    public void H5Lget_info_by_idxTest1()
    {
        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, "/", m_v0_test_file, "A", H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, "/", m_v0_test_file, "AB", H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(H5LFixture.m_v0_class_file_name, "/", m_v0_test_file, "ABC", H5LFixture.m_lcpl) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info_by_idx(m_v0_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, ref info) >= 0);
        Assert.True(info.type == H5L.type_t.EXTERNAL);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        Assert.True(info.u.val_size.ToInt64() > 0);

        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, "/", m_v2_test_file, "A", H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, "/", m_v2_test_file, "AB", H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_external(H5LFixture.m_v2_class_file_name, "/", m_v2_test_file, "ABC", H5LFixture.m_lcpl) >= 0);

        Assert.True(H5L.get_info_by_idx(m_v2_test_file, ".", H5.index_t.NAME, H5.iter_order_t.NATIVE, 1, ref info) >= 0);
        Assert.True(info.type == H5L.type_t.EXTERNAL);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        Assert.True(info.u.val_size.ToInt64() > 0);
    }
}
