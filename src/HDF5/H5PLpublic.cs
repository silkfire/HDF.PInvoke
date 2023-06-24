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
using ssize_t = nint;
using uint32_t = System.UInt32;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

public sealed partial class H5PL
{
    static H5PL() { H5.open(); }

    public enum type_t
    {
        TYPE_ERROR = -1,
        TYPE_FILTER = 0,
        TYPE_NONE = 1
    }

    public const int FILTER_PLUGIN = 0x0001;

    public const int ALL_PLUGIN = 0xffff;

    /// <summary>
    /// Inserts a plugin path at the end of the list.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc1.8/RM/RM_H5PL.html#Plugin-Append" /> for further reference.</para>
    /// </summary>
    /// <param name="search_path">The plugin path.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLappend"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t append(nint search_path);

    /// <summary>
    /// Query the plugin path at the specified index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc1.8/RM/RM_H5PL.html#Plugin-Get" /> for further reference.</para>
    /// </summary>
    /// <param name="index">Index.</param>
    /// <param name="path_buf">Pathname.</param>
    /// <param name="buf_size">Buffer size (in bytes).</param>
    /// <returns>Returns the length of the path, a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLget"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial ssize_t get(uint32_t index, nint path_buf, size_t buf_size);

    /// <summary>
    /// Queries the state of the loading of dynamic plugins.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc1.8/RM/RM_H5PL.html#Plugin-GetLoadingState" /> for further reference.</para>
    /// </summary>
    /// <param name="plugin_control_mask">List of dynamic plugin types that are enabled or disabled.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLget_loading_state"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t get_loading_state(ref uint32_t plugin_control_mask);

    /// <summary>
    /// Inserts a plugin search path at a specified index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc1.8/RM/RM_H5PL.html#Plugin-Insert" /> for further reference.</para>
    /// </summary>
    /// <param name="search_path">The plugin path.</param>
    /// <param name="index">Index.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLinsert"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t insert(nint search_path, uint32_t index);

    /// <summary>
    /// Inserts a plugin search path at the beginning of the list.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc1.8/RM/RM_H5PL.html#Plugin-Prepend" /> for further reference.</para>
    /// </summary>
    /// <param name="search_path">The plugin search path.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLprepend"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t prepend(nint search_path);

    /// <summary>
    /// Removes the plugin path at a specified index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc1.8/RM/RM_H5PL.html#Plugin-Remove" /> for further reference.</para>
    /// </summary>
    /// <param name="index">Index.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLremove"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t remove(uint32_t index);

    /// <summary>
    /// Replace the plugin path at the specified index.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc1.8/RM/RM_H5PL.html#Plugin-Replace" /> for further reference.</para>
    /// </summary>
    /// <param name="search_path">The plugin search path.</param>
    /// <param name="index">Index.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLreplace"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t replace(nint search_path, uint32_t index);

    /// <summary>
    /// Controls the loading of dynamic plugin types.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5PL.html#Plugin-SetLoadingState" /> for further reference.</para>
    /// </summary>
    /// <param name="plugin_control_mask">The list of dynamic plugin types to enable or disable.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLset_loading_state"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t set_loading_state(uint32_t plugin_control_mask);

    /// <summary>
    /// Retrieves the number of stored plugin paths.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5PL.html#Plugin-Size" /> for further reference.</para>
    /// </summary>
    /// <param name="num_paths">Current length of the plugin search path list.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5PLsize"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t size(ref uint32_t num_paths);
}
