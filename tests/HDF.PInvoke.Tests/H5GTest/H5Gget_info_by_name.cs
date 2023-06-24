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
    public void H5Gget_info_by_nameTest1()
    {
        var dotStringPtr = Marshal.StringToHGlobalAnsi(".");

        H5G.info_t info = new H5G.info_t();
        Assert.True(H5G.get_info_by_name(H5GFixture.m_v0_class_file, dotStringPtr, ref info) >= 0);
        Assert.True(H5G.get_info_by_name(m_v0_test_file, dotStringPtr, ref info) >= 0);
        Assert.True(H5G.get_info_by_name(H5GFixture.m_v2_class_file, dotStringPtr, ref info) >= 0);
        Assert.True(H5G.get_info_by_name(m_v2_test_file, dotStringPtr, ref info) >= 0);

        Marshal.FreeHGlobal(dotStringPtr);
    }

    [Fact]
    public void H5Gget_info_by_nameTest2()
    {
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");

        hid_t group = H5G.create(m_v0_test_file, aStringPtr);
        Assert.True(group >= 0);
        H5G.info_t info = new H5G.info_t();
        Assert.True(H5G.get_info_by_name(m_v0_test_file, aStringPtr, ref info) >= 0);
        Assert.True(H5G.close(group) >= 0);

        group = H5G.create(m_v2_test_file, aStringPtr);
        Assert.True(group >= 0);
        Assert.True(H5G.get_info_by_name(m_v2_test_file, aStringPtr, ref info) >= 0);
        Assert.True(H5G.close(group) >= 0);

        Marshal.FreeHGlobal(aStringPtr);
    }
}
