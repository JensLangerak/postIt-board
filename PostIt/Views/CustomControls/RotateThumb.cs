using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PostItProject.Views.CustomControls
{
    class RotateThumb : Thumb
    {
        public RotateThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.RotateDragDelta);
        }

        private void RotateDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (!(this.DataContext is FrameworkElement item)) return;

            if (!(item.RenderTransform is RotateTransform))
            {
                item.RenderTransform = new RotateTransform(0);
            }
            var rotate = (RotateTransform) item.RenderTransform;


            //TODO bind center and calculate angle properly
            rotate.CenterX = item.ActualWidth / 2.0;
            rotate.CenterY = item.ActualHeight / 2.0;
            
            rotate.Angle += e.HorizontalChange / 10; //Just a test, result is much better than expected XD, however is not ideal thus needs to be culculated properly.
        }
    }
}
