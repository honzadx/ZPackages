using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace zhdx.Subsystems.AudioManagement
{
    [CustomEditor(typeof(AudioCatalogSO))]
    public class AudioCatalogSOEditor : Editor
    {
        private AudioCatalogSO _target;

        private bool setDirty;

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

            GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
            
            if(_target.audioLinks != null)
            for(int i = _target.audioLinks.Count - 1; i >= 0; --i)
            {
                var audioLink = _target.audioLinks[i];

                GUILayout.BeginHorizontal();
                if(GUILayout.Button("X", GUILayout.Width(20)))
                {
                    _target.EditorRemoveAudioLinkAt(i);
                    setDirty = true;
                }
                else
                {
                    var id = EditorGUILayout.TextField(audioLink.id);
                    var clip = (AudioClip)EditorGUILayout.ObjectField(audioLink.clip, typeof(AudioClip), false, GUILayout.Width(150));
                    if (GUILayout.Button("Play", GUILayout.Width(50)))
                    {
                        if (clip != null)
                        {
                            AudioUtil.PlayClip(clip, 0, false);
                        }
                    }
                    if(clip != audioLink.clip || id != audioLink.id)
                    {
                        setDirty = true;
                        audioLink.id = id;
                        audioLink.clip = clip;
                        _target.audioLinks[i] = audioLink;
                    }
                }
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            if (setDirty)
            {
                EditorUtility.SetDirty(_target);
                _target.Updated?.Invoke();
            }

            setDirty = false;
        }
    }
}
