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

public sealed class H5IFixture : IDisposable
{
    internal static string[] m_utf8strings = new[] { "Ελληνικά", "日本語", "العربية", "экземпляр", "סקרן" };
    internal static hid_t m_v0_class_file = -1;
    internal static string m_v0_class_file_name;
    internal static hid_t m_v2_class_file = -1;
    internal static string m_v2_class_file_name;
    internal static hid_t m_lcpl;
    internal static hid_t m_lcpl_utf8;

    public H5IFixture()
    {
        // create test files which persists across file tests
        m_v0_class_file = Utilities.H5TempFile(ref m_v0_class_file_name, H5F.libver_t.EARLIEST);
        Assert.True(m_v0_class_file >= 0);
        m_v2_class_file = Utilities.H5TempFile(ref m_v2_class_file_name);
        Assert.True(m_v2_class_file >= 0);

        m_lcpl = H5P.create(H5P.LINK_CREATE);
        Assert.True(H5P.set_create_intermediate_group(m_lcpl, 1) >= 0);

        m_lcpl_utf8 = H5P.copy(m_lcpl);
        Assert.True(H5P.set_char_encoding(m_lcpl_utf8, H5T.cset_t.UTF8) >= 0);
    }

    // Callback for H5I.search
    public int DelegateMethod(IntPtr obj, hid_t id, IntPtr key)
    {
        return 0;
    }

    public void Dispose()
    {
        Assert.True(H5P.close(m_lcpl) >= 0);
        Assert.True(H5P.close(m_lcpl_utf8) >= 0);

        // close the global test files
        Assert.True(H5F.close(m_v0_class_file) >= 0);
        Assert.True(H5F.close(m_v2_class_file) >= 0);
        File.Delete(m_v0_class_file_name);
        File.Delete(m_v2_class_file_name);
    }
}
