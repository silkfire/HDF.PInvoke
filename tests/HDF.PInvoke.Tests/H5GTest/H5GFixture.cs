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

public sealed class H5GFixture : IDisposable
{
    internal static hid_t m_v0_class_file = -1;
    internal static string m_v0_class_file_name;
    internal static hid_t m_v2_class_file = -1;
    internal static string m_v2_class_file_name;

    public H5GFixture()
    {
        // create test files which persists across file tests
        m_v0_class_file = Utilities.H5TempFile(ref m_v0_class_file_name, H5F.libver_t.EARLIEST);
        Assert.True(m_v0_class_file >= 0);
        m_v2_class_file = Utilities.H5TempFile(ref m_v2_class_file_name);
        Assert.True(m_v2_class_file >= 0);
    }

    public void Dispose()
    {
        // close the global test files
        Assert.True(H5F.close(m_v0_class_file) >= 0);
        Assert.True(H5F.close(m_v2_class_file) >= 0);
        File.Delete(m_v0_class_file_name);
        File.Delete(m_v2_class_file_name);
    }
}
