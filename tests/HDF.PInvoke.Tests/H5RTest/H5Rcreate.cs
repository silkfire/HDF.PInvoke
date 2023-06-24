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

using hsize_t = System.UInt64;
using ssize_t = nint;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System.Runtime.InteropServices;

public partial class H5RTest
{
    [Fact]
    public void H5RcreateTest1()
    {
        var path = string.Join("/", H5RFixture.m_utf8strings);
        var pathStringPtr = Marshal.StringToCoTaskMemUTF8(path);

        Assert.True(H5G.close(H5G.create(m_v0_test_file, pathStringPtr, H5RFixture.m_lcpl_utf8)) >= 0);

        var refer = new byte[H5R.OBJ_REF_BUF_SIZE];
        var referPtr = Marshal.AllocHGlobal(refer.Length);
        Marshal.Copy(refer, 0, referPtr, refer.Length);

        Assert.True(H5R.create(referPtr, m_v0_test_file, pathStringPtr, H5R.type_t.OBJECT, -1) >= 0);

        ssize_t size = H5R.get_name(m_v0_test_file, H5R.type_t.OBJECT, referPtr, nint.Zero, ssize_t.Zero);
        Assert.True(size.ToInt32() == path.Length);

        var bufLen = size.ToInt32() + 1;
        var bufPtr = Marshal.AllocHGlobal(bufLen);
        size = H5R.get_name(m_v0_test_file, H5R.type_t.OBJECT, referPtr, bufPtr, new ssize_t(bufLen));
        Assert.True(size.ToInt32() == path.Length);
        var buf = Marshal.PtrToStringUTF8(bufPtr);
        //var buf = Marshal.PtrToStringUTF8(bufPtr, bufLen);

        // we need to account for the leading "/", which was not included in path
        for (int i = 0; i < path.Length; ++i)
        {
            Assert.True(path[i] == buf[i + 1]);
        }

        Marshal.FreeCoTaskMem(pathStringPtr);
        Marshal.FreeHGlobal(referPtr);
        Marshal.FreeHGlobal(bufPtr);
    }

    [Fact]
    public void H5RcreateTest2()
    {
        var path = string.Join("/", H5RFixture.m_utf8strings);
        var pathStringPtr = Marshal.StringToCoTaskMemUTF8(path);

        hsize_t[] dims = { 10, 20 };
        hid_t space = H5S.create_simple(2, dims, null);
        Assert.True(space >= 0);
        hid_t dset = H5D.create(m_v0_test_file, pathStringPtr, H5T.STD_I32LE, space, H5RFixture.m_lcpl_utf8);
        Assert.True(dset >= 0);
        hsize_t[] start = { 5, 10 };
        hsize_t[] count = { 1, 1 };
        hsize_t[] block = { 2, 4 };
        Assert.True(H5S.select_hyperslab(space, H5S.seloper_t.SET, start, null, count, block) >= 0);

        var refer = new byte[H5R.DSET_REG_REF_BUF_SIZE];
        var referPtr = Marshal.AllocHGlobal(refer.Length);
        Marshal.Copy(refer, 0, referPtr, refer.Length);

        Assert.True(H5R.create(referPtr, m_v0_test_file, pathStringPtr, H5R.type_t.DATASET_REGION, space) >= 0);

        ssize_t size = H5R.get_name(m_v0_test_file, H5R.type_t.DATASET_REGION, referPtr, nint.Zero, ssize_t.Zero);
        Assert.True(size.ToInt32() == path.Length);

        var bufLen = size.ToInt32() + 1;
        var bufPtr = Marshal.AllocHGlobal(bufLen);
        size = H5R.get_name(m_v0_test_file, H5R.type_t.DATASET_REGION, referPtr, bufPtr, new ssize_t(bufLen));
        Assert.True(size.ToInt32() == path.Length);
        var buf = Marshal.PtrToStringUTF8(bufPtr);

        // we need to account for the leading "/", which was not included in path
        for (int i = 0; i < path.Length; ++i)
        {
            Assert.True(path[i] == buf[i + 1]);
        }

        hid_t sel = H5R.get_region(dset, H5R.type_t.DATASET_REGION, referPtr);
        Assert.True(sel >= 0);

        Assert.True(H5S.extent_equal(space, sel) > 0);
        Assert.True(H5S.get_select_hyper_nblocks(space) == H5S.get_select_hyper_nblocks(sel));

        Assert.True(H5S.close(sel) >= 0);
        Assert.True(H5D.close(dset) >= 0);
        Assert.True(H5S.close(space) >= 0);

        Marshal.FreeCoTaskMem(pathStringPtr);
        Marshal.FreeHGlobal(referPtr);
        Marshal.FreeHGlobal(bufPtr);
    }

    [Fact]
    public void H5RcreateTest3()
    {
        var path = string.Join("/", H5RFixture.m_utf8strings);
        var pathStringPtr = Marshal.StringToCoTaskMemUTF8(path);

        hsize_t[] dims = { 10, 20 };
        hid_t space = H5S.create_simple(2, dims, null);
        Assert.True(space >= 0);
        hid_t dset = H5D.create(m_v2_test_file, pathStringPtr, H5T.STD_I32LE, space, H5RFixture.m_lcpl_utf8);
        Assert.True(dset >= 0);
        hsize_t[] start = { 5, 10 };
        hsize_t[] count = { 1, 1 };
        hsize_t[] block = { 2, 4 };
        Assert.True(H5S.select_hyperslab(space, H5S.seloper_t.SET, start, null, count, block) >= 0);

        var refer = new byte[H5R.DSET_REG_REF_BUF_SIZE];
        var referPtr = Marshal.AllocHGlobal(refer.Length);
        Marshal.Copy(refer, 0, referPtr, refer.Length);

        Assert.True(H5R.create(referPtr, m_v2_test_file, pathStringPtr, H5R.type_t.DATASET_REGION, space) >= 0);

        ssize_t size = H5R.get_name(m_v2_test_file, H5R.type_t.DATASET_REGION, referPtr, nint.Zero, ssize_t.Zero);
        Assert.True(size.ToInt32() == path.Length);

        var bufLen = size.ToInt32() + 1;
        var bufPtr = Marshal.AllocHGlobal(bufLen);
        size = H5R.get_name(m_v2_test_file, H5R.type_t.DATASET_REGION, referPtr, bufPtr, new ssize_t(bufLen));
        Assert.True(size.ToInt32() == path.Length);
        var buf = Marshal.PtrToStringUTF8(bufPtr);

        // we need to account for the leading "/", which was not included in path
        for (int i = 0; i < path.Length; ++i)
        {
            Assert.True(path[i] == buf[i + 1]);
        }

        hid_t sel = H5R.get_region(dset, H5R.type_t.DATASET_REGION, referPtr);
        Assert.True(sel >= 0);

        Assert.True(H5S.extent_equal(space, sel) > 0);
        Assert.True(H5S.get_select_hyper_nblocks(space) == H5S.get_select_hyper_nblocks(sel));

        Assert.True(H5S.close(sel) >= 0);
        Assert.True(H5D.close(dset) >= 0);
        Assert.True(H5S.close(space) >= 0);

        Marshal.FreeCoTaskMem(pathStringPtr);
        Marshal.FreeHGlobal(referPtr);
        Marshal.FreeHGlobal(bufPtr);
    }
}
