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

using haddr_t = System.UInt64;
using hbool_t = System.UInt32;
using herr_t = System.Int32;
using hsize_t = System.UInt64;
using htri_t = System.Int32;
using size_t = nint;
using ssize_t = nint;
using hid_t = System.Int64;

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

public sealed partial class H5L
{
    static H5L() { H5.open(); }

    /// <summary>
    /// Maximum length of a link's name
    /// (encoded in a 32-bit unsigned integer: 4GB - 1)
    /// </summary>
    public const hbool_t MAX_LINK_NAME_LEN = unchecked((uint)(-1));

    /// <summary>
    /// Constant to indicate operation occurs on same location
    /// </summary>
    public const hid_t SAME_LOC = 0;

    /// <summary>
    /// Current version of the <see cref="class_t"/> struct.
    /// </summary>
    public const int LINK_CLASS_T_VERS = 0;

    /// <summary>
    /// Link class types.
    /// <para>Values less than 64 are reserved for the HDF5 library's internal
    /// use. Values 64 to 255 are for "user-defined" link class types;
    /// these types are defined by HDF5 but their behavior can be
    /// overridden by users. Users who want to create new classes of links
    /// should contact the HDF5 development team at hdfhelp@hdfgroup.org.
    /// These values can never change because they appear in HDF5 files.</para>
    /// </summary>
    public enum type_t
    {
        /// <summary>
        /// Invalid link type = -1
        /// </summary>
        ERROR = -1,

        /// <summary>
        /// Hard link = 0
        /// </summary>
        HARD = 0,

        /// <summary>
        /// Soft link = 1
        /// </summary>
        SOFT = 1,

        /// <summary>
        /// External link = 64
        /// </summary>
        EXTERNAL = 64,

        /// <summary>
        /// Maximum link type = 255
        /// </summary>
        MAX = 255
    }

    /// <summary>
    /// Maximum value link value for "built-in" link types
    /// </summary>
    public const type_t TYPE_BUILTIN_MAX = type_t.SOFT;

    /// <summary>
    /// Link ids at or above this value are "user-defined" link types.
    /// </summary>
    public const type_t TYPE_UD_MIN = type_t.EXTERNAL;

    /// <summary>
    /// Information struct for link (for H5Lget_info/H5Lget_info_by_idx)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct info_t
    {
        /// <summary>
        /// Type of link.
        /// </summary>
        public type_t type;

        /// <summary>
        /// Indicate if creation order is valid.
        /// </summary>
        public hbool_t corder_valid;

        /// <summary>
        /// Creation order.
        /// </summary>
        public long corder;

        /// <summary>
        /// Character set of link name.
        /// </summary>
        public H5T.cset_t cset;

        /// <summary>
        /// Address to which hard link points or size of a soft link or UD link value.
        /// </summary>
        public u_t u;

        /* union -> same field offset, see H5Lpublic.h */
        [StructLayout(LayoutKind.Explicit)]
        public struct u_t
        {
            [FieldOffset(0)]
            public haddr_t address;
            [FieldOffset(0)]
            public size_t val_size;
        }
    }

    /// <summary>
    /// Link creation callback.
    /// </summary>
    /// <param name="link_name"></param>
    /// <param name="loc_group"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    /// <param name="lcpl_id"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t create_func_t(nint link_name, hid_t loc_group, nint lnkdata, size_t lnkdata_size, hid_t lcpl_id);

    /// <summary>
    /// Link creation callback.
    /// </summary>
    /// <param name="link_name"></param>
    /// <param name="loc_group"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    /// <param name="lcpl_id"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t create_func_ascii_t(nint link_name, hid_t loc_group, nint lnkdata, size_t lnkdata_size, hid_t lcpl_id);

    /// <summary>
    /// Callback for when the link is moved.
    /// </summary>
    /// <param name="new_name"></param>
    /// <param name="new_loc"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t move_func_t(nint new_name, hid_t new_loc, nint lnkdata, size_t lnkdata_size);

    /// <summary>
    /// Callback for when the link is moved.
    /// </summary>
    /// <param name="new_name"></param>
    /// <param name="new_loc"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t move_func_ascii_t(nint new_name, hid_t new_loc, nint lnkdata, size_t lnkdata_size);

