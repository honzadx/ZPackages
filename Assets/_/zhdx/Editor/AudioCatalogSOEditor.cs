using UnityEngine;
using UnityEngine.Audio;
using zhdx.Subsystems.AudioManagement;

namespace UnityEditor
{
    [CustomEditor(typeof(AudioCatalogSO))]
    public class AudioCatalogSOEditor : Editor
    {
        private AudioCatalogSO _target;

        private bool setDirty;

        private string searchAudioLink;

        public override void OnInspectorGUI()
        {
            _target = (AudioCatalogSO)target;

            GUILayout.Space(20);

            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);

            Rect header = EditorGUILayout.BeginHorizontal();
            EditorGUI.DrawRect(header, ZEditorStyles.HEADER_AUDIO);
            GUI.contentColor = Color.black;
            GUILayout.Label($"AUDIO CATALOG: {target.name}", EditorStyles.boldLabel);
            GUI.contentColor = Color.white;
            if (GUILayout.Button("+", GUILayout.Width(50)))
            {
                _target.EditorAddAudioLink();
            }
            EditorGUILayout.EndHorizontal();
            var mixerGroup = (AudioMixerGroup)EditorGUILayout.ObjectField("Audio Mixer Group", _target.mixerGroup, typeof(AudioMixerGroup), false);
            if(mixerGroup != _target.mixerGroup)
            {
                _target.mixerGroup = mixerGroup;
                setDirty = true;
            }
            searchAudioLink = EditorGUILayout.TextField(searchAudioLink);
            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
            
            if(_target.audioLinks != null)
            {
                for (int i = _target.audioLinks.Count - 1; i >= 0; --i)
                {
                    if(string.IsNullOrEmpty(searchAudioLink) || _target.audioLinks[i].id.Contains(searchAudioLink))
                    {
                        DrawClipRow(i);
                    }
                }
            }
            
            EditorGUILayout.EndVertical();

            if (setDirty)
            {
                EditorUtility.SetDirty(_target);
                _target.Updated?.Invoke();
            }

            setDirty = false;
        }

        private void DrawClipRow(int index)
        {
            var audioLink = _target.audioLinks[index];

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                _target.EditorRemoveAudioLinkAt(index);
                setDirty = true;
                GUILayout.EndHorizontal();
                return;
            }
            var id = EditorGUILayout.TextField(audioLink.id);
            var newClip = (AudioClip)EditorGUILayout.ObjectField(null, typeof(AudioClip), false);
            if (newClip != null)
            {
                _target.EditorAddClip(newClip, index);
                setDirty = true;
            }
            EditorGUI.BeginDisabledGroup(audioLink.clips == null || audioLink.clips.Count <= 0);
            if(GUILayout.Button("Play", GUILayout.Width(50)))
            {
                var clip = audioLink.clips[Random.Range(0, audioLink.clips.Count)].clip;
                AudioUtility.PlayClip(clip, 0, false);
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();

            if(audioLink.clips != null)
            {
                for (int i = audioLink.clips.Count - 1; i >= 0; --i)
                {
                    var clipSet = audioLink.clips[i];

                    GUILayout.BeginHorizontal();
                    GUILayout.Space(ZEditorStyles.HORIZONTAL_HIERARCHY_SPACE);
                    GUILayout.BeginVertical();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        _target.EditorRemoveClip(index, i);
                        setDirty = true;
                        return;
                    }
                    var clip = (AudioClip)EditorGUILayout.ObjectField(clipSet.clip, typeof(AudioClip), false);
                    if (GUILayout.Button("Play", GUILayout.Width(50)))
                    {
                        if (clip != null)
                        {
                            AudioUtility.PlayClip(clip, 0, false);
                        }
                    }
                    GUILayout.EndHorizontal();


                    var volume = EditorGUILayout.Slider(clipSet.volume, 0, 1);
                    var pitch = EditorGUILayout.Slider(clipSet.pitch, -3, 3);

                    GUILayout.EndVertical();
                    GUILayout.EndHorizontal();

                    if (clip != clipSet.clip || volume != clipSet.volume || pitch != clipSet.pitch)
                    {
                        clipSet.clip = clip;
                        clipSet.volume = volume;
                        clipSet.pitch = pitch;

                        audioLink.clips[i] = clipSet;

                        setDirty = true;
                    }
                }
            }

            if (id != audioLink.id)
            {
                audioLink.id = id;
                _target.audioLinks[index] = audioLink;

                setDirty = true;
            }
        }
    }
}
