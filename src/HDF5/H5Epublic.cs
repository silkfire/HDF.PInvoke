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

using herr_t = System.Int32;
using size_t = nint;
using ssize_t = nint;
using hid_t = System.Int64;

using System.Runtime.InteropServices;
using System.Security;
using System.Text;

public sealed partial class H5E
{
    static H5E()
    {
        H5.open();
    }

    /// <summary>
    /// Value for the default error stack
    /// </summary>
    public const hid_t DEFAULT = 0;

    /// <summary>
    /// Different kinds of error information
    /// </summary>
    public enum type_t
    {
        MAJOR,
        MINOR
    }

    /// <summary>
    /// Information about an error; element of error stack
    /// </summary>

    public struct error_t
    {
        /// <summary>
        /// class ID
        /// </summary>
        public hid_t cls_id;

        /// <summary>
        /// major error ID
        /// </summary>
        public hid_t maj_num;

        /// <summary>
        /// minor error ID
        /// </summary>
        public hid_t min_num;

        /// <summary>
        /// line in file where error occurs
        /// </summary>
        public uint line;

        /// <summary>
        /// function in which error occurred
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string func_name;

        /// <summary>
        /// file in which error occurred
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string file_name;

        /// <summary>
        /// optional supplied description
        /// </summary>
        [MarshalAs(UnmanagedType.LPStr)]
        public string desc;
    }

    /// <summary>
    /// Callback for error handling.
    /// </summary>
    /// <param name="estack">Error stack identifier</param>
    /// <param name="client_data">Pointer to client data in the format
    /// expected by the user-defined function.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t auto_t
    (
        hid_t estack,
        ssize_t client_data
    );

    /// <summary>
    /// Callback for H5E.walk
    /// </summary>
    /// <param name="n">Indexed position of the error in the stack.</param>
    /// <param name="err_desc">Reference to a data structure describing the
    /// error.</param>
    /// <param name="client_data">Pointer to client data in the format
    /// expected by the user-defined function.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate herr_t walk_t
    (
        uint n,
        [In] ref error_t err_desc,
        ssize_t client_data
    );

    /// <summary>
    /// Error stack traversal direction
    /// </summary>
    public enum direction_t
    {
        /// <summary>
        /// begin deep, end at API function [value = 0]
        /// </summary>
        H5E_WALK_UPWARD = 0,
        /// <summary>
        /// begin at API function, end deep [value = 1]
        /// </summary>
        H5E_WALK_DOWNWARD = 1
    }

    /// <summary>
    /// Determines type of error stack.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-AutoIsV2" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">The error stack identifier</param>
    /// <param name="is_stack">A flag indicating which error stack typedef
    /// the specified error stack conforms to.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eauto_is_v2"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t auto_is_v2
        (hid_t estack_id, ref uint is_stack);

    /// <summary>
    /// Clears the specified error stack or the error stack for the current
    /// thread.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Clear2" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Error stack identifier.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eclear2"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t clear(hid_t estack_id);

    /// <summary>
    /// Closes an error message identifier.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-CloseMsg" /> for further reference.</para>
    /// </summary>
    /// <param name="msg_id">Error message identifier.</param>
    /// <returns>Returns a non-negative value on success; otherwise returns
    /// a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eclose_msg"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t close_msg(hid_t msg_id);

    /// <summary>
    /// Closes object handle for error stack.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-CloseStack" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Error stack identifier.</param>
    /// <returns>Returns a non-negative value on success; otherwise returns
    /// a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eclose_stack"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t close_stack(hid_t estack_id);

    /// <summary>
    /// Add major error message to an error class.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-CreateMsg" /> for further reference.</para>
    /// </summary>
    /// <param name="cls">Error class identifier.</param>
    /// <param name="msg_type">The type of the error message.</param>
    /// <param name="msg">Major error message.</param>
    /// <returns>Returns a message identifier on success; otherwise returns
    /// a negative value.</returns>
    /// <remarks>ASCII strings ONLY.</remarks>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Ecreate_msg", StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller)),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create_msg
    (hid_t cls, type_t msg_type,
     [MarshalAs(UnmanagedType.LPStr)] string msg);

    /// <summary>
    /// Creates a new empty error stack.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-CreateStack" /> for further reference.</para>
    /// </summary>
    /// <returns>Returns an error stack identifier on success; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Ecreate_stack"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t create_stack();

    /// <summary>
    /// Returns the settings for the automatic error stack traversal function and its data.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetAuto2" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Error stack identifier. <see cref="DEFAULT"/> indicates the current stack.</param>
    /// <param name="func">The function currently set to be called upon an error condition.</param>
    /// <param name="client_data">Data currently set to be passed to the error function.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eget_auto2"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical] [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t get_auto(hid_t estack_id, ref auto_t func, ref ssize_t client_data);

