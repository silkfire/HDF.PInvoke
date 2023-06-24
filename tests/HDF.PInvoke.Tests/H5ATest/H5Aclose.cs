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

using hid_t = System.Int64;

using HDF5;
using Xunit;
using System.Runtime.InteropServices;

public partial class H5ATest
{
    [Fact]
    public void H5AcloseTest1()
    {
        var nacNamePtr = Marshal.StringToHGlobalAnsi("NAC");
        var sacNamePtr = Marshal.StringToHGlobalAnsi("SAC");

        hid_t att = H5A.create(H5AFixture.m_v0_class_file, nacNamePtr, H5T.IEEE_F32BE, H5AFixture.m_space_null);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(H5AFixture.m_v2_class_file, nacNamePtr, H5T.IEEE_F32BE, H5AFixture.m_space_null);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(H5AFixture.m_v0_class_file, sacNamePtr, H5T.IEEE_F32BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(H5AFixture.m_v2_class_file, sacNamePtr, H5T.IEEE_F32BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);

        Marshal.FreeHGlobal(nacNamePtr);
        Marshal.FreeHGlobal(sacNamePtr);
        Assert.True(H5A.close(att) >= 0);
    }

    [Fact]
    public void H5AcloseTest2()
    {
        Assert.False(H5A.close(Utilities.RandomInvalidHandle()) >= 0);
    }
}
