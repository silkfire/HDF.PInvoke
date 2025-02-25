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

using uint32_t = System.UInt32;

using HDF5;
using Xunit;
using System;
using System.Text;

public partial class H5PLTest
{
    [Fact]
    public void H5PLgetTest1()
    {
        Assert.True(H5PL.append("foo") >= 0);
        uint32_t listsize = 0;
        Assert.True(H5PL.size(ref listsize) >= 0);
        Assert.True(listsize >= 0);
        StringBuilder sb = new StringBuilder();
        IntPtr size = new IntPtr(4);
        Assert.False(H5PL.get(0, sb, size) == IntPtr.Zero);
    }

    [Fact]
    public void H5PLgetTest2()
    {
        uint32_t listsize = 0;
        Assert.True(H5PL.size(ref listsize) >= 0);
        Assert.True(listsize >= 0);
        StringBuilder sb = new StringBuilder();
        IntPtr size = new IntPtr(4);
        Assert.False(H5PL.get(0, sb, size) == IntPtr.Zero);
    }
}
