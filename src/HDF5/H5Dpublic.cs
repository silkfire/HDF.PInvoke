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
using hsize_t = System.UInt64;
using size_t = nint;
using uint32_t = System.UInt32;
using hid_t = System.Int64;

using System.Runtime.InteropServices;
using System.Security;

public sealed unsafe partial class H5D
{
    static H5D() { H5.open(); }

    public readonly size_t CACHE_NSLOTS_DEFAULT = new size_t(-1);
    public readonly size_t CACHE_NBYTES_DEFAULT = new size_t(-1);
    public const float CACHE_W0_DEFAULT = -1.0f;

    /// <summary>
    /// Bit flags for the H5Pset_chunk_opts() and H5Pget_chunk_opts()
    /// </summary>
    public const uint DONT_FILTER_PARTIAL_CHUNKS = 0x0002u;

    public const string XFER_DIRECT_CHUNK_WRITE_FLAG_NAME =
        "direct_chunk_flag";
    public const string XFER_DIRECT_CHUNK_WRITE_FILTERS_NAME =
        "direct_chunk_filters";
    public const string XFER_DIRECT_CHUNK_WRITE_OFFSET_NAME =
        "direct_chunk_offset";
    public const string XFER_DIRECT_CHUNK_WRITE_DATASIZE_NAME =
        "direct_chunk_datasize";

    /// <summary>
    /// Values for the H5D_LAYOUT property
    /// </summary>
    public enum layout_t
    {
        LAYOUT_ERROR = -1,
        COMPACT = 0,
        CONTIGUOUS = 1,
        CHUNKED = 2,
        VIRTUAL = 3,
        NLAYOUTS
    }

    /// <summary>
    /// Types of chunk index data structures
    /// </summary>
    public enum chunk_index_t
    {
        /// <summary>
        /// v1 B-tree index [value = 0]
        /// </summary>
        BTREE = 0,
        /// <summary>
        /// Single Chunk index (cur dims[]=max dims[]=chunk dims[];
        /// filtered and non-filtered)
        /// </summary>
        SINGLE = 1,
        /// <summary>
        /// Implicit: No Index (H5D_ALLOC_TIME_EARLY, non-filtered,
        /// fixed dims)
        /// </summary>
        NONE = 2,
        /// <summary>
        /// Fixed array (for 0 unlimited dims)
        /// </summary>
        FARRAY = 3,
        /// <summary>
        /// Extensible array (for 1 unlimited dim)
        /// </summary>
        EARRAY = 4,
        /// <summary>
        /// v2 B-tree index (for >1 unlimited dims)
        /// </summary>
        BT2 = 5,
        IDX_NTYPES
    }

    /// <summary>
    /// Values for the space allocation time property
    /// </summary>
    public enum alloc_time_t
    {
        ERROR = -1,
        DEFAULT = 0,
        EARLY = 1,
        LATE = 2,
        INCR = 3
    }

    /// <summary>
    /// Values for the status of space allocation
    /// </summary>
    public enum space_status_t
    {
        ERROR = -1,
        NOT_ALLOCATED = 0,
        PART_ALLOCATED = 1,
        ALLOCATED = 2
    }

    /// <summary>
    /// Values for time of writing fill value property
    /// </summary>
    public enum fill_time_t
    {
        ERROR = -1,
        ALLOC = 0,
        NEVER = 1,
        IFSET = 2
    }

    /// <summary>
    /// Values for fill value status
    /// </summary>
    public enum fill_value_t
    {
        ERROR = -1,
        UNDEFINED = 0,
        DEFAULT = 1,
        USER_DEFINED = 2
    }

    /// <summary>
    /// Values for VDS bounds option
    /// </summary>
    public enum vds_view_t
    {
        ERROR = -1,
        FIRST_MISSING = 0,
        LAST_AVAILABLE = 1
    }

    /// <summary>
    /// Callback for <see cref="H5P.set_append_flush"/> in a dataset access property list.
    /// </summary>
    /// <param name="dataset_id"></param>
    /// <param name="cur_dims"></param>
    /// <param name="op_data"></param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t append_cb_t(hid_t dataset_id, hsize_t[] cur_dims, size_t op_data);

