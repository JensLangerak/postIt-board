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
        private enum Orientation
        {
            Horizontal,
            Vertical
        }

        public ResizeThumb()
        {
            DragDelta += new DragDeltaEventHandler(this.ResizeDragDelta);
        }

        private void ResizeDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (!(this.DataContext is FrameworkElement item)) return;

            // When resizing a element we want to keep a certen corner at a fixed position. 
            // Forexample if we resize using the thumb at the top left, we don't want to move the bottoem right corner.

            // Calculate current center coordinates. 
            // canvasCoord is the top left point when the element is not rotated
            // The center coord is undepended of the rotation.
            var canvasCoord = new Vector(Canvas.GetLeft(item), Canvas.GetTop(item));
            var shape = new Vector(item.ActualWidth, item.ActualHeight);
            var centerCoord = canvasCoord + shape * 0.5;

            // We first calculate how the canvasCoord must change for the horizontal mouse movement, next we do this for the vertical mouse movement. And the we calculate the final coord.
            var translateH = HandleHorizontalChange(e.HorizontalChange, ref item, shape, centerCoord, canvasCoord);
            var translateV = HandleVerticalChange(e.VerticalChange, ref item, shape, centerCoord, canvasCoord);
            var topLeft = canvasCoord + translateH + translateV;

            // Set the net coordinates
            Canvas.SetLeft(item, topLeft.X);
            Canvas.SetTop(item, topLeft.Y);

            e.Handled = true;
        }

        /// <summary>
        /// Handle the horizontal mouse movement. Set the new width and calculate how much the coordinates must change for this movement.
        /// </summary>
        /// <param name="pHorizontalChange">Size of the change</param>
        /// <param name="pItem">Item that is resized</param>
        /// <param name="pShape">Original sizes</param>
        /// <param name="pCenterCoord">Coords of the center</param>
        /// <param name="pCanvasCoord">Coords in the canvas</param>
        /// <returns>The translation that is needed to be perfromed on pItem.</returns>
        private Vector HandleHorizontalChange(double pHorizontalChange, ref FrameworkElement pItem, Vector pShape,
            Vector pCenterCoord, Vector pCanvasCoord)
        {
            var horizontalDelta = 0.0;
            var leftIsRef = true; // Left side must stay in position.
            switch (HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    horizontalDelta = -pHorizontalChange;
                    leftIsRef = false;
                    break;
                case HorizontalAlignment.Right:
                    horizontalDelta = pHorizontalChange;
                    leftIsRef = true;
                    break;
                default:
                    return new Vector(0,0);
            }

            // Calculate the the center coord of the side (left or right) that is not allowed to move
            var horizontalRef = CalculateReference(Orientation.Horizontal, leftIsRef, ref pItem, pShape, pCenterCoord);

            // Change the width
            horizontalDelta = Math.Max(horizontalDelta, -(pItem.ActualWidth - pItem.MinWidth));
            pItem.Width = pShape.X + horizontalDelta;

            // Calculate the translataion, based on the new width and the side that is not allowed to move.
            return CalculateTranslation(Orientation.Horizontal, leftIsRef, ref pItem, pShape, horizontalRef, pCanvasCoord);
        }

        /// <summary>
        /// Handle the vertical mouse movement. Set the new Height and calculate how much the coordinates must change for this movement.
        /// </summary>
        /// <param name="pVerticalChange">Size of the change</param>
        /// <param name="pItem">Item that is resized</param>
        /// <param name="pShape">Original sizes</param>
        /// <param name="pCenterCoord">Coords of the center</param>
        /// <param name="pCanvasCoord">Coords in the canvas</param>
        /// <returns>The translation that is needed to be perfromed on pItem.</returns>
        private Vector HandleVerticalChange(double pVerticalChange, ref FrameworkElement pItem, Vector pShape,
            Vector pCenterCoord, Vector pCanvasCoord)
        {
            var verticalDelta = 0.0;
            var topIsRef = true; // Top side must stay in position.
            switch (VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    verticalDelta = -pVerticalChange;
                    topIsRef = false;
                    break;
                case VerticalAlignment.Bottom:
                    verticalDelta = pVerticalChange;
                    topIsRef = true;
                    break;
                default:
                    return new Vector(0, 0);
            }

            // Calculate the the center coord of the side (top or bottom) that is not allowed to move
            var verticalRef = CalculateReference(Orientation.Vertical, topIsRef, ref pItem, pShape, pCenterCoord);

            // Change the Height
            verticalDelta = Math.Max(verticalDelta, -(pItem.ActualHeight - pItem.MinHeight));
            pItem.Height = pShape.Y + verticalDelta;

            // Calculate the new center position, based on the new height and the side that is not allowed to move.
            return CalculateTranslation(Orientation.Vertical, topIsRef, ref pItem, pShape, verticalRef, pCanvasCoord);

        }

        /// <summary>
        /// Calculate the center coordinate of the side that is not allowed to move.
        /// </summary>
        /// <param name="pOrientation">Horizontal for left or right, Vertical for top or bottom</param>
        /// <param name="pRefTopOrLeft">True if if the side has a lowe coordinate than the center. Thus is top or left.</param>
        /// <param name="pItem">Item that is resized.</param>
        /// <param name="pShape">Original shape of the item.</param>
        /// <param name="pCenterCoord">The center coord of the item.</param>
        /// <returns>The center coordinate of the side that is not allowed to move.</returns>
        private static Vector CalculateReference(Orientation pOrientation, bool pRefTopOrLeft, ref FrameworkElement pItem, Vector pShape,
            Vector pCenterCoord)
        {
            var referenceCoord = new Point(
                pOrientation == Orientation.Horizontal ? (pRefTopOrLeft ? -1 : 1) * pShape.X / 2 : 0,
                pOrientation == Orientation.Vertical   ? (pRefTopOrLeft ? -1 : 1) * pShape.Y / 2 : 0);
            referenceCoord = pItem.RenderTransform?.Transform(referenceCoord) ?? referenceCoord;
            return pCenterCoord + (Vector)referenceCoord;
        }

        /// <summary>
        /// Calculate how much the element must move in order to keep the specified side at the same position.
        /// </summary>
        /// <param name="pOrientation">Horizontal for left or right, Vertical for top or bottom</param>
        /// <param name="pRefTopOrLeft">True if if the side has a lowe coordinate than the center. Thus is top or left.</param>
        /// <param name="pItem">Item that is resized.</param>
        /// <param name="pShape">Original shape of the item.</param>
        /// <param name="pReference">Center coordinate of the side that stays at the same position.</param>
        /// <param name="pCanvasCoord">Coords in the canvas.</param>
        /// <returns></returns>
        private static Vector CalculateTranslation(Orientation pOrientation, bool pRefTopOrLeft, ref FrameworkElement pItem, Vector pShape,
            Vector pReference, Vector pCanvasCoord)
        {
            var refToCenterCoord = new Point(
                pOrientation == Orientation.Horizontal ? (pRefTopOrLeft ? 1 : -1) * pItem.Width / 2 : 0,
                pOrientation == Orientation.Vertical ? (pRefTopOrLeft ? 1 : -1) * pItem.Height / 2 : 0);
           
            refToCenterCoord = pItem.RenderTransform?.Transform(refToCenterCoord) ?? refToCenterCoord;
            var newCenter = (Vector)refToCenterCoord + pReference;

            return newCenter - new Vector(
                       (pOrientation == Orientation.Horizontal ? pItem.Width : pShape.X) / 2,
                       (pOrientation == Orientation.Vertical ? pItem.Height : pShape.Y) / 2) - pCanvasCoord;
        }
    }
}
