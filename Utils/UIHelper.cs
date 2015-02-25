using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Xml2PdfDesigner.Utils
{
    public static class UIHelper
    {
        public static T GetVisualAncestor<T>(this DependencyObject d) where T : class
        {
            DependencyObject item = VisualTreeHelper.GetParent(d);

            while (item != null)
            {
                T itemAsT = item as T;
                if (itemAsT != null) return itemAsT;
                item = VisualTreeHelper.GetParent(item);
            }

            return null;
        }

        public static DependencyObject GetVisualAncestor(this DependencyObject d, Type type)
        {
            DependencyObject item = VisualTreeHelper.GetParent(d);

            while (item != null)
            {
                //if (item.GetType().IsAssignableFrom(type)) return item;
                if (item.GetType() == type) return item;
                item = VisualTreeHelper.GetParent(item);
            }

            return null;
        }

        public static T GetVisualDescendent<T>(this DependencyObject d) where T : DependencyObject
        {
            return d.GetVisualDescendents<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetVisualDescendents<T>(this DependencyObject d) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(d);

            for (int n = 0; n < childCount; n++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(d, n);

                if (child is T)
                {
                    yield return (T)child;
                }

                foreach (T match in GetVisualDescendents<T>(child))
                {
                    yield return match;
                }
            }
        }

        /// <summary>
        ///     Get the UIElement that is in the container at the point specified
        /// </summary>
        /// <param name="container"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static UIElement GetUIElement(ItemsControl container, Point position)
        {
            var elementAtPosition = container.InputHitTest(position) as UIElement;
            //move up the UI tree until you find the actual UIElement that is the Item of the container
            if (elementAtPosition == null)
                return null;

            while (elementAtPosition != null)
            {
                object testUIElement = container.ItemContainerGenerator.ItemFromContainer(elementAtPosition);
                if (testUIElement != DependencyProperty.UnsetValue) //if found the UIElement
                {
                    return elementAtPosition;
                }
                elementAtPosition = VisualTreeHelper.GetParent(elementAtPosition) as UIElement;
            }
            return null;
        }

        /// <summary>
        ///     Determines if the relative position is above the UIElement in the coordinate
        /// </summary>
        /// <param name="i"></param>
        /// <param name="relativePosition"></param>
        /// <returns></returns>
        internal static bool IsPositionAboveElement(UIElement i, Point relativePosition)
        {
            return relativePosition.Y < ((FrameworkElement)i).ActualHeight / 2;
        }
    }
}