using BitSynthPlus.Controls;
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
        SoundPlayer soundPlayer;

        private AudioGraph graph;
        private AudioDeviceOutputNode deviceOutputNode;
        private AudioFrameInputNode frameInputNode;
        public double theta = 0;

        private List<ToggleButton> VolumeToggles;
        private List<BitmapIcon> VolumeIcons;

        BitmapIcon pOneVolumeIcon;
        BitmapIcon pTwoVolumeIcon;
        BitmapIcon wOneVolumeIcon;
        BitmapIcon wTwoVolumeIcon;

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

            //await CreateAudioGraph();

            soundPlayer = new SoundPlayer();
            await soundPlayer.InitializeSounds();


            InitializeControls();

            //SetVolumeToggleIcons();

            VisualStateManager.GoToState(this, "Loaded", true);
        }

        private void InitializeControls()
        {

            VolumeIcons = new List<BitmapIcon>();

            pOneVolumeIcon = new BitmapIcon();
            pTwoVolumeIcon = new BitmapIcon();
            wOneVolumeIcon = new BitmapIcon();
            wTwoVolumeIcon = new BitmapIcon();


            pOneVolumeIcon.UriSource = volumeOffIconUri;
            pTwoVolumeIcon.UriSource = volumeOffIconUri;
            wOneVolumeIcon.UriSource = volumeOffIconUri;
            wTwoVolumeIcon.UriSource = volumeOffIconUri;

            VolumeIcons.Add(pOneVolumeIcon);
            VolumeIcons.Add(pTwoVolumeIcon);
            VolumeIcons.Add(wOneVolumeIcon);
            VolumeIcons.Add(wTwoVolumeIcon);

            pOneVolumeToggle.Content = pOneVolumeIcon;
            pTwoVolumeToggle.Content = pTwoVolumeIcon;
            wOneVolumeToggle.Content = wOneVolumeIcon;
            wTwoVolumeToggle.Content = wTwoVolumeIcon;

            VolumeToggles = new List<ToggleButton>();
            VolumeToggles.Add(pOneVolumeToggle);
            VolumeToggles.Add(pTwoVolumeToggle);
            VolumeToggles.Add(wOneVolumeToggle);
            VolumeToggles.Add(wTwoVolumeToggle);


        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (graph != null)
            {
                graph.Dispose();
            }


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





        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            frameInputNode.Start();


            if (btn.Name == "testUp")
                frameInputNode.PlaybackSpeedFactor = frameInputNode.PlaybackSpeedFactor + 0.1;
            else
                frameInputNode.PlaybackSpeedFactor = frameInputNode.PlaybackSpeedFactor - 0.1;

        }


        //private async Task CreateAudioGraph()
        //{
        //    // Create an AudioGraph with default settings
        //    AudioGraphSettings settings = new AudioGraphSettings(AudioRenderCategory.Media);
        //    CreateAudioGraphResult result = await AudioGraph.CreateAsync(settings);

        //    if (result.Status != AudioGraphCreationStatus.Success)
        //    {
        //        // Cannot create graph
        //        //testTextBlock.Text = "AudioGraph Creation Error...";
        //        return;
        //    }

        //    graph = result.Graph;

        //    // Create a device output node
        //    CreateAudioDeviceOutputNodeResult deviceOutputNodeResult = await graph.CreateDeviceOutputNodeAsync();
        //    if (deviceOutputNodeResult.Status != AudioDeviceNodeCreationStatus.Success)
        //    {
        //        // Cannot create device output node
        //        //testTextBlock.Text = "Audio Device Output unavailable...";
        //        //testGrid.Background = new SolidColorBrush(Colors.Red);
        //    }

        //    deviceOutputNode = deviceOutputNodeResult.DeviceOutputNode;
        //    //testTextBlock.Text = "Device Output Node successfully created";
        //    //testGrid.Background = new SolidColorBrush(Colors.Green);

        //    // Create the FrameInputNode at the same format as the graph, except explicitly set mono.
        //    AudioEncodingProperties nodeEncodingProperties = graph.EncodingProperties;
        //    nodeEncodingProperties.ChannelCount = 1;
        //    frameInputNode = graph.CreateFrameInputNode(nodeEncodingProperties);
        //    frameInputNode.AddOutgoingConnection(deviceOutputNode);

        //    // Initialize the Frame Input Node in the stopped state
        //    frameInputNode.Stop();

        //    // Hook up an event handler so we can start generating samples when needed
        //    // This event is triggered when the node is required to provide data
        //    frameInputNode.QuantumStarted += node_QuantumStarted;

        //    // Start the graph since we will only start/stop the frame input node
        //    graph.Start();
        //}



        //unsafe private AudioFrame GenerateAudioData(uint samples)
        //{
        //    // Buffer size is (number of samples) * (size of each sample)
        //    // We choose to generate single channel (mono) audio. For multi-channel, multiply by number of channels
        //    uint bufferSize = samples * sizeof(float);
        //    AudioFrame frame = new Windows.Media.AudioFrame(bufferSize);

        //    using (AudioBuffer buffer = frame.LockBuffer(AudioBufferAccessMode.Write))
        //    using (IMemoryBufferReference reference = buffer.CreateReference())
        //    {
        //        byte* dataInBytes;
        //        uint capacityInBytes;
        //        float* dataInFloat;

        //        // Get the buffer from the AudioFrame
        //        ((IMemoryBufferByteAccess)reference).GetBuffer(out dataInBytes, out capacityInBytes);

        //        // Cast to float since the data we are generating is float
        //        dataInFloat = (float*)dataInBytes;

        //        float freq = 1000; // choosing to generate frequency of 1kHz
        //        float amplitude = 0.3f;
        //        int sampleRate = (int)graph.EncodingProperties.SampleRate;
        //        double sampleIncrement = (freq * (Math.PI * 2)) / sampleRate;

        //        // Generate a 1kHz sine wave and populate the values in the memory buffer
        //        for (int i = 0; i < samples; i++)
        //        {
        //            double sinValue = amplitude * Math.Sin(theta);
        //            dataInFloat[i] = (float)sinValue;
        //            theta += sampleIncrement;
        //        }
        //    }

        //    return frame;
        //}

        //private void node_QuantumStarted(AudioFrameInputNode sender, FrameInputNodeQuantumStartedEventArgs args)
        //{
        //    // GenerateAudioData can provide PCM audio data by directly synthesizing it or reading from a file.
        //    // Need to know how many samples are required. In this case, the node is running at the same rate as the rest of the graph
        //    // For minimum latency, only provide the required amount of samples. Extra samples will introduce additional latency.
        //    uint numSamplesNeeded = (uint)args.RequiredSamples;

        //    if (numSamplesNeeded != 0)
        //    {
        //        AudioFrame audioData = GenerateAudioData(numSamplesNeeded);
        //        frameInputNode.AddFrame(audioData);
        //    }
        //}



        //private void SetVolumeToggleIcons(ToggleButton toggle)
        //{


        //    if (toggle.IsChecked == true)
        //        toggle.Content = volumeHighIcon;
        //    else if (toggle.IsChecked == null)
        //        toggle.Content = volumeLowIcon;
        //    else
        //        toggle.Content = volumeOffIcon;
        //}

        private void VolumeToggle_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggle = sender as ToggleButton;

            if (VolumeToggles == null || VolumeIcons == null)
                return;

            foreach (ToggleButton volumeToggle in VolumeToggles)
            {
                // if any other toggle is set to high volume, set it to low volume (IsChecked = null)
                if (volumeToggle != toggle &&
                    toggle.IsChecked == true &&
                    volumeToggle.IsChecked == true)
                {
                    volumeToggle.IsChecked = null;
                }

                // check each toggle status and set its icon
                BitmapIcon icon = VolumeIcons[VolumeToggles.IndexOf(volumeToggle)];

                switch (volumeToggle.IsChecked)
                {
                    case true:
                        if (icon.UriSource != volumeHighIconUri)
                            icon.UriSource = volumeHighIconUri;
                        break;
                    case null:
                        if (icon.UriSource != volumeLowIconUri)
                            icon.UriSource = volumeLowIconUri;
                        break;
                    case false:
                    default:
                        if (icon.UriSource != volumeOffIconUri)
                            icon.UriSource = volumeOffIconUri;
                        break;
                }
            }

            // adjust the volume for each soundbank
            soundPlayer.ChangeVolume(pOneVolumeToggle.IsChecked,
                pTwoVolumeToggle.IsChecked,
                wOneVolumeToggle.IsChecked,
                wTwoVolumeToggle.IsChecked);
        }
    }
}
