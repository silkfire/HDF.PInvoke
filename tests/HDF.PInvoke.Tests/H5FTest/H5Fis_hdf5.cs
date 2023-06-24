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

using System.IO;
using System.Runtime.InteropServices;

public partial class H5FTest
{
    [Fact]
    public void H5Fis_hdf5Test1()
    {
        string fname = Path.GetTempFileName();
        var fnameStringPtr = Marshal.StringToHGlobalAnsi(fname);

        hid_t file = H5F.create(fnameStringPtr, H5F.ACC_TRUNC);
        Assert.True(file >= 0);
        Assert.True(H5F.close(file) >= 0);
        Assert.True(H5F.is_hdf5(fnameStringPtr) > 0);
        File.Delete(fname);

        Marshal.FreeHGlobal(fnameStringPtr);
    }

    [Fact]
    public void H5Fis_hdf5Test2()
    {
        var emptyFilenameStringPtr = Marshal.StringToHGlobalAnsi("");

        Assert.True(H5F.is_hdf5(emptyFilenameStringPtr) < 0);
        string fname = Path.GetTempFileName();
        var fnameStringPtr = Marshal.StringToHGlobalAnsi(fname);

        Assert.True(H5F.is_hdf5(fnameStringPtr) == 0);
        File.Delete(fname);

        Marshal.FreeHGlobal(emptyFilenameStringPtr);
        Marshal.FreeHGlobal(fnameStringPtr);
    }
}
