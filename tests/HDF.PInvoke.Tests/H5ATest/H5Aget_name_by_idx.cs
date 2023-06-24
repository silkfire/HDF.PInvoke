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
    public void H5Aget_name_by_idxTest1()
    {
        var dotNamePtr = Marshal.StringToHGlobalAnsi(".");

        Assert.False(H5A.get_name_by_idx(Utilities.RandomInvalidHandle(), dotNamePtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 0, nint.Zero, ssize_t.Zero, H5P.DEFAULT) >= 0);

        Marshal.FreeHGlobal(dotNamePtr);
    }

    [Fact]
    public void H5Aget_name_by_idxTest2()
    {
        var getNameNamePtr = Marshal.StringToHGlobalAnsi("H5Aget_name");
        var getNameByIdxNamePtr = Marshal.StringToHGlobalAnsi("H5Aget_name_by_idx");
        var dotNamePtr = Marshal.StringToHGlobalAnsi(".");

        hid_t att = H5A.create(m_v2_test_file, getNameNamePtr, H5T.IEEE_F64LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);
        att = H5A.create(m_v2_test_file, getNameByIdxNamePtr, H5T.STD_I16LE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        string name = string.Join(":", H5AFixture.m_utf8strings);
        var nameNamePtr = Marshal.StringToCoTaskMemUTF8(name);

        att = H5A.create(m_v2_test_file, nameNamePtr, H5T.IEEE_F64BE, H5AFixture.m_space_scalar, H5AFixture.m_acpl);
        Assert.True(att >= 0);
        Marshal.FreeCoTaskMem(nameNamePtr);

        ssize_t buf_size = H5A.get_name(att, ssize_t.Zero, nint.Zero) + 1;
        Assert.True(buf_size > 1);
        var buf = Marshal.AllocHGlobal(buf_size);
        Assert.True(H5A.get_name_by_idx(m_v2_test_file, dotNamePtr, H5.index_t.NAME, H5.iter_order_t.NATIVE, 2, buf, buf_size, H5P.DEFAULT) >= 0);
        var bufString = Marshal.PtrToStringUTF8(buf);
        Marshal.FreeHGlobal(buf);

        Assert.Equal(name, bufString);

        Assert.True(H5A.close(att) >= 0);

        Marshal.FreeHGlobal(getNameNamePtr);
        Marshal.FreeHGlobal(getNameByIdxNamePtr);
        Marshal.FreeHGlobal(dotNamePtr);
    }
}
