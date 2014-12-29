using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Xml2PdfDesigner.Model
{
    /// <summary>
    ///     Type of the Element tag.
    /// </summary>
    public enum ElementType
    {
        Undefined,
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
    ///     Base class for each Element object.
    /// </summary>
    public /*abstract*/ class Element : ObservableObject
    {
        private Coordinate _coordinate;
        private ObservableCollection<Element> _elements = new ObservableCollection<Element>();
        private string _name;
        private ElementType _type;

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public ElementType Type
        {
            get { return _type; }
            set { Set(ref _type, value); }
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
        private int _x;
        private int _y;

        public int X
        {
            get { return _x; }
            set { Set(ref _x, value); }
        }

        public int Y
        {
            get { return _y; }
            set { Set(ref _y, value); }
        }
    }
}