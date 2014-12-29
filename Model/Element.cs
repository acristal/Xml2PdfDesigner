using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xml2PdfDesigner.Behavior;

namespace Xml2PdfDesigner.Model
{
    /// <summary>
    ///     Type of the Element tag.
    /// </summary>
    public enum ElementType
    {
        Undefined,
        Page,
        Bookmark,
        Image,
        Include,
        Line,
        Loop,
        Rectangle,
        Table,
        Text
    }

    /// <summary>
    /// Exlude the Undefined and Page ElementType from the source
    /// </summary>
    public class ElementTypeItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            var values = (ElementType[]) Enum.GetValues(typeof (ElementType));
            var ret = new ItemCollection();

            foreach (ElementType type in values.Skip(2))
                ret.Add(type);
            return ret;
        }
    }

    /// <summary>
    ///     Base class for each Element object.
    /// </summary>
    public /*abstract*/ class Element : ObservableObject, IDropable, IDragable
    {
        private Coordinate _coordinate;
        private ObservableCollection<Element> _elements = new ObservableCollection<Element>();
        private string _name;
        private ElementType _type;

        public Element(Element parent)
        {
            Parent = parent;
        }

        public Element Parent { get; private set; }

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        [ItemsSource(typeof (ElementTypeItemsSource))]
        public ElementType Type
        {
            get { return _type; }
            set
            {
                if (Parent == null)
                    Set(ref _type, ElementType.Page);
                else
                    Set(ref _type, value);
            }
        }

        public Coordinate Coordinate
        {
            get { return _coordinate; }
            set
            {
                if (_coordinate != null)
                    _coordinate.PropertyChanged -= CoordinateOnPropertyChanged;

                Set(ref _coordinate, value);

                if (_coordinate != null)
                    _coordinate.PropertyChanged += CoordinateOnPropertyChanged;
            }
        }

        public ObservableCollection<Element> Elements
        {
            get { return _elements; }
            set { Set(ref _elements, value); }
        }

        #region IDropable, IDragable members

        /// <summary>
        ///     Remove the element from the Parent collection.
        /// </summary>
        /// <param name="data">this object</param>
        public void Remove(object data)
        {
            // We cannot move the root element
            if (Type == ElementType.Page || Parent == null)
                return;
            Parent.Elements.Remove(this);
            Parent = null;
        }

        public Type DataType
        {
            get { return typeof (Element); }
        }

        /// <summary>
        ///     Element is drop onto this object. We add it to the element collection.
        /// </summary>
        /// <param name="data">Dropped element</param>
        /// <param name="index">Index</param>
        public void Drop(object data, int index = -1)
        {
            var element = data as Element;

            if (element == null || this == element || element.Type == ElementType.Page)
                return;

            Elements.Insert(index < 0 ? Elements.Count : index, element);
            element.Parent = this;
        }

        #endregion

        /// <summary>
        ///     Name of the Element
        /// </summary>
        /// <returns>The same result as the Name property of the element</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        ///     Propagation of the Coordinate inner Set
        /// </summary>
        private void CoordinateOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged(() => Coordinate);
        }
    }

    /// <summary>
    ///     Observable Coordinate
    /// </summary>
    public class Coordinate : ObservableObject
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;

        public float X
        {
            get { return _x; }
            set { Set(ref _x, value); }
        }

        public float Y
        {
            get { return _y; }
            set { Set(ref _y, value); }
        }

        public float Width
        {
            get { return _width; }
            set { Set(ref _width, value); }
        }

        public float Height
        {
            get { return _height; }
            set { Set(ref _height, value); }
        }
    }
}