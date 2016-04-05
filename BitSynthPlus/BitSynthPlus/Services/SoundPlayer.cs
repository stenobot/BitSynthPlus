using BitSynthPlus.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Media.Render;
using Windows.Storage;

namespace BitSynthPlus.Services
{
    public class SoundPlayer
    {
        private SoundBanksInitializer soundBankInitializer;

        // main audio graph and output node
        private AudioGraph graph;

        private AudioSubmixNode submixNode;

        private AudioDeviceOutputNode deviceOutputNode;

        // dictionary of file-based input nodes
        private Dictionary<string, AudioFileInputNode> FileInputNodesDictionary = new Dictionary<string, AudioFileInputNode>();

        // public input node for each possible key
        private ObservableCollection<AudioFileInputNode> POneInputNodes;
        private ObservableCollection<AudioFileInputNode> PTwoInputNodes;
        private ObservableCollection<AudioFileInputNode> WOneInputNodes;
        private ObservableCollection<AudioFileInputNode> WTwoInputNodes;

        private ObservableCollection<ObservableCollection<AudioFileInputNode>> InputNodesList;

        private const double VOLUME_HIGH = 1.0;
        private const double VOLUME_MEDIUM = 0.3;

        AudioFileInputNode pOneOneF, pOneOneFSharp, pOneOneG, pOneOneGSharp,
            pOneTwoA, pOneTwoASharp, pOneTwoB, pOneTwoC, pOneTwoCSharp, pOneTwoD, pOneTwoDSharp, pOneTwoE, pOneTwoF, pOneTwoFSharp, pOneTwoG, pOneTwoGSharp,
            pOneThreeA, pOneThreeASharp, pOneThreeB, pOneThreeC, pOneThreeCSharp, pOneThreeD, pOneThreeDSharp, pOneThreeE,
            pTwoOneF, pTwoOneFSharp, pTwoOneG, pTwoOneGSharp,
            pTwoTwoA, pTwoTwoASharp, pTwoTwoB, pTwoTwoC, pTwoTwoCSharp, pTwoTwoD, pTwoTwoDSharp, pTwoTwoE, pTwoTwoF, pTwoTwoFSharp, pTwoTwoG, pTwoTwoGSharp,
            pTwoThreeA, pTwoThreeASharp, pTwoThreeB, pTwoThreeC, pTwoThreeCSharp, pTwoThreeD, pTwoThreeDSharp, pTwoThreeE,
            wOneOneF, wOneOneFSharp, wOneOneG, wOneOneGSharp,
            wOneTwoA, wOneTwoASharp, wOneTwoB, wOneTwoC, wOneTwoCSharp, wOneTwoD, wOneTwoDSharp, wOneTwoE, wOneTwoF, wOneTwoFSharp, wOneTwoG, wOneTwoGSharp,
            wOneThreeA, wOneThreeASharp, wOneThreeB, wOneThreeC, wOneThreeCSharp, wOneThreeD, wOneThreeDSharp, wOneThreeE,
            wTwoOneF, wTwoOneFSharp, wTwoOneG, wTwoOneGSharp,
            wTwoTwoA, wTwoTwoASharp, wTwoTwoB, wTwoTwoC, wTwoTwoCSharp, wTwoTwoD, wTwoTwoDSharp, wTwoTwoE, wTwoTwoF, wTwoTwoFSharp, wTwoTwoG, wTwoTwoGSharp,
            wTwoThreeA, wTwoThreeASharp, wTwoThreeB, wTwoThreeC, wTwoThreeCSharp, wTwoThreeD, wTwoThreeDSharp, wTwoThreeE;


        //public List<AudioSubmixNode> SubmixNodesList;

        //AudioSubmixNode oneFSubmix, oneFSharpSubmix, oneGSubmix, oneGSharpSubmix,
        //    twoASubmix, twoASharpSubmix, twoBSubmix, twoCSubmix, twoCSharpSubmix, twoDSubmix, twoDSharpSubmix, twoESubmix, twoFSubmix, twoFSharpSubmix, twoGSubmix, twoGSharpSubmix,
        //    threeASubmix, threeASharpSubmix, threeBSubmix, threeCSubmix, threeCSharpSubmix, threeDSubmix, threeDSharpSubmix, threeESubmix;


