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

using size_t = nint;

using HDF5;

using System.Runtime.InteropServices;

using Xunit;

public partial class H5FTest
{
    [Fact]
    public void H5Fget_nameTest1()
    {
        var size = H5F.get_name(m_v0_test_file, nint.Zero, size_t.Zero);
        Assert.True(size >= 0);

        var buf = Marshal.AllocHGlobal(size.ToInt32() + 1);
        size = H5F.get_name(m_v0_test_file, buf, size.ToInt32() + 1);
        Assert.True(size >= 0);

        string name = Marshal.PtrToStringAnsi(buf);
        // names should match
        Assert.Equal(m_v0_test_file_name, name);


        size = H5F.get_name(m_v2_test_file, nint.Zero, size_t.Zero);
        Assert.True(size >= 0);

        buf = Marshal.AllocHGlobal(size.ToInt32() + 1);
        size = H5F.get_name(m_v2_test_file, buf, size.ToInt32() + 1);
        Assert.True(size >= 0);

        name = Marshal.PtrToStringAnsi(buf);
        // names should match
        Assert.Equal(m_v2_test_file_name, name);
    }

    [Fact]
    public void H5Fget_nameTest2()
    {
        Assert.True(H5F.get_name(Utilities.RandomInvalidHandle(), nint.Zero, size_t.Zero).ToInt32() < 0);
    }
}
