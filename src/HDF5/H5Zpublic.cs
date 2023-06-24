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
using System.Runtime.InteropServices.Marshalling;
using System.Security;

public sealed partial class H5Z
{
    static H5Z() { H5.open(); }

    /// <summary>
    /// Filter IDs
    /// </summary>
    public enum filter_t
    {
        /// <summary>
        /// no filter [value = -1]
        /// </summary>
        ERROR = -1,
        /// <summary>
        /// reserved indefinitely [value = 0]
        /// </summary>
        NONE = 0,
        /// <summary>
        /// deflation like gzip ]value = 1]
        /// </summary>
        DEFLATE = 1,
        /// <summary>
        /// shuffle the data [value = 2]
        /// </summary>
        SHUFFLE = 2,
        /// <summary>
        /// fletcher32 checksum of EDC [value = 3]
        /// </summary>
        FLETCHER32 = 3,
        /// <summary>
        /// szip compression [value = 4]
        /// </summary>
        SZIP = 4,
        /// <summary>
        /// nbit compression [value = 5]
        /// </summary>
        NBIT = 5,
        /// <summary>
        /// scale+offset compression [value = 6]
        /// </summary>
        SCALEOFFSET = 6,
        /// <summary>
        /// filter ids below this value are reserved for library use [value = 256]
        /// </summary>
        RESERVED = 256,
        /// <summary>
        /// maximum filter id [value = 65535]
        /// </summary>
        MAX = 65535
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
    /// Values to decide if EDC is enabled for reading data
    /// </summary>
    public enum EDC_t
    {
        /// <summary>
        /// error value
        /// </summary>
        ERROR = -1,
        DISABLE = 0,
        ENABLE = 1,
        NO = 2
    }

    public const uint CONFIG_ENCODE_ENABLED = 0x0001u;

    public const uint CONFIG_DECODE_ENABLED = 0x0002u;

    /// <summary>
    /// Return values for filter callback function
    /// </summary>
    public enum cb_return_t
    {
        ERROR = -1,
        /// <summary>
        /// I/O should fail if filter fails.
        /// </summary>
        FAIL = 0,
        /// <summary>
        /// I/O continues if filter fails.
        /// </summary>
        CONT = 1,
        NO = 2
    }

    /// <summary>
    /// Filter callback function definition
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="buf"></param>
    /// <param name="buf_size"></param>
    /// <param name="op_data"></param>
    /// <returns>Valid callback function return values are <see cref="cb_return_t.FAIL"/> and <see cref="cb_return_t.CONT"/>.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate cb_return_t filter_func_t(filter_t filter, size_t buf, size_t buf_size, size_t op_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate htri_t can_apply_func_t(hid_t dcpl_id, hid_t type_id, hid_t space_id);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t set_local_func_t(hid_t dcpl_id, hid_t type_id, hid_t space_id);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate size_t func_t(uint flags, size_t cd_nelmts, uint[] cd_values, size_t nbytes, ref size_t buf_size, ref size_t buf);


    [CustomMarshaller(typeof(class_t), MarshalMode.ManagedToUnmanagedIn, typeof(ClassTMarshaller))]
    private static unsafe class ClassTMarshaller
    {
        public struct ClassTUnmanaged
        {
            public int version;
            public ushort id;
            public uint encoder_present;
            public uint decoder_present;
            public byte* name;
            public nint can_apply;
            public nint set_local;
            public nint filter;
        }

        public static ClassTUnmanaged ConvertToUnmanaged(class_t managed)
        {
            return new ClassTUnmanaged
                   {
                       version = managed.version,
                       id = (ushort)managed.id,
                       encoder_present = managed.encoder_present ? 1U : 0U,
                       decoder_present = managed.decoder_present ? 1U : 0U,
                       name = AnsiStringMarshaller.ConvertToUnmanaged(managed.name),
                       can_apply = Marshal.GetFunctionPointerForDelegate(managed.can_apply),
                       set_local = Marshal.GetFunctionPointerForDelegate(managed.set_local),
                       filter = Marshal.GetFunctionPointerForDelegate(managed.filter),
                   };
        }

        public static void Free(ClassTUnmanaged unmanaged) => AnsiStringMarshaller.Free(unmanaged.name);
    }

    /// <summary>
    /// The filter table maps filter identification numbers to structs that
    /// contain a pointers to the filter function and timing statistics.
    /// </summary>
    [NativeMarshalling(typeof(ClassTMarshaller))]
    public struct class_t
    {
        /// <summary>
        /// Version number of the <c>class_t</c> struct
        /// </summary>
        public int version;
        /// <summary>
        /// Filter ID number
        /// </summary>
        public filter_t id;
        /// <summary>
        /// Does this filter have an encoder?
        /// </summary>
        public bool encoder_present;
        /// <summary>
        /// Does this filter have a decoder?
        /// </summary>
        public bool decoder_present;
        /// <summary>
        /// Comment for debugging
        /// </summary>
        public string name;
        /// <summary>
        /// The "can apply" callback for a filter
        /// </summary>
        public can_apply_func_t can_apply;
        /// <summary>
        /// The "set local" callback for a filter
        /// </summary>
        public set_local_func_t set_local;
        /// <summary>
        /// The actual filter function
        /// </summary>
        public func_t filter;
    }

    /// <summary>
    /// Determines whether a filter is available.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-FilterAvail" /> for further reference.</para>
    /// </summary>
    /// <param name="filter">Filter identifier.</param>
    /// <returns>Returns a Boolean value if successful;
    /// otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Zfilter_avail"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial htri_t filter_avail(filter_t filter);

    /// <summary>
    /// Retrieves information about a filter.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-GetFilterInfo" /> for further reference.</para>
    /// </summary>
    /// <param name="filter">Identifier of the filter to query.</param>
    /// <param name="filter_config">A bit field encoding the returned
    /// filter information</param>
    /// <returns>Returns a non-negative value on success, a negative value
    /// on failure.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Zget_filter_info"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t get_filter_info(filter_t filter, ref uint filter_config);

    /// <summary>
    /// Registers new filter.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-Register" /> for further reference.</para>
    /// </summary>
    /// <param name="filter_class">A pointer to a buffer for the struct
    /// containing filter-definition information.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Zregister"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t register(class_t filter_class);

    /// <summary>
    /// Unregisters a filter.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5Z.html#Compression-Unregister" /> for further reference.</para>
    /// </summary>
    /// <param name="filter">Identifier of the filter to be unregistered.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Zunregister"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
    public static partial herr_t unregister(filter_t filter);
}
