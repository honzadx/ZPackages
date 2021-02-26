using UnityEngine;
using zhdx.Subsystems.AudioManagement;

namespace UnityEditor
{
    [CustomEditor(typeof(AudioPlayer))]
    public class AudioPlayerEditor : Editor
    {
        private AudioPlayer _target;

        private string id;
        private int keyIndex;

        private bool setDirty;

        private void OnEnable()
        {
            _target = (AudioPlayer)target;
            _target.EditorForceInit();
        }

        public override void OnInspectorGUI()
        {
            _target = (AudioPlayer)target;

            GUILayout.Space(20);

            Rect body = EditorGUILayout.BeginVertical();
            EditorGUI.DrawRect(body, ZEditorStyles.ACTIVE_BOX_COLOR);

            Rect header = EditorGUILayout.BeginHorizontal();
            EditorGUI.DrawRect(header, ZEditorStyles.HEADER_AUDIO);
            GUI.contentColor = Color.black;
            GUILayout.Label($"AUDIO CATALOG: {target.name}", EditorStyles.boldLabel);
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();

            var audioCatalog = (AudioCatalogSO)EditorGUILayout.ObjectField(_target.audioCatalog, typeof(AudioCatalogSO), false);
            var audioTrack = (Transform)EditorGUILayout.ObjectField(_target.audioTrack, typeof(Transform), true);

            if(audioCatalog != _target.audioCatalog || audioTrack != _target.audioTrack)
            {
                setDirty = true;
                _target.audioCatalog = audioCatalog;
                _target.audioTrack = audioTrack;
                _target.EditorForceInit();
            }

            var keys = _target.GetKeys();
            if(keys != null && keys.Length > 0)
            {
                GUILayout.Space(ZEditorStyles.LINE_HEIGHT);
                GUILayout.BeginHorizontal();
                keyIndex = EditorGUILayout.Popup(keyIndex, keys);
                id = keys[keyIndex];
                if (GUILayout.Button("Play", GUILayout.Width(200)))
                {
                    if (Application.isPlaying)
                    {
                        _target.PlaySound(id);
                    }
                    else
                    {
                        AudioUtility.PlayClip(_target.GetClip(id), 0, false);
                    }
                }
                GUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            if (setDirty)
            {
                EditorUtility.SetDirty(_target);
            }

            setDirty = false;
        }
    }
}
