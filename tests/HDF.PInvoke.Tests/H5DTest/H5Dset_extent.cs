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

using hsize_t = System.UInt64;
using hid_t = System.Int64;

using HDF5;
using Xunit;

public partial class H5DTest
{
    [Fact]
    public void H5Dset_extentTest1()
    {
        hsize_t[] dims = { 10, 10, 10 };
        hsize_t[] max_dims = { H5S.UNLIMITED, H5S.UNLIMITED, H5S.UNLIMITED };
        hsize_t[] new_dims = { 10, 20, 30 };
        hid_t space = H5S.create_simple(3, dims, max_dims);

        hid_t dcpl = H5P.create(H5P.DATASET_CREATE);
        Assert.True(dcpl >= 0);
        hsize_t[] chunk = { 64, 64, 64 };
        Assert.True(H5P.set_chunk(dcpl, 3, chunk) >= 0);

        hid_t dset = H5D.create_anon(m_v0_test_file, H5T.IEEE_F32BE, space, dcpl);
        Assert.True(dset >= 0);
        Assert.True(H5D.set_extent(dset, new_dims) >= 0);
        Assert.True(H5D.close(dset) >= 0);

        dset = H5D.create_anon(m_v2_test_file, H5T.IEEE_F32BE, space, dcpl);
        Assert.True(dset >= 0);
        Assert.True(H5D.set_extent(dset, new_dims) >= 0);
        Assert.True(H5D.close(dset) >= 0);

        Assert.True(H5P.close(dcpl) >= 0);
        Assert.True(H5S.close(space) >= 0);
    }

    [Fact]
    public void H5Dset_extentTest2()
    {
        hsize_t[] dims = { 10, 10, 10 };
        hsize_t[] max_dims = { H5S.UNLIMITED, H5S.UNLIMITED, H5S.UNLIMITED };
        hid_t space = H5S.create_simple(3, dims, max_dims);

        hid_t dcpl = H5P.create(H5P.DATASET_CREATE);
        Assert.True(dcpl >= 0);
        hsize_t[] chunk = { 64, 64, 64 };
        Assert.True(H5P.set_chunk(dcpl, 3, chunk) >= 0);

        hid_t dset = H5D.create_anon(m_v0_test_file, H5T.IEEE_F32BE, space, dcpl);
        Assert.True(dset >= 0);
        Assert.False(H5D.set_extent(dset, (ulong[])null) >= 0);
        Assert.True(H5D.close(dset) >= 0);

        dset = H5D.create_anon(m_v2_test_file, H5T.IEEE_F32BE, space, dcpl);
        Assert.True(dset >= 0);
        Assert.False(H5D.set_extent(dset, (ulong[])null) >= 0);
        Assert.True(H5D.close(dset) >= 0);

        Assert.True(H5P.close(dcpl) >= 0);
        Assert.True(H5S.close(space) >= 0);
    }
}
