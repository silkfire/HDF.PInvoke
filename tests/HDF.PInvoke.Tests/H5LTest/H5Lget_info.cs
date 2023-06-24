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
    public void H5Lget_infoTest1()
    {
        var v0ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v0_class_file_name);
        var slashStringPtr = Marshal.StringToHGlobalAnsi("/");
        var abcStringPtr = Marshal.StringToHGlobalAnsi("A/B/C");
        var v2ClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(H5LFixture.m_v0_class_file_name);

        Assert.True(H5L.create_external(v0ClassFileNameStringPtr, slashStringPtr, m_v0_test_file, abcStringPtr, H5LFixture.m_lcpl) >= 0);

        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, abcStringPtr, ref info) >= 0);
        Assert.True(info.type == H5L.type_t.EXTERNAL);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        Assert.True(info.u.val_size.ToInt64() > 0);

        Assert.True(H5L.create_external(v2ClassFileNameStringPtr, slashStringPtr, m_v2_test_file, abcStringPtr, H5LFixture.m_lcpl) >= 0);

        Assert.True(H5L.get_info(m_v2_test_file, abcStringPtr, ref info) >= 0);
        Assert.True(info.type == H5L.type_t.EXTERNAL);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        Assert.True(info.u.val_size.ToInt64() > 0);

        Marshal.FreeHGlobal(v0ClassFileNameStringPtr);
        Marshal.FreeHGlobal(slashStringPtr);
        Marshal.FreeHGlobal(abcStringPtr);
        Marshal.FreeHGlobal(v2ClassFileNameStringPtr);
    }

    [Fact]
    public void H5Lget_infoTest2()
    {
        var abcdStringPtr = Marshal.StringToHGlobalAnsi("A/B/C/D");

        Assert.True(H5G.close(H5G.create(m_v0_test_file, abcdStringPtr, H5LFixture.m_lcpl)) >= 0);
        H5L.info_t info = new H5L.info_t();
        Assert.True(H5L.get_info(m_v0_test_file, abcdStringPtr, ref info) >= 0);
        Assert.True(info.type == H5L.type_t.HARD);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        Assert.True(info.u.val_size.ToInt64() == 3896);

        Assert.True(H5G.close(H5G.create(m_v2_test_file, abcdStringPtr, H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5L.get_info(m_v2_test_file, abcdStringPtr, ref info) >= 0);
        Assert.True(info.type == H5L.type_t.HARD);
        Assert.True(info.corder_valid == 0);
        Assert.True(info.cset == H5T.cset_t.ASCII);
        Assert.True(info.u.val_size.ToInt64() == 636);

        Marshal.FreeHGlobal(abcdStringPtr);
    }
}
