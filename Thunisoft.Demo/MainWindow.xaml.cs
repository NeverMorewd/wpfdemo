using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Thunisoft.Demo.Pages;
using Thunisoft.Framework.ExtensionMethod;
using Thunisoft.Framework.Log;
using Thunisoft.Framework.UI.Controls.TaskMonitor;

namespace Thunisoft.Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty OpenCommandProperty =
    DependencyProperty.Register("OpenCommand", typeof(RoutedCommand), typeof(MainWindow), new PropertyMetadata(null));
        public struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            int scanCode;
            public int flags;
            int time;
            int dwExtraInfo;
        }
        private delegate int LowLevelKeyboardProcDelegate(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProcDelegate lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hHook);

        [DllImport("user32.dll")]
        private static extern int CallNextHookEx(int hHook, int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(IntPtr path);

        private IntPtr hHook;
        LowLevelKeyboardProcDelegate hookProc; // prevent gc
        const int WH_KEYBOARD_LL = 13;
        public RoutedCommand OpenCommand
        {
            get { return (RoutedCommand)GetValue(OpenCommandProperty); }
            set { SetValue(OpenCommandProperty, value); }
        }

        public MainWindow()
        {
            InitializeComponent();
            //bind command
            this.OpenCommand = new RoutedCommand();
            var bin = new CommandBinding(this.OpenCommand);
            bin.Executed += bin_Executed;
            this.CommandBindings.Add(bin);
            IntPtr hModule = GetModuleHandle(IntPtr.Zero);
            hookProc = new LowLevelKeyboardProcDelegate(LowLevelKeyboardProc);
            hHook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, hModule, 0);
            if (hHook == IntPtr.Zero)
            {
                MessageBox.Show("Failed to set hook, error = " + Marshal.GetLastWin32Error());
            }
        }
        private static int LowLevelKeyboardProc(int nCode, int wParam, ref KBDLLHOOKSTRUCT lParam)
        {
            if (nCode >= 0)
                switch (wParam)
                {
                    //屏蔽下面的快捷键
                    case 256: // WM_KEYDOWN
                    case 257: // WM_KEYUP
                    case 260: // WM_SYSKEYDOWN
                    case 261: // M_SYSKEYUP
                        if (
                            (lParam.vkCode == 0x09 && lParam.flags == 32) || // Alt+Tab
                            (lParam.vkCode == 0x1b && lParam.flags == 32) || // Alt+Esc
                            (lParam.vkCode == 0x20 && lParam.flags == 32) || // Alt+Space
                            (lParam.vkCode == 0x73 && lParam.flags == 32) || // Alt+F4
                            (lParam.vkCode == 0x1b && lParam.flags == 0) || // Ctrl+Esc
                            (lParam.vkCode == 0x5b && lParam.flags == 1) || // Left Windows Key 
                            (lParam.vkCode == 0x5c && lParam.flags == 1))    // Right Windows Key   
                        {
                            return 1;
                        }
                        break;
                }
            return CallNextHookEx(0, nCode, wParam, ref lParam);
        }

        void bin_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                var btn = e.Source as Button;
                if (btn.Tag.ToString().ToLower().Contains("wps"))
                {
                    new Page_WPS().ShowDialog();
                }
                else
                {
                    this.PageContext.Source = new Uri(btn.Tag.ToString(), UriKind.Relative);
                }
            }
            catch
            {
                
            }
        }


        private async Task UpLoadFileAsync(ITaskItemContext aTaskItem, object[] paramArray)
        {
            //try
            //{
            await Task.Run(async () =>
            {
                int percentComplete = 0;
                aTaskItem.TaskMessage = string.Empty;
                while (true)
                {
                    aTaskItem.TaskStatus = TaskStatusEnum.InProgress;
                    if (aTaskItem.TaskCancellationTokenSource.IsCancellationRequested)
                    {
                        percentComplete = 0;
                        aTaskItem.TaskStatus = TaskStatusEnum.Cancel;
                        aTaskItem.Progress.Report(percentComplete);
                        return;
                    }
                    await Task.Delay(1);
                    percentComplete++;
                    if (aTaskItem.Progress != null)
                    {

                        aTaskItem.Progress.Report(percentComplete);
                    }
                    if (percentComplete == 100)
                    {
                        TFLogger.LogDebug("this is debug");
                        TFLogger.LogInfo("this is info");
                        TFLogger.LogWarning("this is warning");
                        TFLogger.LogError("this is error");
                        TFLogger.LogError(new Exception("exception1", new NullReferenceException("exception2", new InvalidCastException("exception3"))));
                        aTaskItem.TaskStatus = TaskStatusEnum.Completed;
                        break;
                    }
                }
            }, aTaskItem.TaskCancellationTokenSource.Token).ConfigureAwait(false);
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    if (aTaskItem.TaskCancellationTokenSource.IsCancellationRequested)
            //    {
            //        //ignore
            //    }
            //    else
            //    {
            //        aTaskItem.TaskStatus = TaskStatusEnum.Error;
            //        aTaskItem.TaskMessage = ex.Message;
            //    }
            //}
            //return;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Main_Loaded(object sender, RoutedEventArgs e)
        {
            //this.ResolutionAdapt<Viewbox>();
        }

        private void Main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
