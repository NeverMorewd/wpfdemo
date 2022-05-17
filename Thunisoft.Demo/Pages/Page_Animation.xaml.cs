using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Thunisoft.Demo.Pages
{
    /// <summary>
    /// Interaction logic for Page_Animation.xaml
    /// </summary>
    public partial class Page_Animation : Page
    {
        public Page_Animation()
        {
            InitializeComponent();
            //PointAnimationTest(pointAnimation);
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                await this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    testControl.Visibility = Visibility.Visible;
                }));
            });
        }
        private void PointAnimationTest(Canvas canvas)
        {
            var points =
                new List<Point>()
                {
                    new Point(0, 0),
                    new Point(50, 50),
                    new Point(150, -50),
                };
            var sb = new Storyboard();
            for (int i = 0; i < points.Count - 1; i++)
            {
                var lineGeometry = new LineGeometry(points[i], points[i]);
                var path = new Path
                {
                    Stroke = Brushes.Green,
                    StrokeThickness = 8,
                    Data = lineGeometry,
                };
                if (i == 0)
                {
                    path.StrokeStartLineCap = PenLineCap.Round;
                }
                if (i == 1)
                {
                    path.StrokeEndLineCap = PenLineCap.Round;
                }
                canvas.Children.Add(path);
                var animation =
                    new PointAnimation(points[i], points[i + 1], new Duration(TimeSpan.FromMilliseconds(3000)))
                    {
                        BeginTime = TimeSpan.FromMilliseconds(i * 3010)
                    };
                sb.Children.Add(animation);
                RegisterName("geometry" + i, lineGeometry);
                Storyboard.SetTargetName(animation, "geometry" + i);
                Storyboard.SetTargetProperty(animation, new PropertyPath(LineGeometry.EndPointProperty));
            }
            sb.Begin(this);

        }
    }
}
