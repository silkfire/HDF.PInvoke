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
using hssize_t = System.Int64;
using hid_t = System.Int64;

using HDF5;
using Xunit;

public partial class H5STest
{
    [Fact]
    public void H5Soffset_simpleTest1()
    {
        hsize_t[] dims = { 10, 10 };
        hid_t space = H5S.create_simple(dims.Length, dims, null);
        Assert.True(space > 0);

        hsize_t[] start = { 0, 0 };
        hsize_t[] count = { 1, 1 };
        hsize_t[] block = { 2, 3 };
        Assert.True(H5S.select_hyperslab(space, H5S.seloper_t.SET, start, null, count, block) >= 0);

        hssize_t[] offset = { 0, 0 };
        Assert.True(H5S.offset_simple(space, offset) >= 0);

        Assert.True(H5S.close(space) >= 0);
    }

    [Fact]
    public void H5Soffset_simpleTest2()
    {
        Assert.False(H5S.offset_simple(Utilities.RandomInvalidHandle(), null) >= 0);
    }
}
