using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace Xml2PdfDesigner.Model
{
    public class Template : ObservableObject
    {
        private ObservableCollection<Element> _elements = new ObservableCollection<Element>();
        private string _name;

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public ObservableCollection<Element> Elements
        {
            get { return _elements; }
            set { Set(ref _elements, value); }
        }
    }
}