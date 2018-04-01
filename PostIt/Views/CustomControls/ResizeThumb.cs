using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PostItProject.Views.CustomControls
{
    public class ResizeThumb : Thumb
    {
        public ResizeThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.ResizeDragDelta);
        }

        private void ResizeDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (!(this.DataContext is FrameworkElement item)) return;

            var horizontalDelta = 0.0;
            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    horizontalDelta = -e.HorizontalChange;
                    break;
                case HorizontalAlignment.Right:
                    horizontalDelta = e.HorizontalChange;
                    break;
                default:
                    break;
            }
            horizontalDelta = Math.Max(horizontalDelta, -(item.ActualWidth - item.MinWidth));
            item.Width += horizontalDelta;

            if (HorizontalAlignment == HorizontalAlignment.Left)
                Canvas.SetLeft(item, Canvas.GetLeft(item) - horizontalDelta);

            var verticalDelta = 0.0;
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    verticalDelta = -e.VerticalChange;
                    break;
                case VerticalAlignment.Bottom:
                    verticalDelta = e.VerticalChange;
                    break;
                default:
                    break;
            }
            verticalDelta =  Math.Max(verticalDelta, -(item.ActualHeight - item.MinHeight));
            item.Height += verticalDelta;

            if (VerticalAlignment == VerticalAlignment.Top)
                Canvas.SetTop(item, Canvas.GetTop(item) - verticalDelta);

            e.Handled = true;
        }
    }
}
