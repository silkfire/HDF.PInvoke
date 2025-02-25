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
using haddr_t = System.UInt64;
using uint32_t = System.UInt32;
using hid_t = System.Int64;

using HDF5;
using Xunit;

public partial class H5DTest
{
    [Fact]
    public void H5Dget_chunk_infoTest1()
    {
        hsize_t[] dims = { 10, 10 };
        hsize_t[] max_dims = { H5S.UNLIMITED, H5S.UNLIMITED };
        hid_t space = H5S.create_simple(2, dims, max_dims);

        hid_t dcpl = H5P.create(H5P.DATASET_CREATE);
        Assert.True(dcpl >= 0);
        hsize_t[] chunk = { 4, 4 };
        Assert.True(H5P.set_chunk(dcpl, 2, chunk) >= 0);
        Assert.True(H5P.set_alloc_time(dcpl, H5D.alloc_time_t.EARLY) >= 0);
        Assert.True(H5P.set_fill_time(dcpl, H5D.fill_time_t.ALLOC) >= 0);

        hid_t dset = H5D.create(m_v0_test_file, "Early Bird1", H5T.IEEE_F32BE, space, H5P.DEFAULT, dcpl);
        Assert.True(dset >= 0);

        // This should work but doesn't:
        // Assert.True(H5D.get_num_chunks(dset, H5S.ALL, ref nchunks) >= 0);
        hid_t fspace = H5D.get_space(dset);
        Assert.True(fspace >= 0);
        Assert.True(H5S.select_all(fspace) >= 0);

        hsize_t index = 8, size = 0;
        hsize_t[] offset = { 4711, 4712 };
        uint32_t filter_mask = 0;
        haddr_t addr = 0;
        Assert.True(H5D.get_chunk_info(dset, fspace, index, offset, ref filter_mask, ref addr, ref size) >= 0);
        Assert.True(offset[0] > 0);
        Assert.Equal(0U, filter_mask);
        Assert.True(size > 0UL);
        Assert.True(addr > 0UL);

        Assert.True(H5D.close(dset) >= 0);

        dset = H5D.create(m_v2_test_file, "Early Bird1", H5T.IEEE_F32BE, space, H5P.DEFAULT, dcpl);
        Assert.True(dset >= 0);

        // This should work but doesn't:
        // Assert.True(H5D.get_num_chunks(dset, H5S.ALL, ref nchunks) >= 0);
        fspace = H5D.get_space(dset);
        Assert.True(fspace >= 0);
        Assert.True(H5S.select_all(fspace) >= 0);

        Assert.True(H5D.get_chunk_info(dset, fspace, index, offset, ref filter_mask, ref addr, ref size) >= 0);
        Assert.True(offset[0] > 0);
        Assert.Equal(0U, filter_mask);
        Assert.True(size > 0UL);
        Assert.True(addr > 0UL);

        Assert.True(H5D.close(dset) >= 0);
    }
}
