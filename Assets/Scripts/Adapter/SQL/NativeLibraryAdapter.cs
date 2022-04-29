using System;
using SQLitePCL;
using System.Runtime.InteropServices;
using UnityEngine;

class NativeLibraryAdapter : IGetFunctionPointer, System.IDisposable
{
    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern IntPtr LoadLibrary(string name);
    [DllImport("kernel32", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

    private IntPtr _dll = IntPtr.Zero;

    public NativeLibraryAdapter(string path)
    {
        _dll = LoadLibrary(path);
    }


    public void Dispose()
    {
        if (_dll != IntPtr.Zero)
        {
            FreeLibrary(_dll);
            _dll = IntPtr.Zero;
        }
    }

    public IntPtr GetFunctionPointer(string procName) =>
        _dll != IntPtr.Zero ?
        GetProcAddress(_dll, procName)
        : IntPtr.Zero;
}
