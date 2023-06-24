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
using hssize_t = System.Int64;

using System;
using System.Runtime.InteropServices;
using System.Security;

public sealed partial class H5
{
    static H5()
    {
        NativeDependencies.ResolvePathToExternalDependencies();
    }

    public const hsize_t HSIZE_UNDEF = unchecked((hsize_t)(hssize_t)(-1));

    public const hsize_t HADDR_UNDEF = unchecked((haddr_t)(hssize_t)(-1));

    public const hsize_t HADDR_MAX = HADDR_UNDEF - 1;

    /// <summary>
    /// Specifies the encoding to use when marshalling strings.
    /// </summary>
    public enum Encoding
    {
        /// <summary>
        /// Encoding is ASCII.
        /// </summary>
        Ascii,

        /// <summary>
        /// Encoding is UTF-8.
        /// </summary>
        Utf8
    }

    /// <summary>
    /// Common iteration orders
    /// </summary>
    public enum iter_order_t : int
    {
        /// <summary>
        /// Unknown order [value = -1].
        /// </summary>
        UNKNOWN = -1,
        /// <summary>
        /// Increasing order [value = 0].
        /// </summary>
        INC = 0,
        /// <summary>
        /// Decreasing order [value = 1].
        /// </summary>
        DEC = 1,
        /// <summary>
        /// No particular order, whatever is fastest [value = 2].
        /// </summary>
        NATIVE = 2,
        /// <summary>
        /// Number of iteration orders [value = 3].
        /// </summary>
        N = 3
    }

    ///<summary>
    /// Iteration callback values
    /// (Actually, any postive value will cause the iterator to stop and
    /// pass back that positive value to the function that called the
    /// iterator)
    ///</summary>
    public enum H5IterationResult : int
    {
        /// <summary>
        /// Failure [value = -1].
        /// </summary>
        FAILURE = -1,
        /// <summary>
        /// Success and continue [value = 0].
        /// </summary>
        CONT = 0,
        /// <summary>
        /// Success and stop [value = 1].
        /// </summary>
        STOP = 1
    }

    /// <summary>
    /// The types of indices on links in groups/attributes on objects.
    /// Primarily used for "[do] [foo] by index" routines and for iterating
    /// over links in groups/attributes on objects.
    /// </summary>
    public enum index_t : int
    {
        /// <summary>
        /// Unknown index type [value = -1].
        /// </summary>
        UNKNOWN = -1,
        /// <summary>
        /// Index on names [value = 0].
        /// </summary>
        NAME,
        /// <summary>
        /// Index on creation order [value = 1].
        /// </summary>
        CRT_ORDER,
        /// <summary>
        /// Number of indices defined [value = 2].
        /// </summary>
        N
    }

    /// <summary>
    /// Storage info struct used by H5O_info_t and H5F_info_t 
    /// </summary>
    public struct ih_info_t
    {
        /// <summary>
        /// btree and/or list
        /// </summary>
        public hsize_t index_size;
        public hsize_t heap_size;
    }

    /// <summary>
    /// Allocates memory that will later be freed internally by the HDF5
    /// Library.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-AllocateMemory" /> for further reference.</para>
    /// </summary>
    /// <param name="size">Specifies the size in bytes of the buffer to be allocated.</param>
    /// <param name="clear">Specifies whether the new buffer is to be initialized to 0 (zero).</param>
    /// <returns>On success, returns pointer to newly allocated buffer or returns <c>NULL</c> if size is 0 (zero). Returns <c>NULL</c> on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5allocate_memory"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial nint allocate_memory(nint size, hbool_t clear);

    /// <summary>
    /// Flushes all data to disk, closes all open identifiers, and cleans up memory.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-Close" /> for further reference.</para>
    /// </summary>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5close"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t close();

    /// <summary>
    /// Instructs library not to install atexit cleanup routine.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-DontAtExit" /> for further reference.</para>
    /// </summary>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5dont_atexit"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t dont_atexit();

    /// <summary>
    /// Frees memory allocated by the HDF5 Library.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-FreeMemory" /> for further reference.</para>
    /// </summary>
    /// <param name="buf">Buffer to be freed. Can be <c>NULL</c>.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5free_memory"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t free_memory(nint buf);

    /// <summary>
    /// Garbage collects on all free-lists of all types.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-GarbageCollect" /> for further reference.</para>
    /// </summary>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5garbage_collect"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t garbage_collect();

    /// <summary>
    /// Returns the HDF library release number.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-Version" /> for further reference.</para>
    /// </summary>
    /// <param name="majnum">The major version of the library.</param>
    /// <param name="minnum">The minor version of the library.</param>
    /// <param name="relnum">The release number of the library.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5get_libversion"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_libversion(ref uint majnum, ref uint minnum, ref uint relnum);

    /// <summary>
    /// Determine whether the HDF5 Library was built with the thread-safety feature enabled.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-IsLibraryThreadsafe" /> for further reference.</para>
    /// </summary>
    /// <param name="is_ts">Boolean value indicating whether the library was built with thread-safety enabled.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5is_library_threadsafe"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t is_library_threadsafe(ref hbool_t is_ts);

    /// <summary>
    /// Initializes the HDF5 library.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-Open" /> for further reference.</para>
    /// </summary>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5open"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t open();

    /// <summary>
    /// Resizes and possibly re-allocates memory that will later be freed internally by the HDF5 Library.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-ResizeMemory" /> for further reference.</para>
    /// </summary>
    /// <param name="mem">Pointer to a buffer to be resized. May be <c>NULL</c>.</param>
    /// <param name="size">New size of the buffer, in bytes.</param>
    /// <returns>On success, returns pointer to resized or reallocated buffer or returns <c>NULL</c> if size is 0 (zero). Returns <c>NULL</c> on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5resize_memory"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial nint resize_memory(nint mem, nint size);


    /// <summary>
    /// Sets free-list size limits.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5.html#Library-SetFreeListLimits" /> for further reference.</para>
    /// </summary>
    /// <param name="reg_global_lim">The cumulative limit, in bytes, on memory used for all regular free lists. (Default: 1MB)</param>
    /// <param name="reg_list_lim">The limit, in bytes, on memory used for each regular free list. (Default: 64KB)</param>
    /// <param name="arr_global_lim">The cumulative limit, in bytes, on memory used for all array free lists. (Default: 4MB)</param>
    /// <param name="arr_list_lim">The limit, in bytes, on memory used for each array free list. (Default: 256KB)</param>
    /// <param name="blk_global_lim">The cumulative limit, in bytes, on memory used for all block free lists and, separately, for all factory free lists. (Default: 16MB)</param>
    /// <param name="blk_list_lim">The limit, in bytes, on memory used for each block or factory free list. (Default: 1MB)</param>
    /// <returns> Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5set_free_list_limits"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t set_free_list_limits(int reg_global_lim, int reg_list_lim, int arr_global_lim, int arr_list_lim, int blk_global_lim, int blk_list_lim);
}
