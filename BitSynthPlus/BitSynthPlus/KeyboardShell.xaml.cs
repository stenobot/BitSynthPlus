using BitSynthPlus.Controls;
using BitSynthPlus.DataModel;
using BitSynthPlus.Services;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

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

    public sealed partial class KeyboardShell : Page
    {
        static readonly double masterVolumeDefaultVal = (double)Application.Current.Resources["MasterVolumeDefault"];
        private const string removeAdsInAppPurchaseId = "RemoveAds";

        private const double pitchOctaveUpValue = 2.0;
        private const double pitchHalfOctaveUpValue = 1.5;
        private const double pitchNormalValue = 1.0;
        private const double pitchOctaveDownValue = 0.5;

        private SoundPlayer soundPlayer;

        // Lists of XAML controls
        private List<ToggleButton> VolumeToggles;
        private List<ToggleButton> LoopToggles;
        private List<Slider> PitchSliders;

        // Lists of Icons
        private List<VolumeToggleIcon> VolumeIcons;
        private List<ToggleIcon> PitchIcons;

        // Presets
        private PresetInitializer presetInitializer;
        private List<Preset> Presets;

        LicenseInformation licenseInformation;

        SolidColorBrush accentColor = Application.Current.Resources["BitSynthAccentColorBrush"] as SolidColorBrush;
        SolidColorBrush disabledColor = Application.Current.Resources["BitSynthDisabledBrush"] as SolidColorBrush;

        public KeyboardShell()
        {
            this.InitializeComponent();

            licenseInformation = CurrentAppSimulator.LicenseInformation;
        }


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            CheckShowAds();

            VisualStateManager.GoToState(this, "Loading", true);

            soundPlayer = new SoundPlayer();

            await soundPlayer.InitializeSounds();

            InitializeControls();

            InitializePresets();

            VisualStateManager.GoToState(this, "Loaded", true);

            // set first preset as the active preset
            SetPresetValues(Presets[0]);

            VisualStateManager.GoToState(this, "LedNormal", true);
        }


        #region METHODS

        /// <summary>
        /// Create a new PresetInitializer object and add Presets to local Presets List
        /// </summary>
        private void InitializePresets()
        {
            presetInitializer = new PresetInitializer();

            Presets = new List<Preset>();
            Presets.AddRange(presetInitializer.allPresets);
        }


        /// <summary>
        /// Add XAML controls to Lists so we can set values
        /// </summary>
        private void InitializeControls()
        { 
            VolumeIcons = new List<VolumeToggleIcon>();

            VolumeIcons.Add(pOneVolumeToggleIcon);
            VolumeIcons.Add(pTwoVolumeToggleIcon);
            VolumeIcons.Add(wOneVolumeToggleIcon);
            VolumeIcons.Add(wTwoVolumeToggleIcon);

            VolumeToggles = new List<ToggleButton>();
            VolumeToggles.Add(pOneVolumeToggle);
            VolumeToggles.Add(pTwoVolumeToggle);
            VolumeToggles.Add(wOneVolumeToggle);
            VolumeToggles.Add(wTwoVolumeToggle);

            LoopToggles = new List<ToggleButton>();
            LoopToggles.Add(pOneLoopToggle);
            LoopToggles.Add(pTwoLoopToggle);
            LoopToggles.Add(wOneLoopToggle);
            LoopToggles.Add(wTwoLoopToggle);

            PitchIcons = new List<ToggleIcon>();
            PitchIcons.Add(pOnePitchToggleIcon);
            PitchIcons.Add(pTwoPitchToggleIcon);
            PitchIcons.Add(wOnePitchToggleIcon);
            PitchIcons.Add(wTwoPitchToggleIcon);

            PitchSliders = new List<Slider>();
            PitchSliders.Add(pOnePitchSlider);
            PitchSliders.Add(pTwoPitchSlider);
            PitchSliders.Add(wOnePitchSlider);
            PitchSliders.Add(wTwoPitchSlider);
        }

        /// <summary>
        /// Play an audio sample
        /// </summary>
        /// <param name="sampleIndex">Index of the audio sample to play</param>
        /// <param name="play">True to play, false to stop</param>
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
        /// Set the checked toggle state for a volume toggle 
        /// true means volume is High, null means volume is Low, false, means volume is Off
        /// </summary>
        /// <param name="clickedToggle">Toggle that was clicked</param>
        /// <param name="toggleToSet">Toggle to set volume for</param>
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
        /// Set preset values for various controls, based on Preset argument
        /// </summary>
        /// <param name="preset">The Preset object, which includes various control values</param>
        private void SetPresetValues(Preset preset)
        {
            // set values for all volume toggles
            foreach (ToggleButton toggle in VolumeToggles)
            {
                switch (preset.SoundBankSetIndexes[VolumeToggles.IndexOf(toggle), 0])
                {
                    case 2:
                        toggle.IsChecked = true;
                        break;
                    case 1:
                        toggle.IsChecked = null;
                        break;
                    case 0:
                    default:
                        toggle.IsChecked = false;
                        break;
                }
            }

            // set values for all loop toggles
            foreach (ToggleButton toggle in LoopToggles)
            {
                switch (preset.SoundBankSetIndexes[LoopToggles.IndexOf(toggle), 1])
                {
                    case 1:
                        toggle.IsChecked = true;
                        break;
                    case 0:
                    default:
                        toggle.IsChecked = false;
                        break;
                }
            }

            // set values for all pitch sliders
            foreach (Slider slider in PitchSliders)
            {
                switch (preset.SoundBankSetIndexes[PitchSliders.IndexOf(slider), 2])
                {
                    case 3:
                        slider.Value = pitchOctaveUpValue;
                        break;
                    case 2:
                        slider.Value = pitchHalfOctaveUpValue;
                        break;
                    case 0:
                        slider.Value = pitchOctaveDownValue;
                        break;
                    case 1:
                    default:
                        slider.Value = pitchNormalValue;
                        break;
                }
            }

            // set effect values
            echoToggle.IsChecked = preset.IsEchoOn;
            echoDelaySlider.Value = preset.EchoDelayValue;
            echoFeedbackSlider.Value = preset.EchoFeedbackValue;
            reverbToggle.IsChecked = preset.IsReverbOn;
            reverbDecaySlider.Value = preset.ReverbDecayValue;
            reverbDensitySlider.Value = preset.ReverbDensityValue;
            reverbGainSlider.Value = preset.ReverbGainValue;
        }


        /// <summary>
        /// Check license state for whether or not the user has purchased ad removal 
        /// and go to the appropriate visual state
        /// </summary>
        private void CheckShowAds()
        {
            if (licenseInformation.ProductLicenses["RemoveAds"].IsActive)
                VisualStateManager.GoToState(this, "HideAds", true);
            else
                VisualStateManager.GoToState(this, "ShowAds", true);
        }


        /// <summary>
        /// Try to buy the ad removal feature
        /// </summary>
        async void BuyRemoveAds()
        {
            if (!licenseInformation.ProductLicenses[removeAdsInAppPurchaseId].IsActive)
            {
                try
                {
                    // user doesn't own ad removal feature, so show purchase dialog
                    await CurrentAppSimulator.RequestProductPurchaseAsync(removeAdsInAppPurchaseId, false);

                    // check license state again to see if purchase was successful, and go to visual state
                    CheckShowAds();
                }
                catch (Exception e)
                {
                    // in-app purchase was not completed because an error occurred
                    MessageDialog errorDialog = new MessageDialog("Sorry, something went wrong with this purchase. Error details: " + e.Message);
                    await errorDialog.ShowAsync();
                }
            }
            else
            {
                // user already owns this feature
                CheckShowAds();
            }
        }

        #endregion



        #region EVENTS

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

        /// <summary>
        /// For three-state toggles, Checked event gets fired when 
        /// IsChecked changes to true
        /// This corresponds to our High Volume state
        /// </summary>
        /// <param name="sender">sender as object</param>
        /// <param name="e">event argument</param>
        private void VolumeToggle_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton checkedToggle = sender as ToggleButton;

            foreach (ToggleButton toggle in VolumeToggles)
            {
                // only looking for toggles other than the one that was checked
                if (toggle == checkedToggle)
                    continue;

                // if volume state is high, change it to low (null)
                if (toggle.IsChecked == true)
                    toggle.IsChecked = null;               
            }

            // set volume of this sound bank to High
            soundPlayer.ChangeSoundBankVolume(VolumeToggles.IndexOf(checkedToggle), Volume.High);

            // update volume icon and pitch icon
            VolumeIcons[VolumeToggles.IndexOf(checkedToggle)].VolumeLevel = Volume.High;
            PitchIcons[VolumeToggles.IndexOf(checkedToggle)].IsEnabled = true;
        }


        /// <summary>
        /// For three-state toggles, Unchecked event gets fired when 
        /// IsChecked changes to false
        /// This corresponds to our Off Volume state
        /// </summary>
        /// <param name="sender">sender as object</param>
        /// <param name="e">event argument</param>
        private void VolumeToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton uncheckedToggle = sender as ToggleButton;

            // set volume of this sound bank to Off
            soundPlayer.ChangeSoundBankVolume(VolumeToggles.IndexOf(uncheckedToggle), Volume.Off);

            // update volume icon and pitch icon
            VolumeIcons[VolumeToggles.IndexOf(uncheckedToggle)].VolumeLevel = Volume.Off;
            PitchIcons[VolumeToggles.IndexOf(uncheckedToggle)].IsEnabled = false;
        }


        /// <summary>
        /// For three-state toggles, Indeterminate event gets fired when 
        /// IsChecked changes to null
        /// This corresponds to our Low Volume state
        /// </summary>
        /// <param name="sender">sender as object</param>
        /// <param name="e">event argument</param>
        private void VolumeToggle_Indeterminate(object sender, RoutedEventArgs e)
        {
            ToggleButton indeterminatelyCheckedToggle = sender as ToggleButton;

            // set volume of this sound bank to Low
            soundPlayer.ChangeSoundBankVolume(VolumeToggles.IndexOf(indeterminatelyCheckedToggle), Volume.Low);

            // update volume icon and pitch icon
            VolumeIcons[VolumeToggles.IndexOf(indeterminatelyCheckedToggle)].VolumeLevel = Volume.Low;
            PitchIcons[VolumeToggles.IndexOf(indeterminatelyCheckedToggle)].IsEnabled = true;
        }

 
        private void PresetsControl_SelectedPresetChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            foreach (Preset preset in Presets)
            {
                if (Presets.IndexOf(preset) == presetsControl.SelectedPreset)
                    SetPresetValues(preset);
            }
        }


        private void EffectToggle_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedToggle = sender as ToggleButton;

            if (clickedToggle.Name == "reverbToggle")
            {
                reverbDensityIcon.IsEnabled = true;
                reverbGainIcon.IsEnabled = true;
                reverbDecayIcon.IsEnabled = true;
                soundPlayer.EnableReverbEffect();
            } else if (clickedToggle.Name == "echoToggle")
            {
                echoDelayIcon.IsEnabled = true;
                echoFeedbackIcon.IsEnabled = true;
                soundPlayer.EnableEchoEffect();
            }; 
        }


        private void EffectToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedToggle = sender as ToggleButton;

            if (clickedToggle.Name == "reverbToggle")
            {
                reverbDensityIcon.IsEnabled = false;
                reverbGainIcon.IsEnabled = false;
                reverbDecayIcon.IsEnabled = false;
                soundPlayer.EnableReverbEffect(false);
            }
            else if (clickedToggle.Name == "echoToggle")
            {
                echoDelayIcon.IsEnabled = false;
                echoFeedbackIcon.IsEnabled = false;
                soundPlayer.EnableEchoEffect(false);
            };
        }

        private void RemoveAdsHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            BuyRemoveAds();
        }
    }

    #endregion
}