    /// <summary>
    /// Callback for when the link is copied.
    /// </summary>
    /// <param name="new_name"></param>
    /// <param name="new_loc"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t copy_func_t(nint new_name, hid_t new_loc, nint lnkdata, size_t lnkdata_size);

    /// <summary>
    /// Callback for when the link is copied.
    /// </summary>
    /// <param name="new_name"></param>
    /// <param name="new_loc"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t copy_func_ascii_t(nint new_name, hid_t new_loc, nint lnkdata, size_t lnkdata_size);

    /// <summary>
    /// Callback during link traversal.
    /// </summary>
    /// <param name="link_name"></param>
    /// <param name="cur_group"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    /// <param name="lapl_id"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t traverse_func_t(nint link_name, hid_t cur_group, nint lnkdata, size_t lnkdata_size, hid_t lapl_id);

    /// <summary>
    /// Callback during link traversal.
    /// </summary>
    /// <param name="link_name"></param>
    /// <param name="cur_group"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    /// <param name="lapl_id"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t traverse_func_ascii_t(nint link_name, hid_t cur_group, nint lnkdata, size_t lnkdata_size, hid_t lapl_id);

    /// <summary>
    /// Callback for when the link is deleted.
    /// </summary>
    /// <param name="link_name"></param>
    /// <param name="file"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t delete_func_t(nint link_name, hid_t file, nint lnkdata, size_t lnkdata_size);

    /// <summary>
    /// Callback for when the link is deleted.
    /// </summary>
    /// <param name="link_name"></param>
    /// <param name="file"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t delete_func_ascii_t(nint link_name, hid_t file, nint lnkdata, size_t lnkdata_size);

    /// <summary>
    /// Callback for querying the link.
    /// </summary>
    /// <param name="link_name"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    /// <param name="buf"></param>
    /// <param name="buf_size"></param>
    /// <returns>Returns the size of the buffer needed.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ssize_t query_func_t(nint link_name, nint lnkdata, size_t lnkdata_size, nint buf, size_t buf_size);

    /// <summary>
    /// Callback for querying the link.
    /// </summary>
    /// <param name="link_name"></param>
    /// <param name="lnkdata"></param>
    /// <param name="lnkdata_size"></param>
    /// <param name="buf"></param>
    /// <param name="buf_size"></param>
    /// <returns>Returns the size of the buffer needed.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate ssize_t query_func_ascii_t(nint link_name, nint lnkdata, size_t lnkdata_size, nint buf, size_t buf_size);

    /// <summary>
    /// User-defined link types.
    /// </summary>
    public struct class_t
    {
        /// <summary>
        /// Version number of this struct (should always be <see cref="LINK_CLASS_T_VERS"/>).
        /// </summary>
        public int version;

        /// <summary>
        /// Link class identifier.
        /// </summary>
        public type_t id;

        /// <summary>
        /// Comment for debugging.
        /// </summary>
        public nint comment;

        /// <summary>
        /// Callback during link creation.
        /// </summary>
        public create_func_t create_func;

        /// <summary>
        /// Callback after moving link.
        /// </summary>
        public move_func_t move_func;

        /// <summary>
        /// Callback after copying link.
        /// </summary>
        public copy_func_t copy_func;

        /// <summary>
        /// Callback during link traversal.
        /// </summary>
        public traverse_func_t trav_func;

        /// <summary>
        /// Callback for link deletion.
        /// </summary>
        public delete_func_t del_func;

        /// <summary>
        /// Callback for queries.
        /// </summary>
        public query_func_t query_func;
    }

    /// <summary>
    /// Prototype for <see cref="iterate"/>/<see cref="H5L.iterate_by_name(long,nint,H5.index_t,H5.iter_order_t,ref ulong,iterate_t,nint,long)"/> operator
    /// </summary>
    /// <param name="group">Group that serves as root of the iteration.</param>
    /// <param name="name">Name of link, relative to <paramref name="group"/>, being examined at current step of the iteration.</param>
    /// <param name="info">An <see cref="info_t"/> struct containing information regarding that link.</param>
    /// <param name="op_data">User-defined pointer to data required by the application in processing the link.</param>
    /// <returns>Zero causes the visit iterator to continue, returning zero when all group members have been processed. A positive value causes the visit iterator to immediately return that positive value, indicating short-circuit success. A negative value causes the visit iterator to immediately return that value, indicating failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t iterate_t(hid_t group, nint name, ref info_t info, nint op_data);

