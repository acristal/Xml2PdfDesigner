using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Xml2PdfDesigner.Model;

namespace Xml2PdfDesigner.Behavior
{
    /// <summary>
    /// A simple Resizing Behavior that makes use
    /// of a ResizingAdorner
    /// </summary>
    public class ResizeBehavior : Behavior<UIElement>
    {
        #region Data

        private AdornerLayer _adornerLayer;
        private FrameworkElement _fe;
        private UIElement _attachedElement;

        #endregion Data

        #region AttachedElement

        public static Element GetAttachedElement(ResizeBehavior behavior)
        {
            return (Element)behavior.GetValue(AttachedElementProperty);
        }

        public static void SetAttachedElement(ResizeBehavior behavior, Element attachedElement)
        {
            behavior.SetValue(AttachedElementProperty, attachedElement);
        }

        public static readonly DependencyProperty AttachedElementProperty =
            DependencyProperty.Register(
                "AttachedElement",
                typeof(Element),
                typeof(ResizeBehavior),
                new FrameworkPropertyMetadata(null));

        #endregion AttachedElement

        #region Behaviour Overrides

        protected override void OnAttached()
        {
            _attachedElement = AssociatedObject;
            _fe = _attachedElement as FrameworkElement;

            if (_fe != null && _fe.Parent != null)
            {
                var frameworkElement = _fe.Parent as FrameworkElement;
                if (frameworkElement != null)
                    frameworkElement.Loaded += ResizeBehaviorParent_Loaded;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            _adornerLayer = null;
        }

        #endregion Behaviour Overrides

        #region Private Methods

        /// <summary>
        /// Create the AdornerLayer when Parent for current Element loads
        /// </summary>
        private void ResizeBehaviorParent_Loaded(object sender, RoutedEventArgs e)
        {
            if (_adornerLayer == null)
                _adornerLayer = AdornerLayer.GetAdornerLayer(sender as Visual);
            _attachedElement.MouseEnter += AttachedElement_MouseEnter;
        }

        /// <summary>
        /// When mouse enters, create a new Resizing Adorner
        /// </summary>
        private void AttachedElement_MouseEnter(object sender, MouseEventArgs e)
        {
            var attachedElement = GetAttachedElement(this);

            // We cannot resize the page
            if (attachedElement == null || attachedElement.Parent == null)
                return;
            var resizingAdorner = new ResizingAdorner(sender as UIElement, attachedElement);
            resizingAdorner.MouseLeave += ResizingAdorner_MouseLeave;
            _adornerLayer.Add(resizingAdorner);
        }

        /// <summary>
        /// On mouse leave for the Resizing Adorner, remove the Resizing Adorner
        /// from the AdornerLayer
        /// </summary>
        private void ResizingAdorner_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender != null)
            {
                _adornerLayer.Remove(sender as ResizingAdorner);
            }
        }

        #endregion Private Methods
    }
}