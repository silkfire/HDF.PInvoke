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
using H5O_msg_crt_idx_t = System.UInt32;
using ssize_t = nint;
using hid_t = System.Int64;

using System.Runtime.InteropServices;
using System.Security;

public sealed partial class H5A
{
    static H5A() { H5.open(); }

    /// <summary>
    /// Information struct for attribute
    /// (for H5Aget_info/H5Aget_info_by_idx)
    /// </summary>
    public struct info_t
    {
        /// <summary>
        /// Indicate if creation order is valid
        /// </summary>
        public hbool_t corder_valid;
        /// <summary>
        /// Creation order
        /// </summary>
        public H5O_msg_crt_idx_t corder;
        /// <summary>
        /// Character set of attribute name
        /// </summary>
        public H5T.cset_t cset;
        /// <summary>
        /// Size of raw data
        /// </summary>
        public hsize_t data_size;
    }

    /// <summary>
    /// Delegate for H5Aiterate2() callbacks
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Iterate2" /> for further reference.</para>
    /// </summary>
    /// <param name="location_id">The location identifier for the group or
    /// dataset being iterated over</param>
    /// <param name="attr_name">The name of the current object attribute.</param>
    /// <param name="ainfo">The attribute's <c>info</c>struct</param>
    /// <param name="op_data">A pointer referencing operator data passed
    /// to <c>iterate</c></param>
    /// <returns>Valid return values from an operator and the resulting
    /// H5Aiterate2 and op behavior are as follows: Zero causes the iterator
    /// to continue, returning zero when all attributes have been processed.
    /// A positive value causes the iterator to immediately return that
    /// positive value, indicating short-circuit success. The iterator can
    /// be restarted at the next attribute, as indicated by the return
    /// value of <c>n</c>. A negative value causes the iterator to
    /// immediately return that value, indicating failure. The iterator can
    /// be restarted at the next attribute, as indicated by the return value
    /// of <c>n</c>.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t operator_t(hid_t location_id, nint attr_name, ref info_t ainfo, nint op_data);

    /// <summary>
    /// Closes the specified attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Close" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Attribute to release access to.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aclose"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t close(hid_t attr_id);

    /// <summary>
    /// Creates an attribute attached to a specified object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="type_id">Attribute datatype identifier</param>
    /// <param name="space_id">Attribute dataspace identifier</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t create(hid_t loc_id, nint attr_name, hid_t type_id, hid_t space_id) => create(loc_id, attr_name, type_id, space_id, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates an attribute attached to a specified object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="type_id">Attribute datatype identifier</param>
    /// <param name="space_id">Attribute dataspace identifier</param>
    /// <param name="acpl_id">Attribute creation property list identifier</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t create(hid_t loc_id, nint attr_name, hid_t type_id, hid_t space_id, hid_t acpl_id) => create(loc_id, attr_name, type_id, space_id, acpl_id, H5P.DEFAULT);

    /// <summary>
    /// Creates an attribute attached to a specified object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="type_id">Attribute datatype identifier</param>
    /// <param name="space_id">Attribute dataspace identifier</param>
    /// <param name="acpl_id">Attribute creation property list identifier</param>
    /// <param name="aapl_id">Attribute access property list identifier</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Acreate2"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create(hid_t loc_id, nint attr_name, hid_t type_id, hid_t space_id, hid_t acpl_id, hid_t aapl_id);

