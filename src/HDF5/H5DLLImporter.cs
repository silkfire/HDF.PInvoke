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

using hid_t = System.Int64;

using System;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// Helper class used to fetch public variables exported by the native HDF5 library.
/// </summary>
internal abstract class H5DLLImporter
{
    public static readonly H5DLLImporter Instance;

    static H5DLLImporter()
    {
        _ = H5.open();

        // TODO: -----

        //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        //    Instance = new H5LinuxDllImporter(Constants.MainLibraryDllFilename);
        //else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        //    Instance = new H5MacDllImporter(Constants.MainLibraryDllFilename);
        //else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        //    Instance = new H5WindowsDLLImporter(Constants.MainLibraryDllFilename);
        //else
        //    throw new PlatformNotSupportedException();
    }

    protected abstract nint _GetAddress(string varName);

    public nint GetAddress(string exportName)
    {
        var address = _GetAddress(exportName);
        if (address == nint.Zero) throw new ArgumentException($"The export with name '{exportName}' doesn't exist.");

        return address;
    }

    public bool GetAddress(string exportName, out nint address)
    {
        address = _GetAddress(exportName);
        return address == nint.Zero;
    }

    /*public bool GetValue<T>(
        string          varName,
        ref T           value,
        Func<nint, T> converter
        )
    {
        nint address;
        if (!this.GetAddress(varName, out address))
            return false;
        value = converter(address);
        return true;

        //return (T) Marshal.PtrToStructure(address,typeof(T));
    }*/

    public unsafe hid_t GetHid(string varName)
    {
        return *(hid_t*)GetAddress(varName);
    }
}

internal class H5WindowsDLLImporter : H5DLLImporter
{
    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern nint GetModuleHandle(string lpszLib);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern nint GetProcAddress
        (nint hModule, string procName);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern nint LoadLibrary(string lpszLib);

    private nint hLib;

    public H5WindowsDLLImporter(string libName)
    {
        hLib = GetModuleHandle(libName);
        if (hLib == nint.Zero)  // the library hasn't been loaded
        {
            hLib = LoadLibrary(libName);
            if (hLib == nint.Zero)
            {
                try
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetLastWin32Error());
                }
                catch (Exception e)
                {
                    throw new Exception($"Couldn't load library \"{libName}\"", e);
                }
            }
        }
    }

    protected override nint _GetAddress(string varName)
    {
        return GetProcAddress(hLib, varName);
    }
}
