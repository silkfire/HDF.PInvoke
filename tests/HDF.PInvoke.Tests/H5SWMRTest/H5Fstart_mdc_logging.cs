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

using hbool_t = System.UInt32;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System.IO;

public partial class H5SWMRTest
{
    [Fact]
    public void H5Fstart_mdc_loggingTestSWMR1()
    {
        hid_t fapl = H5P.create(H5P.FILE_ACCESS);
        Assert.True(fapl >= 0);
        Assert.True(H5P.set_libver_bounds(fapl, H5F.libver_t.LATEST) >= 0);

        hbool_t is_enabled = 1;
        string location = "mdc.log";
        hbool_t start_on_access = 0;

        Assert.True(H5P.set_mdc_log_options(fapl, is_enabled, location, start_on_access) >= 0);

        string fileName = Path.GetTempFileName();
        hid_t file = H5F.create(fileName, H5F.ACC_TRUNC, H5P.DEFAULT, fapl);
        Assert.True(file >= 0);

        Assert.True(H5F.start_mdc_logging(file) >= 0);

        hid_t group = H5G.create(file, "/A/B/C", H5SWMRFixture.m_lcpl);
        Assert.True(group >= 0);
        Assert.True(H5G.close(group) >= 0);

        Assert.True(H5F.stop_mdc_logging(file) >= 0);

        Assert.True(H5F.close(file) >= 0);

        Assert.True(H5P.close(fapl) >= 0);

        File.Delete("mdc.log");
    }
}
