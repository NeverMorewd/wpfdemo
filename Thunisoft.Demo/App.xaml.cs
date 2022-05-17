using System;
using System.Windows;
using System.Windows.Threading;
using Thunisoft.Framework.Log;

namespace Thunisoft.Demo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            //this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(this.OnDispatcherUnhandledException);
        }
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception exception = args.ExceptionObject as Exception;
            TFLogger.LogError(exception);
            throw exception;
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            TFLogger.LogError(e.Exception);
            throw e.Exception;
        }
    }
}
