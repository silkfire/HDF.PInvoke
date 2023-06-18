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
using System.Text;

public partial class H5LTest
{
    [Fact]
    public void H5Lcreate_softTest1()
    {
        Assert.True(H5L.create_soft("/A/B/C/D", m_v0_test_file, "this/is/a/soft/link", H5LFixture.m_lcpl) >= 0);

        Assert.True(H5L.create_soft("/A/B/C/D", m_v2_test_file, "this/is/a/soft/link", H5LFixture.m_lcpl) >= 0);
    }

    [Fact]
    public void H5Lcreate_softTest2()
    {
        string sym_path = string.Join("/", H5LFixture.m_utf8strings);

        Assert.True(H5L.create_soft(Encoding.ASCII.GetBytes("/A/B/C/D"), m_v0_test_file, Encoding.UTF8.GetBytes(sym_path), H5LFixture.m_lcpl_utf8) >= 0);

        Assert.True(H5L.create_soft(Encoding.ASCII.GetBytes("/A/B/C/D"), m_v2_test_file, Encoding.UTF8.GetBytes(sym_path), H5LFixture.m_lcpl_utf8) >= 0);
    }
}
