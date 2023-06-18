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
using System;
using System.Runtime.InteropServices;

public partial class H5DTest
{
    [Fact]
    public void H5DfillTest1()
    {
        hsize_t[] dims = { 10 };
        hid_t space = H5S.create_simple(1, dims, null);
        Assert.True(H5S.select_all(space) >= 0);

        double[] v = new double[10];
        double fill = 1.0;

        GCHandle v_hnd = GCHandle.Alloc(v, GCHandleType.Pinned);
        GCHandle fill_hnd = GCHandle.Alloc(fill, GCHandleType.Pinned);
        Assert.True(H5D.fill(fill_hnd.AddrOfPinnedObject(), H5T.NATIVE_DOUBLE, v_hnd.AddrOfPinnedObject(), H5T.NATIVE_DOUBLE, space) >= 0);
        fill_hnd.Free();
        v_hnd.Free();

        for (int i = 0; i < v.Length; ++i)
        {
            Assert.Equal(1D, v[i]);
        }

        Assert.True(H5S.close(space) >= 0);
    }

    [Fact]
    public void H5DfillTest2()
    {
        hsize_t[] dims = { 5 };
        hid_t space = H5S.create_simple(1, dims, null);
        Assert.True(H5S.select_all(space) >= 0);

        double[] v = new double[5] { 0.0, 1.0, 2.0, 3.0, 4.0 };
        GCHandle v_hnd = GCHandle.Alloc(v, GCHandleType.Pinned);
        Assert.True(H5D.fill(IntPtr.Zero, H5T.NATIVE_DOUBLE, v_hnd.AddrOfPinnedObject(), H5T.NATIVE_DOUBLE, space) >= 0);
        v_hnd.Free();

        for (int i = 0; i < v.Length; ++i)
        {
            Assert.Equal(0D, v[i]);
        }

        Assert.True(H5S.close(space) >= 0);
    }
}
