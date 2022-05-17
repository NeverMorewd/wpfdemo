using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Thunisoft.Framework.UI.Converters
{
    /// <summary>
    /// 常用转换器的静态引用
    /// </summary>
    public sealed class TFConverter
    {
        private static BooleanToVisibilityConverter booleanToVisibilityConverter;
        private static TrueToFalseConverter trueToFalseConverter;
        private static ThicknessToDoubleConverter thicknessToDoubleConverter;
        private static BackgroundToForegroundConverter backgroundToForegroundConverter;
        private static PercentToAngleConverter percentToAngleConverter;
        private static DoubleToBoolConverter doubleToBoolConverter;
        public static BooleanToVisibilityConverter BooleanToVisibilityConverter
        {
            get { return booleanToVisibilityConverter ?? (booleanToVisibilityConverter = new BooleanToVisibilityConverter()); }
        }

        public static TrueToFalseConverter TrueToFalseConverter
        {
            get { return trueToFalseConverter ?? (trueToFalseConverter = new TrueToFalseConverter()); }
        }

        public static ThicknessToDoubleConverter ThicknessToDoubleConverter
        {
            get { return thicknessToDoubleConverter ?? (thicknessToDoubleConverter = new ThicknessToDoubleConverter()); }
        }
        public static BackgroundToForegroundConverter BackgroundToForegroundConverter
        {
            get { return backgroundToForegroundConverter ?? (backgroundToForegroundConverter = new BackgroundToForegroundConverter()); }
        }

        public static PercentToAngleConverter PercentToAngleConverter
        {
            get { return percentToAngleConverter ?? (percentToAngleConverter = new PercentToAngleConverter()); }
        }

        public static DoubleToBoolConverter DoubleToBoolConverter
        {
            get { return doubleToBoolConverter ?? (doubleToBoolConverter = new DoubleToBoolConverter()); }
        }
    }
    public sealed class TrueToFalseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool)value;
            return !v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PercentToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var percent = double.Parse(value.ToString());
            if (percent >= 1) return 360.0D;
            return percent * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thickness = (Thickness)value;
            return thickness.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
    public class BackgroundToForegroundConverter : IValueConverter
    {
        private Color IdealTextColor(Color bg)
        {
            const int nThreshold = 105;
            var bgDelta = System.Convert.ToInt32((bg.R * 0.299) + (bg.G * 0.587) + (bg.B * 0.114));
            var foreColor = (255 - bgDelta < nThreshold) ? Colors.Black : Colors.White;
            return foreColor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                var idealForegroundColor = this.IdealTextColor(((SolidColorBrush)value).Color);
                var foreGroundBrush = new SolidColorBrush(idealForegroundColor);
                foreGroundBrush.Freeze();
                return foreGroundBrush;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
 
    public class DoubleToBoolConverter : IValueConverter
    {
        public DoubleToBoolConverter() { }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) == System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : null;
        }
    }
}