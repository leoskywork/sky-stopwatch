using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch
{
    public static class PowerTool
    {

        public static bool AnyAppConfigProcessRunning()
        {
            if(GlobalData.ProcessList.Count == 0) { return false; }

            var processes = Process.GetProcesses();
            var currentSessionId = Process.GetCurrentProcess().SessionId;
            var currentUserProcesses = processes.Where(p => p.SessionId == currentSessionId).ToList();

            foreach(string processName in GlobalData.ProcessList)
            {
                if(currentUserProcesses.Any(p => p.ProcessName.Equals(processName, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }

            return false;
        }

















        public static void Test()
        {
            Process[] processes = Process.GetProcesses();
            var currentSessionId = Process.GetCurrentProcess().SessionId;
            var currentUserProcesses = processes.Where(p => p.SessionId == currentSessionId).ToList();

           // foreach (Process process in currentUserProcesses)
            {
                //try
                {
                    Process p = currentUserProcesses.FirstOrDefault(cp => "crossfire".Equals(cp.ProcessName, StringComparison.OrdinalIgnoreCase));
                    //using (Process p = Process.GetProcessById(process.Id))
                    {
                        //get error
                        string processUserName = Environment.UserName;//p.GetCurrentProcessUserName();
                        //if (userName.Equals(processUserName, StringComparison.OrdinalIgnoreCase))
                        //{
                        //    Console.WriteLine($"进程名称: {p.ProcessName}, ID: {p.Id}, 用户名: {processUserName}");
                        //}
                        System.Diagnostics.Debug.WriteLine($"name: {p.ProcessName}, ID: {p.Id}, user: {processUserName}");
                    }
                }
                //catch
                //{
                //    // 无法访问某些进程（可能是权限问题）
                //}
            }
        }

        // 扩展方法，用于获取进程的用户名
        public static string GetCurrentProcessUserName(this Process process)
        {
            IntPtr userToken;
            if (!OpenProcessToken(process.Handle, TOKEN_QUERY, out userToken))
            {
                throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
            }

            //using (userToken)
            {
                uint tokenInfLength = 0;
                bool result = GetTokenInformation(userToken, (uint)TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, tokenInfLength, out tokenInfLength);
                if (!result)
                {
                    int error = Marshal.GetLastWin32Error();
                    if (error != ERROR_INSUFFICIENT_BUFFER) throw new System.ComponentModel.Win32Exception(error);
                }

                IntPtr tokenInf = IntPtr.Zero;
                try
                {
                    tokenInf = Marshal.AllocHGlobal((int)tokenInfLength);
                    result = GetTokenInformation(userToken, (uint)TOKEN_INFORMATION_CLASS.TokenUser, tokenInf, tokenInfLength, out tokenInfLength);

                    if (!result)
                    {
                        throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                    }

                    TOKEN_USER tokenUser = (TOKEN_USER)Marshal.PtrToStructure(tokenInf, typeof(TOKEN_USER));
                    IntPtr pSid = tokenUser.user.Sid;
                    WindowsIdentity wid = new WindowsIdentity(pSid);
                    return wid.Name;
                }
                finally
                {
                    if (tokenInf != IntPtr.Zero)
                        Marshal.FreeHGlobal(tokenInf);

                    Marshal.FreeHGlobal(userToken);

                }
            }

        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetTokenInformation(IntPtr tokenHandle, uint tokenInformationClass, IntPtr tokenInformation, uint tokenInformationLength, out uint returnLength);

        private const uint TOKEN_QUERY = 0x0008;
        private const uint ERROR_INSUFFICIENT_BUFFER = 0x007a; //122


        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_USER
        {
            //public uint reserved;
            //public uint processorArchitecture;
            public USER_ID user;
        }

        //[StructLayout(LayoutKind.Sequential)]
        //public struct USER_ID
        //{
        //    public IntPtr Sid;
        //}

        [StructLayout(LayoutKind.Explicit)]
        public struct USER_ID
        {
            [FieldOffset(0)] public IntPtr Sid;
        }

        private enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            Token
        }

    }
}