    /// <summary>
    /// Creates an attribute attached to a specified object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-CreateByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name, relative to <paramref name="loc_id"/>, of object that attribute is to be attached to</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="type_id">Attribute datatype identifier</param>
    /// <param name="space_id">Attribute dataspace identifier</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t create_by_name(hid_t loc_id, nint obj_name, nint attr_name, hid_t type_id, hid_t space_id) => create_by_name(loc_id, obj_name, attr_name, type_id, space_id, H5P.DEFAULT, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates an attribute attached to a specified object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-CreateByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name, relative to <paramref name="loc_id"/>, of object that attribute is to be attached to</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="type_id">Attribute datatype identifier</param>
    /// <param name="space_id">Attribute dataspace identifier</param>
    /// <param name="acpl_id">Attribute creation property list identifier</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t create_by_name(hid_t loc_id, nint obj_name, nint attr_name, hid_t type_id, hid_t space_id, hid_t acpl_id) => create_by_name(loc_id, obj_name, attr_name, type_id, space_id, acpl_id, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates an attribute attached to a specified object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-CreateByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name, relative to <paramref name="loc_id"/>, of object that attribute is to be attached to</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="type_id">Attribute datatype identifier</param>
    /// <param name="space_id">Attribute dataspace identifier</param>
    /// <param name="acpl_id">Attribute creation property list identifier</param>
    /// <param name="aapl_id">Attribute access property list identifier</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t create_by_name(hid_t loc_id, nint obj_name, nint attr_name, hid_t type_id, hid_t space_id, hid_t acpl_id, hid_t aapl_id) => create_by_name(loc_id, obj_name, attr_name, type_id, space_id, acpl_id, aapl_id, H5P.DEFAULT);

    /// <summary>
    /// Creates an attribute attached to a specified object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-CreateByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name, relative to <paramref name="loc_id"/>, of object that attribute is to be attached to</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="type_id">Attribute datatype identifier</param>
    /// <param name="space_id">Attribute dataspace identifier</param>
    /// <param name="acpl_id">Attribute creation property list identifier</param>
    /// <param name="aapl_id">Attribute access property list identifier</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Acreate_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create_by_name(hid_t loc_id, nint obj_name, nint attr_name, hid_t type_id, hid_t space_id, hid_t acpl_id, hid_t aapl_id, hid_t lapl_id);

    /// <summary>
    /// Deletes an attribute from a specified location.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Delete" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Identifier of the dataset, group, or named
    /// datatype to have the attribute deleted from.</param>
    /// <param name="name">Name of the attribute to delete.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Adelete"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t delete(hid_t loc_id, nint name);

    /// <summary>
    /// Deletes an attribute from an object according to index order.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-DeleteByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name of object, relative to location, from which attribute is to be removed</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Order in which to iterate over index</param>
    /// <param name="n">Offset within index</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    public static herr_t delete_by_idx(hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n) => delete_by_idx(loc_id, obj_name, idx_type, order, n, H5P.DEFAULT);

    /// <summary>
    /// Deletes an attribute from an object according to index order.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-DeleteByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name of object, relative to location, from which attribute is to be removed</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Order in which to iterate over index</param>
    /// <param name="n">Offset within index</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Adelete_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t delete_by_idx(hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, hid_t lapl_id);

    /// <summary>
    /// Removes an attribute from a specified location.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-DeleteByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name of object, relative to location, from which attribute is to be removed</param>
    /// <param name="attr_name">Name of attribute to delete</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    public static herr_t delete_by_name(hid_t loc_id, nint obj_name, nint attr_name) => delete_by_name(loc_id, obj_name, attr_name, H5P.DEFAULT);

    /// <summary>
    /// Removes an attribute from a specified location.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-DeleteByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name of object, relative to location, from which attribute is to be removed</param>
    /// <param name="attr_name">Name of attribute to delete</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Adelete_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t delete_by_name(hid_t loc_id, nint obj_name, nint attr_name, hid_t lapl_id);

    /// <summary>
    /// Determines whether an attribute with a given name exists on an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Exists" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Object identifier</param>
    /// <param name="attr_name">Attribute name</param>
    /// <returns>When successful, returns a positive value, for
    /// <c>TRUE</c>, or 0 (zero), for <c>FALSE</c>. Otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aexists"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial htri_t exists(hid_t obj_id, nint attr_name);

    /// <summary>
    /// Determines whether an attribute with a given name exists on an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-ExistsByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location identifier</param>
    /// <param name="obj_name">Object name</param>
    /// <param name="attr_name">Attribute name</param>
    /// <returns>When successful, returns a positive value, for
    /// <c>TRUE</c>, or 0 (zero), for <c>FALSE</c>. Otherwise
    /// returns a negative value.</returns>
    public static htri_t exists_by_name(hid_t loc_id, nint obj_name, nint attr_name) => exists_by_name(loc_id, obj_name, attr_name, H5P.DEFAULT);

