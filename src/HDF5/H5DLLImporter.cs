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

using hid_t = System.Int64;

using System;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// Helper class used to fetch public variables (e.g. native type values)
/// exported by the HDF5 DLL
/// </summary>
internal abstract class H5DLLImporter
{
    public static readonly H5DLLImporter Instance;

    static H5DLLImporter()
    {
        H5.open();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            Instance = new H5LinuxDllImporter(Constants.MainLibraryDllFilename);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            Instance = new H5MacDllImporter(Constants.MainLibraryDllFilename);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            Instance = new H5WindowsDLLImporter(Constants.MainLibraryDllFilename);
        else
            throw new PlatformNotSupportedException();
    }

    protected abstract IntPtr _GetAddress(string varName);

    public IntPtr GetAddress(string varName)
    {
        var address = _GetAddress(varName);
        if (address == IntPtr.Zero) throw new Exception($"The export with name \"{varName}\" doesn't exist.");

        return address;
    }

    public bool GetAddress(string varName, out IntPtr address)
    {
        address = _GetAddress(varName);
        return address == IntPtr.Zero;
    }

    /*public bool GetValue<T>(
        string          varName,
        ref T           value,
        Func<IntPtr, T> converter
        )
    {
        IntPtr address;
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

#region Windows Importer
internal class H5WindowsDLLImporter : H5DLLImporter
{
    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr GetModuleHandle(string lpszLib);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr GetProcAddress
        (IntPtr hModule, string procName);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr LoadLibrary(string lpszLib);

    private IntPtr hLib;

    public H5WindowsDLLImporter(string libName)
    {
        hLib = GetModuleHandle(libName);
        if (hLib == IntPtr.Zero)  // the library hasn't been loaded
        {
            hLib = LoadLibrary(libName);
            if (hLib == IntPtr.Zero)
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

    protected override IntPtr _GetAddress(string varName)
    {
        return GetProcAddress(hLib, varName);
    }
}
#endregion

internal class H5LinuxDllImporter : H5DLLImporter
{
    [DllImport("libdl.so.2")]
    protected static extern IntPtr dlopen(string filename, int flags);

    [DllImport("libdl.so.2")]
    protected static extern IntPtr dlsym(IntPtr handle, string symbol);

    [DllImport("libdl.so.2")]
    protected static extern IntPtr dlerror();

    private IntPtr hLib;

    public H5LinuxDllImporter(string libName)
    {
        // If the library is referenced directly, i.e. for UnitTests.csproj, the native libs 
        // are located in the same directory as the library itself.
        // If the library is referenced via a NuGet package, the native libs are located
        // in the runtimes/linux-x64/native subfolder of the package.
        var filename = $"lib{libName}.so";
        var libDir = Path.GetDirectoryName(NativeDependencies.GetAssemblyPath());
        var inLibDir = Path.Combine(libDir, filename);
        var inPkgDir = Path.Combine(libDir, "..", "..", "runtimes", "linux-x64", "native", filename);
        var inPkgDir3 = Path.Combine(libDir, "runtimes", "linux-x64", "native", filename);
        if (File.Exists(inLibDir))
            libName = inLibDir;
        else if (File.Exists(inPkgDir))
            libName = inPkgDir;
        else if (File.Exists(inPkgDir3))
            libName = inPkgDir3;

        hLib = dlopen(libName, RTLD_NOW);
        if (hLib == IntPtr.Zero)
        {
            throw new ArgumentException(
                                        $"Unable to load unmanaged module \"{libName}\"");
        }
    }

    const int RTLD_NOW = 2; // for dlopen's flags
    protected override IntPtr _GetAddress(string varName)
    {
        var address = dlsym(hLib, varName);
        var errPtr = dlerror();
        if (errPtr != IntPtr.Zero)
        {
            throw new Exception($"dlsym: {Marshal.PtrToStringAnsi(errPtr)}");
        }
        return address;
    }
}

internal class H5MacDllImporter : H5DLLImporter
{
    [DllImport("libdl")]
    protected static extern IntPtr dlopen(string filename, int flags);

    [DllImport("libdl")]
    protected static extern IntPtr dlsym(IntPtr handle, string symbol);

    [DllImport("libdl")]
    protected static extern IntPtr dlerror();

    private IntPtr hLib;

    public H5MacDllImporter(string libName)
    {
        // If the library is referenced directly, i.e. for UnitTests.csproj, the native libs 
        // are located in the same directory as the library itself.
        // If the library is referenced via a NuGet package, the native libs are located
        // in the runtimes/osx-x64/native subfolder of the package.
        var filename = $"lib{libName}.dylib";
        var libDir = Path.GetDirectoryName(NativeDependencies.GetAssemblyPath());
        var inLibDir = Path.Combine(libDir, filename);
        var inPkgDir = Path.Combine(libDir, "..", "..", "runtimes", "osx-x64", "native", filename);

        if (File.Exists(inLibDir))
            libName = inLibDir;
        else if (File.Exists(inPkgDir))
            libName = inPkgDir;

        hLib = dlopen(libName, RTLD_NOW);
        if (hLib == IntPtr.Zero)
        {
            throw new ArgumentException($"Unable to load unmanaged module \"{libName}\"");
        }
    }

    const int RTLD_NOW = 2; // for dlopen's flags
    protected override IntPtr _GetAddress(string varName)
    {
        var address = dlsym(hLib, varName);
        var errPtr = dlerror();
        if (errPtr != IntPtr.Zero)
        {
            throw new Exception($"dlsym: {Marshal.PtrToStringAnsi(errPtr)}");
        }
        return address;
    }
}
