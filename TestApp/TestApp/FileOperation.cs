using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static TestApp.ShellFileOperation;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestApp
{
    public static class FileOperation
    {
        static ShellNameMapping[] NameMappings;
        static String[] SourceFiles;
        static String[] DestFiles;
        static String ProgressTitle;
        private static bool WithFileOperation(FileOperations Operation, ShellFileOperationFlags OperationFlags)
        {
            ShellApi.SHFILEOPSTRUCT FileOpStruct = new ShellApi.SHFILEOPSTRUCT();

            FileOpStruct.hwnd = IntPtr.Zero;
            FileOpStruct.wFunc = (uint)Operation;

            String multiSource = StringArrayToMultiString(SourceFiles);
            String multiDest = StringArrayToMultiString(DestFiles);
            FileOpStruct.pFrom = Marshal.StringToHGlobalUni(multiSource);
            FileOpStruct.pTo = Marshal.StringToHGlobalUni(multiDest);

            FileOpStruct.fFlags = (ushort)OperationFlags;
            FileOpStruct.lpszProgressTitle = ProgressTitle;
            FileOpStruct.fAnyOperationsAborted = 0;
            FileOpStruct.hNameMappings = IntPtr.Zero; 
            NameMappings = new ShellNameMapping[0];

            int RetVal;
            RetVal = ShellApi.SHFileOperation(ref FileOpStruct);

            ShellApi.SHChangeNotify(
                (uint)ShellChangeNotificationEvents.SHCNE_ALLEVENTS,
                (uint)ShellChangeNotificationFlags.SHCNF_DWORD,
                IntPtr.Zero,
                IntPtr.Zero);

            if (RetVal != 0)
                return false;

            if (FileOpStruct.fAnyOperationsAborted != 0)
                return false;

            return true;
        }
    }
}

