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

using hid_t = System.Int64;

namespace HDF.PInvoke.Tests;

using HDF5;
using Xunit;

public partial class H5TTest
{
    [Fact]
    public void H5TcommitTest1()
    {
        hid_t dtype = H5T.copy(H5T.IEEE_F64LE);
        Assert.True(dtype >= 0);
        Assert.True(H5T.commit(m_v0_test_file, "foo", dtype) >= 0);
        // can't commit twice
        Assert.False(H5T.commit(m_v0_test_file, "bar", dtype) >= 0);
        // can't commit to different files
        Assert.False(H5T.commit(m_v2_test_file, "bar", dtype) >= 0);
        Assert.True(H5T.close(dtype) >= 0);
    }

    [Fact]
    public void H5TcommitTest2()
    {
        // can't commit pre-defined types
        Assert.False(H5T.commit(m_v0_test_file, "foo", H5T.IEEE_F64BE) >= 0);
        Assert.False(H5T.commit(m_v2_test_file, "bar", H5T.IEEE_F64BE) >= 0);
    }

    [Fact]
    public void H5TcommitTest3()
    {
        Assert.False(H5T.commit(m_v0_test_file, "foo", Utilities.RandomInvalidHandle()) >= 0);
        Assert.False(H5T.commit(Utilities.RandomInvalidHandle(), "foo", Utilities.RandomInvalidHandle()) >= 0);
    }
}