using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Xml2PdfDesigner.Behavior
{
    public class FrameworkElementDragBehavior : Behavior<FrameworkElement>
    {
        private bool _isMouseClicked;

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseClicked = true;
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseClicked = false;
        }

        private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            if (_isMouseClicked)
            {
                //set the item's DataContext as the data to be transferred
                var dragObject = AssociatedObject.DataContext as IDragable;
                if (dragObject != null)
                {
                    var data = new DataObject();
                    data.SetData(dragObject.DataType, AssociatedObject.DataContext);
                    DragDrop.DoDragDrop(AssociatedObject, data, DragDropEffects.Move);
                }
            }
            _isMouseClicked = false;
        }
    }
}