        //string[,] audioSamples = new string[,]
        //{
        //    {"p1-1f.wav","p1-1fsharp.wav","p1-1g.wav", "p1-1gsharp.wav",
        //        "p1-2a.wav", "p1-2asharp.wav", "p1-2b.wav", "p1-2c.wav", "p1-2csharp.wav", "p1-2d.wav", "p1-2dsharp.wav", "p1-2e.wav", "p1-2f.wav", "p1-2fsharp.wav", "p1-2g.wav", "p1-2gsharp.wav",
        //        "p1-3a.wav", "p1-3asharp.wav", "p1-3b.wav", "p1-3c.wav", "p1-3csharp.wav", "p1-3d.wav", "p1-3dsharp.wav", "p1-3e.wav" },
        //    {"p2-1f.wav","p2-1fsharp.wav","p2-1g.wav", "p2-1gsharp.wav",
        //        "p2-2a.wav", "p2-2asharp.wav", "p2-2b.wav", "p2-2c.wav", "p2-2csharp.wav", "p2-2d.wav", "p2-2dsharp.wav", "p2-2e.wav", "p2-2f.wav", "p2-2fsharp.wav", "p2-2g.wav", "p2-2gsharp.wav",
        //        "p2-3a.wav", "p2-3asharp.wav", "p2-3b.wav", "p2-3c.wav", "p2-3csharp.wav", "p2-3d.wav", "p2-3dsharp.wav", "p2-3e.wav" }
        //};

        public void ChangeVolume(bool? pOneVolumeToggle, bool? pTwoVolumeToggle, bool? wOneVolumeToggle, bool? wTwoVolumeToggle)
        {
            bool? currentBool = false;

            for (var i = 0; i < InputNodesList.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        currentBool = pOneVolumeToggle;
                        break;
                    case 1:
                        currentBool = pTwoVolumeToggle;
                        break;
                    case 2:
                        currentBool = wOneVolumeToggle;
                        break;
                    case 3:
                        currentBool = wTwoVolumeToggle;
                        break;
                }

                foreach (AudioFileInputNode inputNode in InputNodesList[i])
                {
                    if (currentBool == null)
                        inputNode.OutgoingGain = VOLUME_MEDIUM;
                    else if (currentBool == true)
                        inputNode.OutgoingGain = VOLUME_HIGH;
                    else
                        inputNode.OutgoingGain = 0.0;
                }
            }
        }

        public void PlaySound(
            int sampleIndex, bool play,
            bool? pOneOn, bool? pOneLoop,
            bool? pTwoOn, bool? pTwoLoop,
            bool? wOneOn, bool? wOneLoop,
            bool? wTwoOn, bool? wTwoLoop
            )
        {
            if (play)
            {
                if (pOneOn == true || pOneOn == null)
                {
                    if (pOneLoop == true)
                    {
                        POneInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Reset();
                        POneInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Start();
                    }
                    else
                    {
                        POneInputNodes[sampleIndex].Reset();
                        POneInputNodes[sampleIndex].Start();
                    }
                }

                if (pTwoOn == true || pTwoOn == null)
                {
                    if (pTwoLoop == true)
                    {
                        PTwoInputNodes[sampleIndex + (PTwoInputNodes.Count / 2)].Reset();
                        PTwoInputNodes[sampleIndex + (PTwoInputNodes.Count / 2)].Start();
                    }
                    else
                    {
                        PTwoInputNodes[sampleIndex].Reset();
                        PTwoInputNodes[sampleIndex].Start();
                    }
                }

                if (wOneOn == true || wOneOn == null)
                {
                    if (wOneLoop == true)
                    {
                        WOneInputNodes[sampleIndex + (WOneInputNodes.Count / 2)].Reset();
                        WOneInputNodes[sampleIndex + (WOneInputNodes.Count / 2)].Start();
                    }
                    else
                    {
                        WOneInputNodes[sampleIndex].Reset();
                        WOneInputNodes[sampleIndex].Start();
                    }
                }

                if (wTwoOn == true || wTwoOn == null)
                {
                    if (wTwoLoop == true)
                    {
                        WTwoInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Reset();
                        WTwoInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Start();
                    }
                    else
                    {
                        WTwoInputNodes[sampleIndex].Reset();
                        WTwoInputNodes[sampleIndex].Start();
                    }
                }
            }
            else
            {
                if (pOneOn == true && pOneLoop == true)
                    POneInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Stop();

                if (pTwoOn == true && pTwoLoop == true)
                    PTwoInputNodes[sampleIndex + (PTwoInputNodes.Count / 2)].Stop();

                if (wOneOn == true && wOneLoop == true)
                    WOneInputNodes[sampleIndex + (WOneInputNodes.Count / 2)].Stop();

                if (wTwoOn == true && wTwoLoop == true)
                    WTwoInputNodes[sampleIndex + (WTwoInputNodes.Count / 2)].Stop();
            }

        }


