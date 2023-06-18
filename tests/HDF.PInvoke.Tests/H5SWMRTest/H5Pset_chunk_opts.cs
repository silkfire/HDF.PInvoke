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

using hsize_t = System.UInt64;
using hid_t = System.Int64;

using HDF5;
using Xunit;

public partial class H5SWMRTest
{
    [Fact]
    public void H5Pset_chunk_optsTestSWMR1()
    {
        hid_t dcpl = H5P.create(H5P.DATASET_CREATE);
        Assert.True(dcpl >= 0);

        // without chunking, H5Pset_chunk_opts will throw an error
        hsize_t[] dims = { 4711 };
        Assert.True(H5P.set_chunk(dcpl, 1, dims) >= 0);

        uint opts = H5D.DONT_FILTER_PARTIAL_CHUNKS;
        Assert.True(H5P.set_chunk_opts(dcpl, opts) >= 0);

        Assert.True(H5P.close(dcpl) >= 0);
    }
}
