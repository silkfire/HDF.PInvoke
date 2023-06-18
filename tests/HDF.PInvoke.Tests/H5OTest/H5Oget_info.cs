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

using hid_t = System.Int64;

using HDF5;
using Xunit;

public partial class H5OTest
{
    [Fact]
    public void H5Oget_infoTest1()
    {
        hid_t gid = H5G.create(m_v0_test_file, "A/B/C", H5OFixture.m_lcpl);
        Assert.True(gid >= 0);

        H5O.info_t info = new H5O.info_t();
        Assert.True(H5O.get_info(gid, ref info) >= 0);
        Assert.True(info.type == H5O.type_t.GROUP);

        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, "A/B/C", H5OFixture.m_lcpl);
        Assert.True(gid >= 0);

        info = new H5O.info_t();
        Assert.True(H5O.get_info(gid, ref info) >= 0);
        Assert.True(info.type == H5O.type_t.GROUP);

        Assert.True(H5G.close(gid) >= 0);
    }

    [Fact]
    public void H5Oget_infoTest2()
    {
        H5O.info_t info = new H5O.info_t();
        Assert.False(H5O.get_info(Utilities.RandomInvalidHandle(), ref info) >= 0);
    }
}
