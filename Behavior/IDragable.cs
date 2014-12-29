using System;

namespace Xml2PdfDesigner.Behavior
{
    internal interface IDragable
    {
        /// <summary>
        ///     Type of the data item
        /// </summary>
        Type DataType { get; }

        /// <summary>
        ///     Remove the object from the collection
        /// </summary>
        void Remove(object i);
    }
}