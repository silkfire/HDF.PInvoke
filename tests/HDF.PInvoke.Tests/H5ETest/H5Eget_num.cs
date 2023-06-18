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

using ssize_t = nint;
using hid_t = System.Int64;

using HDF5;
using Xunit;

public partial class H5ETest
{
    [Fact]
    public void H5Eget_numTest1()
    {
        hid_t est = H5E.get_current_stack();
        Assert.True(est >= 0);
        ssize_t size = H5E.get_num(est);
        Assert.True(size.ToInt32() >= 0);
        Assert.True(H5E.close_stack(est) >= 0);
    }

    [Fact]
    public void H5Eget_numTest2()
    {
        Assert.False(H5E.get_num(Utilities.RandomInvalidHandle()).ToInt32() >= 0);
    }
}
