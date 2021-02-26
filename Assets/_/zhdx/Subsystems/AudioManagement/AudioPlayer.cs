using System.Collections.Generic;
using UnityEngine;

namespace zhdx
{
    namespace Subsystems
    {
        namespace AudioManagement
        {
            public class AudioPlayer : MonoBehaviour
            {
                public AudioCatalogSO audioCatalog = null;
                public Transform audioTrack = null;

                private Dictionary<string, AudioCatalogSO.AudioLink> keyToAudioLinks;
                private string[] keys;

                private void Start()
                {
                    PlayInit();
                }

                private void OnDisable()
                {
                    if (audioTrack == null)
                        return;
                    foreach (Transform child in audioTrack.transform)
                    {
                        Destroy(child.gameObject);
                    }
                }

                private void PlayInit()
                {
                    BuildDictionaries();
                    if (audioTrack == null)
                    {
                        CreateAudioTrack();
                    }
                }

#if UNITY_EDITOR
                public void EditorForceInit()
                {
                    PlayInit();
                }
#endif

                public string[] GetKeys()
                {
                    if (keyToAudioLinks == null)
                        PlayInit();

                    return keys;
                }

                private void BuildDictionaries()
                {
                    keyToAudioLinks = null;
                    keys = null;

                    if (audioCatalog == null)
                        return;

                    keyToAudioLinks = new Dictionary<string, AudioCatalogSO.AudioLink>();
                    keys = new string[audioCatalog.audioLinks.Count];

                    for (int i = audioCatalog.audioLinks.Count - 1; i >= 0; --i)
                    {
                        var audioLink = audioCatalog.audioLinks[i];
                        keyToAudioLinks.Add(audioLink.id, audioLink);
                        keys[i] = audioLink.id;
                    }
                }

                private void CreateAudioTrack()
                {
                    Transform newAudioTrack = new GameObject().transform;
                    newAudioTrack.parent = transform;
                    newAudioTrack.transform.localPosition = Vector3.zero;
                    newAudioTrack.name = $"AudioTrack - {gameObject.name}";
                    audioTrack = newAudioTrack;
                }

                public void PlaySound(string id)
                {
                    if (!keyToAudioLinks.ContainsKey(id))
                    {
                        Debug.LogError($"No such sound on AudioPlayer:'{gameObject.name}' called '{id}'!");
                        return;
                    }
                    var audioLink = keyToAudioLinks[id];

                    var clipSet = audioLink.clips[Random.Range(0, audioLink.clips.Count)];
                    var audioSourceObject = new GameObject();
                    audioSourceObject.transform.parent = audioTrack;
                    audioSourceObject.transform.localPosition = Vector3.zero;

                    var audioSource = audioSourceObject.AddComponent<AudioSource>();
                    audioSource.clip = clipSet.clip;
                    audioSource.volume = clipSet.volume;
                    audioSource.pitch = clipSet.pitch;
                    audioSource.outputAudioMixerGroup = audioCatalog.mixerGroup;

                    audioSource.Play();
                    var timeout = audioSourceObject.AddComponent<General.Timeout>();
                    timeout.Initialize(
                        General.Timeout.TimeoutType.DESTROY,
                        General.Timeout.TimeoutStart.CALL,
                        clipSet.clip.length
                        );
                    timeout.StartTimeout();
                }

                public void PlaySoundAt(int index)
                {
                    PlaySound(keys[index]);
                }

                public AudioClip GetClip(string id)
                {
                    if (!keyToAudioLinks.ContainsKey(id))
                    {
                        Debug.LogError($"No such sound on AudioPlayer:'{gameObject.name}' called '{id}'!");
                        return null;
                    }
                    return keyToAudioLinks[id].clip;
                }
            }
        }
    }
}