        public async Task InitializeSounds()
        {
            soundBankInitializer = new SoundBanksInitializer();

            POneInputNodes = new ObservableCollection<AudioFileInputNode>();

            PTwoInputNodes = new ObservableCollection<AudioFileInputNode>();

            WOneInputNodes = new ObservableCollection<AudioFileInputNode>();

            WTwoInputNodes = new ObservableCollection<AudioFileInputNode>();

            InputNodesList = new ObservableCollection<ObservableCollection<AudioFileInputNode>>();
            InputNodesList.Add(POneInputNodes);
            InputNodesList.Add(PTwoInputNodes);
            InputNodesList.Add(WOneInputNodes);
            InputNodesList.Add(WTwoInputNodes);

            //SubmixNodesList = new List<AudioSubmixNode>()
            //{
            //    oneFSubmix, oneFSharpSubmix, oneGSubmix, oneGSharpSubmix,
            //    twoASubmix, twoASharpSubmix, twoBSubmix, twoCSubmix, twoCSharpSubmix, twoDSubmix, twoDSharpSubmix, twoESubmix, twoFSubmix, twoFSharpSubmix, twoGSubmix, twoGSharpSubmix,
            //    threeASubmix, threeASharpSubmix, threeBSubmix, threeCSubmix, threeCSharpSubmix, threeDSubmix, threeDSharpSubmix, threeESubmix
            //};

            AudioGraphSettings settings = new AudioGraphSettings(AudioRenderCategory.Media);
            CreateAudioGraphResult result = await AudioGraph.CreateAsync(settings);

            if (result.Status == AudioGraphCreationStatus.Success)
            {
                graph = result.Graph;


                // create the output device
                CreateAudioDeviceOutputNodeResult deviceOutputNodeResult = await graph.CreateDeviceOutputNodeAsync();

                // make sure the audio output is available
                if (deviceOutputNodeResult.Status == AudioDeviceNodeCreationStatus.Success)
                {
                    deviceOutputNode = deviceOutputNodeResult.DeviceOutputNode;
                    graph.ResetAllNodes();


                    foreach (SoundBank soundBank in soundBankInitializer.SoundBanks)
                    {
                        foreach (string fileName in soundBank.FileNames[0])
                        {
                            await CreateInputNodeFromFile("ms-appx:///Assets/AudioSamples/" + fileName);
                            InputNodesList[soundBankInitializer.SoundBanks.IndexOf(soundBank)].Add(FileInputNodesDictionary[fileName]);
                        }

                        foreach (string fileName in soundBank.FileNames[1])
                        {
                            await CreateInputNodeFromFile("ms-appx:///Assets/AudioSamples/" + fileName);

                            FileInputNodesDictionary[fileName].LoopCount = null;

                            InputNodesList[soundBankInitializer.SoundBanks.IndexOf(soundBank)].Add(FileInputNodesDictionary[fileName]);

                        }


                    }

                    // loop through each sound bank, as a dimension in the array
                    //for (var i = 0; i < audioSamples.Rank; i++)
                    //{
                    //    // loop through the samples in that bank/ dimension
                    //    for (var j = 0; j <= audioSamples.GetUpperBound(1); j++)
                    //    {
                    //        nodeIndex = j + (i * (POneInputNodes.Count / audioSamples.Rank));

                    //        //int nodeIndex = j + (subLoopCount * audioSamples.GetUpperBound(1));
                    //        // add sample to input nodes dictionary
                    //        await AddFileToSounds("ms-appx:///Assets/AudioSamples/" + audioSamples[i, j]);
                    //        // add dictionary item to our publicly consumed input nodes list
                    //        POneInputNodes[nodeIndex] = FileInputNodesDictionary[audioSamples[i, j]];

                    //        //SubmixNodesList[j] = graph.CreateSubmixNode();

                    //        //InputNodesList[j].AddOutgoingConnection(SubmixNodesList[j]);


                    //        //SubmixNodesList[j].AddOutgoingConnection(deviceOutputNode, 0.5);

                    //        //if (PublicInputNodesList.Count <= audioSamples.GetUpperBound(0))
                    //        //{
                    //        //}
                    //    }
                    //}



                    //await AddFileToSounds("ms-appx:///Assets/AudioSamples/p1-1f.wav");
                    //oneF = FileInputNodesDictionary["p1-1f.wav"];

                    graph.Start();
                }
            }
        }

