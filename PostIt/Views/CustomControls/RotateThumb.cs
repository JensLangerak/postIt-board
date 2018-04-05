using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace PostItProject.Views.CustomControls
{
    class RotateThumb : Thumb
    {
        public RotateThumb()
        {
            this.DragDelta += new DragDeltaEventHandler(this.RotateDragDelta);
        }

        private void RotateDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (!(this.DataContext is FrameworkElement item)) return;

            if (!(item.RenderTransform is RotateTransform))
            {
                item.RenderTransform = new RotateTransform(0);
                item.RenderTransformOrigin = new Point(0.5, 0.5);
            }

           
            var center = new Point(item.ActualWidth / 2.0, item.ActualHeight / 2.0);
            var relativeMousePos = Mouse.GetPosition(item) - center;

            var rotate = (RotateTransform)item.RenderTransform;


            /*************
             *        pos.x
             *       ------ pos
             *       |    /
             *       |   /
             * pos.y |  /
             *       | /
             *       |/
             *      (0,0)
             *
             * 
             * tan(angle) = pos.x / (-pos.y)
             */

            if (Math.Abs(relativeMousePos.Y) < double.Epsilon) return;

            // Negative y means above the center and a positive y means below the center.
            // If mouse is below the center rotate the image 180 degrees.
            // And continue the calculation with a inverted y. Note if element is rotatad 180 degrees the new inverted y is the original noninverted y.
            if (relativeMousePos.Y > 0)
            {
                rotate.Angle += 180.0;
            }
            else
            {
                relativeMousePos.Y = -relativeMousePos.Y;
            }

            var angle = Math.Atan(relativeMousePos.X / (relativeMousePos.Y)) * 360 / (2 * Math.PI);
            rotate.Angle += angle;
        }
    }
}
