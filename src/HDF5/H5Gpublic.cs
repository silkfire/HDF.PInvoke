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
using hid_t = System.Int64;

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security;

public sealed partial class H5G
{
    static H5G() { H5.open(); }

    /// <summary>
    /// Types of link storage for groups
    /// </summary>
    public enum storage_type_t
    {
        /// <summary>
        /// Unknown link storage type [value = -1].
        /// </summary>
        UNKNOWN = -1,
        /// <summary>
        /// Links in group are stored with a "symbol table"
        /// (this is sometimes called "old-style" groups) [value = 0].
        /// </summary>
        SYMBOL_TABLE,
        /// <summary>
        /// Links are stored in object header [value = 1].
        /// </summary>
        COMPACT,
        /// <summary>
        /// Links are stored in fractal heap and indexed with v2 B-tree
        /// [value = 2].
        /// </summary>
        DENSE
    }

    /// <summary>
    /// Information struct for group
    /// (for H5Gget_info/H5Gget_info_by_name/H5Gget_info_by_idx)
    /// </summary>
    public struct info_t
    {
        /// <summary>
        /// Type of storage for links in group
        /// </summary>
        public storage_type_t storage_type;
        /// <summary>
        /// Number of links in group
        /// </summary>
        public hsize_t nlinks;
        /// <summary>
        /// Current max. creation order value for group
        /// </summary>
        public long max_corder;
        /// <summary>
        /// Whether group has a file mounted on it
        /// </summary>
        public hbool_t mounted;
    }

    /// <summary>
    /// Closes the specified group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Close" /> for further reference.</para>
    /// </summary>
    /// <param name="group_id">Group identifier to release.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gclose"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t close(hid_t group_id);