    /// <summary>
    /// Callback for external link traversal.
    /// </summary>
    /// <param name="parent_file_name"></param>
    /// <param name="parent_group_name"></param>
    /// <param name="child_file_name"></param>
    /// <param name="child_object_name"></param>
    /// <param name="acc_flags"></param>
    /// <param name="fapl_id"></param>
    /// <param name="op_data"></param>
    /// <remarks>File names MUST be ASCII strings.</remarks>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t elink_traverse_t(nint parent_file_name, nint parent_group_name, nint child_file_name, nint child_object_name, ref uint acc_flags, hid_t fapl_id, nint op_data);

    /// <summary>
    /// Callback for external link traversal.
    /// </summary>
    /// <param name="parent_file_name"></param>
    /// <param name="parent_group_name"></param>
    /// <param name="child_file_name"></param>
    /// <param name="child_object_name"></param>
    /// <param name="acc_flags"></param>
    /// <param name="fapl_id"></param>
    /// <param name="op_data"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t elink_traverse_ascii_t(nint parent_file_name, nint parent_group_name, nint child_file_name, nint child_object_name, ref uint acc_flags, hid_t fapl_id, nint op_data);

    /// <summary>
    /// Copies a link from one location to another.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Copy" /> for further reference.</para>
    /// </summary>
    /// <param name="src_loc">Location identifier of the source link.</param>
    /// <param name="src_name">Name of the link to be copied.</param>
    /// <param name="dst_loc">Location identifier specifying the destination of the copy.</param>
    /// <param name="dst_name">Name to be assigned to the new copy.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t copy(hid_t src_loc, nint src_name, hid_t dst_loc, nint dst_name) => copy(src_loc, src_name, dst_loc, dst_name, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Copies a link from one location to another.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Copy" /> for further reference.</para>
    /// </summary>
    /// <param name="src_loc">Location identifier of the source link.</param>
    /// <param name="src_name">Name of the link to be copied.</param>
    /// <param name="dst_loc">Location identifier specifying the destination of the copy.</param>
    /// <param name="dst_name">Name to be assigned to the new copy.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t copy(hid_t src_loc, nint src_name, hid_t dst_loc, nint dst_name, hid_t lcpl_id) => copy(src_loc, src_name, dst_loc, dst_name, lcpl_id, H5P.DEFAULT);

    /// <summary>
    /// Copies a link from one location to another.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Copy" /> for further reference.</para>
    /// </summary>
    /// <param name="src_loc">Location identifier of the source link.</param>
    /// <param name="src_name">Name of the link to be copied.</param>
    /// <param name="dst_loc">Location identifier specifying the destination of the copy.</param>
    /// <param name="dst_name">Name to be assigned to the new copy.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lcopy"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t copy(hid_t src_loc, nint src_name, hid_t dst_loc, nint dst_name, hid_t lcpl_id, hid_t lapl_id);

    /// <summary>
    /// Creates an external link, a soft link to an object in a different file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateExternal" /> for further reference.</para>
    /// </summary>
    /// <param name="file_name">Name of the target file containing the target object.</param>
    /// <param name="obj_name">Path within the target file to the target object.</param>
    /// <param name="link_loc_id">File or group identifier where the new link is to be created.</param>
    /// <param name="link_name">Name of the new link, relative to <paramref name="link_loc_id"/>.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t create_external(nint file_name, nint obj_name, hid_t link_loc_id, nint link_name) => create_external(file_name, obj_name, link_loc_id, link_name, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates an external link, a soft link to an object in a different file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateExternal" /> for further reference.</para>
    /// </summary>
    /// <param name="file_name">Name of the target file containing the target object.</param>
    /// <param name="obj_name">Path within the target file to the target object.</param>
    /// <param name="link_loc_id">File or group identifier where the new link is to be created.</param>
    /// <param name="link_name">Name of the new link, relative to <paramref name="link_loc_id"/>.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t create_external(nint file_name, nint obj_name, hid_t link_loc_id, nint link_name, hid_t lcpl_id) => create_external(file_name, obj_name, link_loc_id, link_name, lcpl_id, H5P.DEFAULT);

