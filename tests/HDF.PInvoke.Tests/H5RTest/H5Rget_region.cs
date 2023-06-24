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
using System;
using System.Runtime.InteropServices;
using System.Text;

public partial class H5RTest
{
    [Fact]
    public void H5Rget_regionTest1()
    {
        byte[] path =
            Encoding.UTF8.GetBytes(string.Join("/", H5RFixture.m_utf8strings));
        // make room for the trailing \0
        byte[] name = new byte[path.Length + 1];
        Array.Copy(path, name, path.Length);

        hsize_t[] dims = new hsize_t[] { 10, 20 };
        hid_t space = H5S.create_simple(2, dims, null);
        Assert.True(space >= 0);
        hid_t dset = H5D.create(m_v0_test_file, name, H5T.STD_I32LE, space, H5RFixture.m_lcpl_utf8);
        Assert.True(dset >= 0);
        hsize_t[] start = { 5, 10 };
        hsize_t[] count = { 1, 1 };
        hsize_t[] block = { 2, 4 };
        Assert.True(H5S.select_hyperslab(space, H5S.seloper_t.SET, start, null, count, block) >= 0);

        byte[] refer = new byte[H5R.DSET_REG_REF_BUF_SIZE];
        GCHandle hnd = GCHandle.Alloc(refer, GCHandleType.Pinned);

        Assert.True(H5R.create(hnd.AddrOfPinnedObject(), m_v0_test_file, name, H5R.type_t.DATASET_REGION, space) >= 0);

        ssize_t size = H5R.get_name(m_v0_test_file, H5R.type_t.DATASET_REGION, hnd.AddrOfPinnedObject(), (byte[])null, ssize_t.Zero);
        Assert.True(size.ToInt32() == name.Length);

        byte[] buf = new byte[size.ToInt32() + 1];
        size = H5R.get_name(m_v0_test_file, H5R.type_t.DATASET_REGION, hnd.AddrOfPinnedObject(), buf, new ssize_t(buf.Length));
        Assert.True(size.ToInt32() == name.Length);

        // we need to account for the leading "/", which was not included
        // in path
        for (int i = 0; i < name.Length; ++i)
        {
            Assert.True(name[i] == buf[i + 1]);
        }

        hid_t sel = H5R.get_region(dset, H5R.type_t.DATASET_REGION, hnd.AddrOfPinnedObject());
        Assert.True(sel >= 0);

        hnd.Free();

        Assert.True(H5S.extent_equal(space, sel) > 0);
        Assert.True(H5S.get_select_hyper_nblocks(space) == H5S.get_select_hyper_nblocks(sel));

        Assert.True(H5S.close(sel) >= 0);
        Assert.True(H5D.close(dset) >= 0);
        Assert.True(H5S.close(space) >= 0);
    }

    [Fact]
    public void H5Rget_regionTest2()
    {
        byte[] path = Encoding.UTF8.GetBytes(string.Join("/", H5RFixture.m_utf8strings));
        // make room for the trailing \0
        byte[] name = new byte[path.Length + 1];
        Array.Copy(path, name, path.Length);

        hsize_t[] dims = new hsize_t[] { 10, 20 };
        hid_t space = H5S.create_simple(2, dims, null);
        Assert.True(space >= 0);
        hid_t dset = H5D.create(m_v2_test_file, name, H5T.STD_I32LE, space, H5RFixture.m_lcpl_utf8);
        Assert.True(dset >= 0);
        hsize_t[] start = { 5, 10 };
        hsize_t[] count = { 1, 1 };
        hsize_t[] block = { 2, 4 };
        Assert.True(H5S.select_hyperslab(space, H5S.seloper_t.SET, start, null, count, block) >= 0);

        byte[] refer = new byte[H5R.DSET_REG_REF_BUF_SIZE];
        GCHandle hnd = GCHandle.Alloc(refer, GCHandleType.Pinned);

        Assert.True(H5R.create(hnd.AddrOfPinnedObject(), m_v2_test_file, name, H5R.type_t.DATASET_REGION, space) >= 0);

        ssize_t size = H5R.get_name(m_v2_test_file, H5R.type_t.DATASET_REGION, hnd.AddrOfPinnedObject(), (byte[])null, ssize_t.Zero);
        Assert.True(size.ToInt32() == name.Length);

        byte[] buf = new byte[size.ToInt32() + 1];
        size = H5R.get_name(m_v2_test_file, H5R.type_t.DATASET_REGION, hnd.AddrOfPinnedObject(), buf, new ssize_t(buf.Length));
        Assert.True(size.ToInt32() == name.Length);

        // we need to account for the leading "/", which was not included
        // in path
        for (int i = 0; i < name.Length; ++i)
        {
            Assert.True(name[i] == buf[i + 1]);
        }

        hid_t sel = H5R.get_region(dset, H5R.type_t.DATASET_REGION, hnd.AddrOfPinnedObject());
        Assert.True(sel >= 0);

        hnd.Free();

        Assert.True(H5S.extent_equal(space, sel) > 0);
        Assert.True(H5S.get_select_hyper_nblocks(space) == H5S.get_select_hyper_nblocks(sel));

        Assert.True(H5S.close(sel) >= 0);
        Assert.True(H5D.close(dset) >= 0);
        Assert.True(H5S.close(space) >= 0);
    }
}
