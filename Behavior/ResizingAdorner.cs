using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Xml2PdfDesigner.Model;

namespace Xml2PdfDesigner.Behavior
{
    /// <summary>
    /// A simple Resizing Adorner, that allows the user
    /// to resize the Adorned element using 4 corner thumbs
    /// </summary>
    public class ResizingAdorner : Adorner
    {
        #region Data

        // Resizing adorner uses Thumbs for visual elements.
        // The Thumbs have built-in mouse input handling.

        private readonly Thumb _topLeft;
        private readonly Thumb _topRight;
        private readonly Thumb _bottomLeft;
        private readonly Thumb _bottomRight;

        private Element _attachedElement;

        // To store and manage the adorner’s visual children.
        private readonly VisualCollection _visualChildren;

        #endregion Data

        #region Ctor

        // Initialize the ResizingAdorner.
        public ResizingAdorner(UIElement adornedElement, Element element)
            : base(adornedElement)
        {
            _attachedElement = element;

            _visualChildren = new VisualCollection(this);

            // Call a helper method to initialize the Thumbs
            // with a customized cursors.
            //BuildAdornerCorner(ref _topLeft, Cursors.SizeNWSE);
            //BuildAdornerCorner(ref _topRight, Cursors.SizeNESW);
            BuildAdornerCorner(ref _bottomLeft, Cursors.SizeNESW);
            BuildAdornerCorner(ref _bottomRight, Cursors.SizeNWSE);

            // Add handlers for resizing.
            _bottomLeft.DragDelta += HandleBottomLeft;
            _bottomRight.DragDelta += HandleBottomRight;
            //_topLeft.DragDelta += HandleTopLeft;
            //_topRight.DragDelta += HandleTopRight;
        }

        #endregion Ctor

        #region Private Methods

        // Handler for resizing from the bottom-right.
        private void HandleBottomRight(object sender, DragDeltaEventArgs args)
        {
            var adornedElement = AdornedElement as FrameworkElement;
            var hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it’s larger
            // than the width or height of an adorner, respectively.

            var parentCoordinate = _attachedElement.Parent.AbsoluteCoordinate;

            var newWidth = (float)Math.Min(Math.Max(adornedElement.Width + args.HorizontalChange, hitThumb.DesiredSize.Width), parentCoordinate.Width - _attachedElement.AbsoluteCoordinate.X);
            newWidth = newWidth / parentCoordinate.Width * 100;
            _attachedElement.Coordinate.Width = newWidth;

            var newHeight = (float)Math.Min(Math.Max(adornedElement.Height + args.VerticalChange, hitThumb.DesiredSize.Height), parentCoordinate.Height - _attachedElement.AbsoluteCoordinate.Y);
            newHeight = newHeight / parentCoordinate.Height * 100;
            _attachedElement.Coordinate.Height = newHeight;

            //adornedElement.Width = Math.Max(adornedElement.Width + args.HorizontalChange,
            //    hitThumb.DesiredSize.Width);
            //adornedElement.Height = Math.Max(args.VerticalChange + adornedElement.Height,
            //    hitThumb.DesiredSize.Height);
        }

        // Handler for resizing from the bottom-left.
        private void HandleBottomLeft(object sender, DragDeltaEventArgs args)
        {
            var adornedElement = AdornedElement as FrameworkElement;
            var hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it’s larger
            // than the width or height of an adorner, respectively.
            var parentCoordinate = _attachedElement.Parent.AbsoluteCoordinate;

            var newWidth = (float)Math.Min(Math.Max(adornedElement.Width - args.HorizontalChange, hitThumb.DesiredSize.Width), parentCoordinate.Width - _attachedElement.AbsoluteCoordinate.X);
            newWidth = newWidth / parentCoordinate.Width * 100;
            _attachedElement.Coordinate.X -= newWidth - _attachedElement.Coordinate.Width;
            _attachedElement.Coordinate.Width = newWidth;

            var newHeight = (float)Math.Min(Math.Max(adornedElement.Height + args.VerticalChange, hitThumb.DesiredSize.Height), parentCoordinate.Height - _attachedElement.AbsoluteCoordinate.Y);
            newHeight = newHeight / parentCoordinate.Height * 100;
            _attachedElement.Coordinate.Height = newHeight;

            //adornedElement.Width = Math.Max(adornedElement.Width - args.HorizontalChange,
            //    hitThumb.DesiredSize.Width);

            //adornedElement.Height = Math.Max(args.VerticalChange + adornedElement.Height,
            //    hitThumb.DesiredSize.Height);
        }

