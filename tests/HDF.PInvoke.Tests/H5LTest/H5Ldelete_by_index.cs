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
    public void H5Ldelete_by_indexTest1()
    {
        var abcd0StringPtr = Marshal.StringToHGlobalAnsi("A/B/C/D0");
        var abcd1StringPtr = Marshal.StringToHGlobalAnsi("A/B/C/D1");
        var abcd2StringPtr = Marshal.StringToHGlobalAnsi("A/B/C/D2");
        var shortcutStringPtr = Marshal.StringToHGlobalAnsi("shortcut");
        var abcStringPtr = Marshal.StringToHGlobalAnsi("A/B/C");

        Assert.True(H5G.close(H5G.create(m_v0_test_file, abcd0StringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5G.close(H5G.create(m_v0_test_file, abcd1StringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5G.close(H5G.create(m_v0_test_file, abcd2StringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5L.create_hard(m_v0_test_file, abcd1StringPtr, m_v0_test_file, shortcutStringPtr) >= 0);

        Assert.True(H5L.delete_by_idx(m_v0_test_file, abcStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 1) >= 0);

        Assert.True(H5L.exists(m_v0_test_file, shortcutStringPtr) > 0);
        Assert.True(H5L.exists(m_v0_test_file, abcd1StringPtr) == 0);


        Assert.True(H5G.close(H5G.create(m_v2_test_file, abcd0StringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5G.close(H5G.create(m_v2_test_file, abcd1StringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5G.close(H5G.create(m_v2_test_file, abcd2StringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5L.create_hard(m_v2_test_file, abcd1StringPtr, m_v2_test_file, shortcutStringPtr) >= 0);

        Assert.True(H5L.delete_by_idx(m_v2_test_file, abcStringPtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 1) >= 0);

        Assert.True(H5L.exists(m_v2_test_file, shortcutStringPtr) > 0);
        Assert.True(H5L.exists(m_v2_test_file, abcd1StringPtr) == 0);

        Marshal.FreeHGlobal(abcd0StringPtr);
        Marshal.FreeHGlobal(abcd1StringPtr);
        Marshal.FreeHGlobal(abcd2StringPtr);
        Marshal.FreeHGlobal(shortcutStringPtr);
        Marshal.FreeHGlobal(abcStringPtr);
    }
}
