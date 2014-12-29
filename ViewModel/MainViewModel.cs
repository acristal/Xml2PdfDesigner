using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Xml2PdfDesigner.ViewModel
{
    /// <summary>
    ///     This class contains properties that the main View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModelModel;

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _currentViewModelModel = ViewModelLocator.Current.Intro;
        }

        /// <summary>
        ///     Get the OpenCommand from the IntroViewModel
        /// </summary>
        public RelayCommand OpenCommand
        {
            get { return ViewModelLocator.Current.Intro.OpenCommand; }
        }

        /// <summary>
        ///     Get the CreateCommand from the IntroViewModel
        /// </summary>
        public RelayCommand CreateCommand
        {
            get { return ViewModelLocator.Current.Intro.CreateCommand; }
        }

        /// <summary>
        ///     Sets and gets the CurrentViewModel property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get { return _currentViewModelModel; }

            set { Set(ref _currentViewModelModel, value); }
        }
    }
}