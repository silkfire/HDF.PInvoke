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

using hbool_t = System.UInt32;
using hid_t = System.Int64;

using HDF5;
using Xunit;
using System.Threading;
using System.Runtime.InteropServices;

public partial class H5TSTest
{
    private void AttributeCreateProcedure()
    {
        var threadNamePtr = Marshal.StringToHGlobalAnsi(Thread.CurrentThread.Name);

        hid_t space = H5S.create(H5S.class_t.NULL);
        Assert.True(space >= 0);

        Assert.True(H5A.close(H5A.create(H5TSFixture.m_shared_file_id, threadNamePtr, H5T.STD_I32BE, space)) >= 0);
        Marshal.FreeHGlobal(threadNamePtr);

        Assert.True(H5S.close(space) >= 0);
    }

    [Fact]
    public void H5TSattribute_creationTest1()
    {
        // run only if we have a thread-safe build of the library
        hbool_t flag = 0;
        Assert.True(H5.is_library_threadsafe(ref flag) >= 0);
        if (flag > 0)
        {
            // Create the new Thread and use the FileCreateProcedure method
            _fixture.Thread1 = new Thread(new ThreadStart(AttributeCreateProcedure)) { Name = "Thread1" };
            _fixture.Thread2 = new Thread(new ThreadStart(AttributeCreateProcedure)) { Name = "Thread2" };
            _fixture.Thread3 = new Thread(new ThreadStart(AttributeCreateProcedure)) { Name = "Thread3" };
            _fixture.Thread4 = new Thread(new ThreadStart(AttributeCreateProcedure)) { Name = "Thread4" };

            // Start running the thread
            _fixture.Thread4.Start();
            _fixture.Thread2.Start();
            _fixture.Thread1.Start();
            _fixture.Thread3.Start();

            // Join the independent thread to this thread to wait until
            // AttributeCreateProcedure ends
            _fixture.Thread1.Join();
            _fixture.Thread2.Join();
            _fixture.Thread3.Join();
            _fixture.Thread4.Join();
        }
    }
}
