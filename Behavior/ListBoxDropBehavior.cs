using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interactivity;
using Xml2PdfDesigner.Utils;

namespace Xml2PdfDesigner.Behavior
{
    /// <summary>
    ///     For enabling Drop on ItemsControl
    /// </summary>
    public class ListBoxDropBehavior : Behavior<ItemsControl>
    {
        private Type _dataType; //the type of the data that can be dropped into this control
        private ListBoxAdornerManager _insertAdornerManager;

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
            //if the data type can be dropped 
            if (_dataType != null)
            {
                if (e.Data.GetDataPresent(_dataType))
                {
                    //first find the UIElement that it was dropped over, then we determine if it's 
                    //dropped above or under the UIElement, then insert at the correct index.
                    var dropContainer = sender as ItemsControl;
                    //get the UIElement that was dropped over
                    UIElement droppedOverItem = UIHelper.GetUIElement(dropContainer, e.GetPosition(dropContainer));
                    int dropIndex = -1; //the location where the item will be dropped
                    dropIndex = dropContainer.ItemContainerGenerator.IndexFromContainer(droppedOverItem) + 1;
                    //find if it was dropped above or below the index item so that we can insert 
                    //the item in the correct place
                    if (UIHelper.IsPositionAboveElement(droppedOverItem, e.GetPosition(droppedOverItem))) //if above
                    {
                        dropIndex = dropIndex - 1; //we insert at the index above it
                    }
                    //remove the data from the source
                    var source = e.Data.GetData(_dataType) as IDragable;
                    source.Remove(e.Data.GetData(_dataType));

                    //drop the data
                    var target = AssociatedObject.DataContext as IDropable;
                    target.Drop(e.Data.GetData(_dataType), dropIndex);
                }
            }
            if (_insertAdornerManager != null)
                _insertAdornerManager.Clear();
            e.Handled = true;
        }

        private void AssociatedObject_DragLeave(object sender, DragEventArgs e)
        {
            if (_insertAdornerManager != null)
                _insertAdornerManager.Clear();
            e.Handled = true;
        }

        private void AssociatedObject_DragOver(object sender, DragEventArgs e)
        {
            if (_dataType != null)
            {
                if (e.Data.GetDataPresent(_dataType))
                {
                    SetDragDropEffects(e);
                    if (_insertAdornerManager != null)
                    {
                        var dropContainer = sender as ItemsControl;
                        UIElement droppedOverItem = UIHelper.GetUIElement(dropContainer, e.GetPosition(dropContainer));
                        bool isAboveElement = UIHelper.IsPositionAboveElement(droppedOverItem,
                            e.GetPosition(droppedOverItem));
                        _insertAdornerManager.Update(droppedOverItem, isAboveElement);
                    }
                }
            }
            e.Handled = true;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (_dataType == null)
            {
                //if the DataContext implements IDropable, record the data type that can be dropped
                if (AssociatedObject.DataContext != null)
                {
                    if (AssociatedObject.DataContext as IDropable != null)
                    {
                        _dataType = ((IDropable) AssociatedObject.DataContext).DataType;
                    }
                }
            }
            //initialize adorner manager with the adorner layer of the itemsControl
            if (_insertAdornerManager == null)
                _insertAdornerManager = new ListBoxAdornerManager(AdornerLayer.GetAdornerLayer(sender as ItemsControl));

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