    /// <summary>
    /// Creates an external link, a soft link to an object in a different file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateExternal" /> for further reference.</para>
    /// </summary>
    /// <param name="file_name">Name of the target file containing the target object.</param>
    /// <param name="obj_name">Path within the target file to the target object.</param>
    /// <param name="link_loc_id">File or group identifier where the new link is to be created.</param>
    /// <param name="link_name">Name of the new link, relative to <paramref name="link_loc_id"/>.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lcreate_external"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t create_external(nint file_name, nint obj_name, hid_t link_loc_id, nint link_name, hid_t lcpl_id, hid_t lapl_id);

    /// <summary>
    /// Creates a hard link to an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateHard" /> for further reference.</para>
    /// </summary>
    /// <param name="cur_loc">The file or group identifier for the target object.</param>
    /// <param name="cur_name">Name of the target object, which must already exist.</param>
    /// <param name="dst_loc">The file or group identifier for the new link.</param>
    /// <param name="dst_name">The name of the new link.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t create_hard(hid_t cur_loc, nint cur_name, hid_t dst_loc, nint dst_name) => create_hard(cur_loc, cur_name, dst_loc, dst_name, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates a hard link to an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateHard" /> for further reference.</para>
    /// </summary>
    /// <param name="cur_loc">The file or group identifier for the target object.</param>
    /// <param name="cur_name">Name of the target object, which must already exist.</param>
    /// <param name="dst_loc">The file or group identifier for the new link.</param>
    /// <param name="dst_name">The name of the new link.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t create_hard(hid_t cur_loc, nint cur_name, hid_t dst_loc, nint dst_name, hid_t lcpl_id) => create_hard(cur_loc, cur_name, dst_loc, dst_name, lcpl_id, H5P.DEFAULT);

    /// <summary>
    /// Creates a hard link to an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateHard" /> for further reference.</para>
    /// </summary>
    /// <param name="cur_loc">The file or group identifier for the target object.</param>
    /// <param name="cur_name">Name of the target object, which must already exist.</param>
    /// <param name="dst_loc">The file or group identifier for the new link.</param>
    /// <param name="dst_name">The name of the new link.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lcreate_hard"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t create_hard(hid_t cur_loc, nint cur_name, hid_t dst_loc, nint dst_name, hid_t lcpl_id, hid_t lapl_id);

    /// <summary>
    /// Creates a soft link to an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateSoft" /> for further reference.</para>
    /// </summary>
    /// <param name="link_target">Path to the target object, which is not required to exist.</param>
    /// <param name="link_loc_id">The file or group identifier for the new link.</param>
    /// <param name="link_name">The name of the new link.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t create_soft(nint link_target, hid_t link_loc_id, nint link_name) => create_soft(link_target, link_loc_id, link_name, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates a soft link to an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateSoft" /> for further reference.</para>
    /// </summary>
    /// <param name="link_target">Path to the target object, which is not required to exist.</param>
    /// <param name="link_loc_id">The file or group identifier for the new link.</param>
    /// <param name="link_name">The name of the new link.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t create_soft(nint link_target, hid_t link_loc_id, nint link_name, hid_t lcpl_id) => create_soft(link_target, link_loc_id, link_name, lcpl_id, H5P.DEFAULT);

    /// <summary>
    /// Creates a soft link to an object.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateSoft" /> for further reference.</para>
    /// </summary>
    /// <param name="link_target">Path to the target object, which is not required to exist.</param>
    /// <param name="link_loc_id">The file or group identifier for the new link.</param>
    /// <param name="link_name">The name of the new link.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lcreate_soft"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t create_soft(nint link_target, hid_t link_loc_id, nint link_name, hid_t lcpl_id, hid_t lapl_id);

