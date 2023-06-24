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

using herr_t = System.Int32;
using hid_t = System.Int64;

using HDF5;

using Xunit;

using System.Runtime.InteropServices;

public partial class H5ETest
{
    [Fact]
    public void H5EwalkTest1()
    {
        H5E.auto_t auto_cb = ErrorDelegateMethod;
        Assert.True(H5E.set_auto(H5E.DEFAULT, auto_cb, nint.Zero) >= 0);

        H5E.walk_t walk_cb = WalkDelegateMethod;
        Assert.True(H5E.walk(H5E.DEFAULT, H5E.direction_t.H5E_WALK_DOWNWARD, walk_cb, nint.Zero) >= 0);
    }

    [Fact]
    public void H5EwalkTest2()
    {
        var helloCStringPtr = Marshal.StringToHGlobalAnsi("hello.c");
        var sqrtStringPtr = Marshal.StringToHGlobalAnsi("sqrt");
        var sqrStringPtr = Marshal.StringToHGlobalAnsi("sqr");
        var helloWorldStringPtr = Marshal.StringToHGlobalAnsi("Hello, World!");

        H5E.auto_t auto_cb = ErrorDelegateMethod;
        Assert.True(H5E.set_auto(H5E.DEFAULT, auto_cb, nint.Zero) >= 0);

        H5E.walk_t walk_cb = WalkDelegateMethod;

        Assert.True(H5E.push(H5E.DEFAULT, helloCStringPtr, sqrtStringPtr, 77, H5E.ERR_CLS, H5E.NONE_MAJOR, H5E.NONE_MINOR, helloWorldStringPtr) >= 0);
        Assert.True(H5E.push(H5E.DEFAULT, helloCStringPtr, sqrStringPtr, 78, H5E.ERR_CLS, H5E.NONE_MAJOR, H5E.NONE_MINOR, helloWorldStringPtr) >= 0);
        Assert.True(H5E.walk(H5E.DEFAULT, H5E.direction_t.H5E_WALK_DOWNWARD, walk_cb, nint.Zero) >= 0);

        Marshal.FreeHGlobal(helloCStringPtr);
        Marshal.FreeHGlobal(sqrtStringPtr);
        Marshal.FreeHGlobal(sqrStringPtr);
        Marshal.FreeHGlobal(helloWorldStringPtr);
    }

    private static herr_t ErrorDelegateMethod(hid_t estack, nint client_data)
    {
        return 0;
    }

    private static herr_t WalkDelegateMethod(uint n, nint err_desc, nint client_data)
    {
        var errorDescStruct = Marshal.PtrToStructure<H5E.error_t>(err_desc);

        Assert.True(errorDescStruct.line > 0);

        return 0;
    }
}
