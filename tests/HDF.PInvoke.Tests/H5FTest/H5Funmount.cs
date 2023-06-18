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

public partial class H5FTest
{
    [Fact]
    public void H5FunmountTest1()
    {
        hid_t mount_point = H5G.create(H5FFixture.m_v0_class_file, "UA");
        Assert.True(mount_point >= 0);
        Assert.True(H5G.close(mount_point) >= 0);
        Assert.True(H5F.mount(H5FFixture.m_v0_class_file, "UA", m_v0_test_file) >= 0);

        mount_point = H5G.create(H5FFixture.m_v0_class_file, "UB");
        Assert.True(mount_point >= 0);
        Assert.True(H5G.close(mount_point) >= 0);
        Assert.True(H5F.mount(H5FFixture.m_v0_class_file, "UB", m_v2_test_file) >= 0);

        mount_point = H5G.create(H5FFixture.m_v2_class_file, "UC");
        Assert.True(mount_point >= 0);
        Assert.True(H5G.close(mount_point) >= 0);
        Assert.True(H5F.mount(H5FFixture.m_v2_class_file, "UC", H5FFixture.m_v0_class_file) >= 0);

        Assert.True(H5F.unmount(H5FFixture.m_v2_class_file, "UC") >= 0);
        Assert.True(H5F.unmount(H5FFixture.m_v0_class_file, "UB") >= 0);
        Assert.True(H5F.unmount(H5FFixture.m_v0_class_file, "UA") >= 0);
    }

    [Fact]
    public void H5FunmountTest2()
    {
        Assert.False(H5F.unmount(Utilities.RandomInvalidHandle(), "AA") >= 0);

        Assert.False(H5F.unmount(H5FFixture.m_v0_class_file, "") >= 0);
    }
}