    /// <summary>
    /// Delegate for H5Dgather() callback
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Gather" /> for further reference.</para>
    /// </summary>
    /// <param name="dst_buf">Pointer to the destination buffer which has been filled with the next set of elements gathered
    /// <para>This will always be identical to the <paramref name="dst_buf"/> passed to <see cref="gather"/>.</para>
    /// </param>
    /// <param name="dst_buf_bytes_used">Pointer to the number of valid bytes in <paramref name="dst_buf"/>.</param>
    /// <param name="op_data">User-defined pointer to data required by the callback function.</param>
    /// <returns>The callback function should return zero (0) to indicate success, and a negative value to indicate failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t gather_func_t(size_t dst_buf, size_t dst_buf_bytes_used, size_t op_data);

    /// <summary>
    /// Delegate for H5Diterate() callback
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Iterate" /> for further reference.</para>
    /// </summary>
    /// <param name="elem">Pointer to the memory buffer containing the
    /// current data element</param>
    /// <param name="type_id">Datatype identifier for the elements stored
    /// in <paramref name="elem"/></param>
    /// <param name="ndim">Number of dimensions for the
    /// <paramref name="point"/> array</param>
    /// <param name="point">Array containing the location of the element
    /// within the original dataspace</param>
    /// <param name="op_data">Pointer to any user-defined data associated
    /// with the operation</param>
    /// <returns>Zero causes the iterator to continue, returning zero when
    /// all data elements have been processed. A positive value causes the
    /// iterator to immediately return that positive value, indicating
    /// short-circuit success. A negative value causes the iterator to
    /// immediately return that value, indicating failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t operator_t
    (
        size_t elem,
        hid_t type_id,
        uint ndim,
        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
        hsize_t[] point,
        size_t op_data
    );

    /// <summary>
    /// Delegate for H5Dscatter()
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Scatter" /> for further reference.</para>
    /// </summary>
    /// <param name="src_buf">Pointer to the buffer holding the next set of
    /// elements to scatter. </param>
    /// <param name="src_buf_bytes_used">Pointer to the number of valid
    /// bytes in <paramref name="src_buf"/>.</param>
    /// <param name="op_data">User-defined pointer to data required by the
    /// callback function.</param>
    /// <returns>The callback function should return zero (0) to indicate
    /// success, and a negative value to indicate failure.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t scatter_func_t
    (
        ref size_t src_buf/*out*/,
        ref size_t src_buf_bytes_used/*out*/,
        size_t op_data
    );

    /// <summary>
    /// Closes the specified dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Close" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to close access to.
    /// </param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dclose"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t close(hid_t dset_id);

    /// <summary>
    /// Creates a new dataset and links it into the file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location identifier</param>
    /// <param name="name">Dataset name</param>
    /// <param name="type_id">Datatype identifier</param>
    /// <param name="space_id">Dataspace identifier</param>
    /// <param name="lcpl_id">Link creation property list</param>
    /// <param name="dcpl_id">Dataset creation property list</param>
    /// <param name="dapl_id">Dataset access property list</param>
    /// <returns>Returns a dataset identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dcreate2"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create
    (hid_t loc_id, byte[] name, hid_t type_id, hid_t space_id,
     hid_t lcpl_id = H5P.DEFAULT, hid_t dcpl_id = H5P.DEFAULT,
     hid_t dapl_id = H5P.DEFAULT);

    /// <summary>
    /// Creates a new dataset and links it into the file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Create2" /> for further reference.</para>
    /// </summary>
    /// <param name="loc_id">Location identifier</param>
    /// <param name="name">Dataset name</param>
    /// <param name="type_id">Datatype identifier</param>
    /// <param name="space_id">Dataspace identifier</param>
    /// <param name="lcpl_id">Link creation property list</param>
    /// <param name="dcpl_id">Dataset creation property list</param>
    /// <param name="dapl_id">Dataset access property list</param>
    /// <returns>Returns a dataset identifier if successful; otherwise
    /// returns a negative value.</returns>
    /// <remarks>ASCII strings ONLY!</remarks>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dcreate2", StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller)),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create
    (hid_t loc_id, string name, hid_t type_id, hid_t space_id,
     hid_t lcpl_id = H5P.DEFAULT, hid_t dcpl_id = H5P.DEFAULT,
     hid_t dapl_id = H5P.DEFAULT);

