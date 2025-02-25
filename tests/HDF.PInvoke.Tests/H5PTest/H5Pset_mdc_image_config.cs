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

using hid_t = System.Int64;

using HDF5;
using Xunit;
using System;
using System.Runtime.InteropServices;

public partial class H5PTest
{
    [Fact]
    public void H5Pset_mdc_image_configTest1()
    {
        hid_t fapl = H5P.create(H5P.FILE_ACCESS);
        Assert.True(fapl >= 0);

        Assert.True(H5P.set_libver_bounds(fapl, H5F.libver_t.LATEST) >= 0);

        H5AC.cache_image_config_t conf = new H5AC.cache_image_config_t();
        conf.version = H5AC.CURR_CACHE_IMAGE_CONFIG_VERSION;
        conf.entry_ageout = H5AC.CACHE_IMAGE__ENTRY_AGEOUT__NONE;

        IntPtr config_ptr = Marshal.AllocHGlobal(Marshal.SizeOf(conf));
        Marshal.StructureToPtr(conf, config_ptr, false);

        H5AC.cache_image_config_t conf1 = new H5AC.cache_image_config_t();

        conf1 = (H5AC.cache_image_config_t)Marshal.PtrToStructure(config_ptr, typeof(H5AC.cache_image_config_t));

        //Assert.True(H5P.set_mdc_image_config(fapl, config_ptr) >= 0);

        Assert.True(H5P.close(fapl) >= 0);
        Marshal.FreeHGlobal(config_ptr);
    }
}
