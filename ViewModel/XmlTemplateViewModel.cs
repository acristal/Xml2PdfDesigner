using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xml2PdfDesigner.Model;

namespace Xml2PdfDesigner.ViewModel
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class XmlTemplateViewModel : ViewModelBase
    {
        private readonly ITemplateService _templateService;
        private Element _selectedElement;
        private string _templatePath;

        /// <summary>
        ///     Initializes a new instance of the XmlTemplateViewModel class.
        /// </summary>
        public XmlTemplateViewModel(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        /// <summary>
        ///     Template being edited
        /// </summary>
        public Template Template { get; private set; }

        /// <summary>
        ///     Current selected element
        /// </summary>
        public Element SelectedElement
        {
            get { return _selectedElement; }
            set { Set(ref _selectedElement, value); }
        }

        /// <summary>
        ///     Absolute path to the XML document defining the template.
        /// </summary>
        public string TemplatePath
        {
            get { return _templatePath; }
            set
            {
                Set(ref _templatePath, value);
                UpdateTemplate();
                RaisePropertyChanged(() => Template);
            }
        }

        /// <summary>
        ///     Reload and parse the Xml Template from the current TemplatePath
        /// </summary>
        private void UpdateTemplate()
        {
            Template = _templateService.ParseTemplateFrom(TemplatePath);
        }

        /// <summary>
        ///     Update the selected element of the view model
        /// </summary>
        private void SetSelectedElement(Element selectedElement)
        {
            SelectedElement = selectedElement;
        }

        #region Selected item changed Command

        private RelayCommand<Element> _selectedCommand;

        /// <summary>
        ///     Gets the OpenCommand.
        /// </summary>
        public ICommand SelectedCommand
        {
            get { return _selectedCommand ?? (_selectedCommand = new RelayCommand<Element>(SetSelectedElement)); }
        }

        #endregion
    }
}