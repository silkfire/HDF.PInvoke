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

using ssize_t = nint;
using hid_t = System.Int64;

using HDF5;

using Xunit;

using System.Runtime.InteropServices;

public partial class H5ITest
{
    [Fact]
    public void H5Iget_nameTest1()
    {
        var aaaaaaaaaaaaaaaaaaaaaStringPtr = Marshal.StringToHGlobalAnsi("AAAAAAAAAAAAAAAAAAAAA");

        hid_t gid = H5G.create(m_v0_test_file, aaaaaaaaaaaaaaaaaaaaaStringPtr);
        Assert.True(gid > 0);

        ssize_t buf_size = H5I.get_name(gid, nint.Zero, ssize_t.Zero) + 1;
        Assert.True(buf_size.ToInt32() > 1);
        var buf = Marshal.AllocHGlobal(buf_size);
        ssize_t size = H5I.get_name(gid, buf, buf_size);
        Assert.True(size.ToInt32() > 0);
        Assert.Equal("/AAAAAAAAAAAAAAAAAAAAA", Marshal.PtrToStringAnsi(buf));

        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, aaaaaaaaaaaaaaaaaaaaaStringPtr);
        Assert.True(gid > 0);

        buf_size = H5I.get_name(gid, nint.Zero, ssize_t.Zero) + 1;
        Assert.True(buf_size.ToInt32() > 1);
        buf = Marshal.AllocHGlobal(buf_size);
        size = H5I.get_name(gid, buf, buf_size);
        Assert.True(size.ToInt32() > 0);
        Assert.Equal("/AAAAAAAAAAAAAAAAAAAAA", Marshal.PtrToStringAnsi(buf));

        Assert.True(H5G.close(gid) >= 0);

        Marshal.FreeHGlobal(buf);
        Marshal.FreeHGlobal(aaaaaaaaaaaaaaaaaaaaaStringPtr);
    }

    [Fact]
    public void H5Iget_nameTest2()
    {
        ssize_t size = H5I.get_name(Utilities.RandomInvalidHandle(), nint.Zero, ssize_t.Zero);
        Assert.False(size.ToInt32() >= 0);
    }
}
