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

using hbool_t = System.UInt32;
using herr_t = System.Int32;
using hsize_t = System.UInt64;
using htri_t = System.Int32;
using size_t = nint;
using ssize_t = nint;
using hid_t = System.Int64;

using System.Runtime.InteropServices;
using System.Security;
using System.Text;

public sealed partial class H5I
{
    static H5I()
    {
        H5.open();
    }

    public enum type_t
    {
        /// <summary>
        /// uninitialized type [value = -2]
        /// </summary>
        UNINIT = -2,

        /// <summary>
        /// invalid Type [value = -1]
        /// </summary>
        BADID = -1,

        /// <summary>
        /// type ID for File objects [value = 1]
        /// </summary>
        FILE = 1,

        /// <summary>
        /// type ID for Group objects [value = 2]
        /// </summary>
        GROUP,

        /// <summary>
        /// type ID for Datatype objects [value = 3]
        /// </summary>
        DATATYPE,

        /// <summary>
        /// type ID for Dataspace objects [value = 4]
        /// </summary>
        DATASPACE,

        /// <summary>
        /// type ID for Dataset objects [value = 5]
        /// </summary>
        DATASET,

        /// <summary>
        /// type ID for Attribute objects [value = 6]
        /// </summary>
        ATTR,

        /// <summary>
        /// type ID for Reference objects [value = 7]
        /// </summary>
        REFERENCE,

        /// <summary>
        /// type ID for virtual file layer [value = 8]
        /// </summary>
        VFL,

        /// <summary>
        /// type ID for generic property list classes [value = 9]
        /// </summary>
        GENPROP_CLS,

        /// <summary>
        /// type ID for generic property lists [value = 10]
        /// </summary>
        GENPROP_LST,

        /// <summary>
        /// type ID for error classes [value = 11]
        /// </summary>
        ERROR_CLASS,

        /// <summary>
        /// type ID for error messages [value = 12]
        /// </summary>
        ERROR_MSG,

        /// <summary>
        /// type ID for error stacks [value = 13]
        /// </summary>
        ERROR_STACK,

        /// <summary>
        /// number of library types, MUST BE LAST! [value = 14]
        /// </summary>
        NTYPES
    }

    public const int H5_SIZEOF_HID_T = sizeof(long);

    /// <summary>
    /// An invalid object ID. This is also negative for error return.
    /// </summary>
    public const hid_t H5I_INVALID_HID = -1;

    /// <summary>
    /// Function for freeing objects. This function will be called with an
    /// object ID type number and a pointer to the object. The function
    /// should free the object and return non-negative to indicate that
    /// the object can be removed from the ID type. If the function returns
    /// negative (failure) then the object will remain in the ID type.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t free_t(ssize_t obj);

    /// <summary>
    /// Type of the function to compare objects and keys
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int search_func_t(ssize_t obj, hid_t id, ssize_t key);

    /// <summary>
    /// Deletes all identifiers of the given type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-ClearType" /> for further reference.</para>
    /// </summary>
    /// <param name="type">Identifier of identifier type which is to be
    /// cleared of identifiers</param>
    /// <param name="force">Whether or not to force deletion of all
    /// identifiers</param>
    /// <returns>Returns non-negative on success, negative on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iclear_type"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t clear_type(type_t type, hbool_t force);

    /// <summary>
    /// Decrements the reference count for an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-DecRef" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Object identifier whose reference count will
    /// be modified.</param>
    /// <returns>Returns a non-negative reference count of the object
    /// identifier after decrementing it, if successful; otherwise a
    /// negative value is returned.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Idec_ref"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial int dec_ref(hid_t obj_id);

    /// <summary>
    /// Decrements the reference count on an identifier type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-DecTypeRef" /> for further reference.</para>
    /// </summary>
    /// <param name="type">The identifier of the type whose reference count
    /// is to be decremented</param>
    /// <returns>Returns the current reference count on success, negative
    /// on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Idec_type_ref"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial int dec_type_ref(type_t type);

    /// <summary>
    /// Removes the type type and all identifiers within that type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-DestroyType" /> for further reference.</para>
    /// </summary>
    /// <param name="type">Identifier of identifier type which is to be
    /// destroyed</param>
    /// <returns>Returns non-negative on success, negative on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Idestroy_type"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t destroy_type(type_t type);

    /// <summary>
    /// Retrieves an identifier for the file containing the specified object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-GetFileId" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Identifier of the object whose associated file identifier will be returned.</param>
    /// <returns>Returns a file identifier on success, negative on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iget_file_id"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_file_id(hid_t obj_id);

