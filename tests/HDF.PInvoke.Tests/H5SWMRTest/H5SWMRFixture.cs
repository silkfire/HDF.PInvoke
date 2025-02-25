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

using hsize_t = System.UInt64;
using herr_t = System.Int32;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System;
using System.IO;
using System.Runtime.InteropServices;

public sealed class H5SWMRFixture : IDisposable
{
    internal static hid_t m_v3_class_file = -1;
    internal static string m_v3_class_file_name;
    internal static hid_t m_lcpl;
    internal static hid_t m_lcpl_utf8;

    public H5SWMRFixture()
    {
        // create test files which persists across file tests
        m_v3_class_file = Utilities.H5TempFileSWMR(ref m_v3_class_file_name);
        Assert.True(m_v3_class_file >= 0);

        m_lcpl = H5P.create(H5P.LINK_CREATE);
        Assert.True(H5P.set_create_intermediate_group(m_lcpl, 1) >= 0);

        m_lcpl_utf8 = H5P.copy(m_lcpl);
        Assert.True(H5P.set_char_encoding(m_lcpl_utf8, H5T.cset_t.UTF8) >= 0);

        // create a sample dataset

        hsize_t[] dims = { 6, 6 };
        hsize_t[] maxdims = { 6, H5S.UNLIMITED };
        hsize_t[] chunk_dims = { 2, 5 };
        int[] cbuf = new int[36];

        hid_t dsp = H5S.create_simple(2, dims, maxdims);
        Assert.True(dsp >= 0);

        hid_t dcpl = H5P.create(H5P.DATASET_CREATE);
        Assert.True(dcpl >= 0);
        Assert.True(H5P.set_chunk(dcpl, 2, chunk_dims) >= 0);

        hid_t dst = H5D.create(m_v3_class_file, "int6x6", H5T.NATIVE_INT, dsp, H5P.DEFAULT, dcpl);
        Assert.True(dst >= 0);

        GCHandle hnd = GCHandle.Alloc(cbuf, GCHandleType.Pinned);

        Assert.True(H5D.write(dst, H5T.NATIVE_INT, H5S.ALL, H5S.ALL, H5P.DEFAULT, hnd.AddrOfPinnedObject()) >= 0);

        hnd.Free();

        Assert.True(H5D.flush(dst) >= 0);

        Assert.True(H5D.close(dst) >= 0);
        Assert.True(H5P.close(dcpl) >= 0);
        Assert.True(H5S.close(dsp) >= 0);
    }

    // Two simple callbacks for H5Pset_append_flush and
    // H5Pset_object_flush_cb. We assume that a pointer to a counter in
    // unmanaged memory is passed as user data (op_data).

    internal static herr_t append_func(hid_t dset_id, hsize_t[] cur_dims, IntPtr op_data)
    {
        int append_ct = Marshal.ReadInt32(op_data);
        Marshal.WriteInt32(op_data, ++append_ct);

        return 0;
    }

    internal static herr_t flush_func(hid_t obj_id, IntPtr op_data)
    {
        int flush_ct = Marshal.ReadInt32(op_data);
        Marshal.WriteInt32(op_data, ++flush_ct);

        return 0;
    }

    public void Dispose()
    {
        Assert.True(H5P.close(m_lcpl) >= 0);
        Assert.True(H5P.close(m_lcpl_utf8) >= 0);

        // close the global test files
        Assert.True(H5F.close(m_v3_class_file) >= 0);
        File.Delete(m_v3_class_file_name);
    }
}
