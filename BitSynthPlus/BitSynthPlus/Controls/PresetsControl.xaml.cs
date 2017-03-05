using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;


namespace BitSynthPlus.Controls
{
    /// <summary>
    /// The Control Panel for Presets, deriving from UserControl
    /// </summary>
    public sealed partial class PresetsControl : UserControl
    {
        private List<ToggleButton> PresetToggles;

        public static readonly DependencyProperty SelectedPresetProperty =
          DependencyProperty.Register("SelectedPreset", typeof(int), typeof(PresetsControl), new PropertyMetadata(int.MaxValue));


        public int SelectedPreset
        {
            get { return (int)GetValue(SelectedPresetProperty); }
            private set
            {
                SetValue(SelectedPresetProperty, value);
                NotifySelectedPresetChanged("SelectedPreset");
            }
        }


        public event PropertyChangedEventHandler SelectedPresetChanged;

        void NotifySelectedPresetChanged(string info)
        {
            SelectedPresetChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }


        public PresetsControl()
        {

            this.InitializeComponent();

            PresetToggles = new List<ToggleButton>();
            PresetToggles.Add(presetToggleOne);
            PresetToggles.Add(presetToggleTwo);
            PresetToggles.Add(presetToggleThree);
            PresetToggles.Add(presetToggleFour);
            PresetToggles.Add(presetToggleFive);
            PresetToggles.Add(presetToggleSix);

            presetToggleOne.IsChecked = true;
        }

        private void PresetToggle_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;

            SelectedPreset = PresetToggles.IndexOf(toggle);

            if (PresetToggles == null)
                return;

            // only one preset can be selected at a time
            foreach (ToggleButton presetToggle in PresetToggles)
            {
                if (presetToggle != toggle)
                    presetToggle.IsChecked = false;
            }
        }
    }
}