        // Handler for resizing from the top-right.
        private void HandleTopRight(object sender, DragDeltaEventArgs args)
        {
            var adornedElement = AdornedElement as FrameworkElement;
            var hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it’s larger
            // than the width or height of an adorner, respectively.
            adornedElement.Width = Math.Max(adornedElement.Width + args.HorizontalChange,
                hitThumb.DesiredSize.Width);

            adornedElement.Height = Math.Max(adornedElement.Height - args.VerticalChange,
                hitThumb.DesiredSize.Height);
        }

        // Handler for resizing from the top-left.
        private void HandleTopLeft(object sender, DragDeltaEventArgs args)
        {
            var adornedElement = AdornedElement as FrameworkElement;
            var hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it’s larger
            // than the width or height of an adorner, respectively.
            adornedElement.Width = Math.Max(adornedElement.Width - args.HorizontalChange,
                hitThumb.DesiredSize.Width);

            adornedElement.Height = Math.Max(adornedElement.Height - args.VerticalChange,
                hitThumb.DesiredSize.Height);
        }

        // Helper method to instantiate the corner Thumbs, set the Cursor property,
        // set some appearance properties, and add the elements to the visual tree.
        private void BuildAdornerCorner(ref Thumb cornerThumb, Cursor customizedCursor)
        {
            if (cornerThumb != null) return;

            cornerThumb = new Thumb { Cursor = customizedCursor };

            // Set some arbitrary visual characteristics.
            cornerThumb.Height = cornerThumb.Width = 10;
            cornerThumb.Opacity = 0.40;
            cornerThumb.Background = new SolidColorBrush(Colors.MediumBlue);

            _visualChildren.Add(cornerThumb);
        }

        // This method ensures that the Widths and Heights are initialized.
        // Sizing to content produces Width and Height values of Double.NaN.
        // Because this Adorner explicitly resizes, the Width and Height
        // need to be set first.  It also sets the maximum size of the adorned element.
        private void EnforceSize(FrameworkElement adornedElement)
        {
            if (adornedElement.Width.Equals(Double.NaN))
                adornedElement.Width = adornedElement.DesiredSize.Width;
            if (adornedElement.Height.Equals(Double.NaN))
                adornedElement.Height = adornedElement.DesiredSize.Height;

            var parent = adornedElement.Parent as FrameworkElement;
            if (parent != null)
            {
                adornedElement.MaxHeight = parent.ActualHeight;
                adornedElement.MaxWidth = parent.ActualWidth;
            }
        }

        #endregion Private Methods

        #region Overrides

        // Arrange the Adorners.
        protected override Size ArrangeOverride(Size finalSize)
        {
            // desiredWidth and desiredHeight are the width and height of the element
            //that’s being adorned.
            // These will be used to place the ResizingAdorner at the corners of the
            //adorned element.
            var desiredWidth = AdornedElement.DesiredSize.Width;
            var desiredHeight = AdornedElement.DesiredSize.Height;
            // adornerWidth & adornerHeight are used for placement as well.
            var adornerWidth = DesiredSize.Width;
            var adornerHeight = DesiredSize.Height;

            //_topLeft.Arrange(new Rect(-adornerWidth / 2, -adornerHeight / 2,
            //    adornerWidth, adornerHeight));

            //_topRight.Arrange(new Rect(desiredWidth - adornerWidth / 2,
            //    -adornerHeight / 2, adornerWidth, adornerHeight));

            _bottomLeft.Arrange(new Rect(-adornerWidth / 2,
                desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));

            _bottomRight.Arrange(new Rect(desiredWidth - adornerWidth / 2,
                desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));

            // Return the final size.
            return finalSize;
        }

        // Override the VisualChildrenCount and GetVisualChild properties to interface with
        // the adorner’s visual collection.
        protected override int VisualChildrenCount { get { return _visualChildren.Count; } }

        protected override Visual GetVisualChild(int index)
        {
            return _visualChildren[index];
        }

        #endregion Overrides
    }
}