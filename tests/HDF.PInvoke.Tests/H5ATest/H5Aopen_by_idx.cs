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
using System.Runtime.InteropServices;

public partial class H5ATest
{
    [Fact]
    public void H5Aopen_by_idxTest1()
    {
        var aNamePtr = Marshal.StringToHGlobalAnsi("A");
        var aaNamePtr = Marshal.StringToHGlobalAnsi("AA");
        var dotNamePtr = Marshal.StringToHGlobalAnsi(".");

        hid_t att = H5A.create(m_v2_test_file, aNamePtr, H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);
        att = H5A.create(m_v2_test_file, aaNamePtr, H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.open_by_idx(m_v2_test_file, dotNamePtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 0);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.open_by_idx(m_v2_test_file, dotNamePtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 1);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v0_test_file, aNamePtr, H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.open_by_idx(m_v0_test_file, dotNamePtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 0);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        Marshal.FreeHGlobal(aNamePtr);
        Marshal.FreeHGlobal(aaNamePtr);
        Marshal.FreeHGlobal(dotNamePtr);
    }

    [Fact]
    public void H5Aopen_by_idxTest2()
    {
        var dotNamePtr = Marshal.StringToHGlobalAnsi(".");

        Assert.False(H5A.open_by_idx(Utilities.RandomInvalidHandle(), dotNamePtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 44) >= 0);
        Assert.False(H5A.open_by_idx(H5AFixture.m_v2_class_file, dotNamePtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, hsize_t.MaxValue) >= 0);

        Marshal.FreeHGlobal(dotNamePtr);
    }
}
