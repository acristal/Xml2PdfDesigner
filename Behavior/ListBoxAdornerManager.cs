using System.Windows;
using System.Windows.Documents;

namespace Xml2PdfDesigner.Behavior
{
    internal class ListBoxAdornerManager
    {
        private readonly AdornerLayer _adornerLayer;
        private ListBoxAdorner _adorner;

        private bool _shouldCreateNewAdorner;

        internal ListBoxAdornerManager(AdornerLayer layer)
        {
            _adornerLayer = layer;
        }

        internal void Update(UIElement adornedElement, bool isAboveElement)
        {
            if (_adorner != null && !_shouldCreateNewAdorner)
            {
                //exit if nothing changed
                if (_adorner.AdornedElement == adornedElement && _adorner.IsAboveElement == isAboveElement)
                    return;
            }
            Clear();
            //draw new adorner
            _adorner = new ListBoxAdorner(adornedElement, _adornerLayer) {IsAboveElement = isAboveElement};
            _adorner.Update();
            _shouldCreateNewAdorner = false;
        }


        /// <summary>
        ///     Remove the adorner
        /// </summary>
        internal void Clear()
        {
            if (_adorner != null)
            {
                _adorner.Remove();
                _shouldCreateNewAdorner = true;
            }
        }
    }
}