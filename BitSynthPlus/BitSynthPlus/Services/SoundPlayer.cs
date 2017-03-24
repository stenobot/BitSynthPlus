using BitSynthPlus.Controls;
using BitSynthPlus.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Media.Render;
using Windows.Storage;
using Windows.UI.Xaml;

namespace BitSynthPlus.Services
{
    public class SoundPlayer
    {
        static readonly double masterVolumeDefaultVal = (double)Application.Current.Resources["MasterVolumeDefault"];
        static readonly double echoDelayMinVal = (double)Application.Current.Resources["EchoDelayMin"];
        static readonly double echoFeedbackMinVal = (double)Application.Current.Resources["EchoFeedbackMin"];
        static readonly double reverbDecayMinVal = (double)Application.Current.Resources["ReverbDecayMin"];
        static readonly double reverbDensityMinVal = (double)Application.Current.Resources["ReverbDensityMin"];
        static readonly double reverbGainMinVal = (double)Application.Current.Resources["ReverbGainMin"];
        static readonly double reverbWetDryMixDefaultVal = (double)Application.Current.Resources["ReverbWetDryMixDefault"];
        static readonly int reverbDelayDefaultVal = (int)Application.Current.Resources["ReverbDelayDefault"];
        static readonly int reverbRearDelayDefaultVal = (int)Application.Current.Resources["ReverbRearDelayDefault"];
        static readonly double reverbRoomSizeDefaultVal = (double)Application.Current.Resources["ReverbRoomSizeDefault"];
        static readonly int reverbReflectionsDelayDefaultVal = (int)Application.Current.Resources["ReverbReflectionsDelayDefault"];
        static readonly int reverbLowEqCutoffDefaultVal = (int)Application.Current.Resources["ReverbLowEqCutoffDefault"];
        static readonly int reverbLowEqGainDefaultVal = (int)Application.Current.Resources["ReverbLowEqGainDefault"];
        static readonly int reverbHighEqCutoffDefaultVal = (int)Application.Current.Resources["ReverbHighEqCutoffDefault"];
        static readonly int reverbHighEqGainDefaultVal = (int)Application.Current.Resources["ReverbHighEqGainDefault"];

        private SoundBanksInitializer soundBankInitializer;

        // main audio graph and output node
        private AudioGraph graph;

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
        private const double VOLUME_LOW = 0.3;
        private const double VOLUME_OFF = 0.0;

        private double _masterVolume;
        private EchoEffectDefinition _echoEffect;
        private ReverbEffectDefinition _reverbEffect;
        
        private double _pOnePlaybackSpeed;
        private double _pTwoPlaybackSpeed;
        private double _wOnePlaybackSpeed;
        private double _wTwoPlaybackSpeed;


        public double MasterVolume
        {
            get { return _masterVolume; }
            set
            {
                _masterVolume = value;
                ChangeMasterVolume(_masterVolume);
            }
        }

        public double POnePlaybackSpeed
        {
            get { return _pOnePlaybackSpeed; }
            set
            {
                _pOnePlaybackSpeed = value;
                ChangePlaybackSpeed(InputNodesList.IndexOf(POneInputNodes), _pOnePlaybackSpeed);
            }
        }

        public double PTwoPlaybackSpeed
        {
            get { return _pTwoPlaybackSpeed; }
            set
            {
                _pTwoPlaybackSpeed = value;
                ChangePlaybackSpeed(InputNodesList.IndexOf(PTwoInputNodes), _pTwoPlaybackSpeed);
            }
        }

        public double WOnePlaybackSpeed
        {
            get { return _wOnePlaybackSpeed; }
            set
            {
                _wOnePlaybackSpeed = value;
                ChangePlaybackSpeed(InputNodesList.IndexOf(WOneInputNodes), _wOnePlaybackSpeed);
            }
        }

        public double WTwoPlaybackSpeed
        {
            get { return _wTwoPlaybackSpeed; }
            set
            {
                _wTwoPlaybackSpeed = value;
                ChangePlaybackSpeed(InputNodesList.IndexOf(WTwoInputNodes), _wTwoPlaybackSpeed);
            }
        }

