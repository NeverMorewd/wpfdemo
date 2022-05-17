using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Thunisoft.Framework.UI.Controls
{
    /// <summary>
    /// SpreadCircleControl.xaml 的交互逻辑
    /// </summary>
    public partial class SpreadCircleControl : UserControl
    {
        public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register("TextContent", typeof(string), typeof(SpreadCircleControl), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty SpreadColorProperty = DependencyProperty.Register("SpreadColor", typeof(Color), typeof(SpreadCircleControl), new PropertyMetadata(default(Color)));
        public static readonly DependencyProperty SpreadMarginProperty = DependencyProperty.Register("SpreadMargin", typeof(Thickness), typeof(SpreadCircleControl), new PropertyMetadata(default(Thickness)));
        
        public string TextContent
        {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }
        public Color SpreadColor
        {
            get { return (Color)GetValue(SpreadColorProperty); }
            set { SetValue(SpreadColorProperty, value); }
        }
        public Thickness SpreadMargin
        {
            get { return (Thickness)GetValue(SpreadMarginProperty); }
            set { SetValue(SpreadMarginProperty, value); }
        }
        public SpreadCircleControl()
        {
            InitializeComponent();
        }


    }
}
