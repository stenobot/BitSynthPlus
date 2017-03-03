using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace BitSynthPlus.Controls
{
    public class ToggleIcon: BitmapIcon
    {
        SolidColorBrush disabledColor = Application.Current.Resources["BitSynthDisabledBrush"] as SolidColorBrush;
        SolidColorBrush whiteColor = new SolidColorBrush(Colors.White);

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(ToggleIcon), new PropertyMetadata(false));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set
            {
                SetValue(IsEnabledProperty, value);
                ChangeIsEnabledProperty();
            }
        }

        public ToggleIcon()
        {
            IsEnabled = false;
        }


        private void ChangeIsEnabledProperty()
        {
            if (IsEnabled)
                Foreground = whiteColor;
            else
                Foreground = disabledColor;
        }
    }   
}
