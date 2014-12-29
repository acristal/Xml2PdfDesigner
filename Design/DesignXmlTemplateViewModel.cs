using GalaSoft.MvvmLight;
using Xml2PdfDesigner.Model;

namespace Xml2PdfDesigner.Design
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class DesignXmlTemplateViewModel : ViewModelBase
    {
        private Template _template;

        /// <summary>
        ///     Current template
        /// </summary>
        public Template Template
        {
            get { return _template ?? (_template = new DesignTemplateService().ParseTemplateFrom("dummy")); }
        }

        /// <summary>
        ///     Current selected element
        /// </summary>
        public Element SelectedElement
        {
            get { return Template.Elements[0]; }
        }

        /// <summary>
        ///     Absolute path to the XML document defining the template.
        /// </summary>
        public string TemplatePath
        {
            get { return "Temporary path"; }
        }
    }
}