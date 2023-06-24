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
using hsize_t = System.UInt64;
using hssize_t = System.Int64;
using htri_t = System.Int32;
using size_t = nint;
using hid_t = System.Int64;

using System.Runtime.InteropServices;
using System.Security;

public sealed unsafe partial class H5S
{
    static H5S() { H5.open(); }

    // Define atomic datatypes
    public const int ALL = 0;
    public const hsize_t UNLIMITED = unchecked((hsize_t)(-1));

    /// <summary>
    /// Define user-level maximum number of dimensions
    /// </summary>
    public const int MAX_RANK = 32;

    /// <summary>
    /// Different types of dataspaces
    /// </summary>
    public enum class_t
    {
        /// <summary>
        /// error [value = -1].
        /// </summary>
        NO_CLASS = -1,
        /// <summary>
        /// scalar variable [value = 0].
        /// </summary>
        SCALAR = 0,
        /// <summary>
        /// simple data space [value = 1].
        /// </summary>
        SIMPLE = 1,
        /// <summary>
        /// null data space [value = 2].
        /// </summary>
        <c>NULL</c> = 2
    }

    /// <summary>
    /// Different ways of combining selections
    /// </summary>
    public enum seloper_t
    {
        /// <summary>
        /// error
        /// </summary>
        NOOP = -1,
        /// <summary>
        /// Select "set" operation
        /// </summary>
        SET = 0,
        /// <summary>
        /// Binary "or" operation for hyperslabs
        /// (add new selection to existing selection)
        /// Original region:  AAAAAAAAAA
        /// New region:             BBBBBBBBBB
        /// A or B:           CCCCCCCCCCCCCCCC
        /// </summary>
        OR,
        /// <summary>
        /// Binary "and" operation for hyperslabs
        /// (only leave overlapped regions in selection)
        /// Original region:  AAAAAAAAAA
        /// New region:             BBBBBBBBBB
        /// A and B:                CCCC
        /// </summary>
        AND,
        /// <summary>
        /// Binary "xor" operation for hyperslabs
        /// (only leave non-overlapped regions in selection)
        /// Original region:  AAAAAAAAAA
        /// New region:             BBBBBBBBBB
        /// A xor B:          CCCCCC    CCCCCC
        /// </summary>
        XOR,
        /// <summary>
        /// Binary "not" operation for hyperslabs
        /// (only leave non-overlapped regions in original selection)
        /// Original region:  AAAAAAAAAA
        /// New region:             BBBBBBBBBB
        /// A not B:          CCCCCC
        /// </summary>
        NOTB,
        /// <summary>
        /// Binary "not" operation for hyperslabs
        /// (only leave non-overlapped regions in new selection)
        /// Original region:  AAAAAAAAAA
        /// New region:             BBBBBBBBBB
        /// B not A:                    CCCCCC
        /// </summary>
        NOTA,
        /// <summary>
        /// Append elements to end of point selection
        /// </summary>
        APPEND,
        /// <summary>
        /// Prepend elements to beginning of point selection
        /// </summary>
        PREPEND,
        /// <summary>
        /// Invalid upper bound on selection operations
        /// </summary>
        INVALID
    }

    /// <summary>
    /// Enumerated type for the type of selection
    /// </summary>
    public enum sel_type
    {
        /// <summary>
        /// Error
        /// </summary>
        ERROR = -1,
        /// <summary>
        /// Nothing selected
        /// </summary>
        NONE = 0,
        /// <summary>
        /// Sequence of points selected
        /// </summary>
        POINTS = 1,
        /// <summary>
        /// "New-style" hyperslab selection defined
        /// </summary>
        HYPERSLABS = 2,
        /// <summary>
        /// Entire extent selected
        /// </summary>
        ALL = 3,
        N
    }

    /// <summary>
    /// Releases and terminates access to a dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-Close" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of dataspace to release.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sclose"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t close(hid_t space_id);

    /// <summary>
    /// Creates an exact copy of a dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-Copy" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of dataspace to copy.</param>
    /// <returns>Returns a dataspace identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Scopy"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t copy(hid_t space_id);

    /// <summary>
    /// Creates a new dataspace of a specified type.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-Create" /> for further reference.</para>
    /// </summary>
    /// <param name="type">Type of dataspace to be created.</param>
    /// <returns>Returns a dataspace identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Screate"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create(class_t type);

    /// <summary>
    /// Creates a new simple dataspace and opens it for access.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-CreateSimple" /> for further reference.</para>
    /// </summary>
    /// <param name="rank">Number of dimensions of dataspace.</param>
    /// <param name="dims">Array specifying the size of each dimension.</param>
    /// <param name="maxdims">Array specifying the maximum size of each
    /// dimension.</param>
    /// <returns>Returns a dataspace identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Screate_simple"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create_simple
    (int rank,
     [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] hsize_t[] dims,
     [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] hsize_t[] maxdims);

