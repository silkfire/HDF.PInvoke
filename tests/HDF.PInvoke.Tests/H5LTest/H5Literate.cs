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

using HDF5;

using Xunit;

using System;
using System.Collections;
using System.Runtime.InteropServices;

public partial class H5LTest
{
    [Fact]
    public void H5LiterateTest1()
    {
        var thisIsASoftLinkStringPtr = Marshal.StringToHGlobalAnsi("this/is/a/soft/link");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var bStringPtr = Marshal.StringToHGlobalAnsi("B");
        var cStringPtr = Marshal.StringToHGlobalAnsi("C");

        Assert.True(H5L.create_soft(thisIsASoftLinkStringPtr, m_v0_test_file, aStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_soft(thisIsASoftLinkStringPtr, m_v0_test_file, bStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_soft(thisIsASoftLinkStringPtr, m_v0_test_file, cStringPtr, H5LFixture.m_lcpl) >= 0);

        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        nint op_data = (nint)hnd;
        hsize_t n = 0;
        // the callback is defined in H5LTest.cs
        H5L.iterate_t cb = H5LFixture.DelegateMethod;
        Assert.True(H5L.iterate(m_v0_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);
        // we should have 3 elements in the array list
        Assert.True(al.Count == 3);

        Assert.True(H5L.create_soft(thisIsASoftLinkStringPtr, m_v2_test_file, aStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_soft(thisIsASoftLinkStringPtr, m_v2_test_file, bStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_soft(thisIsASoftLinkStringPtr, m_v2_test_file, cStringPtr, H5LFixture.m_lcpl) >= 0);

        n = 0;
        Assert.True(H5L.iterate(m_v2_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);
        // we should have 6 (3 + 3) elements in the array list
        Assert.True(al.Count == 6);

        hnd.Free();

        Marshal.FreeHGlobal(thisIsASoftLinkStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(bStringPtr);
        Marshal.FreeHGlobal(cStringPtr);
    }

    [Fact]
    public void H5LiterateTest2()
    {
        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        nint op_data = (nint)hnd;
        hsize_t n = 0;
        // the callback is defined in H5ATest.cs
        H5L.iterate_t cb = H5LFixture.DelegateMethod;

        Assert.False(H5L.iterate(Utilities.RandomInvalidHandle(), H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);

        hnd.Free();
    }

    [Fact]
    public void H5LiterateTest3()
    {
        var thisIsASoftLinkStringPtr = Marshal.StringToHGlobalAnsi("this/is/a/soft/link");

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            var utf8StringPtr = Marshal.StringToCoTaskMemUTF8(H5LFixture.m_utf8strings[i]);

            Assert.True(H5L.create_soft(thisIsASoftLinkStringPtr, m_v0_test_file, utf8StringPtr, H5LFixture.m_lcpl_utf8) >= 0);
            Assert.True(H5L.create_soft(thisIsASoftLinkStringPtr, m_v2_test_file, utf8StringPtr, H5LFixture.m_lcpl_utf8) >= 0);

            Marshal.FreeCoTaskMem(utf8StringPtr);
        }

        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        nint op_data = (nint)hnd;
        hsize_t n = 0;
        // the callback is defined in H5LTest.cs
        H5L.iterate_t cb = H5LFixture.DelegateMethod;
        Assert.True(H5L.iterate(m_v0_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);
        Assert.True(al.Count == H5LFixture.m_utf8strings.Length);

        n = 0;
        Assert.True(H5L.iterate(m_v2_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, ref n, cb, op_data) >= 0);
        Assert.True(al.Count == 2 * H5LFixture.m_utf8strings.Length);

        hnd.Free();

        Marshal.FreeHGlobal(thisIsASoftLinkStringPtr);
    }
}