        public EchoEffectDefinition EchoEffect
        {
            get { return _echoEffect; }
            set
            {
                _echoEffect = value;
            }
        }

        public ReverbEffectDefinition ReverbEffect
        {
            get { return _reverbEffect; }
            set
            {
                _reverbEffect = value;
            }
        }


        public SoundPlayer()
        {
            _masterVolume = masterVolumeDefaultVal;
            _pOnePlaybackSpeed = 1;
            _pTwoPlaybackSpeed = 1;
            _wOnePlaybackSpeed = 1;
            _wTwoPlaybackSpeed = 1;
        }


        /// <summary>
        /// Change volume of an individual SoundBank
        /// </summary>
        /// <param name="index">The index of the SoundBank to change</param>
        /// <param name="volume">The volume level (High, Low, or Off)</param>
        public void ChangeSoundBankVolume(int index, Volume volume)
        {
            foreach (AudioFileInputNode inputNode in InputNodesList[index])
            {
                switch (volume)
                {
                    case Volume.High:
                        inputNode.OutgoingGain = VOLUME_HIGH;
                        break;
                    case Volume.Low:
                        inputNode.OutgoingGain = VOLUME_LOW;
                        break;
                    case Volume.Off:
                        inputNode.OutgoingGain = VOLUME_OFF;
                        break;
                }              
            }
        }


        private void ChangeMasterVolume(double volume)
        {
            deviceOutputNode.OutgoingGain = volume;
        }

        private void ChangePlaybackSpeed(int soundBankIndex, double speed)
        {
            foreach (AudioFileInputNode inputNode in InputNodesList[soundBankIndex])
            {
                inputNode.PlaybackSpeedFactor = speed;
            }
        }

        /// <summary>
        /// Play a sound. Since it's possible to have so many sounds playing simultaneously,
        /// always check ConsumeInput when accessing an input node. Otherwise, app can crash if 
        /// too much is happening at once.
        /// </summary>
        /// <param name="sampleIndex"></param>
        /// <param name="play"></param>
        /// <param name="pOneOn"></param>
        /// <param name="pOneLoop"></param>
        /// <param name="pTwoOn"></param>
        /// <param name="pTwoLoop"></param>
        /// <param name="wOneOn"></param>
        /// <param name="wOneLoop"></param>
        /// <param name="wTwoOn"></param>
        /// <param name="wTwoLoop"></param>
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
                    if (pOneLoop == true && POneInputNodes[sampleIndex + (POneInputNodes.Count / 2)].ConsumeInput)
                    {
                        POneInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Reset();
                        POneInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Start();
                    }
                    else if (POneInputNodes[sampleIndex].ConsumeInput)
                    {                        
                        {
                            POneInputNodes[sampleIndex].Reset();
                            POneInputNodes[sampleIndex].Start();
                        }
                    }
                }

                if (pTwoOn == true || pTwoOn == null)
                {
                    if (pTwoLoop == true && PTwoInputNodes[sampleIndex + (PTwoInputNodes.Count / 2)].ConsumeInput)
                    {
                        PTwoInputNodes[sampleIndex + (PTwoInputNodes.Count / 2)].Reset();
                        PTwoInputNodes[sampleIndex + (PTwoInputNodes.Count / 2)].Start();
                    }
                    else if (PTwoInputNodes[sampleIndex].ConsumeInput)
                    {
                        PTwoInputNodes[sampleIndex].Reset();
                        PTwoInputNodes[sampleIndex].Start();
                    }
                }

                if (wOneOn == true || wOneOn == null)
                {
                    if (wOneLoop == true && WOneInputNodes[sampleIndex + (WOneInputNodes.Count / 2)].ConsumeInput)
                    {
                        WOneInputNodes[sampleIndex + (WOneInputNodes.Count / 2)].Reset();
                        WOneInputNodes[sampleIndex + (WOneInputNodes.Count / 2)].Start();
                    }
                    else if (WOneInputNodes[sampleIndex].ConsumeInput)
                    {
                        WOneInputNodes[sampleIndex].Reset();
                        WOneInputNodes[sampleIndex].Start();
                    }
                }

