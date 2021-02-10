using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.Subsystems.AudioManagement
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

        internal void EditorForceInit()
        {
            PlayInit();
        }
        
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
            var audioSourceObject = new GameObject();
            audioSourceObject.transform.parent = audioTrack;
            audioSourceObject.transform.localPosition = Vector3.zero;
            audioSourceObject.AddComponent<AudioSource>();
            var audioSource = audioSourceObject.GetComponent<AudioSource>();
            audioSource.clip = keyToAudioLinks[id].clip;
            audioSource.Play();
            audioSourceObject.AddComponent<General.Timeout>().Initialize(General.Timeout.TimeoutType.DESTROY,
                General.Timeout.TimeoutStart.CALL,
                keyToAudioLinks[id].clip.length)
                .StartTimeout();
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