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

using hbool_t = System.UInt32;
using hid_t = System.Int64;

using HDF5;
using Xunit;

public partial class H5SWMRTest
{
    [Fact]
    public void H5Odisable_mdc_flushesTestSWMR1()
    {
        hid_t grp = H5G.create(m_v3_test_file_swmr, "/A/B/C", H5SWMRFixture.m_lcpl);
        Assert.True(grp >= 0);
        Assert.True(H5O.disable_mdc_flushes(grp) >= 0);
        Assert.True(H5G.flush(grp) >= 0);
        Assert.True(H5G.close(grp) >= 0);
    }

    [Fact]
    public void H5Odisable_mdc_flushesTestSWMR2()
    {
        hid_t grp = H5G.create(m_v3_test_file_no_swmr, "/A/B/C", H5SWMRFixture.m_lcpl);
        Assert.True(grp >= 0);
        Assert.True(H5O.disable_mdc_flushes(grp) >= 0);
        Assert.True(H5G.flush(grp) >= 0);
        Assert.True(H5G.close(grp) >= 0);
    }

    [Fact]
    public void H5Odisable_mdc_flushesTestSWMR3()
    {
        hid_t grp = H5G.create(m_v3_test_file_swmr, "/A/B/C", H5SWMRFixture.m_lcpl);
        Assert.True(grp >= 0);

        hbool_t flag = 11;
        Assert.True(H5O.are_mdc_flushes_disabled(grp, ref flag) >= 0);

        Assert.True(flag == 0);

        Assert.True(H5O.disable_mdc_flushes(grp) >= 0);

        Assert.True(H5O.are_mdc_flushes_disabled(grp, ref flag) >= 0);
        Assert.True(flag > 0);

        Assert.True(H5G.flush(grp) >= 0);

        Assert.True(H5G.close(grp) >= 0);
    }
}
