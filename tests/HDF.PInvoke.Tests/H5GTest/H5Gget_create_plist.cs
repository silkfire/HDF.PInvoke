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

public partial class H5GTest
{
    [Fact]
    public void H5Gget_create_plistTest1()
    {
        hid_t gid = H5G.create(m_v0_test_file, "A");
        Assert.True(gid > 0);

        hid_t gcpl = H5G.get_create_plist(gid);
        Assert.True(gcpl > 0);

        hid_t gid1 = H5G.create_anon(gid);
        Assert.True(gid1 > 0);

        hid_t gcpl1 = H5G.get_create_plist(gid1);
        Assert.True(gcpl1 > 0);

        Assert.True(H5P.close(gcpl1) >= 0);
        Assert.True(H5P.close(gcpl) >= 0);
        Assert.True(H5G.close(gid1) >= 0);
        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, "A");
        Assert.True(gid > 0);

        gcpl = H5G.get_create_plist(gid);
        Assert.True(gcpl > 0);

        gid1 = H5G.create_anon(gid);
        Assert.True(gid1 > 0);

        gcpl1 = H5G.get_create_plist(gid1);
        Assert.True(gcpl1 > 0);

        Assert.True(H5P.close(gcpl1) >= 0);
        Assert.True(H5P.close(gcpl) >= 0);
        Assert.True(H5G.close(gid1) >= 0);
        Assert.True(H5G.close(gid) >= 0);
    }

    [Fact]
    public void H5Gget_create_plistTest2()
    {
        hid_t group = Utilities.RandomInvalidHandle();
        hid_t gcpl = H5G.get_create_plist(group);
        Assert.True(gcpl < 0);
    }
}
