using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Xml2PdfDesigner.Behavior
{
    public class FrameworkElementDropBehavior : Behavior<FrameworkElement>
    {
        private FrameworkElementAdorner _adorner;
        private Type _dataType; //the type of the data that can be dropped into this control

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.AllowDrop = true;
            AssociatedObject.DragEnter += AssociatedObject_DragEnter;
            AssociatedObject.DragOver += AssociatedObject_DragOver;
            AssociatedObject.DragLeave += AssociatedObject_DragLeave;
            AssociatedObject.Drop += AssociatedObject_Drop;
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (_dataType != null)
            {
                //if the data type can be dropped 
                if (e.Data.GetDataPresent(_dataType))
                {
                    var source = e.Data.GetData(_dataType) as IDragable;
                    var target = AssociatedObject.DataContext as IDropable;

                    if (source != target && source != null && target != null)
                    {
                        //remove the data from the source
                        source.Remove(e.Data.GetData(_dataType));

                        //drop the data
                        target.Drop(e.Data.GetData(_dataType));                        
                    }
                }
            }
            if (_adorner != null)
                _adorner.Remove();

            e.Handled = true;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            if (_adorner != null)
                _adorner.Remove();
            e.Handled = true;
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (_dataType != null)
            {
                //if item can be dropped
                if (e.Data.GetDataPresent(_dataType))
                {
                    //give mouse effect
                    SetDragDropEffects(e);
                    //draw the dots
                    if (_adorner != null)
                        _adorner.Update();
                }
            }
            e.Handled = true;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            //if the DataContext implements IDropable, record the data type that can be dropped
            if (_dataType == null)
            {
                if (AssociatedObject.DataContext != null)
                {
                    var dropObject = AssociatedObject.DataContext as IDropable;
                    if (dropObject != null)
                    {
                        _dataType = dropObject.DataType;
                    }
                }
            }

            if (_adorner == null)
                _adorner = new FrameworkElementAdorner(sender as UIElement);
            e.Handled = true;
        }

        /// <summary>
        ///     Provides feedback on if the data can be dropped
        /// </summary>
        /// <param name="e"></param>
        private void SetDragDropEffects(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None; //default to None

            //if the data type can be dropped 
            if (e.Data.GetDataPresent(_dataType))
            {
                e.Effects = DragDropEffects.Move;
            }
        }
    }
}