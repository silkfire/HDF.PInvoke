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

using HDF5;
using Xunit;
using System;
using System.Runtime.InteropServices;

public partial class H5Test
{
    [Fact]
    public void H5allocate_memoryTest1()
    {
        IntPtr size = new IntPtr(1024 * 1024);

        // uninitialized allocation
        IntPtr ptr = H5.allocate_memory(size, 0);
        Assert.False(ptr == IntPtr.Zero);
        Assert.True(H5.free_memory(ptr) >= 0);

        // initialize with zeros
        ptr = H5.allocate_memory(size, 1);
        Assert.False(ptr == IntPtr.Zero);
        Assert.True(H5.free_memory(ptr) >= 0);

        // size = 0 -> NULL return
        size = new IntPtr(0);
        ptr = H5.allocate_memory(size, 0);
        Assert.True(ptr == IntPtr.Zero);
        Assert.True(H5.free_memory(ptr) >= 0);
    }

    [Fact]
    public void H5allocate_memoryTest2()
    {
        IntPtr size = new IntPtr(1024 * 1024);

        // uninitialized allocation
        IntPtr ptr = H5.allocate_memory(size, 0);
        Assert.False(ptr == IntPtr.Zero);
        Marshal.FreeHGlobal(ptr);

        // initialize with zeros
        ptr = H5.allocate_memory(size, 1);
        Assert.False(ptr == IntPtr.Zero);
        Marshal.FreeHGlobal(ptr);

        // size = 0 -> NULL return
        size = new IntPtr(0);
        ptr = H5.allocate_memory(size, 0);
        Assert.True(ptr == IntPtr.Zero);
        Marshal.FreeHGlobal(ptr);
    }
}
