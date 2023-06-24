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

using HDF5;

using Xunit;

using System.Runtime.InteropServices;

public partial class H5ETest
{
    [Fact]
    public void H5EpopTest1()
    {
        var helloCStringPtr = Marshal.StringToHGlobalAnsi("hello.c");
        var sqrtStringPtr = Marshal.StringToHGlobalAnsi("sqrt");
        var helloWorldStringPtr = Marshal.StringToHGlobalAnsi("Hello, World!");
        var sqrStringPtr = Marshal.StringToHGlobalAnsi("sqr");

        Assert.True(H5E.push(H5E.DEFAULT, helloCStringPtr, sqrtStringPtr, 77, H5E.ERR_CLS, H5E.NONE_MAJOR, H5E.NONE_MINOR, helloWorldStringPtr) >= 0);
        Assert.True(H5E.push(H5E.DEFAULT, helloCStringPtr, sqrStringPtr, 78, H5E.ERR_CLS, H5E.NONE_MAJOR, H5E.NONE_MINOR, helloWorldStringPtr) >= 0);
        Assert.True(H5E.pop(H5E.DEFAULT, new nint(2)) >= 0);

        Marshal.FreeHGlobal(helloCStringPtr);
        Marshal.FreeHGlobal(helloWorldStringPtr);
    }

    [Fact]
    public void H5EpopTest2()
    {
        Assert.False(H5E.pop(Utilities.RandomInvalidHandle(), nint.Zero) >= 0);
    }
}
