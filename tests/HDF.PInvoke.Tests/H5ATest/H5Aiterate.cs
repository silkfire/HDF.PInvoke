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
using System;
using System.Collections;
using System.Runtime.InteropServices;

public partial class H5ATest
{
    [Fact]
    public void H5AiterateTest1()
    {
        hid_t att = H5A.create(m_v2_test_file, "IEEE_F32BE", H5T.IEEE_F32BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v2_test_file, "IEEE_F64BE", H5T.IEEE_F64BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v2_test_file, "NATIVE_B8", H5T.NATIVE_B8, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        IntPtr op_data = (IntPtr)hnd;
        hsize_t n = 0;
        // the callback is defined in H5ATest.cs
        H5A.operator_t cb = H5AFixture.DelegateMethod;
        Assert.True(H5A.iterate(m_v2_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);
        // we should have 3 elements in the array list
        Assert.Equal(3, al.Count);

        att = H5A.create(m_v0_test_file, "IEEE_F32BE", H5T.IEEE_F32BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v0_test_file, "IEEE_F64BE", H5T.IEEE_F64BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v0_test_file, "NATIVE_B8", H5T.NATIVE_B8, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        al.Clear();
        n = 0;
        Assert.True(H5A.iterate(m_v0_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);
        // we should have 3 elements in the array list
        Assert.Equal(3, al.Count);

        hnd.Free();
    }

    [Fact]
    public void H5AiterateTest2()
    {
        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        IntPtr op_data = (IntPtr)hnd;
        hsize_t n = 0;
        // the callback is defined in H5ATest.cs
        H5A.operator_t cb = H5AFixture.DelegateMethod;

        Assert.False(H5A.iterate(Utilities.RandomInvalidHandle(), H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);

        hnd.Free();
    }

    [Fact]
    public void H5AiterateTest3()
    {
        hid_t att = H5A.create(m_v2_test_file, "IEEE_F32BE", H5T.IEEE_F32BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v2_test_file, "IEEE_F64BE", H5T.IEEE_F64BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v2_test_file, "NATIVE_B8", H5T.NATIVE_B8, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        IntPtr op_data = (IntPtr)hnd;
        hsize_t n = 0;
        // the callback is defined in H5ATest.cs
        H5A.operator_t cb = H5AFixture.DelegateMethod;
        Assert.True(H5A.iterate(m_v2_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);
        // we should have 3 elements in the array list
        Assert.Equal(3, al.Count);

        att = H5A.create(m_v0_test_file, "IEEE_F32BE", H5T.IEEE_F32BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v0_test_file, "IEEE_F64BE", H5T.IEEE_F64BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(m_v0_test_file, "NATIVE_B8", H5T.NATIVE_B8, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        al.Clear();
        n = 0;
        Assert.True(H5A.iterate(m_v0_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);
        // we should have 3 elements in the array list
        Assert.Equal(3, al.Count);

        hnd.Free();
    }

    [Fact]
    public void H5AiterateTest4()
    {
        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        IntPtr op_data = (IntPtr)hnd;
        hsize_t n = 0;
        // the callback is defined in H5ATest.cs
        H5A.operator_t cb = H5AFixture.DelegateMethod;

        Assert.False(H5A.iterate(Utilities.RandomInvalidHandle(), H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);

        hnd.Free();
    }
}
