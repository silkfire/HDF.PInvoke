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

public partial class H5GTest
{
    [Fact]
    public void H5GcreateTest1()
    {
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var bStringPtr = Marshal.StringToHGlobalAnsi("B");

        hid_t gid = H5G.create(m_v0_test_file, aStringPtr);
        Assert.True(gid > 0);

        hid_t gid1 = H5G.create(gid, bStringPtr);
        Assert.True(gid1 > 0);

        Assert.True(H5G.close(gid1) >= 0);
        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, aStringPtr);
        Assert.True(gid > 0);

        gid1 = H5G.create(gid, bStringPtr);
        Assert.True(gid1 > 0);

        Assert.True(H5G.close(gid1) >= 0);
        Assert.True(H5G.close(gid) >= 0);

        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(bStringPtr);
    }

    [Fact]
    public void H5GcreateTest2()
    {
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");

        hid_t file = Utilities.RandomInvalidHandle();
        hid_t gid = H5G.create(file, aStringPtr);
        Assert.True(gid < 0);

        Marshal.FreeHGlobal(aStringPtr);
    }

    [Fact]
    public void H5GcreateTest3()
    {
        var abcdefghStringPtr = Marshal.StringToHGlobalAnsi("A/B/C/D/E/F/G/H");

        hid_t lcpl = H5P.create(H5P.LINK_CREATE);
        Assert.True(lcpl >= 0);
        Assert.True(H5P.set_create_intermediate_group(lcpl, 1) >= 0);
        hid_t gid = H5G.create(m_v0_test_file, abcdefghStringPtr, lcpl);
        Assert.True(gid > 0);
        Assert.True(H5G.close(gid) >= 0);
        hid_t gid1 = H5G.create(m_v2_test_file, abcdefghStringPtr, lcpl);
        Assert.True(gid1 > 0);
        Assert.True(H5G.close(gid1) >= 0);
        Assert.True(H5P.close(lcpl) >= 0);

        Marshal.FreeHGlobal(abcdefghStringPtr);
    }
}
