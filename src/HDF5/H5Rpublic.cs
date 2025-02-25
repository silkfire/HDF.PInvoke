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


namespace HDF.PInvoke.HDF5;

using haddr_t = System.UInt64;
using herr_t = System.Int32;
using size_t = nint;
using ssize_t = nint;
using hid_t = System.Int64;

using System.Runtime.InteropServices;
using System.Security;

public sealed partial class H5R
{
    static H5R() { H5.open(); }

    /// <summary>
    /// Reference types allowed.
    /// </summary>
    public enum type_t
    {
        /// <summary>
        /// invalid Reference Type
        /// </summary>
        BADTYPE = -1,
        /// <summary>
        /// Object reference
        /// </summary>
        OBJECT,
        /// <summary>
        /// Dataset Region Reference
        /// </summary>
        DATASET_REGION,
        /// <summary>
        /// highest type (Invalid as true type)
        /// </summary>
        MAXTYPE
    }

    public const int OBJ_REF_BUF_SIZE = sizeof(haddr_t);

    public const int DSET_REG_REF_BUF_SIZE = sizeof(haddr_t) + 4;

    /// <summary>
    /// Creates a reference.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-Create" /> for further reference.</para>
    /// </summary>
    /// <param name="refer">Reference created by the function call.</param>
    /// <param name="loc_id">Location identifier used to locate the object
    /// being pointed to.</param>
    /// <param name="name">Name of object at location
    /// <paramref name="loc_id"/>.</param>
    /// <param name="ref_type">Type of reference.</param>
    /// <param name="space_id">Dataspace identifier with selection. Used
    /// only for dataset region references; pass as -1 if reference is an
    /// object reference.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Rcreate"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t create(ssize_t refer, hid_t loc_id, nint name, type_t ref_type, hid_t space_id);

    /// <summary>
    /// Opens the HDF5 object referenced.
    /// </summary>
    /// <param name="obj_id">Valid identifier for the file containing the
    /// referenced object or any object in that file.</param>
    /// <param name="oapl_id"></param>
    /// <param name="ref_type">The reference type of
    /// <paramref name="refer"/>.</param>
    /// <param name="refer">Reference to open.</param>
    /// <returns>Returns identifier of referenced object if successful;
    /// otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Rdereference2"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t dereference(hid_t obj_id, hid_t oapl_id, type_t ref_type, size_t refer);

    /// <summary>
    /// Retrieves a name for a referenced object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-GetName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Identifier for the file containing the
    /// reference or for any object in that file.</param>
    /// <param name="ref_type">Type of reference.</param>
    /// <param name="refer">An object or dataset region reference.</param>
    /// <param name="name">A buffer to place the name of the referenced
    /// object or dataset region. If <c>NULL</c>, then this call will
    /// return the size in bytes of the name.</param>
    /// <param name="size">The size of the <paramref name="name"/>
    /// buffer.</param>
    /// <returns>Returns the length of the name if successful, returning 0
    /// (zero) if no name is associated with the identifier. Otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Rget_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial ssize_t get_name(hid_t loc_id, type_t ref_type, ssize_t refer, nint name, size_t size);

    /// <summary>
    /// Retrieves the type of object that an object reference points to.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-GetObjType2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">The dataset containing the reference object or
    /// the group containing that dataset.</param>
    /// <param name="ref_type">Type of reference to query.</param>
    /// <param name="refer">Reference to query.</param>
    /// <param name="obj_type">Type of referenced object.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Rget_obj_type2"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_obj_type(hid_t loc_id, type_t ref_type, ssize_t refer, ref H5O.type_t obj_type);

    /// <summary>
    /// Sets up a dataspace and selection as specified by a region reference.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5R.html#Reference-GetRegion" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File identifier or identifier for any object
    /// in the file containing the referenced region</param>
    /// <param name="ref_type">Reference type of <paramref name="refer"/>,
    /// which must be <c>DATASET_REGION</c>.</param>
    /// <param name="refer">Region reference to open</param>
    /// <returns>Returns a valid dataspace identifier if successful;
    /// otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Rget_region"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_region(hid_t loc_id, type_t ref_type, ssize_t refer);
}
