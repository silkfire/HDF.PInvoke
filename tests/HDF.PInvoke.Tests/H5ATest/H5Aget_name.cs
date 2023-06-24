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

public partial class H5ATest
{
    [Fact]
    public void H5Aget_nameTest1()
    {
        Assert.False(H5A.get_name(Utilities.RandomInvalidHandle(), nint.Zero, nint.Zero) >= 0);
    }

    [Fact]
    public void H5Aget_nameTest2()
    {
        string name = string.Join(":", H5AFixture.m_utf8strings);
        var namePtr = Marshal.StringToCoTaskMemUTF8(name);

        hid_t att = H5A.create(m_v2_test_file, namePtr, H5T.C_S1, H5AFixture.m_space_scalar, H5AFixture.m_acpl);
        Marshal.FreeCoTaskMem(namePtr);
        Assert.True(att >= 0);

        ssize_t buf_size = H5A.get_name(att, ssize_t.Zero, nint.Zero) + 1;
        Assert.True(buf_size > 1);

        var buf = Marshal.AllocHGlobal(buf_size);
        var attrLen = H5A.get_name(att, buf_size, buf);
        Assert.True(attrLen >= 0);

        var bufString = Marshal.PtrToStringUTF8(buf, attrLen.ToInt32());
        Marshal.FreeHGlobal(buf);
        Assert.Equal(name, bufString);

        Assert.True(H5A.close(att) >= 0);
    }
}