    /// <summary>
    /// Determines whether an attribute with a given name exists on an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-ExistsByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location identifier</param>
    /// <param name="obj_name">Object name</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="lapl_id">Link access property list identifier</param>
    /// <returns>When successful, returns a positive value, for
    /// <c>TRUE</c>, or 0 (zero), for <c>FALSE</c>. Otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aexists_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial htri_t exists_by_name(hid_t loc_id, nint obj_name, nint attr_name, hid_t lapl_id);

    /// <summary>
    /// Gets an attribute creation property list identifier.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetCreatePlist" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Identifier of the attribute.</param>
    /// <returns>Returns an identifier for the attribute's creation property
    /// list if successful. Otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_create_plist"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_create_plist(hid_t attr_id);

    /// <summary>
    /// Retrieves attribute information, by attribute identifier.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetInfo" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Attribute identifier</param>
    /// <param name="ainfo">Attribute information struct</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_info"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_info(hid_t attr_id, ref info_t ainfo);

    /// <summary>
    /// Retrieves attribute information, by attribute index position.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetInfoByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Index traversal order</param>
    /// <param name="n">Attribute's position in index</param>
    /// <param name="ainfo">Struct containing returned attribute
    /// information</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    public static herr_t get_info_by_idx(hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, ref info_t ainfo) => get_info_by_idx(loc_id, obj_name, idx_type, order, n, ref ainfo, H5P.DEFAULT);

    /// <summary>
    /// Retrieves attribute information, by attribute index position.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetInfoByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Index traversal order</param>
    /// <param name="n">Attribute's position in index</param>
    /// <param name="ainfo">Struct containing returned attribute
    /// information</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_info_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_info_by_idx(hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, ref info_t ainfo, hid_t lapl_id);

    /// <summary>
    /// Retrieves attribute information, by attribute name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetInfoByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="ainfo">Struct containing returned attribute
    /// information</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    public static herr_t get_info_by_name(hid_t loc_id, nint obj_name, nint attr_name, ref info_t ainfo) => get_info_by_name(loc_id, obj_name, attr_name, ref ainfo, H5P.DEFAULT);

    /// <summary>
    /// Retrieves attribute information, by attribute name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetInfoByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="attr_name">Attribute name</param>
    /// <param name="ainfo">Struct containing returned attribute
    /// information</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_info_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_info_by_name(hid_t loc_id, nint obj_name, nint attr_name, ref info_t ainfo, hid_t lapl_id);

    /// <summary>
    /// Gets an attribute name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetName" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Identifier of the attribute.</param>
    /// <param name="buf_size">The size of the buffer to store the name
    /// in.</param>
    /// <param name="name">Buffer to store name in.</param>
    /// <returns>Returns the length of the attribute's name, which may be
    /// longer than <c>buf_size</c>, if successful. Otherwise returns
    /// a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial ssize_t get_name(hid_t attr_id, size_t buf_size, nint name);

    /// <summary>
    /// Gets an attribute name, by attribute index position.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetNameByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Index traversal order</param>
    /// <param name="n">Attribute's position in index</param>
    /// <param name="name">Attribute name</param>
    /// <param name="size">Size, in bytes, of attribute name</param>
    /// <returns>Returns attribute name size, in bytes, if successful;
    /// otherwise returns a negative value.</returns>
    public static ssize_t get_name_by_idx(hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, nint name, size_t size) => get_name_by_idx(loc_id, obj_name, idx_type, order, n, name, size, H5P.DEFAULT);

    /// <summary>
    /// Gets an attribute name, by attribute index position.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetNameByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Index traversal order</param>
    /// <param name="n">Attribute's position in index</param>
    /// <param name="name">Attribute name</param>
    /// <param name="size">Size, in bytes, of attribute name</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns attribute name size, in bytes, if successful;
    /// otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_name_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial ssize_t get_name_by_idx(hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, nint name, size_t size, hid_t lapl_id);

