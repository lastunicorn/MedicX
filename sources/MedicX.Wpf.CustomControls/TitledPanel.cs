using System.Windows;
using System.Windows.Controls;

namespace DustInTheWind.MedicX.Wpf.CustomControls
{
    public class TitledPanel : ContentControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(TitledPanel));

        public object Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        static TitledPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitledPanel), new FrameworkPropertyMetadata(typeof(TitledPanel)));
        }
    }
}