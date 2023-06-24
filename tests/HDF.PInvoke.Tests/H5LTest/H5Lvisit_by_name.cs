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

using HDF5;
using Xunit;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public partial class H5LTest
{
    [Fact]
    public void H5Lvisit_by_nameTest1()
    {
        var abcdStringPtr = Marshal.StringToHGlobalAnsi("A/B/C/D");
        var shortcutStringPtr = Marshal.StringToHGlobalAnsi("shortcut");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");

        Assert.True(H5G.create(m_v0_test_file, abcdStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_hard(m_v0_test_file, abcdStringPtr, m_v0_test_file, shortcutStringPtr) >= 0);

        Assert.True(H5G.create(m_v2_test_file, abcdStringPtr, H5LFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_hard(m_v2_test_file, abcdStringPtr, m_v2_test_file, shortcutStringPtr) >= 0);

        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        nint op_data = (nint)hnd;
        // the callback is defined in H5LTest.cs
        H5L.iterate_t cb = H5LFixture.DelegateMethod;

        Assert.True(H5L.visit_by_name(m_v0_test_file, aStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, cb, op_data) >= 0);
        // we should have 3 elements in the array list
        Assert.True(al.Count == 3);

        Assert.True(H5L.visit_by_name(m_v2_test_file, aStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, cb, op_data) >= 0);
        // we should have 6 (3 + 3) elements in the array list
        Assert.True(al.Count == 6);

        hnd.Free();
        Marshal.FreeHGlobal(abcdStringPtr);
        Marshal.FreeHGlobal(shortcutStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
    }

    [Fact]
    public void H5Lvisit_by_nameTest2()
    {
        var path = string.Join("/", H5LFixture.m_utf8strings);
        var pathStringPtr = Marshal.StringToCoTaskMemUTF8(path);
        var firstUtf8StringPtr = Marshal.StringToCoTaskMemUTF8(H5LFixture.m_utf8strings[0]);

        Assert.True(H5G.create(m_v0_test_file, pathStringPtr, H5LFixture.m_lcpl_utf8) >= 0);
        Assert.True(H5G.create(m_v2_test_file, pathStringPtr, H5LFixture.m_lcpl_utf8) >= 0);

        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        nint op_data = (nint)hnd;
        // the callback is defined in H5LTest.cs
        H5L.iterate_t cb = H5LFixture.DelegateMethod;

        Assert.True(H5L.visit_by_name(m_v0_test_file, firstUtf8StringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, cb, op_data) >= 0);
        // we should have 4 elements in the array list
        Assert.True(al.Count == 4);

        Assert.True(H5L.visit_by_name(m_v2_test_file, firstUtf8StringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, cb, op_data) >= 0);
        // we should have 8 (4 + 4) elements in the array list
        Assert.True(al.Count == 8);

        hnd.Free();
        Marshal.FreeCoTaskMem(pathStringPtr);
        Marshal.FreeCoTaskMem(firstUtf8StringPtr);
    }
}
