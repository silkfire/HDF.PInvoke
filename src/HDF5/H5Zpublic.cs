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
using htri_t = System.Int32;
using size_t = nint;
using hid_t = System.Int64;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

public sealed partial class H5Z
{
    static H5Z() { H5.open(); }

    /// <summary>
    /// Filter identifiers.
    /// </summary>
    public enum filter_t
    {
        /// <summary>
        /// No filter = -1
        /// </summary>
        ERROR = -1,

        /// <summary>
        /// Reserved indefinitely = 0
        /// </summary>
        NONE = 0,

        /// <summary>
        /// The gzip compression, or deflation, filter = 1
        /// </summary>
        DEFLATE = 1,

        /// <summary>
        /// The shuffle algorithm filter = 2
        /// </summary>
        SHUFFLE = 2,

        /// <summary>
        /// The Fletcher32 checksum, or error checking, filter = 3
        /// </summary>
        FLETCHER32 = 3,

        /// <summary>
        /// The SZIP compression filter = 4
        /// </summary>
        SZIP = 4,

        /// <summary>
        /// The N-bit compression filter = 5
        /// </summary>
        NBIT = 5,

        /// <summary>
        /// The scale-offset compression filter = 6
        /// </summary>
        SCALEOFFSET = 6,

        /// <summary>
        /// Filter IDs below this value are reserved for library use = 256
        /// </summary>
        RESERVED = 256,

        /// <summary>
        /// Maximum filter ID = 65535
        /// </summary>
        MAX = 65_535
    }

    /// <summary>
    /// Special parameters for ScaleOffset filter
    /// </summary>
    public enum SO_scale_type_t
    {
        FLOAT_DSCALE = 0,
        FLOAT_ESCALE = 1,
        INT = 2
    }

    /// <summary>
    /// Values to decide if EDC (error is enabled for reading data.
    /// </summary>
    public enum EDC_t
    {
        /// <summary>
        /// Error = -1
        /// </summary>
        ERROR = -1,

        /// <summary>
        /// Disabled = 0
        /// </summary>
        DISABLE = 0,

        /// <summary>
        /// Enabled = 1
        /// </summary>
        ENABLE = 1,

        /// <summary>
        /// No = 2
        /// </summary>
        NO = 2
    }

    public const uint CONFIG_ENCODE_ENABLED = 0x0001u;

    public const uint CONFIG_DECODE_ENABLED = 0x0002u;

    /// <summary>
    /// Return values for filter callback function used in <see cref="H5P.set_filter_callback"/>
    /// </summary>
    public enum cb_return_t
    {
        /// <summary>
        /// Error = -1
        /// </summary>
        ERROR = -1,

        /// <summary>
        /// I/O should fail if filter fails = 0
        /// </summary>
        FAIL = 0,

        /// <summary>
        /// I/O continues if filter fails = 1
        /// </summary>
        CONT = 1,

        /// <summary>
        /// No = 2
        /// </summary>
        NO = 2
    }

    /// <summary>
    /// Filter callback function definition.
    /// </summary>
    /// <param name="filter">Indicates which filter has failed.</param>
    /// <param name="buf">Buffer for failed data.</param>
    /// <param name="buf_size">Size of buffer.</param>
    /// <param name="op_data">Required input data for this callback function.</param>
    /// <returns>Valid callback function return values are <see cref="cb_return_t.FAIL"/> and <see cref="cb_return_t.CONT"/>.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate cb_return_t filter_func_t(filter_t filter, size_t buf, size_t buf_size, size_t op_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate htri_t can_apply_func_t(hid_t dcpl_id, hid_t type_id, hid_t space_id);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t set_local_func_t(hid_t dcpl_id, hid_t type_id, hid_t space_id);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate size_t func_t(uint flags, size_t cd_nelmts, uint[] cd_values, size_t nbytes, ref size_t buf_size, ref size_t buf);


    /// <summary>
    /// The filter table maps filter identification numbers to structs that contain a pointers to the filter function and timing statistics.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct class_t
    {
        /// <summary>
        /// Library-defined value reporting the version number of the <see cref="class_t"/> struct. This currently must be set to <c>H5Z_CLASS_T_VERS</c>.
        /// </summary>
        public int version;

        /// <summary>
        /// The identifier for the new filter. This is a user-defined value between <see cref="filter_t.RESERVED"/> and <see cref="filter_t.MAX"/>.
        /// </summary>
        public filter_t id;

        /// <summary>
        /// Library-defined value indicating whether the filter’s encoding capability is available to the application.
        /// </summary>
        public uint encoder_present;

        /// <summary>
        /// Library-defined value indicating whether the filter’s decoding capability is available to the application.
        /// </summary>
        public uint decoder_present;

        /// <summary>
        /// A descriptive comment used for debugging, may contain a descriptive name for the filter, and may be the null pointer.
        /// </summary>
        public nint name;

        /// <summary>
        /// A user-defined callback function which determines whether the combination of the dataset creation property list values, the datatype, and the dataspace represent a valid combination to apply this filter to.
        /// </summary>
        public can_apply_func_t can_apply;

        /// <summary>
        /// A user-defined callback function which sets any parameters that are specific to this dataset, based on the combination of the dataset creation property list values, the datatype, and the dataspace.
        /// </summary>
        public set_local_func_t set_local;

        /// <summary>
        /// A user-defined callback function which performs the action of the filter.
        /// </summary>
        public func_t filter;
    }

    /// <summary>
    /// Determines whether a filter is available.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-FilterAvail" /> for further reference.</para>
    /// </summary>
    /// <param name="filter">Filter identifier.</param>
    /// <returns>Returns a Boolean value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Zfilter_avail"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial htri_t filter_avail(filter_t filter);

    /// <summary>
    /// Retrieves information about a filter.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-GetFilterInfo" /> for further reference.</para>
    /// </summary>
    /// <param name="filter">Identifier of the filter to query.</param>
    /// <param name="filter_config">A bit field encoding the returned filter information.</param>
    /// <returns>Returns a non-negative value on success, a negative value on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Zget_filter_info"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t get_filter_info(filter_t filter, ref uint filter_config);

    /// <summary>
    /// Registers a new filter with the HDF5 library.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-Register" /> for further reference.</para>
    /// </summary>
    /// <param name="filter_class">A struct containing filter-definition information.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    public static herr_t register(class_t filter_class)
    {
        var filterClassPtr = Marshal.AllocHGlobal(Marshal.SizeOf(filter_class));
        Marshal.StructureToPtr(filter_class, filterClassPtr, false);
        var @return = register(filterClassPtr);
        Marshal.FreeHGlobal(filterClassPtr);

        return @return;
    }

    /// <summary>
    /// Registers a new filter with the HDF5 library.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-Register" /> for further reference.</para>
    /// </summary>
    /// <param name="filter_class">A pointer to a buffer for the struct containing filter-definition information.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Zregister"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t register(nint filter_class);

    /// <summary>
    /// Unregisters a filter.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-Unregister" /> for further reference.</para>
    /// </summary>
    /// <param name="filter">Identifier of the filter to be unregistered.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Zunregister"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t unregister(filter_t filter);
}
