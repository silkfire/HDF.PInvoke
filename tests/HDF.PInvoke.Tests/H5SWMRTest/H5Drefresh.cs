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


using hid_t = System.Int64;

namespace HDF.PInvoke.Tests;

using HDF5;
using Xunit;

public partial class H5SWMRTest
{
    [Fact]
    public void H5DrefreshTestSWMR1()
    {
        hid_t dst = H5D.open(H5SWMRFixture.m_v3_class_file, "int6x6");
        Assert.True(dst >= 0);
        Assert.True(H5D.refresh(dst) >= 0);
        Assert.True(H5D.close(dst) >= 0);
    }
}
