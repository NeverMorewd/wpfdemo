using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Thunisoft.Framework.UI.Controls
{
    /// <summary>
    /// StoryboardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class GifHostControl : UserControl
    {
        public static readonly DependencyProperty GifSourceProperty =
           DependencyProperty.Register("GifSource", typeof(string), typeof(GifHostControl), new UIPropertyMetadata(string.Empty, GifSourcePropertyChanged));
        public static readonly DependencyProperty LoadingSizeProperty =
           DependencyProperty.Register("LoadingSize", typeof(LoadingSizeEnum), typeof(GifHostControl), new UIPropertyMetadata(LoadingSizeEnum.NA, LoadingSizePropertyChanged));

        public GifHostControl()
        {
            InitializeComponent();
        }
        public string GifSource
        {
            get { return (string)GetValue(GifSourceProperty); }
            set { SetValue(GifSourceProperty, value); }
        }
        public LoadingSizeEnum LoadingSize
        {
            get { return (LoadingSizeEnum)GetValue(LoadingSizeProperty); }
            set { SetValue(LoadingSizeProperty, value); }
        }
        private static void GifSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            GifHostControl gifHostControl = sender as GifHostControl;
            var decoder = new GifBitmapDecoder(new Uri(gifHostControl.GifSource, UriKind.RelativeOrAbsolute), BitmapCreateOptions.DelayCreation, BitmapCacheOption.OnDemand);
            var frame = decoder.Frames[0];
            gifHostControl.Width = frame.PixelWidth;
            gifHostControl.Height = frame.PixelHeight;
        }
        private static void LoadingSizePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            GifHostControl gifHostControl = sender as GifHostControl;
            if (gifHostControl.LoadingSize == LoadingSizeEnum.Max)
            {
                gifHostControl.GifSource = "Pack://application:,,,/Thunisoft.Framework.UI;Component/Resources/loading_100.gif";
            }
            else if (gifHostControl.LoadingSize == LoadingSizeEnum.Min)
            {
                gifHostControl.GifSource = "Pack://application:,,,/Thunisoft.Framework.UI;Component/Resources/loading_80.gif";
            }
        }
    }
    public enum LoadingSizeEnum
    {
        Max = 0,
        Min = 1,
        NA = 99,
    }
}