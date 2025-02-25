﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
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

using hssize_t = System.Int64;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System;
using System.Runtime.InteropServices;
using System.Text;

public partial class H5DTest
{
    [Fact]
    public void H5DreadTest1()
    {
        byte[] rdata = new byte[512];

        hid_t mem_type = H5T.copy(H5T.C_S1);
        Assert.True(H5T.set_size(mem_type, new IntPtr(2)) >= 0);

        GCHandle hnd = GCHandle.Alloc(rdata, GCHandleType.Pinned);
        Assert.True(H5D.read(H5DFixture.m_v0_ascii_dset, mem_type, H5S.ALL, H5S.ALL, H5P.DEFAULT, hnd.AddrOfPinnedObject()) >= 0);
        for (int i = 0; i < 256; ++i)
        {
            // H5T.FORTRAN_S1 is space (= ASCII dec. 32) padded
            if (i != 32)
            {
                Assert.True(rdata[2 * i] == (byte)i);
            }
            else
            {
                Assert.True(rdata[64] == (byte)0);
            }

            Assert.True(rdata[2 * i + 1] == (byte)0);
        }

        hnd.Free();

        Assert.True(H5T.close(mem_type) >= 0);
    }

    [Fact]
    public void H5DreadTest2()
    {
        hid_t mem_type = H5T.create(H5T.class_t.STRING, H5T.VARIABLE);
        Assert.True(H5T.set_cset(mem_type, H5T.cset_t.UTF8) >= 0);
        Assert.True(H5T.set_strpad(mem_type, H5T.str_t.NULLTERM) >= 0);

        hid_t fspace = H5D.get_space(H5DFixture.m_v0_utf8_dset);
        Assert.True(fspace >= 0);

        hssize_t count = H5S.get_simple_extent_npoints(fspace);
        Assert.True(count > 0);
        Assert.True(H5S.close(fspace) >= 0);

        IntPtr[] rdata = new IntPtr[count];
        GCHandle hnd = GCHandle.Alloc(rdata, GCHandleType.Pinned);
        Assert.True(H5D.read(H5DFixture.m_v0_utf8_dset, mem_type, H5S.ALL, H5S.ALL, H5P.DEFAULT, hnd.AddrOfPinnedObject()) >= 0);

        for (int i = 0; i < rdata.Length; ++i)
        {
            int len = 0;
            while (Marshal.ReadByte(rdata[i], len) != 0)
            {
                ++len;
            }

            byte[] buffer = new byte[len];
            Marshal.Copy(rdata[i], buffer, 0, buffer.Length);
            string s = Encoding.UTF8.GetString(buffer);

            Assert.Equal(s, (string)H5DFixture.m_utf8strings[i]);

            Assert.True(H5.free_memory(rdata[i]) >= 0);
        }

        Assert.True(H5D.read(H5DFixture.m_v2_utf8_dset, mem_type, H5S.ALL, H5S.ALL, H5P.DEFAULT, hnd.AddrOfPinnedObject()) >= 0);

        for (int i = 0; i < rdata.Length; ++i)
        {
            int len = 0;
            while (Marshal.ReadByte(rdata[i], len) != 0)
            {
                ++len;
            }

            byte[] buffer = new byte[len];
            Marshal.Copy(rdata[i], buffer, 0, buffer.Length);
            string s = Encoding.UTF8.GetString(buffer);

            Assert.Equal(s, (string)H5DFixture.m_utf8strings[i]);

            Assert.True(H5.free_memory(rdata[i]) >= 0);
        }

        hnd.Free();
    }
}
