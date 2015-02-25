using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace Xml2PdfDesigner.Behavior
{
    /// <summary>
    ///     A simple Resizing Behavior that makes use
    ///     of a ResizingAdorner
    /// </summary>
    public class DragBehavior : Behavior<UIElement>
    {
        #region Data

        private UIElement _attachedElement;
        private bool _isDragging;
        private Point _lastPosition;
        private Window _parent;

        #endregion Data

        #region Behaviour Overrides

        protected override void OnAttached()
        {
            _attachedElement = AssociatedObject;
            _parent = Application.Current.MainWindow;

            _attachedElement.MouseLeftButtonDown += MouseIsDown;
            _attachedElement.MouseLeftButtonUp += MouseIsUp;
            _attachedElement.MouseMove += MouseIsMoving;
        }

        #endregion Behaviour Overrides

        #region Private Methods

        private void MouseIsMoving(object sender, MouseEventArgs e)
        {
            if (!_isDragging)
                return;

            Point currentPosition = e.GetPosition(_parent);

            double dX = currentPosition.X - _lastPosition.X;
            double dY = currentPosition.Y - _lastPosition.Y;

            _lastPosition = currentPosition;

            Transform oldTransform = _attachedElement.RenderTransform;
            var rt = new TransformGroup();
            var newPos = new TranslateTransform { X = dX, Y = dY };

            if (oldTransform != null)
            {
                rt.Children.Add(oldTransform);
            }
            rt.Children.Add(newPos);

            var mt = new MatrixTransform { Matrix = rt.Value };

            if (currentPosition.X < 0 || currentPosition.Y < 0)
                return;

            _attachedElement.RenderTransform = mt;
        }

        private void MouseIsUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;

            _attachedElement.ReleaseMouseCapture();
        }

        private void MouseIsDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _lastPosition = e.GetPosition(_parent);
            _attachedElement.CaptureMouse();
        }

        #endregion Private Methods
    }
}