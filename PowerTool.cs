using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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


    public static class FormLeoExt
    {
        public static void OnError(this Form form, Exception e)
        {
            System.Diagnostics.Debug.WriteLine(e.ToString());

            if (GlobalData.Default.IsDebugging)
            {
                MessageBox.Show(e.ToString());
            }
            else
            {
                MessageBox.Show(e.Message);

                form.RunOnMain(() => GlobalData.Default.FireCloseApp());
            }
        }

        public static bool IsDead(this Form form)
        {
            return form.Disposing || form.IsDisposed;
        }

        public static void RunOnMain(this Form form, Action action)
        {
            if (action == null) return;
            if (form.IsDead()) return;

            //System.Diagnostics.Debug.WriteLine($"RunOnMain - is dead: {form.IsDead()}, disp: {form.Disposing}, disped:{form.IsDisposed} - before if");
            if (form.InvokeRequired)
            {
                if (form.IsDead()) return; //not sure why, the is dead check above not working sometimes, do it again here
                if (form.Disposing || form.IsDisposed) return;
                //System.Diagnostics.Debug.WriteLine($"RunOnMain - is dead: {form.IsDead()}, disp: {form.Disposing}, disped:{form.IsDisposed}");
                form.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void RunOnMain(this Form form, Action action, int delayMS)
        {
            Task.Run(() =>
            {
                Thread.Sleep(delayMS);
                RunOnMain(form, action);
            });
        }

        public static void RunOnMainAsync(this Form form, Action action)
        {
            if (action == null) return;
            if (form.IsDead()) return;

            if (form.InvokeRequired)
            {
                form.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

        public static void RunOnMainAsync(this Form form, Action action, int delayMS)
        {
            Task.Run(() =>
            {
                Thread.Sleep(delayMS);
                RunOnMainAsync(form, action);
            });
        }

        public static PowerLog Log(this Form form)
        {
            return PowerLog.One;
        }

        public static void DisableButtonShortTime(this Form form, Label control)
        {
            var oldForeColor = control.ForeColor;
            var oldBackColor = control.BackColor;


            control.ForeColor = System.Drawing.Color.White;
            control.BackColor = System.Drawing.Color.LightGray;
            control.Enabled = false;

            form.RunOnMain(() =>
            {
                control.ForeColor = oldForeColor;
                control.BackColor = oldBackColor;
                control.Enabled = true;
            }, 300);
        }

        public static void DisableButtonShortTime(this Form form, Button control)
        {
            var oldForeColor = control.ForeColor;
            var oldBackColor = control.BackColor;

            //leotodo, tmp fix for action bar, or else only the first click will work, and button become disabled forever
            //is this caused by the transparent background/color ?
            oldForeColor = System.Drawing.Color.Black;
            oldBackColor = System.Drawing.Color.White;


            control.ForeColor = System.Drawing.Color.White;
            control.BackColor = System.Drawing.Color.LightGray;
            control.Enabled = false;

            form.RunOnMainAsync(() =>
            {
                control.ForeColor = oldForeColor;
                control.BackColor = oldBackColor;
                control.Enabled = true;
            }, 300);
        }

    }
}
