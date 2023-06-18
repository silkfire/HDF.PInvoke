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
using System;
using System.IO;
using System.Threading;

public partial class H5TSTest
{
    private void FileCreateProcedure()
    {
        Random r = new Random();
        Thread.Sleep(r.Next(1000));

        string fname = Path.GetTempFileName();

        hid_t fapl = H5P.create(H5P.FILE_ACCESS);
        Assert.True(fapl >= 0);
        Assert.True(H5P.set_libver_bounds(fapl, H5F.libver_t.LATEST) >= 0);
        hid_t file = H5F.create(fname, H5F.ACC_TRUNC, H5P.DEFAULT, fapl);
        Assert.True(H5P.close(fapl) >= 0);

        Thread.Sleep(r.Next(2000));

        Assert.True(file >= 0);
        Assert.True(H5F.close(file) >= 0);

        File.Delete(fname);
    }

    [Fact]
    public void H5TSfile_creationTest1()
    {
        // run only if we have a thread-safe build of the library
        hbool_t flag = 0;
        Assert.True(H5.is_library_threadsafe(ref flag) >= 0);
        if (flag > 0)
        {
            // Create the new Thread and use the FileCreateProcedure method
            _fixture.Thread1 = new Thread(new ThreadStart(FileCreateProcedure)) { Name = "Thread1" };
            _fixture.Thread2 = new Thread(new ThreadStart(FileCreateProcedure)) { Name = "Thread2" };
            _fixture.Thread3 = new Thread(new ThreadStart(FileCreateProcedure)) { Name = "Thread3" };
            _fixture.Thread4 = new Thread(new ThreadStart(FileCreateProcedure)) { Name = "Thread4" };

            // Start running the thread
            _fixture.Thread4.Start();
            _fixture.Thread2.Start();
            _fixture.Thread1.Start();
            _fixture.Thread3.Start();

            // Join the independent thread to this thread to wait until
            // FileCreateProcedure ends
            _fixture.Thread1.Join();
            _fixture.Thread2.Join();
            _fixture.Thread3.Join();
            _fixture.Thread4.Join();
        }
    }
}
