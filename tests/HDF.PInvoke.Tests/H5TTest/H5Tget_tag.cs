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

using System.Runtime.InteropServices;

public partial class H5TTest
{
    [Fact]
    public void H5Tget_tagTest1()
    {
        var maryHadALittleLambString = "Mary had a little lamb...";
        var maryHadALittleLambStringPtr = Marshal.StringToHGlobalAnsi(maryHadALittleLambString);
        
        hid_t dtype = H5T.create(H5T.class_t.OPAQUE, new nint(1024));
        Assert.True(dtype >= 0);
        Assert.True(H5T.set_tag(dtype, maryHadALittleLambStringPtr) >= 0);

        nint tag = H5T.get_tag(dtype);
        Assert.True(tag.ToInt64() >= 0);
        Assert.Equal(maryHadALittleLambString, Marshal.PtrToStringAnsi(tag));
        Assert.True(H5.free_memory(tag) >= 0);
        Assert.True(H5T.close(dtype) >= 0);

        Marshal.FreeHGlobal(maryHadALittleLambStringPtr);
    }

    [Fact]
    public void H5Tget_tagTest2()
    {
        nint tag = H5T.get_tag(Utilities.RandomInvalidHandle());
        Assert.Equal(nint.Zero, tag);
    }
}
