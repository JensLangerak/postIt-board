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
            Canvas.SetLeft(item, Canvas.GetLeft(item) + e.HorizontalChange);
            Canvas.SetTop(item, Canvas.GetTop(item) + e.VerticalChange);

            e.Handled = true;
        }
    }
}