    /// <summary>
    /// Creates a new group and links it into the file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="name">Absolute or relative name of the link to the new group.
    /// <para>ASCII strings only!</para></param>
    /// <returns>Returns a group identifier if successful; otherwise returns a negative value.</returns>
    public static hid_t create(hid_t loc_id, string name) => create(loc_id, name, H5.Encoding.Ascii, H5P.DEFAULT, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates a new group and links it into the file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="name">Absolute or relative name of the link to the new group</param>
    /// <param name="nameEncoding">Specifies the encoding used for <paramref name="name"/>.</param>
    /// <param name="lcpl_id">Link creation property list identifier</param>
    /// <returns>Returns a group identifier if successful; otherwise returns a negative value.</returns>
    public static hid_t create(hid_t loc_id, string name, H5.Encoding nameEncoding, hid_t lcpl_id) => create(loc_id, name, nameEncoding, lcpl_id, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates a new group and links it into the file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="name">Absolute or relative name of the link to the new group</param>
    /// <param name="nameEncoding">Specifies the encoding used for <paramref name="name"/>.</param>
    /// <param name="lcpl_id">Link creation property list identifier</param>
    /// <param name="gcpl_id">Group creation property list identifier</param>
    /// <returns>Returns a group identifier if successful; otherwise returns a negative value.</returns>
    public static hid_t create(hid_t loc_id, string name, H5.Encoding nameEncoding, hid_t lcpl_id, hid_t gcpl_id) => create(loc_id, name, nameEncoding, lcpl_id, gcpl_id, H5P.DEFAULT);

    /// <summary>
    /// Creates a new group and links it into the file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="name">Absolute or relative name of the link to the new group</param>
    /// <param name="nameEncoding">Specifies the encoding used for <paramref name="name"/>.</param>
    /// <param name="lcpl_id">Link creation property list identifier</param>
    /// <param name="gcpl_id">Group creation property list identifier</param>
    /// <param name="gapl_id">Group access property list identifier</param>
    /// <returns>Returns a group identifier if successful; otherwise returns a negative value.</returns>
    public static hid_t create(hid_t loc_id, string name, H5.Encoding nameEncoding, hid_t lcpl_id, hid_t gcpl_id, hid_t gapl_id)
    {
        hid_t groupId;

        unsafe
        {
            byte* namePtr;

            switch (nameEncoding)
            {
                case H5.Encoding.Utf8:
                    var utf8StringMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
                    try
                    {
                        byte* ptr = stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
                        utf8StringMarshaller.FromManaged(name, new System.Span<byte>(ptr, Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize));
                        {
                            namePtr = utf8StringMarshaller.ToUnmanaged();
                            groupId = Create();
                        }
                    }
                    finally
                    {
                        utf8StringMarshaller.Free();
                    }

                    break;
                default:
                    var ansiStringMarshaller = new AnsiStringMarshaller.ManagedToUnmanagedIn();
                    try
                    {
                        byte* ptr = stackalloc byte[AnsiStringMarshaller.ManagedToUnmanagedIn.BufferSize];
                        ansiStringMarshaller.FromManaged(name, new System.Span<byte>(ptr, AnsiStringMarshaller.ManagedToUnmanagedIn.BufferSize));
                        {
                            namePtr = ansiStringMarshaller.ToUnmanaged();
                            groupId = Create();
                        }
                    }
                    finally
                    {
                        ansiStringMarshaller.Free();
                    }

                    break;
            }
            
            hid_t Create() => create(loc_id, (nint)namePtr, lcpl_id, gcpl_id, gapl_id);
        }

        return groupId;
    }

    /// <summary>
    /// Creates a new group and links it into the file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="name">Absolute or relative name of the link to the new group</param>
    /// <param name="lcpl_id">Link creation property list identifier</param>
    /// <param name="gcpl_id">Group creation property list identifier</param>
    /// <param name="gapl_id">Group access property list identifier</param>
    /// <returns>Returns a group identifier if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gcreate2"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create(hid_t loc_id, nint name, hid_t lcpl_id, hid_t gcpl_id, hid_t gapl_id);

    /// <summary>
    /// Creates a new empty group without linking it into the file structure.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-CreateAnon" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying the file in which the new group is to be created</param>
    /// <returns>Returns a new group identifier if successful; otherwise returns a negative value.</returns>
    public static hid_t create_anon(hid_t loc_id) => create_anon(loc_id, H5P.DEFAULT, H5P.DEFAULT);

    /// <summary>
    /// Creates a new empty group without linking it into the file structure.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-CreateAnon" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying the file in which the new group is to be created</param>
    /// <param name="gcpl_id">Group creation property list identifier</param>
    /// <returns>Returns a new group identifier if successful; otherwise returns a negative value.</returns>
    public static hid_t create_anon(hid_t loc_id, hid_t gcpl_id) => create_anon(loc_id, gcpl_id, H5P.DEFAULT);

    /// <summary>
    /// Creates a new empty group without linking it into the file structure.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-CreateAnon" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying the file in which the new group is to be created</param>
    /// <param name="gcpl_id">Group creation property list identifier</param>
    /// <param name="gapl_id">Group access property list identifier</param>
    /// <returns>Returns a new group identifier if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gcreate_anon"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create_anon(hid_t loc_id, hid_t gcpl_id, hid_t gapl_id);

    /// <summary>
    /// Gets a group creation property list identifier.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetCreatePlist" /> for further reference.</para>
    /// </summary>
    /// <param name="group_id"> Identifier of the group.</param>
    /// <returns>Returns an identifier for the group's creation property list if successful. Otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gget_create_plist"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_create_plist(hid_t group_id);

    /// <summary>
    /// Flushes all buffers associated with a group to disk.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Flush" /> for further reference.</para>
    /// </summary>
    /// <param name="group_id">Identifier of the group to be flushed.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gflush"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t flush(hid_t group_id);

    /// <summary>
    /// Retrieves information about a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfo" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Group identifier</param>
    /// <param name="ginfo">Struct in which group information is returned</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gget_info"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_info(hid_t loc_id, ref info_t ginfo);

    /// <summary>
    /// Retrieves information about a group, according to the group's
    /// position within an index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfoByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="group_name">Name of group containing group for which information is to be retrieved</param>
    /// <param name="idx_type">Index type</param>
    /// <param name="order">Order of the count in the index</param>
    /// <param name="n">Position in the index of the group for which information is retrieved</param>
    /// <param name="ginfo">Struct in which group information is returned</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    public static herr_t get_info_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, ref info_t ginfo) => get_info_by_idx(loc_id, group_name, idx_type, order, n, ref ginfo, H5P.DEFAULT);

    /// <summary>
    /// Retrieves information about a group, according to the group's
    /// position within an index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfoByIdx" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="group_name">Name of group containing group for which information is to be retrieved</param>
    /// <param name="idx_type">Index type</param>
    /// <param name="order">Order of the count in the index</param>
    /// <param name="n">Position in the index of the group for which information is retrieved</param>
    /// <param name="ginfo">Struct in which group information is returned</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gget_info_by_idx"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_info_by_idx(hid_t loc_id, nint group_name, H5.index_t idx_type, H5.iter_order_t order, hsize_t n, ref info_t ginfo, hid_t lapl_id);

    /// <summary>
    /// Retrieves information about a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfoByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="name">Name of group for which information is to be retrieved</param>
    /// <param name="ginfo">Struct in which group information is returned</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    public static herr_t get_info_by_name(hid_t loc_id, nint name, ref info_t ginfo) => get_info_by_name(loc_id, name, ref ginfo, H5P.DEFAULT);

    /// <summary>
    /// Retrieves information about a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-GetInfoByName" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier</param>
    /// <param name="name">Name of group for which information is to be retrieved</param>
    /// <param name="ginfo">Struct in which group information is returned</param>
    /// <param name="lapl_id">Link access property list</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gget_info_by_name"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_info_by_name(hid_t loc_id, nint name, ref info_t ginfo, hid_t lapl_id);

    /// <summary>
    /// Opens an existing group with a group access property list.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Open2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying the location of the group to be opened</param>
    /// <param name="name">Name of the group to open</param>
    /// <returns>Returns a group identifier if successful; otherwise
    /// returns a negative value.</returns>
    public static hid_t open(hid_t loc_id, nint name) => open(loc_id, name, H5P.DEFAULT);

    /// <summary>
    /// Opens an existing group with a group access property list.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Open2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">File or group identifier specifying the location of the group to be opened</param>
    /// <param name="name">Name of the group to open</param>
    /// <param name="gapl_id">Group access property list identifier</param>
    /// <returns>Returns a group identifier if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Gopen2"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t open(hid_t loc_id, nint name, hid_t gapl_id);

    /// <summary>
    /// Refreshes all buffers associated with a group.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5G.html#Group-Refresh" /> for further reference.</para>
    /// </summary>
    /// <param name="group_id">Identifier of the group to be refreshed.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Grefresh"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t refresh(hid_t group_id);
}
