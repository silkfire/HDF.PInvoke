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

public partial class H5STest
{
    [Fact]
    public void H5Sset_extent_simpleTest1()
    {
        hid_t space = H5S.create(H5S.class_t.NULL);
        Assert.True(space > 0);

        hsize_t[] dims = { 10, 20, 30 };
        Assert.True(H5S.set_extent_simple(space, 3, dims, null) >= 0);

        Assert.True(H5S.close(space) >= 0);
    }

    [Fact]
    public void H5Sset_extent_simpleTest2()
    {
        Assert.False(H5S.set_extent_simple(Utilities.RandomInvalidHandle(), 3, null, null) >= 0);
    }
}