    /// <summary>
    /// Creates a link of a user-defined type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateUD" /> for further reference.</para>
    /// </summary>
    /// <param name="link_loc_id">Link location identifier.</param>
    /// <param name="link_name">Link name.</param>
    /// <param name="link_type">User-defined link class.</param>
    /// <param name="udata">User-supplied link information.</param>
    /// <param name="udata_size">Size of udata buffer.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t create_ud(hid_t link_loc_id, nint link_name, type_t link_type, nint udata, size_t udata_size) => create_ud(link_loc_id, link_name, link_type, udata, udata_size, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates a link of a user-defined type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateUD" /> for further reference.</para>
    /// </summary>
    /// <param name="link_loc_id">Link location identifier.</param>
    /// <param name="link_name">Link name.</param>
    /// <param name="link_type">User-defined link class.</param>
    /// <param name="udata">User-supplied link information.</param>
    /// <param name="udata_size">Size of udata buffer.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t create_ud(hid_t link_loc_id, nint link_name, type_t link_type, nint udata, size_t udata_size, hid_t lcpl_id) => create_ud(link_loc_id, link_name, link_type, udata, udata_size, lcpl_id, H5P.DEFAULT);

    /// <summary>
    /// Creates a link of a user-defined type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-CreateUD" /> for further reference.</para>
    /// </summary>
    /// <param name="link_loc_id">Link location identifier.</param>
    /// <param name="link_name">Link name.</param>
    /// <param name="link_type">User-defined link class.</param>
    /// <param name="udata">User-supplied link information.</param>
    /// <param name="udata_size">Size of udata buffer.</param>
    /// <param name="lcpl_id">Link creation property list identifier.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lcreate_ud"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t create_ud(hid_t link_loc_id, nint link_name, type_t link_type, nint udata, size_t udata_size, hid_t lcpl_id, hid_t lapl_id);

    /// <summary>
    /// Removes a link from a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Delete" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Identifier of the file or group containing the object.</param>
    /// <param name="name">Name of the link to delete.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t delete(hid_t loc_id, nint name) => delete(loc_id, name, H5P.DEFAULT);

    /// <summary>
    /// Removes a link from a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Delete" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Identifier of the file or group containing the object.</param>
    /// <param name="name">Name of the link to delete.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Ldelete"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t delete(hid_t loc_id, nint name, hid_t lapl_id);

    /// <summary>
    /// Removes the n-th link in a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-DeleteByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Index or field which determines the order.</param>
    /// <param name="order">Order within field or index.</param>
    /// <param name="n">Link for which to retrieve information.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t delete_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n) => delete_by_idx(loc_id, group_name, idx_type, order, n, H5P.DEFAULT);

    /// <summary>
    /// Removes the n-th link in a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-DeleteByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Index or field which determines the order.</param>
    /// <param name="order">Order within field or index.</param>
    /// <param name="n">Link for which to retrieve information.</param>
    /// <param name="lapl_id">Link access property list.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Ldelete_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t delete_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, hid_t lapl_id);

    /// <summary>
    /// Determine whether a link with the specified name exists in a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Exists" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Identifier of the file or group to query.</param>
    /// <param name="name">The name of the link to check.</param>
    /// <returns>Returns 1 or 0 if successful; otherwise returns a negative value.</returns>
    public static htri_t exists(hid_t loc_id, nint name) => exists(loc_id, name, H5P.DEFAULT);

    /// <summary>
    /// Determine whether a link with the specified name exists in a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Exists" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Identifier of the file or group to query.</param>
    /// <param name="name">The name of the link to check.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>Returns 1 or 0 if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lexists"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial htri_t exists(hid_t loc_id, nint name, hid_t lapl_id);

    /// <summary>
    /// Returns information about a link.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetInfo" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier.</param>
    /// <param name="name">Name of the link for which information is being sought.</param>
    /// <param name="linfo">Buffer in which link information is returned.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t get_info(hid_t loc_id, nint name, ref info_t linfo) => get_info(loc_id, name, ref linfo, H5P.DEFAULT);

    /// <summary>
    /// Returns information about a link.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetInfo" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier.</param>
    /// <param name="name">Name of the link for which information is being sought.</param>
    /// <param name="linfo">Buffer in which link information is returned.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lget_info"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t get_info(hid_t loc_id, nint name, ref info_t linfo, hid_t lapl_id);

    /// <summary>
    /// Retrieves metadata for a link in a group, according to the order within a field or index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetInfoByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Index or field which determines the order.</param>
    /// <param name="order">Order within field or index.</param>
    /// <param name="n">Link for which to retrieve information.</param>
    /// <param name="linfo">Buffer in which link value is returned.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t get_info_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, ref info_t linfo) => get_info_by_idx(loc_id, group_name, idx_type, order, n, ref linfo, H5P.DEFAULT);

