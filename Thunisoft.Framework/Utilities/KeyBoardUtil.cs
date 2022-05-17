using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Thunisoft.Framework.Utilities
{
    public class HotKey
    {
        readonly int _keyId; //热键编号
        readonly IntPtr _handle; //窗体句柄
        readonly Window _window; //热键所在窗体
        public delegate void OnHotKeyEventHandler(); //热键事件委托
        public event OnHotKeyEventHandler OnHotKey; //热键事件
        private static readonly Hashtable KeyPair = new Hashtable(); //热键哈希表
        private const int WmHotkey = 0x0312; // 热键消息编号

        public enum KeyFlags //控制键编码
        {
            ModAlt = 0x1,
            ModControl = 0x2,
            ModShift = 0x4,
            ModWin = 0x8
        }

        public HotKey(Window win, KeyFlags control, Keys key)
        {
            _handle = new WindowInteropHelper(win).Handle;
            _window = win;
            var controlKey = (uint)control;
            var key1 = (uint)key;
            _keyId = (int)controlKey + (int)key1 * 10;
            if (KeyPair.ContainsKey(_keyId))
            {
                throw new Exception("热键已经被注册!");
            }
            //注册热键
            if (false == RegisterHotKey(_handle, _keyId, controlKey, key1))
            {
                throw new Exception("热键注册失败!");
            }
            //消息挂钩只能连接一次!!
            if (KeyPair.Count == 0)
            {
                if (false == InstallHotKeyHook(this))
                {
                    throw new Exception("消息挂钩连接失败!");
                }
            }
            //添加这个热键索引
            KeyPair.Add(_keyId, this);
        }

        ~HotKey()
        {
            UnregisterHotKey(_handle, _keyId);
        }

        [DllImport("user32")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint controlKey, uint virtualKey);

        [DllImport("user32")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private static bool InstallHotKeyHook(HotKey hk)
        {
            if (hk._window == null || hk._handle == IntPtr.Zero)
            {
                return false;
            }
            //获得消息源
            HwndSource source = HwndSource.FromHwnd(hk._handle);
            if (source == null)
            {
                return false;
            }
            //挂接事件
            source.AddHook(HotKeyHook);
            return true;
        }

        private static IntPtr HotKeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg != WmHotkey) return IntPtr.Zero;
            var hk = (HotKey)KeyPair[(int)wParam];
            hk.OnHotKey?.Invoke();
            return IntPtr.Zero;
        }
    }
}