    /// <summary>
    /// Creates a new simple dataspace and opens it for access.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-CreateSimple" /> for further reference.</para>
    /// </summary>
    /// <param name="rank">Number of dimensions of dataspace.</param>
    /// <param name="dims">Array specifying the size of each dimension.</param>
    /// <param name="maxdims">Array specifying the maximum size of each
    /// dimension.</param>
    /// <returns>Returns a dataspace identifier if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Screate_simple"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create_simple(int rank, hsize_t* dims, hsize_t* maxdims);

    /// <summary>
    /// Decode a binary object description of data space and return a new
    /// object handle.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-Decode" /> for further reference.</para>
    /// </summary>
    /// <param name="buf">Buffer for the data space object to be decoded.</param>
    /// <returns>Returns an object ID(non-negative) if successful;
    /// otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sdecode"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t decode(byte[] buf);

    /// <summary>
    /// Encode a data space object description into a binary buffer.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-Encode" /> for further reference.</para>
    /// </summary>
    /// <param name="obj_id">Identifier of the object to be encoded.</param>
    /// <param name="buf">Buffer for the object to be encoded into. If the
    /// provided buffer is <c>NULL</c>, only the size of buffer
    /// needed is returned through <paramref name="nalloc"/>.</param>
    /// <param name="nalloc">The size of the allocated buffer or the size
    /// of the buffer needed.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sencode",
               CallingConvention = CallingConvention.Cdecl),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern herr_t encode
    (hid_t obj_id,
     [MarshalAs(UnmanagedType.LPArray)][In, Out] byte[] buf,
     ref size_t nalloc);

    /// <summary>
    /// Copies the extent of a dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-ExtentCopy" /> for further reference.</para>
    /// </summary>
    /// <param name="dest_space_id">The identifier for the dataspace to
    /// which the extent is copied.</param>
    /// <param name="source_space_id">The identifier for the dataspace from
    /// which the extent is copied.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sextent_copy"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t extent_copy
        (hid_t dest_space_id, hid_t source_space_id);

    /// <summary>
    /// Determines whether two dataspace extents are equal.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-ExtentEqual" /> for further reference.</para>
    /// </summary>
    /// <param name="space1_id">First dataspace identifier.</param>
    /// <param name="space2_id">Second dataspace identifier.</param>
    /// <returns>Returns 1 if equal, 0 if unequal, if successful;
    /// otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sextent_equal"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial htri_t extent_equal
        (hid_t space1_id, hid_t space2_id);

    /// <summary>
    /// Retrieves a regular hyperslab selection.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-GetRegularHyperslab" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">The identifier of the dataspace.</param>
    /// <param name="start">Offset of the start of the regular hyperslab.</param>
    /// <param name="stride">Stride of the regular hyperslab.</param>
    /// <param name="count">Number of blocks in the regular hyperslab.</param>
    /// <param name="block">Size of a block in the regular hyperslab.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    /// <remarks>If a hyperslab selection is originally regular, then
    /// becomes irregular through selection operations, and then becomes
    /// regular again, the final regular selection may be equivalent but
    /// not identical to the original regular selection.</remarks>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_regular_hyperslab"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t H5Sget_regular_hyperslab
        (hid_t space_id, hsize_t[] start, hsize_t[] stride,
        hsize_t[] count, hsize_t[] block);

    /// <summary>
    /// Gets the bounding box containing the current selection.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectBounds" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of dataspace to query.</param>
    /// <param name="start">Starting coordinates of the bounding box.</param>
    /// <param name="end">Ending coordinates of the bounding box, i.e.,
    /// the coordinates of the diagonally opposite corner.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    /// <remarks>The <c>start</c> and <c>end</c> buffers must
    /// be large enough to hold the dataspace rank number of coordinates.</remarks>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_select_bounds",
               CallingConvention = CallingConvention.Cdecl),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern herr_t get_select_bounds
        (hid_t space_id, [In][Out] hsize_t[] start, [In][Out] hsize_t[] end);

    /// <summary>
    /// Gets the number of points in the current point selection.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectElemNPoints" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of dataspace to query.</param>
    /// <returns>Returns the number of points in the current dataspace
    /// point selection if successful. Otherwise returns a negative
    /// value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_select_elem_npoints"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hssize_t get_select_elem_npoints(hid_t space_id);

    /// <summary>
    /// Gets the list of points in a point selection.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectElemPointList" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Dataspace identifier of selection to query.</param>
    /// <param name="startpoint">Element point to start with.</param>
    /// <param name="numpoints">Number of element points to get.</param>
    /// <param name="buf">List of element points selected.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [DllImport(Constants.MainLibraryDllFilename,
               EntryPoint = "H5Sget_select_elem_pointlist",
               CallingConvention = CallingConvention.Cdecl),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern herr_t get_select_elem_pointlist(hid_t space_id, hsize_t startpoint, hsize_t numpoints, [In][Out] hsize_t[] buf);

    /// <summary>
    /// Gets the list of hyperslab blocks in a hyperslab selection.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectHyperBlockList" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Dataspace identifier of selection to query.</param>
    /// <param name="startblock">Hyperslab block to start with.</param>
    /// <param name="numblocks">Number of hyperslab blocks to get.</param>
    /// <param name="buf">List of hyperslab blocks selected.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_select_hyper_blocklist", CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern herr_t get_select_hyper_blocklist(hid_t space_id, hsize_t startblock, hsize_t numblocks, [In][Out] hsize_t[] buf);

    /// <summary>
    /// Get number of hyperslab blocks in a hyperslab selection.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectHyperNBlocks" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of dataspace to query.</param>
    /// <returns>Returns the number of hyperslab blocks in a hyperslab
    /// selection if successful. Otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_select_hyper_nblocks"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hssize_t get_select_hyper_nblocks(hid_t space_id);

    /// <summary>
    /// Determines the number of elements in a dataspace selection.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectNpoints" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Dataspace identifier.</param>
    /// <returns>Returns the number of elements in the selection if
    /// successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_select_npoints"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hssize_t get_select_npoints(hid_t space_id);

    /// <summary>
    /// Determines the type of the dataspace selection.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-GetSelectType" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Dataspace identifier.</param>
    /// <returns>Returns the dataspace selection type, a value of the
    /// enumerated datatype <c>H5S.sel_type</c>, if successful.
    /// Otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_select_type"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial sel_type get_select_type(hid_t space_id);

    /// <summary>
    /// Retrieves dataspace dimension size and maximum size.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-ExtentDims" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of the dataspace object to query</param>
    /// <param name="dims">Pointer to array to store the size of each dimension.</param>
    /// <param name="maxdims">Pointer to array to store the maximum size of each dimension.</param>
    /// <returns>Returns the number of dimensions in the dataspace if
    /// successful; otherwise returns a negative value.</returns>
    /// <remarks>Either or both of <paramref name="dims"/> and
    /// <paramref name="maxdims"/> may be <c>NULL</c>.</remarks>
    [DllImport(Constants.MainLibraryDllFilename,
               EntryPoint = "H5Sget_simple_extent_dims",
               CallingConvention = CallingConvention.Cdecl),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern int get_simple_extent_dims
        (hid_t space_id, [In][Out] hsize_t[] dims, [In][Out] hsize_t[] maxdims);


    /// <summary>
    /// Retrieves dataspace dimension size and maximum size.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-ExtentDims" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of the dataspace object to query</param>
    /// <param name="dims">Pointer to array to store the size of each dimension.</param>
    /// <param name="maxdims">Pointer to array to store the maximum size of each dimension.</param>
    /// <returns>Returns the number of dimensions in the dataspace if
    /// successful; otherwise returns a negative value.</returns>
    /// <remarks>Either or both of <paramref name="dims"/> and
    /// <paramref name="maxdims"/> may be <c>NULL</c>.</remarks>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_simple_extent_dims"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial int get_simple_extent_dims(
        hid_t space_id, hsize_t* dims, hsize_t* maxdims);

    /// <summary>
    /// Determines the dimensionality of a dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-ExtentNdims" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of the dataspace</param>
    /// <returns>Returns the number of dimensions in the dataspace if
    /// successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_simple_extent_ndims"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial int get_simple_extent_ndims(hid_t space_id);

    /// <summary>
    /// Determines the number of elements in a dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-ExtentNpoints" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of the dataspace object to query</param>
    /// <returns>Returns the number of elements in the dataspace if
    /// successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_simple_extent_npoints"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hssize_t get_simple_extent_npoints(hid_t space_id);

    /// <summary>
    /// Determines the current class of a dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-ExtentType" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Dataspace identifier.</param>
    /// <returns>Returns a dataspace class name if successful; otherwise
    /// <c>H5S.class_t.NO_CLASS</c>.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sget_simple_extent_type"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial class_t get_simple_extent_type(hid_t space_id);

    /// <summary>
    /// Determines whether a hyperslab selection is regular.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-IsRegularHyperslab" /> for further reference.</para>
    /// </summary>
    /// <param name="spaceid">The identifier of the dataspace.</param>
    /// <returns>Returns <c>TRUE</c> or <c>FALSE</c> for
    /// hyperslab selection if successful. Returns <c>FAIL</c>on
    /// error or when querying other selection types such as point
    /// selection.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sis_regular_hyperslab"),
    SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial htri_t is_regular_hyperslab(hid_t spaceid);

    /// <summary>
    /// Determines whether a dataspace is a simple dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-IsSimple" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of the dataspace to query</param>
    /// <returns>When successful, returns a positive value, for
    /// <c>TRUE</c>, or 0 (zero), for <c>FALSE</c>. Otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sis_simple"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial htri_t is_simple(hid_t space_id);

    /// <summary>
    /// Sets the offset of a simple dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-OffsetSimple" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">The identifier for the dataspace object to
    /// reset.</param>
    /// <param name="offset">The offset at which to position the selection.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    /// <remarks>The offset array must be the same number of elements as
    /// the number of dimensions for the dataspace. If the offset array is
    /// set to <c>NULL</c>, the offset for the dataspace is reset
    /// to 0.</remarks>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Soffset_simple"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t offset_simple
    (hid_t space_id,
     [MarshalAs(UnmanagedType.LPArray)] hssize_t[] offset);

    /// <summary>
    /// Selects an entire dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectAll" /> for further reference.</para>
    /// </summary>
    /// <param name="dspace_id">The identifier for the dataspace for which
    /// the selection is being made.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sselect_all"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t select_all(hid_t dspace_id);

    /// <summary>
    /// Selects array elements to be included in the selection for a
    /// dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectElements" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of the dataspace.</param>
    /// <param name="op">Operator specifying how the new selection is to be
    /// combined with the existing selection for the dataspace.</param>
    /// <param name="num_elements">Number of elements to be selected.</param>
    /// <param name="coord">A pointer to a buffer containing a serialized
    /// copy of a 2-dimensional array of zero-based values specifying the
    /// coordinates of the elements in the point selection.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sselect_elements"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t select_elements
    (hid_t space_id, seloper_t op, size_t num_elements,
     [MarshalAs(UnmanagedType.LPArray)] hsize_t[] coord);

    /// <summary>
    /// Selects a hyperslab region to add to the current selected region.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectHyperslab" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of dataspace selection to modify</param>
    /// <param name="op">Operation to perform on current selection.</param>
    /// <param name="start">Offset of start of hyperslab</param>
    /// <param name="stride">Number of blocks included in hyperslab.</param>
    /// <param name="count">Hyperslab stride.</param>
    /// <param name="block">Size of block in hyperslab.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sselect_hyperslab"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t select_hyperslab
    (hid_t space_id, seloper_t op,
     [MarshalAs(UnmanagedType.LPArray)] hsize_t[] start,
     [MarshalAs(UnmanagedType.LPArray)] hsize_t[] stride,
     [MarshalAs(UnmanagedType.LPArray)] hsize_t[] count,
     [MarshalAs(UnmanagedType.LPArray)] hsize_t[] block);

    /// <summary>
    /// Selects a hyperslab region to add to the current selected region.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectHyperslab" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier of dataspace selection to modify</param>
    /// <param name="op">Operation to perform on current selection.</param>
    /// <param name="start">Offset of start of hyperslab</param>
    /// <param name="stride">Number of blocks included in hyperslab.</param>
    /// <param name="count">Hyperslab stride.</param>
    /// <param name="block">Size of block in hyperslab.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sselect_hyperslab"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t select_hyperslab(hid_t space_id, seloper_t op, hsize_t* start, hsize_t* stride, hsize_t* count, hsize_t* block);

    /// <summary>
    /// Resets the selection region to include no elements.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectNone" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">The identifier for the dataspace in which
    /// the selection is being reset.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sselect_none"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t select_none(hid_t space_id);

    /// <summary>
    /// Verifies that the selection is within the extent of the dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SelectValid" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Identifier for the dataspace being queried.</param>
    /// <returns>Returns a positive value, for <c>TRUE</c>, if the
    /// selection is contained within the extent or 0 (zero), for
    /// <c>FALSE</c>, if it is not. Returns a negative value on error
    /// conditions such as the selection or extent not being defined.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sselect_valid"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial htri_t select_valid(hid_t space_id);

    /// <summary>
    /// Removes the extent from a dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SetExtentNone" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">The identifier for the dataspace from which
    /// the extent is to be removed.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sset_extent_none"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t set_extent_none(hid_t space_id);

    /// <summary>
    /// Sets or resets the size of an existing dataspace.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5S.html#Dataspace-SetExtentSimple" /> for further reference.</para>
    /// </summary>
    /// <param name="space_id">Dataspace identifier.</param>
    /// <param name="rank">Rank, or dimensionality, of the dataspace.</param>
    /// <param name="current_size">Array containing current size of
    /// dataspace.</param>
    /// <param name="maximum_size">Array containing maximum size of
    /// dataspace.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Sset_extent_simple"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t set_extent_simple
    (hid_t space_id, int rank,
     [MarshalAs(UnmanagedType.LPArray)] hsize_t[] current_size,
     [MarshalAs(UnmanagedType.LPArray)] hsize_t[] maximum_size);
}