        private async Task CreateInputNodeFromFile(string uri)
        {
            var soundFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));

            //var soundFileTwo = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/AudioSamples/w2-1f.wav"));

            CreateAudioFileInputNodeResult fileInputNodeResult = await graph.CreateFileInputNodeAsync(soundFile);

            //CreateAudioFileInputNodeResult fileInputNodeResultTwo = await graph.CreateFileInputNodeAsync(soundFileTwo);

            //AudioSubmixNode submixNode = graph.CreateSubmixNode();

            if (AudioFileNodeCreationStatus.Success == fileInputNodeResult.Status)
            {
                FileInputNodesDictionary.Add(soundFile.Name, fileInputNodeResult.FileInputNode);
                fileInputNodeResult.FileInputNode.Stop();
                fileInputNodeResult.FileInputNode.AddOutgoingConnection(deviceOutputNode);
                //fileInputNodeResultTwo.FileInputNode.AddOutgoingConnection(submixNode, 0.5);

                //submixNode.AddOutgoingConnection(deviceOutputNode);
            }
        }


        //public async Task InitializeGraphs()
        //{
        //    await CreateAudioGraph(graphFLow);
        //}

        //public async Task CreateAudioGraph(AudioGraph graph)
        //{
        //    // Create an AudioGraph with default settings
        //    AudioGraphSettings settings = new AudioGraphSettings(AudioRenderCategory.Media);
        //    CreateAudioGraphResult result = await AudioGraph.CreateAsync(settings);

        //    if (result.Status != AudioGraphCreationStatus.Success)
        //    {
        //        // Cannot create graph
        //        //rootPage.NotifyUser(String.Format("AudioGraph Creation Error because {0}", result.Status.ToString()), NotifyType.ErrorMessage);
        //        return;
        //    }

        //    graph = result.Graph;

        //    // Create a device output node
        //    CreateAudioDeviceOutputNodeResult deviceOutputNodeResult = await graph.CreateDeviceOutputNodeAsync();

        //    if (deviceOutputNodeResult.Status != AudioDeviceNodeCreationStatus.Success)
        //    {
        //        // Cannot create device output node
        //        //rootPage.NotifyUser(String.Format("Device Output unavailable because {0}", deviceOutputNodeResult.Status.ToString()), NotifyType.ErrorMessage);
        //        //speakerContainer.Background = new SolidColorBrush(Colors.Red);
        //        return;
        //    }

        //    deviceOutput = deviceOutputNodeResult.DeviceOutputNode;
        //    //rootPage.NotifyUser("Device Output Node successfully created", NotifyType.StatusMessage);
        //    //speakerContainer.Background = new SolidColorBrush(Colors.Green);

        //}

        //private void PopulateAudioGraphsList()
        //{
        //    AudioGraphsList = new List<AudioGraph>();
        //    AudioGraphsList.Add(graphFLow);
        //    AudioGraphsList.Add(graphFSharpLow);
        //    AudioGraphsList.Add(graphALow);
        //    AudioGraphsList.Add(graphASharpLow);
        //    AudioGraphsList.Add(graphBLow);
        //    AudioGraphsList.Add(graphCLow);
        //    AudioGraphsList.Add(graphCSharpLow);

        //}
    }
}
