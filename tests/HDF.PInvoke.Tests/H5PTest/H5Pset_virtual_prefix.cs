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
using System;
using System.Text;

public partial class H5PTest
{
    [Fact]
    public void H5Pset_virtual_prefixTest1()
    {
        hid_t dapl = H5P.create(H5P.DATASET_ACCESS);
        Assert.True(dapl >= 0);
        string prefix = "foo";
        Assert.True(H5P.set_virtual_prefix(dapl, prefix) >= 0);

        StringBuilder sb = new StringBuilder(4);
        IntPtr size = new IntPtr(4);
        Assert.True(H5P.get_virtual_prefix(dapl, sb, size).ToInt32() == 3);

        Assert.True(H5P.close(dapl) >= 0);
    }
}
