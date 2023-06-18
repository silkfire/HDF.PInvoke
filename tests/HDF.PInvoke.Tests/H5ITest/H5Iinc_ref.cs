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

public partial class H5ITest
{
    [Fact]
    public void H5Iinc_refTest1()
    {
        hid_t gid = H5G.create(m_v0_test_file, "A");
        Assert.True(gid > 0);
        Assert.True(H5I.inc_ref(gid) >= 0);
        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, "A");
        Assert.True(gid > 0);
        Assert.True(H5I.inc_ref(gid) >= 0);
        Assert.True(H5G.close(gid) >= 0);
    }

    [Fact]
    public void H5Iinc_refTest2()
    {
        hid_t gid = H5G.create(m_v0_test_file, "A");
        Assert.True(gid > 0);
        Assert.True(H5I.inc_ref(gid) >= 0);
        Assert.True(H5I.dec_ref(gid) >= 0);
        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, "A");
        Assert.True(gid > 0);
        Assert.True(H5I.inc_ref(gid) >= 0);
        Assert.True(H5I.dec_ref(gid) >= 0);
        Assert.True(H5G.close(gid) >= 0);
    }

    [Fact]
    public void H5Iinc_refTest3()
    {
        Assert.False(H5I.inc_ref(Utilities.RandomInvalidHandle()) >= 0);
    }
}
