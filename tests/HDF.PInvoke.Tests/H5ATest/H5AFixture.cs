﻿/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
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

using herr_t = System.Int32;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

public sealed class H5AFixture : IDisposable
{
    internal static string[] m_utf8strings = new string[] { "Ελληνικά", "日本語", "العربية", "экземпляр", "סקרן" };
    internal static hid_t m_v0_class_file = -1;
    internal static string m_v0_class_file_name;
    internal static hid_t m_v2_class_file = -1;
    internal static string m_v2_class_file_name;
    internal static hid_t m_space_null = -1;
    internal static hid_t m_space_scalar = -1;
    internal static hid_t m_acpl = -1;

    public H5AFixture()
    {
        // create test files which persists across file tests
        m_v0_class_file = Utilities.H5TempFile(ref m_v0_class_file_name, H5F.libver_t.EARLIEST);
        Assert.True(m_v0_class_file >= 0);
        m_v2_class_file = Utilities.H5TempFile(ref m_v2_class_file_name);
        Assert.True(m_v2_class_file >= 0);

        m_space_null = H5S.create(H5S.class_t.NULL);
        Assert.True(m_space_null >= 0);
        m_space_scalar = H5S.create(H5S.class_t.SCALAR);
        Assert.True(m_space_scalar >= 0);

        m_acpl = H5P.create(H5P.ATTRIBUTE_CREATE);
        Assert.True(m_acpl >= 0);
        Assert.True(H5P.set_char_encoding(m_acpl, H5T.cset_t.UTF8) >= 0);
    }

    // Callback for H5A.iterate and H5A.iterate_by_name
    // We expect an array list as op_data; add the attribute names to the
    // array list as we go
    internal static herr_t DelegateMethod(hid_t location_id, IntPtr attr_name, ref H5A.info_t ainfo, IntPtr op_data)
    {
        GCHandle hnd = (GCHandle)op_data;
        ArrayList al = hnd.Target as ArrayList;
        int len = 0;
        while (Marshal.ReadByte(attr_name, len) != 0)
        {
            ++len;
        }

        byte[] buf = new byte[len];
        Marshal.Copy(attr_name, buf, 0, len);
        al.Add(Encoding.UTF8.GetString(buf));

        return 0;
    }

    public void Dispose()
    {
        Assert.True(H5P.close(m_acpl) >= 0);
        // close the global test files
        Assert.True(H5F.close(m_v0_class_file) >= 0);
        Assert.True(H5F.close(m_v2_class_file) >= 0);
        Assert.True(H5S.close(m_space_null) >= 0);
        Assert.True(H5S.close(m_space_scalar) >= 0);
        File.Delete(m_v0_class_file_name);
        File.Delete(m_v2_class_file_name);
    }
}
