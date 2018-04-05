using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using PostItProject.ViewModels.UserControls;

namespace PostItProject.Views.CustomControls
{
    public class MoveThumb : Thumb
    {
        public MoveThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.MoveDragDelta);
        }

        private void MoveDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (!(this.DataContext is UIElement item)) return;

            var drag = new Point(e.HorizontalChange, e.VerticalChange);
            drag = item.RenderTransform?.Transform(drag) ?? drag;

            Canvas.SetLeft(item, Canvas.GetLeft(item) + drag.X);
            Canvas.SetTop(item, Canvas.GetTop(item) + drag.Y);

            e.Handled = true;
        }
    }
}
