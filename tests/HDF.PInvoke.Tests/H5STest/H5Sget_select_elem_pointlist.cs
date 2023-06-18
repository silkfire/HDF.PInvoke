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

public partial class H5STest
{
    [Fact]
    public void H5Sget_select_elem_pointlistTest1()
    {
        hsize_t[] dims = { 1, 2, 3 };
        hid_t space = H5S.create_simple(dims.Length, dims, dims);
        Assert.True(space > 0);
        hsize_t[] sel = { 0, 1, 2, 0, 2, 2 };
        Assert.True(H5S.select_elements(space, H5S.seloper_t.SET, new IntPtr(2), sel) >= 0);
        hsize_t[] buf = new hsize_t[sel.Length];
        Assert.True(H5S.get_select_elem_pointlist(space, 0, 2, buf) >= 0);

        for (int i = 0; i < buf.Length; ++i)
        {
            Assert.True(sel[i] == buf[i]);
        }

        Assert.True(H5S.close(space) >= 0);
    }

    [Fact]
    public void H5Sget_select_elem_pointlistTest2()
    {
        Assert.False(H5S.get_select_elem_pointlist(Utilities.RandomInvalidHandle(), 0, 0, null) >= 0);
    }
}