    /// <summary>
    /// Creates a dataset in a file without linking it into the file structure.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-CreateAnon" /> for further reference.</para>
    /// </summary>
    /// <param name="file_id">Identifier of the file or group within which
    /// to create the dataset.</param>
    /// <param name="type_id">Identifier of the datatype to use when
    /// creating the dataset.</param>
    /// <param name="space_id">Identifier of the dataspace to use when
    /// creating the dataset.</param>
    /// <param name="dcpl_id">Dataset creation property list identifier.</param>
    /// <param name="dapl_id">Dataset access property list identifier.</param>
    /// <returns>Returns a dataset identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dcreate_anon"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create_anon
    (hid_t file_id, hid_t type_id, hid_t space_id,
     hid_t dcpl_id = H5P.DEFAULT, hid_t dapl_id = H5P.DEFAULT);

    /// <summary>
    /// Fills dataspace elements with a fill value in a memory buffer.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Fill" /> for further reference.</para>
    /// </summary>
    /// <param name="fill">Pointer to the fill value to be used.</param>
    /// <param name="fill_type">Fill value datatype identifier.</param>
    /// <param name="buf">Pointer to the memory buffer containing the
    /// selection to be filled.</param>
    /// <param name="buf_type">Datatype of dataspace elements to be filled.
    /// </param>
    /// <param name="space">Dataspace describing memory buffer and
    /// containing the selection to be filled.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dfill"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t fill
    (size_t fill, hid_t fill_type, size_t buf, hid_t buf_type,
     hid_t space);

    /// <summary>
    /// Flushes all buffers associated with a dataset to disk.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Flush" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to be flushed.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dflush"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t flush(hid_t dset_id);

    /// <summary>
    /// Gathers data from a selection within a memory buffer.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Gather" /> for further reference.</para>
    /// </summary>
    /// <param name="src_space_id">Identifier for the dataspace describing
    /// both the dimensions of the source buffer and the selection within
    /// the source buffer to gather data from.</param>
    /// <param name="src_buf">Source buffer which the data will be gathered
    /// from.</param>
    /// <param name="type_id"> Identifier for the datatype describing the
    /// data in both the source and definition buffers. This is only used
    /// to calculate the element size.</param>
    /// <param name="dst_buf_size">Size in bytes of
    /// <paramref name="dst_buf"/>.</param>
    /// <param name="dst_buf">Destination buffer where the gathered data
    /// will be placed.</param>
    /// <param name="op">Callback function which handles the gathered data.
    /// Optional if <paramref name="dst_buf"/> is large enough to hold all
    /// of the gathered data; required otherwise.</param>
    /// <param name="op_data">User-defined pointer to data required by
    /// <paramref name="op"/>.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dgather"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t gather
    (hid_t src_space_id, size_t src_buf, hid_t type_id,
     size_t dst_buf_size, size_t dst_buf, gather_func_t op,
     size_t op_data);

    /// <summary>
    /// Returns the dataset access property list associated with a dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-GetAccessPlist" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to get access
    /// property list of.</param>
    /// <returns>Returns a dataset access property list identifier if
    /// Ssuccessful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_access_plist"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_access_plist(hid_t dset_id);

    /// <summary>
    /// Retrieves information about a chunk specified by the chunk index.
    /// </summary>
    /// <param name="dset_id"> Dataset identifier.</param>
    /// <param name="fspace_id">File dataspace selection identifier.</param>
    /// <param name="index">Chunk index in the selection.</param>
    /// <param name="offset">Pointer to a one-dimensional array with a size
    /// equal to the dataset's rank.</param>
    /// <param name="filter_mask"> Filter mask that indicates which filters
    /// were used with the chunk when written.</param>
    /// <param name="addr">Chunk address in the file.</param>
    /// <param name="size">Chunk size in bytes.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_chunk_info"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_chunk_info
        (hid_t dset_id, hid_t fspace_id, hsize_t index, hsize_t* offset,
        ref uint32_t filter_mask, ref haddr_t addr, ref hsize_t size);

