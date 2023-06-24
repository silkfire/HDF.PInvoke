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


namespace HDF.PInvoke.HDF5;

using herr_t = System.Int32;
using size_t = nint;
using hid_t = System.Int64;

using System.Runtime.InteropServices;
using System.Security;

public sealed partial class H5DO
{
    static H5DO()
    {
        H5.open();
    }

    /// <summary>
    /// Appends data to a dataset along a specified dimension.
    /// <para>See <see href="https://portal.hdfgroup.org/display/HDF5/H5DO_APPEND" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Dataset identifier.</param>
    /// <param name="dxpl_id">Dataset transfer property list identifier.</param>
    /// <param name="axis">Dimension number (0-based).</param>
    /// <param name="num_elem">Number of elements to add along the dimension.</param>
    /// <param name="memtype">Memory type identifier.</param>
    /// <param name="buf">Data buffer.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.HighLevelApisDllFilename, EntryPoint = "H5DOappend"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t append(hid_t dset_id, hid_t dxpl_id, uint axis, size_t num_elem, hid_t memtype, size_t buf);
}
