using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Xml2PdfDesigner.Model;

namespace Xml2PdfDesigner.Converter
{
    /// <summary>
    /// Convert an ElementType to its related BitmapImage.
    /// ConvertBack is not implemented.
    /// </summary>
    public class ElementTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var elementType = value as ElementType? ?? ElementType.Undefined;

            switch (elementType)
            {
                case ElementType.Bookmark:
                    return Application.Current.Resources["EBookmarkImage"];
                case ElementType.Image:
                    return Application.Current.Resources["EImageImage"];
                case ElementType.Include:
                    return Application.Current.Resources["EIncludeImage"];
                case ElementType.Line:
                    return Application.Current.Resources["ELineImage"];
                case ElementType.Loop:
                    return Application.Current.Resources["ELoopImage"];
                case ElementType.Rectangle:
                    return Application.Current.Resources["ERectangleImage"];
                case ElementType.Table:
                    return Application.Current.Resources["ETableImage"];
                case ElementType.Text:
                    return Application.Current.Resources["ETextImage"];
                default:
                    return Application.Current.Resources["EUndefinedImage"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}