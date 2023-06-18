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
using System.Text;

public partial class H5OTest
{
    [Fact]
    public void H5OvisitTest1()
    {
        Assert.True(H5G.create(m_v0_test_file, "A/B/C/D", H5OFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_hard(m_v0_test_file, "A/B/C/D", m_v0_test_file, "shortcut") >= 0);

        Assert.True(H5G.create(m_v2_test_file, "A/B/C/D", H5OFixture.m_lcpl) >= 0);
        Assert.True(H5L.create_hard(m_v2_test_file, "A/B/C/D", m_v2_test_file, "shortcut") >= 0);

        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        IntPtr op_data = (IntPtr)hnd;
        // the callback is defined in H5LTest.cs
        H5O.iterate_t cb = H5OFixture.DelegateMethod;

        Assert.True(H5O.visit(m_v0_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, cb, op_data) >= 0);
        // we should have 5 elements in the array list
        Assert.True(al.Count == 5);

        Assert.True(H5O.visit(m_v2_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, cb, op_data) >= 0);
        // we should have 10 (5 + 5) elements in the array list
        Assert.True(al.Count == 10);

        hnd.Free();
    }

    [Fact]
    public void H5OvisitTest2()
    {
        string path = string.Join("/", H5OFixture.m_utf8strings);
        Assert.True(H5G.create(m_v0_test_file, Encoding.UTF8.GetBytes(path), H5OFixture.m_lcpl_utf8) >= 0);
        Assert.True(H5G.create(m_v2_test_file, Encoding.UTF8.GetBytes(path), H5OFixture.m_lcpl_utf8) >= 0);

        ArrayList al = new ArrayList();
        GCHandle hnd = GCHandle.Alloc(al);
        IntPtr op_data = (IntPtr)hnd;
        // the callback is defined in H5LTest.cs
        H5O.iterate_t cb = H5OFixture.DelegateMethod;

        Assert.True(H5O.visit(m_v0_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, cb, op_data) >= 0);
        // we should have 6 elements in the array list
        Assert.True(al.Count == 6);

        Assert.True(H5O.visit(m_v2_test_file, H5.index_t.NAME, H5.iter_order_t.NATIVE, cb, op_data) >= 0);
        // we should have 12 (6 + 6) elements in the array list
        Assert.True(al.Count == 12);

        hnd.Free();
    }
}
