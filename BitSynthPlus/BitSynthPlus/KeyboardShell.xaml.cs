using BitSynthPlus.Controls;
using BitSynthPlus.DataModel;
using BitSynthPlus.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Audio;
using Windows.Media.MediaProperties;
using Windows.Media.Render;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BitSynthPlus
{
    // We are initializing a COM interface for use within the namespace
    // This interface allows access to memory at the byte level which we need to populate audio data that is generated
    //[ComImport]
    //[Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    //[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]

    //unsafe interface IMemoryBufferByteAccess
    //{
    //    void GetBuffer(out byte* buffer, out uint capacity);
    //}


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class KeyboardShell : Page
    {
        static readonly double masterVolumeDefaultVal = (double)Application.Current.Resources["MasterVolumeDefault"];

        private SoundPlayer soundPlayer;

        private List<ToggleButton> VolumeToggles;
        private List<BitmapIcon> VolumeIcons;

        private List<ToggleButton> AllPOneToggles;
        private List<ToggleButton> AllPTwoToggles;
        private List<ToggleButton> AllWOneToggles;
        private List<ToggleButton> AllWTwoToggles;

        private List<List<ToggleButton>> AllTogglesLists;

        private PresetInitializer presetInitializer;
        private List<Preset> Presets;

        SolidColorBrush accentColor = Application.Current.Resources["BitSynthAccentColorBrush"] as SolidColorBrush;
        SolidColorBrush disabledColor = Application.Current.Resources["ToggleButtonDisabledBrush"] as SolidColorBrush;

        Uri volumeHighIconUri = new Uri("ms-appx:///Assets/PixArt/volume-high.png");
        Uri volumeLowIconUri = new Uri("ms-appx:///Assets/PixArt/volume-low.png");
        Uri volumeOffIconUri = new Uri("ms-appx:///Assets/PixArt/volume-off.png");

        public KeyboardShell()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            VisualStateManager.GoToState(this, "Loading", true);


            soundPlayer = new SoundPlayer();
            await soundPlayer.InitializeSounds();


            InitializeControls();

            InitializePresets();

            masterVolumeSlider.Value = masterVolumeDefaultVal;

            VisualStateManager.GoToState(this, "Loaded", true);
        }

        private void InitializePresets()
        {
            presetInitializer = new PresetInitializer();

            Presets = new List<Preset>();
            Presets.AddRange(presetInitializer.allPresets);
        }

        private void InitializeControls()
        { 
            VolumeIcons = new List<BitmapIcon>();

            pOneVolumeIcon.UriSource = volumeOffIconUri;
            pTwoVolumeIcon.UriSource = volumeOffIconUri;
            wOneVolumeIcon.UriSource = volumeOffIconUri;
            wTwoVolumeIcon.UriSource = volumeOffIconUri;

            VolumeIcons.Add(pOneVolumeIcon);
            VolumeIcons.Add(pTwoVolumeIcon);
            VolumeIcons.Add(wOneVolumeIcon);
            VolumeIcons.Add(wTwoVolumeIcon);

            VolumeToggles = new List<ToggleButton>();
            VolumeToggles.Add(pOneVolumeToggle);
            VolumeToggles.Add(pTwoVolumeToggle);
            VolumeToggles.Add(wOneVolumeToggle);
            VolumeToggles.Add(wTwoVolumeToggle);

            AllPOneToggles = new List<ToggleButton>();
            AllPOneToggles.Add(pOneVolumeToggle);
            AllPOneToggles.Add(pOneLoopToggle);

            AllPTwoToggles = new List<ToggleButton>();
            AllPTwoToggles.Add(pTwoVolumeToggle);
            AllPTwoToggles.Add(pTwoLoopToggle);

            AllWOneToggles = new List<ToggleButton>();
            AllWOneToggles.Add(wOneVolumeToggle);
            AllWOneToggles.Add(wOneLoopToggle);

            AllWTwoToggles = new List<ToggleButton>();
            AllWTwoToggles.Add(wTwoVolumeToggle);
            AllWTwoToggles.Add(wTwoLoopToggle);

            AllTogglesLists = new List<List<ToggleButton>>();
            AllTogglesLists.Add(AllPOneToggles);
            AllTogglesLists.Add(AllPTwoToggles);
            AllTogglesLists.Add(AllWOneToggles);
            AllTogglesLists.Add(AllWTwoToggles);
        }


        private void Key_IsPressedPropertyChanged(object sender, EventArgs e)
        {
            Key key = sender as Key;

            switch (key.Name)
            {
                case "key1F":
                    PlaySample(0, key.IsPressed);
                    break;
                case "key1FSharp":
                    PlaySample(1, key.IsPressed);
                    break;
                case "key1G":
                    PlaySample(2, key.IsPressed);
                    break;
                case "key1GSharp":
                    PlaySample(3, key.IsPressed);
                    break;
                case "key2A":
                    PlaySample(4, key.IsPressed);
                    break;
                case "key2ASharp":
                    PlaySample(5, key.IsPressed);
                    break;
                case "key2B":
                    PlaySample(6, key.IsPressed);
                    break;
                case "key2C":
                    PlaySample(7, key.IsPressed);
                    break;
                case "key2CSharp":
                    PlaySample(8, key.IsPressed);
                    break;
                case "key2D":
                    PlaySample(9, key.IsPressed);
                    break;
                case "key2DSharp":
                    PlaySample(10, key.IsPressed);
                    break;
                case "key2E":
                    PlaySample(11, key.IsPressed);
                    break;
                case "key2F":
                    PlaySample(12, key.IsPressed);
                    break;
                case "key2FSharp":
                    PlaySample(13, key.IsPressed);
                    break;
                case "key2G":
                    PlaySample(14, key.IsPressed);
                    break;
                case "key2GSharp":
                    PlaySample(15, key.IsPressed);
                    break;
                case "key3A":
                    PlaySample(16, key.IsPressed);
                    break;
                case "key3ASharp":
                    PlaySample(17, key.IsPressed);
                    break;
                case "key3B":
                    PlaySample(18, key.IsPressed);
                    break;
                case "key3C":
                    PlaySample(19, key.IsPressed);
                    break;
                case "key3CSharp":
                    PlaySample(20, key.IsPressed);
                    break;
                case "key3D":
                    PlaySample(21, key.IsPressed);
                    break;
                case "key3DSharp":
                    PlaySample(22, key.IsPressed);
                    break;
                case "key3E":
                    PlaySample(23, key.IsPressed);
                    break;
                case "key3F":
                    PlaySample(24, key.IsPressed);
                    break;
                case "key3FSharp":
                    PlaySample(25, key.IsPressed);
                    break;
                case "key3G":
                    PlaySample(26, key.IsPressed);
                    break;
                case "key3GSharp":
                    PlaySample(27, key.IsPressed);
                    break;
                case "key4A":
                    PlaySample(28, key.IsPressed);
                    break;
                case "key4ASharp":
                    PlaySample(29, key.IsPressed);
                    break;
                case "key4B":
                    PlaySample(30, key.IsPressed);
                    break;
                case "key4C":
                    PlaySample(31, key.IsPressed);
                    break;
                case "key4CSharp":
                    PlaySample(32, key.IsPressed);
                    break;
                case "key4D":
                    PlaySample(33, key.IsPressed);
                    break;
                case "key4DSharp":
                    PlaySample(34, key.IsPressed);
                    break;
                case "key4E":
                    PlaySample(35, key.IsPressed);
                    break;
                case "key4F":
                    PlaySample(36, key.IsPressed);
                    break;
                case "key4FSharp":
                    PlaySample(37, key.IsPressed);
                    break;
                case "key4G":
                    PlaySample(38, key.IsPressed);
                    break;
            }



            if (key.IsPressed)
                VisualStateManager.GoToState(this, "LedLit", true);
            else
                VisualStateManager.GoToState(this, "LedNormal", true);
        }

        private void PlaySample(int sampleIndex, bool play)
        {
            soundPlayer.PlaySound(
                sampleIndex, play,
                pOneVolumeToggle.IsChecked, pOneLoopToggle.IsChecked,
                pTwoVolumeToggle.IsChecked, pTwoLoopToggle.IsChecked,
                wOneVolumeToggle.IsChecked, wOneLoopToggle.IsChecked,
                wTwoVolumeToggle.IsChecked, wTwoLoopToggle.IsChecked
                );
        }






        /// <summary>
        /// 
        /// </summary>
        /// <param name="clickedToggle"></param>
        /// <param name="toggleToSet"></param>
        private void SetVolumeToggleCheckedState(ToggleButton clickedToggle, ToggleButton toggleToSet)
        {
            // if any other toggle is set to high volume, set it to low volume (IsChecked = null)
            if (toggleToSet != clickedToggle &&
                clickedToggle.IsChecked == true &&
                toggleToSet.IsChecked == true)
            {
                toggleToSet.IsChecked = null;
            }
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="volumeIcon"></param>
        /// <param name="volumeLevel"></param>
        private void SetVolumeIcon(BitmapIcon volumeIcon, bool? volumeLevel = false)
        {
            switch (volumeLevel)
            {
                case true:
                    if (volumeIcon.UriSource != volumeHighIconUri)
                    {
                        volumeIcon.UriSource = volumeHighIconUri;
                        volumeIcon.Foreground = accentColor;
                        volumeIcon.Opacity = 1;
                    }                 
                    break;
                case null:
                    if (volumeIcon.UriSource != volumeLowIconUri)
                    {
                        volumeIcon.UriSource = volumeLowIconUri;
                        volumeIcon.Foreground = accentColor;
                        volumeIcon.Opacity = 0.5;
                    }               
                    break;
                case false:
                default:
                    if (volumeIcon.UriSource != volumeOffIconUri)
                    {
                        volumeIcon.UriSource = volumeOffIconUri;
                        volumeIcon.Foreground = disabledColor;
                        volumeIcon.Opacity = 0.3;
                    } 
                    break;
            }           
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="volumeToggle"></param>
        private void EnableOrDisableGeneralToggles(ToggleButton volumeToggle)
        {
            switch (volumeToggle.IsChecked)
            {
                case true:
                    foreach (ToggleButton generalToggle in AllTogglesLists[VolumeToggles.IndexOf(volumeToggle)])
                    {
                        if (AllTogglesLists[VolumeToggles.IndexOf(volumeToggle)].IndexOf(generalToggle) != 0)
                            generalToggle.IsEnabled = true;
                    }
                    break;
                case null:
                    foreach (ToggleButton generalToggle in AllTogglesLists[VolumeToggles.IndexOf(volumeToggle)])
                    {
                        if (AllTogglesLists[VolumeToggles.IndexOf(volumeToggle)].IndexOf(generalToggle) != 0)
                            generalToggle.IsEnabled = true;
                    }
                    break;
                case false:
                default:
                    foreach (ToggleButton generalToggle in AllTogglesLists[VolumeToggles.IndexOf(volumeToggle)])
                    {
                        if (AllTogglesLists[VolumeToggles.IndexOf(volumeToggle)].IndexOf(generalToggle) != 0)
                            generalToggle.IsEnabled = false;
                    }
                    break;
            }
        }

        private void VolumeToggle_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedToggle = sender as ToggleButton;

            if (VolumeToggles == null || VolumeIcons == null)
                return;

            foreach (ToggleButton volumeToggle in VolumeToggles)
            {
                // lower toggle's level to medium volume if another toggle is high volume
                SetVolumeToggleCheckedState(clickedToggle, volumeToggle);

                // check each toggle status and set its icon
                BitmapIcon icon = VolumeIcons[VolumeToggles.IndexOf(volumeToggle)];
                SetVolumeIcon(icon, volumeToggle.IsChecked);

                // enable all general toggles in a soundbank of the volume is on, otherwise disable
                EnableOrDisableGeneralToggles(volumeToggle);
            }

            // adjust the volume for each soundbank
            soundPlayer.ChangeIndividualVolume(pOneVolumeToggle.IsChecked,
                pTwoVolumeToggle.IsChecked,
                wOneVolumeToggle.IsChecked,
                wTwoVolumeToggle.IsChecked);
        }



        private void PresetsControl_SelectedPresetChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            foreach (Preset preset in Presets)
            {
                if (Presets.IndexOf(preset) == presetsControl.SelectedPreset)
                {
                    SetPresetValues(preset);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="preset"></param>
        private void SetPresetValues(Preset preset)
        {
            foreach (List<ToggleButton> togglesList in AllTogglesLists)
            {
                foreach (ToggleButton toggle in togglesList)
                {
                    switch (preset.SoundBankSetIndexes[AllTogglesLists.IndexOf(togglesList), togglesList.IndexOf(toggle)])
                    {
                        case 2:
                            toggle.IsChecked = true;
                            break;
                        case 1:
                            if (toggle.Name.Contains("Volume"))
                                toggle.IsChecked = null;
                            else
                                toggle.IsChecked = true;
                            break;
                        case 0:
                            toggle.IsChecked = false;
                            break;
                    }
                }
            }
        }

        private void DelayToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedToggle = sender as ToggleButton;

            if (clickedToggle.IsChecked == true)
                soundPlayer.EnableEchoEffect();
            else
                soundPlayer.EnableEchoEffect(false);
        }

        private void ReverbToggle_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedToggle = sender as ToggleButton;

            if (clickedToggle.IsChecked == true)
                soundPlayer.EnableReverbEffect();
            else
                soundPlayer.EnableReverbEffect(false);
        }
    }
}
