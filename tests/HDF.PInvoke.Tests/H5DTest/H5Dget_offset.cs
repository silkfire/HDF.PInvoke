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

using hid_t = System.Int64;

using HDF5;
using Xunit;

public partial class H5DTest
{
    [Fact]
    public void H5Dget_offsetTest1()
    {
        hid_t dset = H5D.create(m_v0_test_file, "dset", H5T.IEEE_F64BE, H5DFixture.m_space_null);
        Assert.True(dset >= 0);

        Assert.True(H5D.get_offset(dset) > 0);

        Assert.True(H5D.close(dset) >= 0);

        dset = H5D.create(m_v2_test_file, "dset", H5T.IEEE_F64BE, H5DFixture.m_space_null);
        Assert.True(dset >= 0);

        Assert.True(H5D.get_offset(dset) > 0);

        Assert.True(H5D.close(dset) >= 0);
    }

    [Fact]
    public void H5Dget_offsetTest2()
    {
        Assert.True(H5D.get_offset(Utilities.RandomInvalidHandle()) == H5.HADDR_UNDEF);
    }
}
