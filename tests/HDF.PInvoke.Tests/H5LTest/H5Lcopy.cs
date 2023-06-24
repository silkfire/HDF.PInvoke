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

public partial class H5LTest
{
    [Fact]
    public void H5LcopyTest1()
    {
        var abcStringPtr = Marshal.StringToHGlobalAnsi("A/B/C");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var aCopyStringPtr = Marshal.StringToHGlobalAnsi("A_copy");
        var abcACopyBStringPtr = Marshal.StringToHGlobalAnsi("A/B/C/A_copy/B");

        hid_t gid = H5G.create(m_v0_test_file, abcStringPtr, H5LFixture.m_lcpl);
        Assert.True(gid >= 0);

        Assert.True(H5L.copy(m_v0_test_file, aStringPtr, gid, aCopyStringPtr) >= 0);
        Assert.True(H5L.exists(m_v0_test_file, abcACopyBStringPtr) > 0);

        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, abcStringPtr, H5LFixture.m_lcpl);
        Assert.True(gid >= 0);

        Assert.True(H5L.copy(m_v2_test_file, aStringPtr, gid, aCopyStringPtr) >= 0);
        Assert.True(H5L.exists(m_v2_test_file, abcACopyBStringPtr) > 0);

        Assert.True(H5G.close(gid) >= 0);

        Marshal.FreeHGlobal(abcStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(aCopyStringPtr);
        Marshal.FreeHGlobal(abcACopyBStringPtr);
    }

    [Fact]
    public void H5LcopyTest2()
    {
        var abcStringPtr = Marshal.StringToHGlobalAnsi("A/B/C");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var cbaCopyStringPtr = Marshal.StringToHGlobalAnsi("C/B/A_copy");

        Assert.True(H5L.create_soft(abcStringPtr, m_v0_test_file, aStringPtr) >= 0);

        // copy symlink to the other test file

        Assert.True(H5L.copy(m_v0_test_file, aStringPtr, m_v2_test_file, cbaCopyStringPtr, H5LFixture.m_lcpl) >= 0);

        Marshal.FreeHGlobal(abcStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(cbaCopyStringPtr);
    }

    [Fact]
    public void H5LcopyTest3()
    {
        var abcStringPtr = Marshal.StringToHGlobalAnsi("A/B/C");
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");

        hid_t gid = H5G.create(m_v0_test_file, abcStringPtr, H5LFixture.m_lcpl);
        Assert.True(gid >= 0);

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            var utf8StringPtr = Marshal.StringToCoTaskMemUTF8(H5LFixture.m_utf8strings[i]);
            var pathStringPtr = Marshal.StringToCoTaskMemUTF8($"A/B/C/{H5LFixture.m_utf8strings[i]}/B");

            Assert.True(H5L.copy(m_v0_test_file, aStringPtr, gid, utf8StringPtr, H5LFixture.m_lcpl_utf8) >= 0);

            Assert.True(H5L.exists(m_v0_test_file, pathStringPtr) > 0);

            Marshal.FreeCoTaskMem(utf8StringPtr);
            Marshal.FreeCoTaskMem(pathStringPtr);
        }

        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, abcStringPtr, H5LFixture.m_lcpl);
        Assert.True(gid >= 0);

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            var utf8StringPtr = Marshal.StringToCoTaskMemUTF8(H5LFixture.m_utf8strings[i]);
            var pathStringPtr = Marshal.StringToCoTaskMemUTF8($"A/B/C/{H5LFixture.m_utf8strings[i]}/B");

            Assert.True(H5L.copy(m_v2_test_file, aStringPtr, gid, utf8StringPtr, H5LFixture.m_lcpl_utf8) >= 0);

            Assert.True(H5L.exists(m_v2_test_file, pathStringPtr) > 0);

            Marshal.FreeCoTaskMem(utf8StringPtr);
            Marshal.FreeCoTaskMem(pathStringPtr);
        }

        Assert.True(H5G.close(gid) >= 0);

        Marshal.FreeHGlobal(abcStringPtr);
        Marshal.FreeHGlobal(aStringPtr);
    }
}
