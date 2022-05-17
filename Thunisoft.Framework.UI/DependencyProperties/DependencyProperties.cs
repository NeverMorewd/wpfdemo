using System.Windows;
using System.Windows.Controls;

namespace Thunisoft.Framework.UI.DependencyProperties
{
    public class DependencyProperties : FrameworkElement
    {
        public static readonly DependencyProperty BorderCornerRadiusProperty = DependencyProperty.Register("BorderCornerRadius", typeof(CornerRadius), typeof(Button), new PropertyMetadata(0));
        public CornerRadius BorderCornerRadius
        {
            get { return (CornerRadius)GetValue(BorderCornerRadiusProperty); }
            set { SetValue(BorderCornerRadiusProperty, value); }
        }
    }
}
