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

public partial class H5OTest
{
    [Fact]
    public void H5OcopyTest1()
    {
        var abcStringPtr = Marshal.StringToHGlobalAnsi("A/B/C");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var aCopyStringPtr = Marshal.StringToHGlobalAnsi("A_copy");
        var aCopyBCStringPtr = Marshal.StringToHGlobalAnsi("A_copy/B/C");

        hid_t gid = H5G.create(m_v0_test_file, abcStringPtr, H5OFixture.m_lcpl);
        Assert.True(gid >= 0);

        Assert.True(H5O.copy(m_v0_test_file, aStringPtr, m_v2_test_file, aCopyStringPtr) >= 0);

        Assert.True(H5L.exists(m_v2_test_file, aCopyBCStringPtr) > 0);

        Assert.True(H5G.close(gid) >= 0);

        Marshal.FreeHGlobal(abcStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(aCopyStringPtr);
        Marshal.FreeHGlobal(aCopyBCStringPtr);
    }

    [Fact]
    public void H5OcopyTest2()
    {
        var myFantasticPathStringPtr = Marshal.StringToHGlobalAnsi("/my/fantastic/path");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var dotStringPtr = Marshal.StringToHGlobalAnsi(".");
        var cbaCopyStringPtr = Marshal.StringToHGlobalAnsi("C/B/A_copy");

        Assert.True(H5L.create_soft(myFantasticPathStringPtr, m_v0_test_file, aStringPtr) >= 0);

        Assert.True(H5O.copy(m_v0_test_file, dotStringPtr, m_v2_test_file, cbaCopyStringPtr, H5P.DEFAULT, H5OFixture.m_lcpl) >= 0);

        Marshal.FreeHGlobal(myFantasticPathStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(dotStringPtr);
        Marshal.FreeHGlobal(cbaCopyStringPtr);
    }
}
