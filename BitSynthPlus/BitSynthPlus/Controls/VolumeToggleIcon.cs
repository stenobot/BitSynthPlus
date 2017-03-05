using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace BitSynthPlus.Controls
{
    public class VolumeToggleIcon : BitmapIcon
    {
        Uri volumeHighIconUri = new Uri("ms-appx:///Assets/PixArt/volume-high.png");
        Uri volumeLowIconUri = new Uri("ms-appx:///Assets/PixArt/volume-low.png");
        Uri volumeOffIconUri = new Uri("ms-appx:///Assets/PixArt/volume-off.png");

        SolidColorBrush disabledColor = Application.Current.Resources["BitSynthDisabledBrush"] as SolidColorBrush;
        SolidColorBrush accentColor = Application.Current.Resources["BitSynthAccentColorBrush"] as SolidColorBrush;

        private Volume _volumeLevel;

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(ToggleIcon), new PropertyMetadata(false));


        public Volume VolumeLevel
        {
            get { return _volumeLevel; }
            set
            {
                _volumeLevel = value;
                SetVolumeIcons();
            }
        }


        public VolumeToggleIcon()
        {
            _volumeLevel = Volume.Off;
        }


        private void SetVolumeIcons()
        {
            switch (_volumeLevel)
            {
                case Volume.Off:
                    UriSource = volumeOffIconUri;
                    Foreground = disabledColor;
                    break;
                case Volume.Low:
                    UriSource = volumeLowIconUri;
                    Foreground = accentColor;
                    break;
                case Volume.High:
                    UriSource = volumeHighIconUri;
                    Foreground = accentColor;
                    break;
            }
        }
    }
}
