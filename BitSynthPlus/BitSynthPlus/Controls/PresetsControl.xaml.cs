using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BitSynthPlus.Controls
{
    public sealed partial class PresetsControl : UserControl
    {
        private List<ToggleButton> PresetToggles;


        public static readonly DependencyProperty SelectedPresetProperty =
          DependencyProperty.Register("SelectedPreset", typeof(int), typeof(PresetsControl), new PropertyMetadata(int.MaxValue));

        /// <summary>
        /// Gets or sets the number of Columns the GridView can have
        /// Set on Resize()
        /// </summary>
        public int SelectedPreset
        {
            get { return (int)GetValue(SelectedPresetProperty); }
            private set { SetValue(SelectedPresetProperty, value); }
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
