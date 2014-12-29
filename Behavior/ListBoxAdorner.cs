using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Xml2PdfDesigner.Behavior
{
    internal class ListBoxAdorner : Adorner
    {
        private readonly AdornerLayer _adornerLayer;

        public ListBoxAdorner(UIElement adornedElement, AdornerLayer adornerLayer)
            : base(adornedElement)
        {
            _adornerLayer = adornerLayer;
            _adornerLayer.Add(this);
        }

        public bool IsAboveElement { get; set; }

        /// <summary>
        ///     Update UI
        /// </summary>
        internal void Update()
        {
            _adornerLayer.Update(AdornedElement);
            Visibility = Visibility.Visible;
        }

        public void Remove()
        {
            Visibility = Visibility.Collapsed;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var adornedElementRect = new Rect(AdornedElement.DesiredSize);

            var renderBrush = new SolidColorBrush(Colors.Red) {Opacity = 0.5};
            var renderPen = new Pen(new SolidColorBrush(Colors.White), 1.5);
            const double renderRadius = 5.0;

            if (IsAboveElement)
            {
                drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius,
                    renderRadius);
                drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius,
                    renderRadius);
            }
            else
            {
                drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius,
                    renderRadius);
                drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius,
                    renderRadius);
            }
        }
    }
}