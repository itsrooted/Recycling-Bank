using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Windows
{
    public class APIs
    {
        public static readonly Random rand = new Random();
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        public class SOUND_API
        {
            [DllImport("ole32.dll")]
            public static extern int CoInitialize(IntPtr pvReserved);

            [DllImport("ole32.dll")]
            public static extern void CoUninitialize();

            [ComImport]
            [Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
            internal class MMDeviceEnumerator { }

            internal enum EDataFlow
            {
                eRender,
                eCapture,
                eAll
            }

            internal enum ERole
            {
                eConsole,
                eMultimedia,
                eCommunications
            }

            [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"),
             InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface IMMDeviceEnumerator
            {
                int NotImpl1();
                [PreserveSig]
                int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppDevice);
            }

            [Guid("D666063F-1587-4E43-81F1-B948E807363F"),
             InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface IMMDevice
            {
                [PreserveSig]
                int Activate(ref Guid iid, int dwClsCtx, IntPtr pActivationParams, [MarshalAs(UnmanagedType.IUnknown)] out object ppInterface);
            }

            [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"),
             InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            internal interface IAudioEndpointVolume
            {
                int RegisterControlChangeNotify(IntPtr pNotify);
                int UnregisterControlChangeNotify(IntPtr pNotify);
                int GetChannelCount(out int channelCount);
                int SetMasterVolumeLevel(float level, Guid eventContext);
                int SetMasterVolumeLevelScalar(float level, Guid eventContext);
                int GetMasterVolumeLevel(out float level);
                int GetMasterVolumeLevelScalar(out float level);
            }
        }
    }
}