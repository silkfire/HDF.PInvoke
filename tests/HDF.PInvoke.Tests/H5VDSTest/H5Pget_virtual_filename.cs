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

using size_t = nint;
using ssize_t = nint;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System;
using System.Text;

public partial class H5VDSTest
{
    [Fact]
    public void H5Pget_virtual_filenameTestVDS1()
    {
        hid_t vds = H5D.open(H5VDSFixture.m_vds_class_file, "VDS");
        Assert.True(vds >= 0);

        hid_t dcpl = H5D.get_create_plist(vds);
        Assert.True(dcpl >= 0);

        IntPtr count = IntPtr.Zero;
        Assert.True(H5P.get_virtual_count(dcpl, ref count) >= 0);
        Assert.True(3 == count.ToInt32());

        string[] names = { H5VDSFixture.m_a_class_file_name, H5VDSFixture.m_b_class_file_name, H5VDSFixture.m_c_class_file_name };

        for (int i = 0; i < count.ToInt32(); ++i)
        {
            size_t index = new ssize_t(i);
            ssize_t len = H5P.get_virtual_filename(dcpl, index, null, IntPtr.Zero);
            Assert.True(len.ToInt32() > 0);
            StringBuilder name = new StringBuilder(len.ToInt32() + 1);
            len = H5P.get_virtual_filename(dcpl, index, name, len + 1);
            Assert.True(len.ToInt32() > 0);
            Assert.True(name.ToString() == names[i]);
        }

        Assert.True(H5P.close(dcpl) >= 0);
        Assert.True(H5D.close(vds) >= 0);
    }
}
