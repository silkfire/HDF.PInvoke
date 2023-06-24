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
using System.Runtime.InteropServices;

public partial class H5ATest
{
    [Fact]
    public void H5AopenTest1()
    {
        var aNamePtr = Marshal.StringToHGlobalAnsi("A");

        hid_t att = H5A.create(m_v2_test_file, aNamePtr, H5T.IEEE_F64LE,  H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.open(m_v2_test_file, aNamePtr);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v0_test_file, aNamePtr, H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.open(m_v0_test_file, aNamePtr);
        Assert.True(att >= 0);
        Marshal.FreeHGlobal(aNamePtr);
        Assert.True(H5A.close(att) >= 0);
    }

    [Fact]
    public void H5AopenTest2()
    {
        var dotNamePtr = Marshal.StringToHGlobalAnsi(".");
        var emptyNamePtr = Marshal.StringToHGlobalAnsi("");

        Assert.False(H5A.open(Utilities.RandomInvalidHandle(), dotNamePtr) >= 0);
        Assert.False(H5A.open(H5AFixture.m_v2_class_file, emptyNamePtr) >= 0);

        Marshal.FreeHGlobal(dotNamePtr);
        Marshal.FreeHGlobal(emptyNamePtr);
    }
}
