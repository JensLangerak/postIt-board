using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace PostItProject.Views.CustomControls
{
    [ContentProperty("Content")]
    public class ResizeControl : Control
    {
        public ResizeControl()
        {
            DefaultStyleKey = typeof(ResizeControl);
        }


        private MenuItem _toForeground;

        private MenuItem ToForeground
        {
            get => _toForeground;

            set
            {
                if (_toForeground != null)
                {
                    _toForeground.Click -=
                        new RoutedEventHandler(ToForeground_Click);
                }

                _toForeground = value;

                if (_toForeground != null)
                {
                    _toForeground.Click +=
                        new RoutedEventHandler(ToForeground_Click);
                }
            }
        }

        private MenuItem _toBackground;

        private MenuItem ToBackground
        {
            get => _toBackground;

            set
            {
                if (_toBackground != null)
                {
                    _toBackground.Click -=
                        new RoutedEventHandler(ToBackground_Click);
                }

                _toBackground = value;

                if (_toBackground != null)
                {
                    _toBackground.Click +=
                        new RoutedEventHandler(ToBackground_Click);
                }
            }
        }

        private void ToBackground_Click(object sender, RoutedEventArgs e)
        {
            ChangeZIndexHelper(Math.Min, -1);
        }

        private void ToForeground_Click(object sender, RoutedEventArgs e)
        {
            ChangeZIndexHelper(Math.Max, 1);
        }

        private void ChangeZIndexHelper(Func<int, int, int> pZIndexSelector, int changeValue)
        {
            if (!(this.DataContext is UIElement item)) return;
            if (!(VisualTreeHelper.GetParent(item) is Panel parent)) return;
            var intex = 0;
            var itemCount = 0;
            foreach (UIElement child in parent.Children)
            {
                itemCount++;
                if (child == item) continue;
                var zIndex = Panel.GetZIndex(child);
                intex = pZIndexSelector(intex, zIndex);
            }

            var newIndex = intex + changeValue;
            Panel.SetZIndex(item, newIndex);

            if (newIndex < -itemCount || newIndex > 2 * itemCount)
                Reorder(parent);

        }

        private static void Reorder(Panel pPanel)
        {
            List<int> existingZIndex = new List<int>();
            foreach (UIElement child in pPanel.Children)
            {
                existingZIndex.Add(Panel.GetZIndex(child));
            }
            existingZIndex.Sort();
            foreach (UIElement child in pPanel.Children)
            {
                var zIndex = Panel.GetZIndex(child);
                Panel.SetZIndex(child, existingZIndex.FindIndex(x=> x == zIndex));
            }
        }

        public override void OnApplyTemplate()
        {
            ToForeground = GetTemplateChild("ItemToForeground") as MenuItem;
            ToBackground = GetTemplateChild("ItemToBackground") as MenuItem;
        }

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(ResizeControl), null);
    }
}