    /// <summary>
    /// Retrieves error class name.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetClassName" /> for further reference.</para>
    /// </summary>
    /// <param name="class_id">Error class identifier.</param>
    /// <param name="name">The name of the class to be queried.</param>
    /// <param name="size">The length of class name to be returned by
    /// this function.</param>
    /// <returns>Returns non-negative value as on success; otherwise
    /// returns negative value.</returns>
    /// <remarks>ASCII strings ONLY!</remarks>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eget_class_name",
               CharSet = CharSet.Ansi,
               CallingConvention = CallingConvention.Cdecl),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern ssize_t get_class_name(
        hid_t class_id, [In][Out] StringBuilder name, size_t size);

    /// <summary>
    /// Returns copy of current error stack.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetCurrentStack" /> for further reference.</para>
    /// </summary>
    /// <returns>Returns an error stack identifier on success; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eget_current_stack"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t get_current_stack();

    /// <summary>
    /// Retrieves an error message.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetMsg" /> for further reference.</para>
    /// </summary>
    /// <param name="msg_id">Idenfier for error message to be queried.</param>
    /// <param name="msg_type">The type of the error message.</param>
    /// <param name="msg">Error message buffer.</param>
    /// <param name="size">The length of error message to be returned by
    /// this function.</param>
    /// <returns>Returns the size of the error message in bytes on success;
    /// otherwise returns a negative value.</returns>
    [DllImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eget_msg",
               CallingConvention = CallingConvention.Cdecl),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    public static extern ssize_t get_msg(
        hid_t msg_id, ref type_t msg_type, [In][Out] StringBuilder msg, size_t size);

    /// <summary>
    /// Retrieves the number of error messages in an error stack.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-GetNum" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Error stack identifier.</param>
    /// <returns>Returns a non-negative value on success; otherwise returns
    /// a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eget_num"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial ssize_t get_num(hid_t estack_id);

    /// <summary>
    /// Deletes specified number of error messages from the error stack.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Pop" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Error stack identifier.</param>
    /// <param name="count">The number of error messages to be deleted from the top of error stack.</param>
    /// <returns>Returns a non-negative value on success; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Epop"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t pop(hid_t estack_id, size_t count);

    /// <summary>
    /// Prints the specified error stack in a default manner.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Print2" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Identifier of the error stack to be printed.</param>
    /// <param name="stream">File pointer, or stderr if <c>NULL</c>.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eprint2"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t print
        (hid_t estack_id, ssize_t stream);

    /// <summary>
    /// Pushes new error record onto error stack.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Push2" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Identifier of the error stack to which the
    /// error record is to be pushed. If the identifier is
    /// <c>H5E.DEFAULT</c> , the error record will be pushed to the
    /// current stack.</param>
    /// <param name="file">Name of the file in which the error was
    /// detected.</param>
    /// <param name="func">Name of the function in which the error was
    /// detected.</param>
    /// <param name="line">Line number within the file at which the error
    /// was detected.</param>
    /// <param name="class_id">Error class identifier.</param>
    /// <param name="major_id">Major error identifier.</param>
    /// <param name="minor_id">Minor error identifier.</param>
    /// <param name="msg">Error description string.</param>
    /// <returns>Returns a non-negative value if successful; otherwise
    /// returns a negative value.</returns>
    /// <remarks>ASCII strings ONLY!</remarks>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Epush2", StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller)),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t push
    (hid_t estack_id, string file, string func, uint line,
     hid_t class_id, hid_t major_id, hid_t minor_id, string msg);

    /// <summary>
    /// Registers a client library or application program to the HDF5 error
    /// API.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-RegisterClass" /> for further reference.</para>
    /// </summary>
    /// <param name="cls_name">Name of the error class.</param>
    /// <param name="lib_name">Name of the client library or application to
    /// which the error class belongs.</param>
    /// <param name="version">Version of the client library or application
    /// to which the error class belongs. A <c>NULL</c> can be passed in.</param>
    /// <returns>Returns a class identifier on success; otherwise returns a
    /// negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eregister_class"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial hid_t register_class
    ([MarshalAs(UnmanagedType.LPStr)] string cls_name,
     [MarshalAs(UnmanagedType.LPStr)] string lib_name,
     [MarshalAs(UnmanagedType.LPStr)] string version);

    /// <summary>
    /// Turns automatic error printing on or off.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-SetAuto2" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Error stack identifier.</param>
    /// <param name="func">Function to be called upon an error condition.</param>
    /// <param name="client_data">Data passed to the error function.</param>
    /// <returns>Returns a non-negative value on success; otherwise returns
    /// a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eset_auto2"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t set_auto
        (hid_t estack_id, auto_t func, ssize_t client_data);

    /// <summary>
    /// Replaces the current error stack.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-SetCurrentStack" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Error stack identifier.</param>
    /// <returns>Returns a non-negative value on success; otherwise returns
    /// a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eset_current_stack"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t set_current_stack(hid_t estack_id);

    /// <summary>
    /// Removes an error class.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-UnregisterClass" /> for further reference.</para>
    /// </summary>
    /// <param name="class_id">Error class identifier.</param>
    /// <returns>Returns a non-negative value on success; otherwise returns
    /// a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Eunregister_class"),
     SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t unregister_class(hid_t class_id);

    /// <summary>
    /// Walks the specified error stack, calling the specified function.
    /// <para>See <see href="https://support.hdfgroup.org/HDF5/doc/RM/RM_H5E.html#Error-Walk2" /> for further reference.</para>
    /// </summary>
    /// <param name="estack_id">Error stack identifier.</param>
    /// <param name="direction">Direction in which the error stack is to be walked.</param>
    /// <param name="func">Function to be called for each error encountered.</param>
    /// <param name="client_data">Data to be passed with <paramref name="func"/>.</param>
    /// <returns>Returns a non-negative value if successful; otherwise returns a negative value.</returns>
    [LibraryImport(Constants.MainLibraryDllFilename, EntryPoint = "H5Ewalk2"), SuppressUnmanagedCodeSecurity, SecuritySafeCritical]
    [UnmanagedCallConv(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static partial herr_t walk(hid_t estack_id, direction_t direction, walk_t func, ssize_t client_data);
}
