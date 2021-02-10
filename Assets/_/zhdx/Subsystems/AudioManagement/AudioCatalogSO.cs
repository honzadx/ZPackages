using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.Subsystems.AudioManagement
{
    [CreateAssetMenu(menuName = "zhdx/Audio/Audio Catalog")]
    public class AudioCatalogSO : ScriptableObject
    {
        [System.Serializable]
        public struct AudioLink
        {
            public string id;
            public AudioClip clip;
        }

        public List<AudioLink> audioLinks;

        public System.Action Updated;

        #region EDITOR_FUNCTIONS

        internal void EditorAddAudioLink()
        {
            if (audioLinks == null)
                audioLinks = new List<AudioLink>();
            var newAudioLink = new AudioLink();
            newAudioLink.id = Utils.ZRandom.RandomString(12);
            audioLinks.Add(newAudioLink);
        }

        internal void EditorRemoveAudioLinkAt(int index)
        {
            audioLinks.RemoveAt(index);
        }

        #endregion // EDITOR_FUNCTIONS
    }
}