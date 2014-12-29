using GalaSoft.MvvmLight.Threading;

namespace Xml2PdfDesigner
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }
    }
}