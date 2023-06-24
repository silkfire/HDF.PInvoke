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

using System.Runtime.InteropServices;

public partial class H5LTest
{
    [Fact]
    public void H5LexistsTest1()
    {
        var abcdStringPtr = Marshal.StringToHGlobalAnsi("A/B/C/D");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var abStringPtr = Marshal.StringToHGlobalAnsi("A/B");
        var acbStringPtr = Marshal.StringToHGlobalAnsi("A/C/B");

        Assert.True(H5G.close(H5G.create(m_v0_test_file, abcdStringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5L.exists(m_v0_test_file, aStringPtr) > 0);
        Assert.True(H5L.exists(m_v0_test_file, abStringPtr) > 0);
        Assert.True(H5L.exists(m_v0_test_file, acbStringPtr) < 0);

        Assert.True(H5G.close(H5G.create(m_v2_test_file, abcdStringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5L.exists(m_v2_test_file, aStringPtr) > 0);
        Assert.True(H5L.exists(m_v2_test_file, abStringPtr) > 0);
        Assert.True(H5L.exists(m_v2_test_file, acbStringPtr) < 0);

        Marshal.FreeHGlobal(abcdStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(abStringPtr);
        Marshal.FreeHGlobal(acbStringPtr);
    }

    [Fact]
    public void H5LexistsTest2()
    {
        var dotStringPtr = Marshal.StringToHGlobalAnsi(".");

        Assert.True(H5L.exists(m_v0_test_file, dotStringPtr) == 0);
        Assert.True(H5L.exists(m_v2_test_file, dotStringPtr) == 0);

        Marshal.FreeHGlobal(dotStringPtr);
    }

    [Fact]
    public void H5LexistsTest3()
    {
        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            var utf8StringPtr = Marshal.StringToCoTaskMemUTF8(H5LFixture.m_utf8strings[i]);

            Assert.True(H5G.close(H5G.create(m_v0_test_file, utf8StringPtr, H5LFixture.m_lcpl_utf8)) >= 0);
            Assert.True(H5L.exists(m_v0_test_file, utf8StringPtr) > 0);

            Assert.True(H5G.close(H5G.create(m_v2_test_file, utf8StringPtr, H5LFixture.m_lcpl_utf8)) >= 0);
            Assert.True(H5L.exists(m_v2_test_file, utf8StringPtr) > 0);

            Marshal.FreeCoTaskMem(utf8StringPtr);
        }
    }
}
