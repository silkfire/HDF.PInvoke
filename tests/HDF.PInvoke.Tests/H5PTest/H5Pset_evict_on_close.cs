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

public partial class H5PTest
{
    [Fact]
    public void H5Pset_evict_on_closeTest1()
    {
        hid_t fapl = H5P.create(H5P.FILE_ACCESS);
        Assert.True(fapl >= 0);
        Assert.True(H5P.set_evict_on_close(fapl, 1) >= 0);
        Assert.True(H5P.close(fapl) >= 0);
    }

    [Fact]
    public void H5Pset_evict_on_closeTest2()
    {
        hid_t fcpl = H5P.create(H5P.FILE_CREATE);
        Assert.True(fcpl >= 0);
        Assert.False(H5P.set_evict_on_close(fcpl, 1) >= 0);
        Assert.True(H5P.close(fcpl) >= 0);
    }
}
