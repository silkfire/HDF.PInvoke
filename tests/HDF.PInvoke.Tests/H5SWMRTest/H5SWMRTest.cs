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
using System;
using System.IO;

[Collection("Global collection")]
public sealed partial class H5SWMRTest :  IClassFixture<H5SWMRFixture>, IDisposable
{
    private readonly hid_t m_v3_test_file_no_swmr = -1;
    private readonly string m_v3_test_file_name_no_swmr;
    private readonly hid_t m_v3_test_file_swmr = -1;
    private readonly string m_v3_test_file_name_swmr;

    public H5SWMRTest()
    {
        Utilities.DisableErrorPrinting();

        m_v3_test_file_no_swmr = Utilities.H5TempFileNoSWMR(ref m_v3_test_file_name_no_swmr);
        Assert.True(m_v3_test_file_no_swmr >= 0);

        m_v3_test_file_swmr = Utilities.H5TempFileSWMR(ref m_v3_test_file_name_swmr);
        Assert.True(m_v3_test_file_swmr >= 0);
    }

    public void Dispose()
    {
        // close the test-local files
        Assert.True(H5F.close(m_v3_test_file_no_swmr) >= 0);
        File.Delete(m_v3_test_file_name_no_swmr);
        Assert.True(H5F.close(m_v3_test_file_swmr) >= 0);
        File.Delete(m_v3_test_file_name_swmr);
    }
}
