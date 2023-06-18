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

public partial class H5SWMRTest
{
    [Fact]
    public void H5Fget_metadata_read_retry_infoTestSWMR1()
    {
        H5F.retry_info_t info = new H5F.retry_info_t();

        info.retries0 = new IntPtr(10);
        info.retries6 = new IntPtr(60);
        info.retries14 = new IntPtr(140);
        info.retries20 = new IntPtr(200);

        Assert.True(H5F.get_metadata_read_retry_info(m_v3_test_file_swmr, ref info) >= 0);

        Assert.True(info.nbins == 2);

        Assert.True(info.retries0 == IntPtr.Zero);
        Assert.True(info.retries6 == IntPtr.Zero);
        Assert.True(info.retries14 == IntPtr.Zero);
        Assert.True(info.retries20 == IntPtr.Zero);
    }
}
