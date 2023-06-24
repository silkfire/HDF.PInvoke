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

using hsize_t = System.UInt64;
using hid_t = System.Int64;

using HDF5;

using Xunit;

using System;
using System.IO;
using System.Runtime.InteropServices;

public sealed class H5VDSFixture : IDisposable
{
    internal static hid_t m_vds_class_file = 1;
    internal static string m_vds_class_file_name;
    internal static hid_t m_a_class_file = 1;
    internal static string m_a_class_file_name;
    internal static hid_t m_b_class_file = 1;
    internal static string m_b_class_file_name;
    internal static hid_t m_c_class_file = 1;
    internal static string m_c_class_file_name;

    public H5VDSFixture()
    {
        createVDS();
    }

    private static void createVDS()
    {
        var aStringPtr = Marshal.StringToHGlobalAnsi("A");
        var bStringPtr = Marshal.StringToHGlobalAnsi("B");
        var cStringPtr = Marshal.StringToHGlobalAnsi("C");
        var vdsStringPtr = Marshal.StringToHGlobalAnsi("VDS");

        // create files
        m_a_class_file = Utilities.H5TempFile(out m_a_class_file_name, H5F.libver_t.LATEST, true);
        Assert.True(m_a_class_file >= 0);
        m_b_class_file = Utilities.H5TempFile(out m_b_class_file_name, H5F.libver_t.LATEST, true);
        Assert.True(m_b_class_file >= 0);
        m_c_class_file = Utilities.H5TempFile(out m_c_class_file_name, H5F.libver_t.LATEST, true);
        Assert.True(m_c_class_file >= 0);
        m_vds_class_file = Utilities.H5TempFile(out m_vds_class_file_name);
        Assert.True(m_vds_class_file >= 0);

        var aClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(m_a_class_file_name);
        var bClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(m_b_class_file_name);
        var cClassFileNameStringPtr = Marshal.StringToHGlobalAnsi(m_c_class_file_name);

        //
        // create target datasets
        //
        hid_t dcpl = H5P.create(H5P.DATASET_CREATE);
        Assert.True(dcpl >= 0);
        int fill_value = 1;
        GCHandle hnd = GCHandle.Alloc(fill_value, GCHandleType.Pinned);
        Assert.True(H5P.set_fill_value(dcpl, H5T.NATIVE_INT, hnd.AddrOfPinnedObject()) >= 0);

        hsize_t[] dims = { 6 };
        hid_t src_dsp = H5S.create_simple(1, dims, null);

        // A
        fill_value = 1;
        hid_t a = H5D.create(m_a_class_file, aStringPtr, H5T.STD_I32LE, src_dsp);
        Assert.True(a >= 0);
        Assert.True(H5D.close(a) >= 0);
        // B
        fill_value = 2;
        hid_t b = H5D.create(m_b_class_file, bStringPtr, H5T.STD_I32LE, src_dsp);
        Assert.True(b >= 0);
        Assert.True(H5D.close(b) >= 0);
        // B
        fill_value = 3;
        hid_t c = H5D.create(m_c_class_file, cStringPtr, H5T.STD_I32LE, src_dsp);
        Assert.True(c >= 0);
        Assert.True(H5D.close(c) >= 0);

        //
        // create the VDS
        //
        fill_value = -1;
        hsize_t[] vds_dims = { 4, 6 };
        hid_t vds_dsp = H5S.create_simple(2, vds_dims, null);

        hsize_t[] start = { 0, 0 };
        hsize_t[] count = { 1, 1 };
        hsize_t[] block = { 1, 6 };

        start[0] = 0;
        Assert.True(H5S.select_hyperslab(vds_dsp, H5S.seloper_t.SET, start, null, count, block) >= 0);
        Assert.True(H5P.set_virtual(dcpl, vds_dsp, aClassFileNameStringPtr, aStringPtr, src_dsp) >= 0);

        start[0] = 1;
        Assert.True(H5S.select_hyperslab(vds_dsp, H5S.seloper_t.SET, start, null, count, block) >= 0);
        Assert.True(H5P.set_virtual(dcpl, vds_dsp, bClassFileNameStringPtr, bClassFileNameStringPtr, src_dsp) >= 0);

        start[0] = 2;
        Assert.True(H5S.select_hyperslab(vds_dsp, H5S.seloper_t.SET, start, null, count, block) >= 0);
        Assert.True(H5P.set_virtual(dcpl, vds_dsp, cClassFileNameStringPtr, cClassFileNameStringPtr, src_dsp) >= 0);

        hid_t vds = H5D.create(m_vds_class_file, vdsStringPtr, H5T.STD_I32LE, vds_dsp, H5P.DEFAULT, dcpl, H5P.DEFAULT);
        Assert.True(vds >= 0);
        Assert.True(H5D.close(vds) >= 0);

        Assert.True(H5S.close(vds_dsp) >= 0);
        Assert.True(H5S.close(src_dsp) >= 0);
        Assert.True(H5P.close(dcpl) >= 0);

        hnd.Free();

        // close the satellite files
        Assert.True(H5F.close(m_a_class_file) >= 0);
        Assert.True(H5F.close(m_b_class_file) >= 0);
        Assert.True(H5F.close(m_c_class_file) >= 0);

        Marshal.FreeHGlobal(aStringPtr);
        Marshal.FreeHGlobal(bStringPtr);
        Marshal.FreeHGlobal(cStringPtr);
        Marshal.FreeHGlobal(vdsStringPtr);
        Marshal.FreeHGlobal(aClassFileNameStringPtr);
        Marshal.FreeHGlobal(bClassFileNameStringPtr);
        Marshal.FreeHGlobal(cClassFileNameStringPtr);
    }

    private static void cleanupVDS()
    {
        Assert.True(H5F.close(m_vds_class_file) >= 0);

        File.Delete(m_vds_class_file_name);
        File.Delete(m_a_class_file_name);
        File.Delete(m_b_class_file_name);
        File.Delete(m_c_class_file_name);
    }

    public void Dispose()
    {
    }
}
