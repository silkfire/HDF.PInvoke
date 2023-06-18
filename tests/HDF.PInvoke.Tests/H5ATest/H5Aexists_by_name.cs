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

using htri_t = System.Int32;

using HDF5;
using Xunit;

public partial class H5ATest
{
    [Fact]
    public void H5Aexists_by_nameTest1()
    {
        htri_t check = H5A.exists_by_name(H5AFixture.m_v0_class_file, ".", ".");
        Assert.True(check >= 0);
        check = H5A.exists_by_name(H5AFixture.m_v0_class_file, ".", "NAC");
        Assert.True(check >= 0);
        check = H5A.exists_by_name(H5AFixture.m_v2_class_file, ".", "A");
        Assert.True(check >= 0);
    }

    [Fact]
    public void H5Aexists_by_nameTest2()
    {
        Assert.False(H5A.exists(Utilities.RandomInvalidHandle(), ".") >= 0);
        Assert.False(H5A.exists(H5AFixture.m_v2_class_file, "") >= 0);
    }
}