    /// <summary>
    /// Retrieves a name of an object based on the object identifier.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-GetName" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Identifier of the object. This identifier can
    /// refer to a group, dataset, or named datatype.</param>
    /// <param name="name">A name associated with the identifier.</param>
    /// <param name="size">The size of the name buffer; must be the size of
    /// the name in bytes plus 1 for a <c>NULL</c> terminator.</param>
    /// <returns>Returns the length of the name if successful, returning 0
    /// (zero) if no name is associated with the identifier. Otherwise 
    /// returns a negative value.</returns>
    /// <remarks>ASCII strings ONLY! This function does not work with UTF-8
    /// encoded strings. See JIRA issue HDF5/HDFFV-9686.</remarks>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iget_name",
               CharSet = CharSet.Ansi,
               CallingConvention = CallingConvention.Cdecl),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern ssize_t get_name
        (hid_t obj_id, [In] [Out] StringBuilder name, size_t size);

    /// <summary>
    /// Retrieves the reference count for an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-GetRef" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Object identifier whose reference count will
    /// be retrieved.</param>
    /// <returns>Returns a non-negative current reference count of the
    /// object identifier if successful; otherwise a negative value is
    /// returned.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iget_ref"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial int get_ref(hid_t obj_id);

    /// <summary>
    /// Retrieves the type of an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-GetType" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Object identifier whose type is to be
    /// determined.</param>
    /// <returns>Returns the object type if successful; otherwise
    /// <c>H5I_BADID</c>.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iget_type"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial type_t get_type(hid_t obj_id);

    /// <summary>
    /// Retrieves the reference count on an ID type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-GetTypeRef" /> for further reference.</para>
    /// </summary>
    /// <param name="type">The identifier of the type whose reference count
    /// is to be retrieved</param>
    /// <returns>Returns the current reference count on success, negative
    /// on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iget_type_ref"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial int get_type_ref(type_t type);

    /// <summary>
    /// Increments the reference count for an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-IncRef" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Object identifier whose reference count will
    /// be modified.</param>
    /// <returns>Returns a non-negative reference count of the object ID
    /// after incrementing it if successful; otherwise a negative value is
    /// returned.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iinc_ref"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial int inc_ref(hid_t obj_id);

    /// <summary>
    /// Increments the reference count on an ID type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-IncTypeRef" /> for further reference.</para>
    /// </summary>
    /// <param name="type">The identifier of the type whose reference count
    /// is to be incremented</param>
    /// <returns>Returns the current reference count on success, negative
    /// on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iinc_type_ref"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial int inc_type_ref(type_t type);

    /// <summary>
    /// Determines whether an identifier is valid.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-IsValid" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Identifier to validate</param>
    /// <returns>Returns <c>TRUE</c> if <paramref name="obj_id"/> is
    /// valid and <c>FALSE</c> if invalid.
    /// Otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iis_valid"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial htri_t is_valid(hid_t obj_id);

    /// <summary>
    /// Returns the number of identifiers in a given identifier type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-NMembers" /> for further reference.</para>
    /// </summary>
    /// <param name="type">Identifier for the identifier type whose member
    /// count will be retrieved</param>
    /// <param name="num_members">Number of identifiers of the specified
    /// identifier type.</param>
    /// <returns>Returns a non-negative value on success; otherwise returns
    /// negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Inmembers"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t nmembers
        (type_t type, ref hsize_t num_members);

    /// <summary>
    /// Returns the object referenced by id.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-ObjectVerify" /> for further reference.</para>
    /// </summary>
    /// <param name="id">ID to be dereferenced</param>
    /// <param name="id_type">ID type to which id should belong</param>
    /// <returns>Pointer to the object referenced by id on success,
    /// <c>NULL</c> on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iobject_verify"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial ssize_t object_verify
        (hid_t id, type_t id_type);

    /// <summary>
    /// Creates and returns a new ID.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-Register" /> for further reference.</para>
    /// </summary>
    /// <param name="type">The identifier of the type to which the new ID
    /// will belong</param>
    /// <param name="obj">Pointer to memory for the library to store</param>
    /// <returns>Returns the new ID on success, negative on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iregister"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t register(type_t type, ssize_t obj);

    /// <summary>
    /// Creates and returns a new ID type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-RegisterType" /> for further reference.</para>
    /// </summary>
    /// <param name="hash_size">Size of the hash table (in entries) used to
    /// store IDs for the new type</param>
    /// <param name="reserved">Number of reserved IDs for the new type</param>
    /// <param name="free_func">Function used to deallocate space for a
    /// single ID</param>
    /// <returns>Returns the type identifier on success, negative on
    /// failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iregister_type"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial type_t register_type
        (size_t hash_size, uint reserved, free_t free_func);

    /// <summary>
    /// Removes an ID from internal storage.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-RemoveVerify" /> for further reference.</para>
    /// </summary>
    /// <param name="id">The ID to be removed from internal storage</param>
    /// <param name="id_type">The identifier of the type whose reference
    /// count is to be retrieved</param>
    /// <returns>Returns a pointer to the memory referred to by id on
    /// success, <c>NULL</c> on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Iremove_verify"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial ssize_t remove_verify
        (hid_t id, type_t id_type);

    /// <summary>
    /// Finds the memory referred to by an ID within the given ID type such
    /// that some criterion is satisfied.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-Search" /> for further reference.</para>
    /// </summary>
    /// <param name="type">The identifier of the type to be searched</param>
    /// <param name="func">The function defining the search criteria</param>
    /// <param name="key">A key for the search function</param>
    /// <returns>Returns a pointer to the object which satisfies the search
    /// function on success, <c>NULL</c> on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Isearch"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial ssize_t search
        (type_t type, search_func_t func, ssize_t key);

    /// <summary>
    /// Determines whether an identifier type is registered.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5I.html#Identify-TypeExists" /> for further reference.</para>
    /// </summary>
    /// <param name="type">Identifier type.</param>
    /// <returns>Returns 1 if the type is registered and 0 if not. Returns
    /// a negative value on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Itype_exists"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial htri_t type_exists(type_t type);
}
