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

using HDF5;
using Xunit;

public partial class H5FTest
{
    [Fact]
    public void H5Fset_mdc_configTest1()
    {
        H5AC.cache_config_t conf = new H5AC.cache_config_t(H5AC.CURR_CACHE_CONFIG_VERSION);
        // TODO: define sensible defaults for MDC config.
        //Assert.True(H5F.set_mdc_config(m_v0_class_file, ref conf) >= 0);
        //Assert.True(H5F.set_mdc_config(m_v2_class_file, ref conf) >= 0);
    }

    [Fact]
    public void H5Fset_mdc_configTest2()
    {
        H5AC.cache_config_t conf = new H5AC.cache_config_t(H5AC.CURR_CACHE_CONFIG_VERSION);
        Assert.False(H5F.set_mdc_config(Utilities.RandomInvalidHandle(), ref conf) >= 0);
    }
}
