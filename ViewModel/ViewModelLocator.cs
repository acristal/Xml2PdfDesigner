/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Xml2PdfDesigner.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using Xml2PdfDesigner.Design;
using Xml2PdfDesigner.Model;

namespace Xml2PdfDesigner.ViewModel
{
    /// <summary>
    ///     This class contains static references to all the view models in the
    ///     application and provides an entry point for the bindings.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IIOService, DesignIOService>();
                SimpleIoc.Default.Register<ITemplateService, DesignTemplateService>();
            }
            else
            {
                SimpleIoc.Default.Register<IIOService, IOService>();
                SimpleIoc.Default.Register<ITemplateService, TemplateService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<IntroViewModel>();
            SimpleIoc.Default.Register<XmlTemplateViewModel>();
        }

        /// <summary>
        ///     Get the ViewModelLocator from the Resources of the application
        /// </summary>
        public static ViewModelLocator Current
        {
            get { return (ViewModelLocator)Application.Current.Resources["Locator"]; }
        }

        /// <summary>
        ///     Gets the Main property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        /// <summary>
        ///     Gets the XmlTemplate property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public XmlTemplateViewModel XmlTemplate
        {
            get { return ServiceLocator.Current.GetInstance<XmlTemplateViewModel>(); }
        }

        /// <summary>
        ///     Gets the Intro property.
        /// </summary>
        [SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public IntroViewModel Intro
        {
            get { return ServiceLocator.Current.GetInstance<IntroViewModel>(); }
        }

        /// <summary>
        ///     Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}