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

using haddr_t = System.UInt64;
using hsize_t = System.UInt64;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System.Runtime.InteropServices;

public partial class H5RTest
{
    [Fact]
    public void H5RdereferenceTest1()
    {
        var path = string.Join("/", H5RFixture.m_utf8strings);
        var pathStringPtr = Marshal.StringToCoTaskMemUTF8(path);

        hid_t gid = H5G.create(m_v0_test_file, pathStringPtr, H5RFixture.m_lcpl_utf8);
        Assert.True(gid >= 0);
        H5O.info_t info = new H5O.info_t();
        Assert.True(H5O.get_info(gid, ref info) >= 0);
        haddr_t address = info.addr;
        Assert.True(H5G.close(gid) >= 0);

        var refer = new byte[H5R.OBJ_REF_BUF_SIZE];
        var referPtr = Marshal.AllocHGlobal(refer.Length);

        Assert.True(H5R.create(referPtr, m_v0_test_file, pathStringPtr, H5R.type_t.OBJECT, -1) >= 0);

        gid = H5R.dereference(m_v0_test_file, H5P.DEFAULT, H5R.type_t.OBJECT, referPtr);
        Assert.True(gid >= 0);

        Assert.True(H5O.get_info(gid, ref info) >= 0);
        Assert.True(address == info.addr);
        Assert.True(H5G.close(gid) >= 0);

        Marshal.FreeCoTaskMem(pathStringPtr);
        Marshal.FreeHGlobal(referPtr);
    }

    [Fact]
    public void H5RdereferenceTest2()
    {
        var path = string.Join("/", H5RFixture.m_utf8strings);
        var pathStringPtr = Marshal.StringToCoTaskMemUTF8(path);

        hid_t gid = H5G.create(m_v2_test_file, pathStringPtr, H5RFixture.m_lcpl_utf8);
        Assert.True(gid >= 0);
        H5O.info_t info = new H5O.info_t();
        Assert.True(H5O.get_info(gid, ref info) >= 0);
        haddr_t address = info.addr;
        Assert.True(H5G.close(gid) >= 0);

        var refer = new byte[H5R.OBJ_REF_BUF_SIZE];
        var referPtr = Marshal.AllocHGlobal(refer.Length);
        Marshal.Copy(refer, 0, referPtr, refer.Length);

        Assert.True(H5R.create(referPtr, m_v2_test_file, pathStringPtr, H5R.type_t.OBJECT, -1) >= 0);

        gid = H5R.dereference(m_v2_test_file, H5P.DEFAULT,  H5R.type_t.OBJECT, referPtr);
        Assert.True(gid >= 0);

        Assert.True(H5O.get_info(gid, ref info) >= 0);
        Assert.True(address == info.addr);
        Assert.True(H5G.close(gid) >= 0);

        Marshal.FreeCoTaskMem(pathStringPtr);
        Marshal.FreeHGlobal(referPtr);
    }

    [Fact]
    public void H5RdereferenceTest3()
    {
        var path = string.Join("/", H5RFixture.m_utf8strings);
        var pathStringPtr = Marshal.StringToCoTaskMemUTF8(path);

        hsize_t[] dims = { 10, 20 };
        hid_t space = H5S.create_simple(2, dims, null);
        Assert.True(space >= 0);
        hid_t dset = H5D.create(m_v0_test_file, pathStringPtr, H5T.STD_I32LE, space, H5RFixture.m_lcpl_utf8);

        H5O.info_t info = new H5O.info_t();
        Assert.True(H5O.get_info(dset, ref info) >= 0);
        haddr_t address = info.addr;
        Assert.True(H5D.close(dset) >= 0);

        Assert.True(dset >= 0);
        hsize_t[] start = { 5, 10 };
        hsize_t[] count = { 1, 1 };
        hsize_t[] block = { 2, 4 };
        Assert.True(H5S.select_hyperslab(space, H5S.seloper_t.SET, start, null, count, block) >= 0);

        var refer = new byte[H5R.OBJ_REF_BUF_SIZE];
        var referPtr = Marshal.AllocHGlobal(refer.Length);
        Marshal.Copy(refer, 0, referPtr, refer.Length);

        Assert.True(H5R.create(referPtr, m_v0_test_file, pathStringPtr, H5R.type_t.DATASET_REGION, space) >= 0);

        dset = H5R.dereference(m_v0_test_file, H5P.DEFAULT, H5R.type_t.DATASET_REGION, referPtr);
        Assert.True(dset >= 0);

        Assert.True(H5O.get_info(dset, ref info) >= 0);
        Assert.True(address == info.addr);
        Assert.True(H5D.close(dset) >= 0);

        Assert.True(H5S.close(space) >= 0);

        Marshal.FreeCoTaskMem(pathStringPtr);
        Marshal.FreeHGlobal(referPtr);
    }
}
