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
using System.Text;

public partial class H5LTest
{
    [Fact]
    public void H5LcopyTest1()
    {
        hid_t gid = H5G.create(m_v0_test_file, "A/B/C", H5LFixture.m_lcpl);
        Assert.True(gid >= 0);

        Assert.True(H5L.copy(m_v0_test_file, "A", gid, "A_copy") >= 0);
        Assert.True(H5L.exists(m_v0_test_file, "A/B/C/A_copy/B") > 0);

        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, "A/B/C", H5LFixture.m_lcpl);
        Assert.True(gid >= 0);

        Assert.True(H5L.copy(m_v2_test_file, "A", gid, "A_copy") >= 0);
        Assert.True(H5L.exists(m_v2_test_file, "A/B/C/A_copy/B") > 0);

        Assert.True(H5G.close(gid) >= 0);
    }

    [Fact]
    public void H5LcopyTest2()
    {
        Assert.True(H5L.create_soft("/my/fantastic/path", m_v0_test_file, "A") >= 0);

        // copy symlink to the other test file

        Assert.True(H5L.copy(m_v0_test_file, "A", m_v2_test_file, "C/B/A_copy", H5LFixture.m_lcpl) >= 0);
    }

    [Fact]
    public void H5LcopyTest3()
    {
        hid_t gid = H5G.create(m_v0_test_file, "A/B/C", H5LFixture.m_lcpl);
        Assert.True(gid >= 0);

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            Assert.True(H5L.copy(m_v0_test_file, Encoding.ASCII.GetBytes("A"), gid, Encoding.UTF8.GetBytes(H5LFixture.m_utf8strings[i]), H5LFixture.m_lcpl_utf8) >= 0);

            string path = $"A/B/C/{H5LFixture.m_utf8strings[i]}/B";
            Assert.True(H5L.exists(m_v0_test_file, Encoding.UTF8.GetBytes(path)) > 0);
        }

        Assert.True(H5G.close(gid) >= 0);

        gid = H5G.create(m_v2_test_file, "A/B/C", H5LFixture.m_lcpl);
        Assert.True(gid >= 0);

        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            Assert.True(H5L.copy(m_v2_test_file, Encoding.ASCII.GetBytes("A"), gid, Encoding.UTF8.GetBytes(H5LFixture.m_utf8strings[i]), H5LFixture.m_lcpl_utf8) >= 0);

            string path = $"A/B/C/{H5LFixture.m_utf8strings[i]}/B";
            Assert.True(H5L.exists(m_v2_test_file, Encoding.UTF8.GetBytes(path)) > 0);
        }

        Assert.True(H5G.close(gid) >= 0);
    }
}