    /// <summary>
    /// Gets a copy of the dataspace for an attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetSpace" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Identifier of an attribute.</param>
    /// <returns>Returns attribute dataspace identifier if successful;
    /// otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_space"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_space(hid_t attr_id);

    /// <summary>
    /// Returns the amount of storage required for an attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetStorageSize" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Identifier of the attribute to query.</param>
    /// <returns>Returns the amount of storage size allocated for the
    /// attribute; otherwise returns 0 (zero).</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_storage_size"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hsize_t get_storage_size(hid_t attr_id);

    /// <summary>
    /// Returns the amount of storage required for an attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-GetType" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Identifier of an attribute.</param>
    /// <returns>Returns a datatype identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aget_type"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_type(hid_t attr_id);

    /// <summary>
    /// Calls user-defined function for each attribute on an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Iterate2" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Identifier for object to which attributes are attached; may be group, dataset, or named datatype.</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Order in which to iterate over index</param>
    /// <param name="n">Initial and returned offset within index</param>
    /// <param name="op">User-defined function to pass each attribute to</param>
    /// <param name="op_data">User data to pass through to and to be returned by iterator operator function</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value. Further note that this function returns the return value of the last operator if it was non-zero, which can be a negative value, zero if all attributes were processed, or a positive value indicating short-circuit success.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aiterate2"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t iterate(hid_t obj_id, H5.index_t idx_type, H5.iter_order_t order, ref hsize_t n, operator_t op, nint op_data);

    /// <summary>
    /// Calls user-defined function for each attribute on an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-IterateByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name of object, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Order in which to iterate over index</param>
    /// <param name="n">Initial and returned offset within index</param>
    /// <param name="op">User-defined function to pass each attribute to</param>
    /// <param name="op_data">User data to pass through to and to be returned by iterator operator function</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value. Further note that this function returns the return value of the last operator if it was non-zero, which can be a negative value, zero if all attributes were processed, or a positive value indicating short-circuit success.</returns>
    public static herr_t iterate_by_name(hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, ref hsize_t n, operator_t op, nint op_data) => iterate_by_name(loc_id, obj_name, idx_type, order, ref n, op, op_data, H5P.DEFAULT);

    /// <summary>
    /// Calls user-defined function for each attribute on an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-IterateByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset or group</param>
    /// <param name="obj_name">Name of object, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Order in which to iterate over index</param>
    /// <param name="n">Initial and returned offset within index</param>
    /// <param name="op">User-defined function to pass each attribute to</param>
    /// <param name="op_data">User data to pass through to and to be returned by iterator operator function</param>
    /// <param name="lapd_id">Link access property list</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value. Further note that this function returns the return value of the last operator if it was non-zero, which can be a negative value, zero if all attributes were processed, or a positive value indicating short-circuit success.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aiterate_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t iterate_by_name(hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, ref hsize_t n, operator_t op, nint op_data, hid_t lapd_id);

    /// <summary>
    /// Opens an attribute for an object specified by object identifier
    /// and attribute name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Open" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Identifer for object to which attribute is
    /// attached</param>
    /// <param name="attr_name">Name of attribute to open</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t open(hid_t obj_id, nint attr_name) => open(obj_id, attr_name, H5P.DEFAULT);

    /// <summary>
    /// Opens an attribute for an object specified by object identifier
    /// and attribute name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Open" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Identifer for object to which attribute is
    /// attached</param>
    /// <param name="attr_name">Name of attribute to open</param>
    /// <param name="aapl_id">Attribute access property list</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aopen"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t open(hid_t obj_id, nint attr_name, hid_t aapl_id);

    /// <summary>
    /// Opens an attribute for an object specified by attribute index
    /// position.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-OpenByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Index traversal order</param>
    /// <param name="n">Attribute's position in index</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t open_by_idx (hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n) => open_by_idx(loc_id, obj_name, idx_type, order, n, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Opens an attribute for an object specified by attribute index
    /// position.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-OpenByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Index traversal order</param>
    /// <param name="n">Attribute's position in index</param>
    /// <param name="aapl_id">Attribute access property list</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t open_by_idx (hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, hid_t aapl_id) => open_by_idx(loc_id, obj_name, idx_type, order, n, aapl_id, H5P.DEFAULT);

