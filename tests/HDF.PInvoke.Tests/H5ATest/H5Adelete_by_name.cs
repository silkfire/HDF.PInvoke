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

public partial class H5ATest
{
    [Fact]
    public void H5Adelete_by_nameTest1()
    {
        hid_t att = H5A.create(H5AFixture.m_v0_class_file, "DNA", H5T.IEEE_F32BE, H5AFixture.m_space_null);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(H5AFixture.m_v2_class_file, "DNA", H5T.IEEE_F32BE, H5AFixture.m_space_null);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(H5AFixture.m_v0_class_file, "DSA", H5T.IEEE_F32BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        att = H5A.create(H5AFixture.m_v2_class_file, "DSA", H5T.IEEE_F32BE, H5AFixture.m_space_scalar);
        Assert.True(att >= 0);
        Assert.True(H5A.close(att) >= 0);

        Assert.True(H5A.delete_by_name(H5AFixture.m_v0_class_file, ".", "DNA") >= 0);
        Assert.True(H5A.delete_by_name(H5AFixture.m_v0_class_file, ".", "DSA") >= 0);
        Assert.True(H5A.delete_by_name(H5AFixture.m_v2_class_file, ".", "DNA") >= 0);
        Assert.True(H5A.delete_by_name(H5AFixture.m_v2_class_file, ".", "DSA") >= 0);
    }

    [Fact]
    public void H5Adelete_by_nameTest2()
    {
        Assert.False(H5A.delete_by_name(Utilities.RandomInvalidHandle(), "A", ".") >= 0);
        Assert.False(H5A.delete_by_name(m_v0_test_file, ".", ".") >= 0);
    }
}
