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
    public sealed partial class PresetsGrid : UserControl
    {
        private List<ToggleButton> PresetToggles;


        //public static readonly DependencyProperty ColumnsProperty =
        //   DependencyProperty.Register("Columns", typeof(int), typeof(ColumnGridView), new PropertyMetadata(int.MaxValue));

        /// <summary>
        /// Gets or sets the number of Columns the GridView can have
        /// Set on Resize()
        /// </summary>
        //public int Columns
        //{
        //    get { return (int)GetValue(ColumnsProperty); }
        //    set { SetValue(ColumnsProperty, value); }
        //}


        public PresetsGrid()
        {

            this.InitializeComponent();

            PresetToggles = new List<ToggleButton>();
            PresetToggles.Add(presetToggleOne);
            PresetToggles.Add(presetToggleTwo);
            PresetToggles.Add(presetToggleThree);
            PresetToggles.Add(presetToggleFour);
            PresetToggles.Add(presetToggleFive);
            PresetToggles.Add(presetToggleSix);
        }

        private void PresetToggle_Checked(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;

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