    /// <summary>
    /// Retrieves metadata for a link in a group, according to the order within a field or index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetInfoByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Index or field which determines the order.</param>
    /// <param name="order">Order within field or index.</param>
    /// <param name="n">Link for which to retrieve information.</param>
    /// <param name="linfo">Buffer in which link value is returned.</param>
    /// <param name="lapl_id">Link access property list.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lget_info_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t get_info_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, ref info_t linfo, hid_t lapl_id);

    /// <summary>
    /// Retrieves name of the nth link in a group, according to the order within a specified field or index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetNameByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Index or field which determines the order.</param>
    /// <param name="order">Order within field or index.</param>
    /// <param name="n">Link for which to retrieve information.</param>
    /// <param name="name">Buffer in which link value is returned.</param>
    /// <param name="size">Size in bytes of <paramref name="name"/>.</param>
    /// <returns>Returns the size of the link name if successful; otherwise returns a negative value.</returns>
    public static ssize_t get_name_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, nint name, size_t size) => get_name_by_idx(loc_id, group_name, idx_type, order, n, group_name, size, H5P.DEFAULT);

    /// <summary>
    /// Retrieves name of the nth link in a group, according to the order within a specified field or index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetNameByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Index or field which determines the order.</param>
    /// <param name="order">Order within field or index.</param>
    /// <param name="n">Link for which to retrieve information.</param>
    /// <param name="name">Buffer in which link value is returned.</param>
    /// <param name="size">Size in bytes of <paramref name="name"/>.</param>
    /// <param name="lapl_id">Link access property list.</param>
    /// <returns>Returns the size of the link name if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lget_name_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ssize_t get_name_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, nint name, size_t size, hid_t lapl_id);

    /// <summary>
    /// Returns the value of a symbolic link.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetVal" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier.</param>
    /// <param name="name">Link whose value is to be returned.</param>
    /// <param name="buf">The buffer to hold the returned link value.</param>
    /// <param name="size">Maximum number of characters of link value to be returned.</param>
    /// <returns>Returns a non-negative value, with the link value in <paramref name="buf"/>, if successful. Otherwise returns a negative value.</returns>
    public static herr_t get_val(hid_t loc_id, nint name, nint buf, size_t size) => get_val(loc_id, name, buf, size, H5P.DEFAULT);

    /// <summary>
    /// Returns the value of a symbolic link.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetVal" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier.</param>
    /// <param name="name">Link whose value is to be returned.</param>
    /// <param name="buf">The buffer to hold the returned link value.</param>
    /// <param name="size">Maximum number of characters of link value to be returned.</param>
    /// <param name="lapl_id">List access property list identifier.</param>
    /// <returns>Returns a non-negative value, with the link value in <paramref name="buf"/>, if successful. Otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lget_val"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t get_val(hid_t loc_id, nint name, nint buf, size_t size, hid_t lapl_id);

    /// <summary>
    /// Retrieves value of the nth link in a group, according to the order within an index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetValByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Type of index.</param>
    /// <param name="order">Order within field or index.</param>
    /// <param name="n">Link for which to retrieve information.</param>
    /// <param name="buf">Pointer to buffer in which link value is returned.</param>
    /// <param name="size">Size in bytes of <paramref name="group_name"/>.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t get_val_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, nint buf, size_t size) => get_val_by_idx(loc_id, group_name, idx_type, order, n, buf, size, H5P.DEFAULT);

    /// <summary>
    /// Retrieves value of the nth link in a group, according to the order within an index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-GetValByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Type of index.</param>
    /// <param name="order">Order within field or index.</param>
    /// <param name="n">Link for which to retrieve information.</param>
    /// <param name="buf">Pointer to buffer in which link value is returned.</param>
    /// <param name="size">Size in bytes of <paramref name="group_name"/>.</param>
    /// <param name="lapl_id">Link access property list.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lget_val_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t get_val_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, nint buf, size_t size, hid_t lapl_id);

    /// <summary>
    /// Determines whether a class of user-defined links is registered.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-IsRegistered" /> for further reference.</para>
    /// </summary>
    /// <param name="id">User-defined link class identifier.</param>
    /// <returns>Returns a positive value if the link class has been registered and zero if it is unregistered. Otherwise returns a negative value; this may mean that the identifier is not a valid user-defined class identifier.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lis_registered"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial htri_t is_registered(type_t id);

    /// <summary>
    /// Iterates through links in a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Iterate" /> for further reference.</para>
    /// </summary>
    /// <param name="grp_id">Identifier specifying subject group.</param>
    /// <param name="idx_type">Type of index which determines the order.</param>
    /// <param name="order">Order within index.</param>
    /// <param name="idx">Iteration position at which to start.</param>
    /// <param name="op">Callback function passing data regarding the link to the calling application.</param>
    /// <param name="op_data">User-defined pointer to data required by the application for its processing of the link.</param>
    /// <returns>On success, returns the return value of the first operator that returns a positive value, or zero if all members were processed with no operator returning non-zero. On failure, returns a negative value if something goes wrong within the library, or the first negative value returned by an operator.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Literate"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t iterate(hid_t grp_id, H5.index_t idx_type, H5.iter_order_t order, ref hsize_t idx, iterate_t op, nint op_data);

    /// <summary>
    /// Iterates through links in a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-IterateByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Type of index which determines the order.</param>
    /// <param name="order">Order within index.</param>
    /// <param name="idx">Iteration position at which to start.</param>
    /// <param name="op">Callback function passing data regarding the link to the calling application.</param>
    /// <param name="op_data">User-defined pointer to data required by the application for its processing of the link.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t iterate_by_name(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, ref hsize_t idx, iterate_t op, nint op_data) => iterate_by_name(loc_id, group_name, idx_type, order, ref idx, op, op_data, H5P.DEFAULT);

    /// <summary>
    /// Iterates through links in a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-IterateByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying location of subject group.</param>
    /// <param name="group_name">Name of subject group.</param>
    /// <param name="idx_type">Type of index which determines the order.</param>
    /// <param name="order">Order within index.</param>
    /// <param name="idx">Iteration position at which to start.</param>
    /// <param name="op">Callback function passing data regarding the link to the calling application.</param>
    /// <param name="op_data">User-defined pointer to data required by the application for its processing of the link.</param>
    /// <param name="lapl_id">Link access property list.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Literate_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t iterate_by_name(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, ref hsize_t idx, iterate_t op, nint op_data, hid_t lapl_id);

    /// <summary>
    /// Moves a link within an HDF5 file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Move" /> for further reference.</para>
    /// </summary>
    /// <param name="src_loc">Original file or group identifier.</param>
    /// <param name="src_name">Original link name.</param>
    /// <param name="dst_loc">Destination file or group identifier.</param>
    /// <param name="dst_name">New link name.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t move(hid_t src_loc, nint src_name, hid_t dst_loc, nint dst_name) => move(src_loc, src_name, dst_loc, dst_name, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Moves a link within an HDF5 file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Move" /> for further reference.</para>
    /// </summary>
    /// <param name="src_loc">Original file or group identifier.</param>
    /// <param name="src_name">Original link name.</param>
    /// <param name="dst_loc">Destination file or group identifier.</param>
    /// <param name="dst_name">New link name.</param>
    /// <param name="lcpl_id">Link creation property list identifier to be associated with the new link.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t move(hid_t src_loc, nint src_name, hid_t dst_loc, nint dst_name, hid_t lcpl_id) => move(src_loc, src_name, dst_loc, dst_name, lcpl_id, H5P.DEFAULT);

    /// <summary>
    /// Moves a link within an HDF5 file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Move" /> for further reference.</para>
    /// </summary>
    /// <param name="src_loc">Original file or group identifier.</param>
    /// <param name="src_name">Original link name.</param>
    /// <param name="dst_loc">Destination file or group identifier.</param>
    /// <param name="dst_name">New link name.</param>
    /// <param name="lcpl_id">Link creation property list identifier to be associated with the new link.</param>
    /// <param name="lapl_id">Link access property list identifier to be associated with the new link.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lmove"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t move(hid_t src_loc, nint src_name, hid_t dst_loc, nint dst_name, hid_t lcpl_id, hid_t lapl_id);

    /// <summary>
    /// Registers a user-defined link class or changes behavior of an existing class.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Register" /> for further reference.</para>
    /// </summary>
    /// <param name="cls">Pointer to a buffer containing the struct describing the user-defined link class.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t register(class_t cls)
    {
        var filterClassPtr = Marshal.AllocHGlobal(Marshal.SizeOf(cls));
        Marshal.StructureToPtr(cls, filterClassPtr, false);
        var @return = register(filterClassPtr);
        Marshal.FreeHGlobal(filterClassPtr);

        return @return;
    }

    /// <summary>
    /// Registers a user-defined link class or changes behavior of an existing class.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Register" /> for further reference.</para>
    /// </summary>
    /// <param name="cls">Pointer to a buffer containing the struct describing the user-defined link class.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lregister"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t register(nint cls);

    /// <summary>
    /// Decodes external link information.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-UnpackELinkVal" /> for further reference.</para>
    /// </summary>
    /// <param name="ext_linkval">Buffer containing external link information.</param>
    /// <param name="link_size">Size, in bytes, of the <paramref name="ext_linkval"/> buffer.</param>
    /// <param name="flags">External link flags, packed as a bitmap.</param>
    /// <param name="filename">Returned filename.</param>
    /// <param name="obj_path">Returned object path, relative to <paramref name="filename"/>.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lunpack_elink_val"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t unpack_elink_val(nint ext_linkval, size_t link_size, ref uint flags, nint filename, nint obj_path);

    /// <summary>
    /// Unregisters a class of user-defined links.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Unregister" /> for further reference.</para>
    /// </summary>
    /// <param name="id">User-defined link class identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lunregister"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t unregister(type_t id);

    /// <summary>
    /// Recursively visits all links starting from a specified group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-Visit" /> for further reference.</para>
    /// </summary>
    /// <param name="grp_id">Identifier of the group at which the recursive iteration begins.</param>
    /// <param name="idx_type">Type of index.</param>
    /// <param name="order">Order in which index is traversed.</param>
    /// <param name="op">Callback function passing data regarding the link to the calling application.</param>
    /// <param name="op_data">User-defined pointer to data required by the application for its processing of the link.</param>
    /// <returns>On success, returns the return value of the first operator that returns a positive value, or zero if all members were processed with no operator returning non-zero. On failure, returns a negative value if something goes wrong within the library, or the first negative value returned by an operator.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lvisit"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t visit(hid_t grp_id, H5.index_t idx_type, H5.iter_order_t order, iterate_t op, nint op_data);

    /// <summary>
    /// Recursively visits all links starting from a specified group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-VisitByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Identifier of a file or group.</param>
    /// <param name="group_name">Name of the group, generally relative to <paramref name="loc_id"/>, that will serve as root of the iteration.</param>
    /// <param name="idx_type">Type of index.</param>
    /// <param name="order">Order in which index is traversed.</param>
    /// <param name="op">Callback function passing data regarding the link to the calling application.</param>
    /// <param name="op_data">User-defined pointer to data required by the application for its processing of the link.</param>
    /// <returns>On success, returns the return value of the first operator that returns a positive value, or zero if all members were processed with no operator returning non-zero. On failure, returns a negative value if something goes wrong within the library, or the first negative value returned by an operator.</returns>
    public static herr_t visit_by_name(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, iterate_t op, nint op_data) => visit_by_name(loc_id, group_name, idx_type, order, op, op_data, H5P.DEFAULT);

    /// <summary>
    /// Recursively visits all links starting from a specified group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5L.html#Link-VisitByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Identifier of a file or group.</param>
    /// <param name="group_name">Name of the group, generally relative to <paramref name="loc_id"/>, that will serve as root of the iteration.</param>
    /// <param name="idx_type">Type of index.</param>
    /// <param name="order">Order in which index is traversed.</param>
    /// <param name="op">Callback function passing data regarding the link to the calling application.</param>
    /// <param name="op_data">User-defined pointer to data required by the application for its processing of the link.</param>
    /// <param name="lapl_id">Link access property list identifier.</param>
    /// <returns>On success, returns the return value of the first operator that returns a positive value, or zero if all members were processed with no operator returning non-zero. On failure, returns a negative value if something goes wrong within the library, or the first negative value returned by an operator.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Lvisit_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t visit_by_name(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, iterate_t op, nint op_data, hid_t lapl_id);
}