    /// <summary>
    /// Opens an attribute for an object specified by attribute index
    /// position.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-OpenByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of object to which attribute is
    /// attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to location</param>
    /// <param name="idx_type">Type of index</param>
    /// <param name="order">Index traversal order</param>
    /// <param name="n">Attribute's position in index</param>
    /// <param name="aapl_id">Attribute access property list</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aopen_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t open_by_idx (hid_t loc_id, nint obj_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, hid_t aapl_id, hid_t lapl_id);

    /// <summary>
    /// Opens an attribute for an object by object name and attribute name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-OpenByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location from which to find object to which
    /// attribute is attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to <paramref name="loc_id"/></param>
    /// <param name="attr_name">Name of attribute to open</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t open_by_name(hid_t loc_id, nint obj_name, nint attr_name) => open_by_name(loc_id, obj_name, attr_name, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Opens an attribute for an object by object name and attribute name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-OpenByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location from which to find object to which
    /// attribute is attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to <paramref name="loc_id"/></param>
    /// <param name="attr_name">Name of attribute to open</param>
    /// <param name="aapl_id">Attribute access property list </param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t open_by_name(hid_t loc_id, nint obj_name, nint attr_name, hid_t aapl_id) => open_by_name(loc_id, obj_name, attr_name, aapl_id, H5P.DEFAULT);

    /// <summary>
    /// Opens an attribute for an object by object name and attribute name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-OpenByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location from which to find object to which
    /// attribute is attached</param>
    /// <param name="obj_name">Name of object to which attribute is
    /// attached, relative to <paramref name="loc_id"/></param>
    /// <param name="attr_name">Name of attribute to open</param>
    /// <param name="aapl_id">Attribute access property list </param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns an attribute identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aopen_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t open_by_name(hid_t loc_id, nint obj_name, nint attr_name, hid_t aapl_id, hid_t lapl_id);

    /// <summary>
    /// Reads an attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Read" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Identifier of an attribute to read.</param>
    /// <param name="type_id"> Identifier of the attribute datatype
    /// (in memory).</param>
    /// <param name="buf">Buffer for data to be read.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Aread"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t read(hid_t attr_id, hid_t type_id, nint buf);

    /// <summary>
    /// Renames an attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Rename" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location of the attribute.</param>
    /// <param name="old_name">Name of the attribute to be changed.</param>
    /// <param name="new_name">New name for the attribute.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Arename"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t rename(hid_t loc_id, nint old_name, nint new_name);

    /// <summary>
    /// Renames an attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-RenameByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset
    /// or group</param>
    /// <param name="obj_name">Name of object, relative to location, whose
    /// attribute is to be renamed</param>
    /// <param name="old_attr_name">Prior attribute name</param>
    /// <param name="new_attr_name">New attribute name</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    public static herr_t rename_by_name(hid_t loc_id, nint obj_name, nint old_attr_name, nint new_attr_name) => rename_by_name(loc_id, obj_name, old_attr_name, new_attr_name, H5P.DEFAULT);

    /// <summary>
    /// Renames an attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-RenameByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location or object identifier; may be dataset
    /// or group</param>
    /// <param name="obj_name">Name of object, relative to location, whose
    /// attribute is to be renamed</param>
    /// <param name="old_attr_name">Prior attribute name</param>
    /// <param name="new_attr_name">New attribute name</param>
    /// <param name="lapl_id">Link access property list identifier</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Arename_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t rename_by_name(hid_t loc_id, nint obj_name, nint old_attr_name, nint new_attr_name, hid_t lapl_id);

    /// <summary>
    /// Writes data to an attribute.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5A.html#Annot-Write" /> for further reference.</para>
    /// </summary>
    /// <param name="attr_id">Identifier of an attribute to write.</param>
    /// <param name="mem_type_id">Identifier of the attribute datatype (in memory).</param>
    /// <param name="buf">Data to be written.</param>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Awrite"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t write(hid_t attr_id, hid_t mem_type_id, nint buf);
}