    /// <summary>
    /// Retrieves information about a chunk specified by the chunk index.
    /// </summary>
    /// <param name="dset_id"> Dataset identifier.</param>
    /// <param name="fspace_id">File dataspace selection identifier.</param>
    /// <param name="index">Chunk index in the selection.</param>
    /// <param name="offset">Reference to a one-dimensional array with a size
    /// equal to the dataset's rank.</param>
    /// <param name="filter_mask"> Filter mask that indicates which filters
    /// were used with the chunk when written.</param>
    /// <param name="addr">Chunk address in the file.</param>
    /// <param name="size">Chunk size in bytes.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_chunk_info",
        CallingConvention = CallingConvention.Cdecl),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern herr_t get_chunk_info
        (hid_t dset_id, hid_t fspace_id, hsize_t index, [In][Out] hsize_t[] offset,
        ref uint32_t filter_mask, ref haddr_t addr, ref hsize_t size);

    /// <summary>
    /// Retrieves information about a chunk specified by its coordinates
    /// </summary>
    /// <param name="dset_id"> Dataset identifier.</param>
    /// <param name="offset">Pointer to a one-dimensional array with a size
    /// equal to the dataset's rank.</param>
    /// <param name="filter_mask"> Filter mask that indicates which filters
    /// were used with the chunk when written.</param>
    /// <param name="addr">Chunk address in the file.</param>
    /// <param name="size">Chunk size in bytes.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_chunk_info_by_coord"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_chunk_info_by_coord
        (hid_t dset_id, hsize_t* offset, ref uint32_t filter_mask,
        ref haddr_t addr, ref hsize_t size);

    /// <summary>
    /// Retrieves information about a chunk specified by its coordinates
    /// </summary>
    /// <param name="dset_id"> Dataset identifier.</param>
    /// <param name="offset">Reference to a one-dimensional array with a size
    /// equal to the dataset's rank.</param>
    /// <param name="filter_mask"> Filter mask that indicates which filters
    /// were used with the chunk when written.</param>
    /// <param name="addr">Chunk address in the file.</param>
    /// <param name="size">Chunk size in bytes.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_chunk_info_by_coord",
        CallingConvention = CallingConvention.Cdecl),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern herr_t get_chunk_info_by_coord
        (hid_t dset_id, [In][Out] hsize_t[] offset, ref uint32_t filter_mask,
        ref haddr_t addr, ref hsize_t size);

    /// <summary>
    /// Determines the storage size (in bytes) of a chunk.
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to query.</param>
    /// /// <param name="offset">Logical position of the chunk's first
    /// element in the dataspace</param>
    /// <param name="chunk_bytes">The chunk size in bytes.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_chunk_storage_size"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_chunk_storage_size
        (hid_t dset_id, hsize_t* offset, ref hsize_t chunk_bytes);

    /// <summary>
    /// Determines the storage size (in bytes) of a chunk.
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to query.</param>
    /// /// <param name="offset">Logical position of the chunk's first
    /// element in the dataspace</param>
    /// <param name="chunk_bytes">The chunk size in bytes.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_chunk_storage_size",
        CallingConvention = CallingConvention.Cdecl),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern herr_t get_chunk_storage_size
        (hid_t dset_id, [In][Out] hsize_t[] offset, ref hsize_t chunk_bytes);

    /// <summary>
    /// Returns an identifier for a copy of the dataset creation property
    /// list for a dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-GetCreatePlist" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to query.</param>
    /// <returns>Returns a dataset creation property list identifier if
    /// successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_create_plist"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_create_plist(hid_t dset_id);

    /// <summary>
    /// Retrieves number of chunks that have nonempty intersection with a specified selection.
    /// <para>See <see href="https://portal.hdfgroup.org/display/HDF5/H5D_GET_NUM_CHUNKS" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Dataset identifier.</param>
    /// <param name="fspace_id">File dataspace selection identifier.</param>
    /// <param name="nchunks">Number of chunks in the selection.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_num_chunks"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_num_chunks(hid_t dset_id, hid_t fspace_id, ref hsize_t nchunks);

    /// <summary>
    /// Returns dataset address in file.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-GetOffset" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Dataset identifier.</param>
    /// <returns>Returns the offset in bytes; otherwise returns
    /// <c>HADDR_UNDEF</c>, a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_offset"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial haddr_t get_offset(hid_t dset_id);

    /// <summary>
    /// Returns an identifier for a copy of the dataspace for a dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-GetSpace" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to query.</param>
    /// <returns>Returns a dataspace identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_space"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_space(hid_t dset_id);

    /// <summary>
    /// Determines whether space has been allocated for a dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-GetSpaceStatus" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to query.</param>
    /// <param name="allocation">Space allocation status.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_space_status"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_space_status
        (hid_t dset_id, ref space_status_t allocation);

    /// <summary>
    /// Returns the amount of storage allocated for a dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-GetStorageSize" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to query.</param>
    /// <returns>Returns the amount of storage space, in bytes, allocated for the dataset, not counting metadata; otherwise returns 0 (zero).</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_storage_size"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hsize_t get_storage_size(hid_t dset_id);

    /// <summary>
    /// Returns an identifier for a copy of the datatype for a dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-GetType" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to query.</param>
    /// <returns>Returns a datatype identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dget_type"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_type(hid_t dset_id);

    /// <summary>
    /// Iterates over all selected elements in a dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Iterate" /> for further reference.</para>
    /// </summary>
    /// <param name="buf">Pointer to the buffer in memory containing the
    /// elements to iterate over</param>
    /// <param name="type_id">Datatype identifier for the elements stored
    /// in <c>buf</c></param>
    /// <param name="space_id">Dataspace identifier for <c>buf</c></param>
    /// <param name="op">Function pointer to the routine to be called for
    /// each element in buf iterated over</param>
    /// <param name="op_data">Pointer to any user-defined data
    /// associated with the operation</param>
    /// <returns>Returns the return value of the last operator if it was
    /// non-zero, or zero if all elements have been processed. Otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Diterate"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t iterate
    (size_t buf, hid_t type_id, hid_t space_id,
     operator_t op, size_t op_data);

    /// <summary>
    /// Opens an existing dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Open2" /> for further reference.</para>
    /// </summary>
    /// <param name="file_id">Location identifier</param>
    /// <param name="name">Dataset name</param>
    /// <param name="dapl_id">Dataset access property list</param>
    /// <returns>Returns a dataset identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dopen2"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t open
        (hid_t file_id, byte[] name, hid_t dapl_id = H5P.DEFAULT);

    /// <summary>
    /// Opens an existing dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Open2" /> for further reference.</para>
    /// </summary>
    /// <param name="file_id">Location identifier</param>
    /// <param name="name">Dataset name</param>
    /// <param name="dapl_id">Dataset access property list</param>
    /// <returns>Returns a dataset identifier if successful; otherwise
    /// returns a negative value.</returns>
    /// <remarks>ASCII strings ONLY!</remarks>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dopen2", StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller)),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t open
        (hid_t file_id, string name, hid_t dapl_id = H5P.DEFAULT);

    /// <summary>
    /// Reads raw data from a dataset into a buffer.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Read" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset read from.</param>
    /// <param name="mem_type_id">Identifier of the memory datatype.</param>
    /// <param name="mem_space_id">Identifier of the memory dataspace.</param>
    /// <param name="file_space_id">Identifier of the dataset's dataspace
    /// in the file.</param>
    /// <param name="plist_id">Identifier of a transfer property list for
    /// this I/O operation.</param>
    /// <param name="buf">Buffer to receive data read from file.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dread"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t read
    (hid_t dset_id, hid_t mem_type_id, hid_t mem_space_id,
     hid_t file_space_id, hid_t plist_id, size_t buf);


    /// <summary>
    /// Reads a raw data chunk directly from a dataset in a file into a buffer.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/HL/RM_HDF5Optimized.html#H5DOread_chunk" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier for the dataset to be read.</param>
    /// <param name="dxpl_id">Transfer property list identifier for this I/O operation.</param>
    /// <param name="filters">Mask for identifying the filters used with the chunk.</param>
    /// <param name="offset">Logical position of the chunk's first element in the dataspace.</param>
    /// <param name="buf">Buffer containing the chunk read from the dataset.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.HighLevelApisDllFilename, EntryPoint = "H5Dread_chunk"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t read_chunk(hid_t dset_id, hid_t dxpl_id, in hsize_t[] offset, ref uint32_t filters, size_t buf);

    /// <summary>
    /// Refreshes all buffers associated with a dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Refresh" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to be refreshed.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Drefresh"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t refresh(hid_t dset_id);

    /// <summary>
    /// Scatters data into a selection within a memory buffer.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Scatter" /> for further reference.</para>
    /// </summary>
    /// <param name="op">Callback function which provides data to be
    /// scattered.</param>
    /// <param name="op_data">User-defined pointer to data required by op.</param>
    /// <param name="type_id">Identifier for the datatype describing the
    /// data in both the source and definition buffers. This is only used
    /// to calculate the element size.</param>
    /// <param name="dst_space_id">Identifier for the dataspace describing
    /// both the dimensions of the destination buffer and the selection
    /// within the destination buffer that data will be scattered to.</param>
    /// <param name="dst_buf">Destination buffer which the data will be
    /// scattered to.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dscatter"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t scatter
    (scatter_func_t op, size_t op_data, hid_t type_id,
     hid_t dst_space_id, size_t dst_buf);

    /// <summary>
    /// Changes the sizes of a dataset's dimensions.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-SetExtent" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Dataset identifier</param>
    /// <param name="size">Array containing the new magnitude of each
    /// dimension of the dataset.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dset_extent"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t set_extent(hid_t dset_id, hsize_t[] size);

    /// <summary>
    /// Changes the sizes of a dataset's dimensions.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-SetExtent" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Dataset identifier</param>
    /// <param name="size">Array containing the new magnitude of each
    /// dimension of the dataset.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dset_extent"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t set_extent(hid_t dset_id, hsize_t* size);

    /// <summary>
    /// Determines the number of bytes required to store variable-length
    /// (VL) data.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-VLGetBuf" /> for further reference.</para>
    /// </summary>
    /// <param name="dataset_id">Identifier of the dataset to query.</param>
    /// <param name="type_id">Datatype identifier.</param>
    /// <param name="space_id">Dataspace identifier.</param>
    /// <param name="size">The size in bytes of the memory buffer required
    /// to store the VL data.</param>
    /// <returns>Returns non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dvlen_get_buf_size"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t vlen_get_buf_size
        (hid_t dataset_id, hid_t type_id, hid_t space_id, ref hsize_t size);

    /// <summary>
    /// Reclaims variable-length (VL) datatype memory buffers.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-VLReclaim" /> for further reference.</para>
    /// </summary>
    /// <param name="type_id">Identifier of the datatype.</param>
    /// <param name="space_id">Identifier of the dataspace.</param>
    /// <param name="plist_id">Identifier of the property list used to create the buffer.</param>
    /// <param name="buf">Pointer to the buffer to be reclaimed.</param>
    /// <returns>Returns non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dvlen_reclaim"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t vlen_reclaim(hid_t type_id, hid_t space_id, hid_t plist_id, size_t buf);

    /// <summary>
    /// Writes raw data from a buffer to a dataset.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5D.html#Dataset-Write" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier of the dataset to write to.</param>
    /// <param name="mem_type_id">Identifier of the memory datatype.</param>
    /// <param name="mem_space_id">Identifier of the memory dataspace.</param>
    /// <param name="file_space_id">Identifier of the dataset's dataspace
    /// in the file.</param>
    /// <param name="plist_id">Identifier of a transfer property list for
    /// this I/O operation.</param>
    /// <param name="buf">Buffer with data to be written to the file.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Dwrite"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t write
    (hid_t dset_id, hid_t mem_type_id, hid_t mem_space_id,
     hid_t file_space_id, hid_t plist_id, size_t buf);


    /// <summary>
    /// Writes a raw data chunk from a buffer directly to a dataset.
    /// <para>See <see href="https://portal.hdfgroup.org/display/HDF5/H5D_WRITE_CHUNK" /> for further reference.</para>
    /// </summary>
    /// <param name="dset_id">Identifier for the dataset to write to.</param>
    /// <param name="dxpl_id">Transfer property list identifier for this I/O operation.</param>
    /// <param name="filters">Mask for identifying the filters in use.</param>
    /// <param name="offset">Logical position of the chunk's first element in the dataspace.</param>
    /// <param name="data_size">Size of the actual data to be written in bytes.</param>
    /// <param name="buf">Buffer containing data to be written to the chunk.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.HighLevelApisDllFilename, EntryPoint = "H5Dwrite_chunk"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t write_chunk(hid_t dset_id, hid_t dxpl_id, uint32_t filters, in hsize_t[] offset, size_t data_size, size_t buf);
}
