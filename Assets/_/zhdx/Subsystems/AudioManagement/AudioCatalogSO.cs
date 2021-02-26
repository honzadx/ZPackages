using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace zhdx
{
    namespace Subsystems
    {
        namespace AudioManagement
        {
            [CreateAssetMenu(menuName = "zhdx/Audio/Audio Catalog")]
            public class AudioCatalogSO : ScriptableObject
            {
                [System.Serializable]
                public struct AudioLink
                {
                    public string id;
                    public List<ClipSet> clips;
                    public AudioClip clip;
                }

                [System.Serializable]
                public struct ClipSet
                {
                    public AudioClip clip;
                    public float volume;
                    public float pitch;
                }

                public List<AudioLink> audioLinks;
                public AudioMixerGroup mixerGroup;

                public System.Action Updated;

                #region EDITOR_FUNCTIONS
#if UNITY_EDITOR
                public void EditorAddAudioLink()
                {
                    if (audioLinks == null)
                        audioLinks = new List<AudioLink>();
                    var newAudioLink = new AudioLink();
                    newAudioLink.id = Utils.RandomUtility.RandomString(12);
                    audioLinks.Add(newAudioLink);
                }

                public void EditorRemoveAudioLinkAt(int index)
                {
                    audioLinks.RemoveAt(index);
                }

                public void EditorAddClip(AudioClip clip, int index)
                {
                    var audioLink = audioLinks[index];
                    if (audioLink.clips == null)
                        audioLink.clips = new List<ClipSet>();

                    ClipSet clipSet = new ClipSet();
                    clipSet.clip = clip;
                    clipSet.volume = 1;
                    clipSet.pitch = 1;
                    audioLink.clips.Add(clipSet);

                    audioLinks[index] = audioLink;
                }

                public void EditorRemoveClip(int audioLinkIndex, int clipIndex)
                {
                    var audioLink = audioLinks[audioLinkIndex];

                    audioLink.clips.RemoveAt(clipIndex);

                    audioLinks[audioLinkIndex] = audioLink;
                }
#endif
#endregion // EDITOR_FUNCTIONS
            }
        }
    }
}