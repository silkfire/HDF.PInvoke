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

public partial class H5FTest
{
    [Fact]
    public void H5FunmountTest1()
    {
        var uaStringPtr = Marshal.StringToHGlobalAnsi("UA");
        var ubStringPtr = Marshal.StringToHGlobalAnsi("UB");
        var ucStringPtr = Marshal.StringToHGlobalAnsi("UC");

        hid_t mount_point = H5G.create(H5FFixture.m_v0_class_file, uaStringPtr);
        Assert.True(mount_point >= 0);
        Assert.True(H5G.close(mount_point) >= 0);
        Assert.True(H5F.mount(H5FFixture.m_v0_class_file, uaStringPtr, m_v0_test_file) >= 0);

        mount_point = H5G.create(H5FFixture.m_v0_class_file, ubStringPtr);
        Assert.True(mount_point >= 0);
        Assert.True(H5G.close(mount_point) >= 0);
        Assert.True(H5F.mount(H5FFixture.m_v0_class_file, ubStringPtr, m_v2_test_file) >= 0);

        mount_point = H5G.create(H5FFixture.m_v2_class_file, ucStringPtr);
        Assert.True(mount_point >= 0);
        Assert.True(H5G.close(mount_point) >= 0);
        Assert.True(H5F.mount(H5FFixture.m_v2_class_file, ucStringPtr, H5FFixture.m_v0_class_file) >= 0);

        Assert.True(H5F.unmount(H5FFixture.m_v2_class_file, ucStringPtr) >= 0);
        Assert.True(H5F.unmount(H5FFixture.m_v0_class_file, ubStringPtr) >= 0);
        Assert.True(H5F.unmount(H5FFixture.m_v0_class_file, uaStringPtr) >= 0);

        Marshal.FreeHGlobal(uaStringPtr);
        Marshal.FreeHGlobal(ubStringPtr);
        Marshal.FreeHGlobal(ucStringPtr);
    }

    [Fact]
    public void H5FunmountTest2()
    {
        var aaStringPtr = Marshal.StringToHGlobalAnsi("AA");
        var emptyStringPtr = Marshal.StringToHGlobalAnsi("");

        Assert.False(H5F.unmount(Utilities.RandomInvalidHandle(), aaStringPtr) >= 0);
        Assert.False(H5F.unmount(H5FFixture.m_v0_class_file, emptyStringPtr) >= 0);

        Marshal.FreeHGlobal(aaStringPtr);
        Marshal.FreeHGlobal(emptyStringPtr);
    }
}
