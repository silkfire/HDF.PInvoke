﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
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

public partial class H5FTest
{
    [Fact]
    public void H5Fget_mdc_hit_rateTest1()
    {
        double rate = 0.0;
        Assert.True(H5F.get_mdc_hit_rate(H5FFixture.m_v0_class_file, ref rate) >= 0);
        Assert.True(H5F.get_mdc_hit_rate(H5FFixture.m_v2_class_file, ref rate) >= 0);
        Assert.True(H5F.get_mdc_hit_rate(m_v0_test_file, ref rate) >= 0);
        Assert.True(H5F.get_mdc_hit_rate(m_v2_test_file, ref rate) >= 0);
    }

    [Fact]
    public void H5Fget_mdc_hit_rateTest2()
    {
        double rate = 0.0;
        Assert.False(H5F.get_mdc_hit_rate(Utilities.RandomInvalidHandle(), ref rate) >= 0);
    }
}