                if (wTwoOn == true || wTwoOn == null)
                {
                    if (wTwoLoop == true && WTwoInputNodes[sampleIndex + (POneInputNodes.Count / 2)].ConsumeInput)
                    {
                        WTwoInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Reset();
                        WTwoInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Start();
                    }
                    else if (WTwoInputNodes[sampleIndex].ConsumeInput)
                    {
                        WTwoInputNodes[sampleIndex].Reset();
                        WTwoInputNodes[sampleIndex].Start();
                    }
                }
            }
            else
            {
                if (pOneLoop == true)
                    POneInputNodes[sampleIndex + (POneInputNodes.Count / 2)].Stop();

                if (pTwoLoop == true)
                    PTwoInputNodes[sampleIndex + (PTwoInputNodes.Count / 2)].Stop();

                if (wOneLoop == true)
                    WOneInputNodes[sampleIndex + (WOneInputNodes.Count / 2)].Stop();

                if (wTwoLoop == true)
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

                    InitializeEffects();
                    graph.Start();
                }
            }
        }

        private void InitializeEffects()
        {
            _echoEffect = new EchoEffectDefinition(graph);
            _reverbEffect = new ReverbEffectDefinition(graph);

            _echoEffect.Delay = echoDelayMinVal;
            _echoEffect.Feedback = echoFeedbackMinVal;

            _reverbEffect.DecayTime = reverbDecayMinVal;
            _reverbEffect.Density = reverbDensityMinVal;
            _reverbEffect.ReverbGain = reverbGainMinVal;

            _reverbEffect.WetDryMix = reverbWetDryMixDefaultVal;
            _reverbEffect.ReverbDelay = (byte)reverbDelayDefaultVal;
            _reverbEffect.RearDelay = (byte)reverbRearDelayDefaultVal;
            _reverbEffect.RoomSize = reverbRoomSizeDefaultVal;
            _reverbEffect.ReflectionsDelay = (byte)reverbReflectionsDelayDefaultVal;
            _reverbEffect.LowEQCutoff = (byte)reverbLowEqCutoffDefaultVal;
            _reverbEffect.LowEQGain = (byte)reverbLowEqGainDefaultVal;
            _reverbEffect.HighEQCutoff = (byte)reverbHighEqCutoffDefaultVal;
            _reverbEffect.HighEQGain = (byte)reverbHighEqGainDefaultVal;
        }

        public void EnableEchoEffect(bool enable = true)
        {
            if (enable && !deviceOutputNode.EffectDefinitions.Contains(_echoEffect))
                deviceOutputNode.EffectDefinitions.Add(_echoEffect);
            else if (deviceOutputNode.EffectDefinitions.Contains(_echoEffect))
                deviceOutputNode.EffectDefinitions.RemoveAt(deviceOutputNode.EffectDefinitions.IndexOf(_echoEffect));
        }

        public void EnableReverbEffect(bool enable = true)
        {
            if (enable && !deviceOutputNode.EffectDefinitions.Contains(_reverbEffect))
                deviceOutputNode.EffectDefinitions.Add(_reverbEffect);
            else if (deviceOutputNode.EffectDefinitions.Contains(_reverbEffect))
                deviceOutputNode.EffectDefinitions.RemoveAt(deviceOutputNode.EffectDefinitions.IndexOf(_reverbEffect));

        }

        private async Task CreateInputNodeFromFile(string uri)
        {
            var soundFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));

            CreateAudioFileInputNodeResult fileInputNodeResult = await graph.CreateFileInputNodeAsync(soundFile);

            if (AudioFileNodeCreationStatus.Success == fileInputNodeResult.Status)
            {
                FileInputNodesDictionary.Add(soundFile.Name, fileInputNodeResult.FileInputNode);
                fileInputNodeResult.FileInputNode.Stop();
                fileInputNodeResult.FileInputNode.AddOutgoingConnection(deviceOutputNode);
            }
        }


    }
}
