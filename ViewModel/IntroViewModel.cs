using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using Xml2PdfDesigner.Model;

namespace Xml2PdfDesigner.ViewModel
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class IntroViewModel : ViewModelBase
    {
        private readonly IIOService _ioService;

        /// <summary>
        ///     Initializes a new instance of the IntroViewModel class.
        /// </summary>
        public IntroViewModel(IIOService ioService)
        {
            _ioService = ioService;
        }

        /// <summary>
        ///     Open the template and load it.
        /// </summary>
        /// <param name="documentPath">Path to the xml document</param>
        private void OpenDocument(string documentPath)
        {
            ViewModelLocator.Current.Main.CurrentViewModel = ViewModelLocator.Current.XmlTemplate;
            ViewModelLocator.Current.XmlTemplate.TemplatePath = documentPath;
        }

        #region Open Command

        private RelayCommand _openCommand;

        /// <summary>
        ///     Gets the OpenCommand.
        /// </summary>
        public RelayCommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new RelayCommand(ExecuteOpenCommand)); }
        }

        /// <summary>
        ///     Open a file dialog to load a file
        /// </summary>
        private void ExecuteOpenCommand()
        {
            var file = _ioService.OpenFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            if (file != null)
                OpenDocument(file);
        }

        #endregion

        #region Create Command

        private RelayCommand _createCommand;

        /// <summary>
        ///     Gets the CreateCommand.
        /// </summary>
        public RelayCommand CreateCommand
        {
            get { return _createCommand ?? (_createCommand = new RelayCommand(ExecuteCreateCommand)); }
        }

        /// <summary>
        ///     Open a file dialog to create a file
        /// </summary>
        private void ExecuteCreateCommand()
        {
            var file = _ioService.CreateFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            if (file != null)
                OpenDocument(file);
        }

        #endregion
    }
}