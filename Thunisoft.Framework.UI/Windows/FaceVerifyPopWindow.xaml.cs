using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Thunisoft.Framework.UI.Windows
{
    /// <summary>
    /// FaceVerifyPopWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FaceVerifyPopWindow : Window
    {
        private Storyboard _story;
        private readonly Border _bor;
        public readonly float _dpiPercent;

        public FaceVerifyPopWindow(Border bor)
        {
            InitializeComponent();

            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
            {
                _dpiPercent = g.DpiX / 96;
            }

            _bor = bor;
            CompositionTarget.Rendering += WindowPositionChange;
            KeyDown += FaceVerifyPopWindowKeyDown;

            this.lineScan.RenderTransform = new TranslateTransform();

            this.lineScan.Name = "button1";
            this.Name = "window1";
            NameScope.SetNameScope(this, new NameScope());
            this.RegisterName(this.lineScan.Name, this.lineScan);

            DoubleAnimation xAnimation = new DoubleAnimation();
            xAnimation.From = 0;
            xAnimation.To = 360;
            xAnimation.Duration = new Duration(TimeSpan.FromSeconds(1.5));

            DependencyProperty[] propertyChain = new DependencyProperty[]
            {
                Line.RenderTransformProperty,
                TranslateTransform.YProperty
            };

            _story = new Storyboard();
            _story.AutoReverse = false;
            _story.RepeatBehavior = RepeatBehavior.Forever;
            _story.Children.Add(xAnimation);

            Storyboard.SetTargetName(xAnimation, this.lineScan.Name);
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath("(0).(1)", propertyChain));
        }

        private void FaceVerifyPopWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.Space)
            {
                e.Handled = true;
            }
            else if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
            else
            {
                OnKeyDown(e);
            }
        }

        private void WindowPositionChange(object sender, EventArgs e)
        {
            try
            {
                if (_bor == null) return;
                if (PresentationSource.FromVisual(_bor) == null) return;
                this.Show();
                var temPoint = _bor.PointToScreen(new System.Windows.Point(0, 0));

                if (this._dpiPercent > 0)
                    Left = temPoint.X / this._dpiPercent;
                else
                    Left = temPoint.X;

                if (this._dpiPercent > 0)
                    Top = temPoint.Y / this._dpiPercent;
                else
                    Top = temPoint.Y;

                Width = _bor.ActualWidth * SystemParameters.PrimaryScreenWidth / 1920;
                Height = _bor.ActualHeight * SystemParameters.PrimaryScreenHeight / 1080;

                CaculateSize();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void CaculateSize()
        {
            double AspectRatio = 1326 / 710.0d;

            double tWidth = this.Width;
            double tHeight = this.Height;
            if (tWidth / tHeight > AspectRatio)
            {
                tWidth = tHeight * AspectRatio;
            }
            else
            {
                tHeight = tWidth / AspectRatio;
            }

            FaceColumn.Width = new GridLength(tWidth);
            FaceRow.Height = new GridLength(tHeight);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _story?.Begin(this);
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _story?.Stop();
        }
    }
}
