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

using HDF5;
using Xunit;
using System;
using System.Text;

public partial class H5LTest
{
    [Fact]
    public void H5LexistsTest1()
    {
        Assert.True(H5G.close(H5G.create(m_v0_test_file, "A/B/C/D", H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5L.exists(m_v0_test_file, "A") > 0);
        Assert.True(H5L.exists(m_v0_test_file, "A/B") > 0);
        Assert.True(H5L.exists(m_v0_test_file, "A/C/B") < 0);

        Assert.True(H5G.close(H5G.create(m_v2_test_file, "A/B/C/D", H5LFixture.m_lcpl)) >= 0);
        Assert.True(H5L.exists(m_v2_test_file, "A") > 0);
        Assert.True(H5L.exists(m_v2_test_file, "A/B") > 0);
        Assert.True(H5L.exists(m_v2_test_file, "A/C/B") < 0);
    }

    [Fact]
    public void H5LexistsTest2()
    {
        Assert.True(H5L.exists(m_v0_test_file, ".") == 0);
        Assert.True(H5L.exists(m_v2_test_file, ".") == 0);
    }

    [Fact]
    public void H5LexistsTest3()
    {
        for (int i = 0; i < H5LFixture.m_utf8strings.Length; ++i)
        {
            int size = Encoding.UTF8.GetBytes(H5LFixture.m_utf8strings[i]).Length;
            byte[] buf = new byte[size + 1];
            Array.Copy(Encoding.UTF8.GetBytes(H5LFixture.m_utf8strings[i]), buf, size);
            Assert.True(H5G.close(H5G.create(m_v0_test_file, buf, H5LFixture.m_lcpl_utf8)) >= 0);
            Assert.True(H5L.exists(m_v0_test_file, buf) > 0);

            Assert.True(H5G.close(H5G.create(m_v2_test_file, buf, H5LFixture.m_lcpl_utf8)) >= 0);
            Assert.True(H5L.exists(m_v2_test_file, buf) > 0);
        }
    }
}
