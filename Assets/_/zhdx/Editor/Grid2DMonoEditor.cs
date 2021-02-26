using UnityEngine;
using zhdx.Grid2D;

namespace UnityEditor
{
    [CustomEditor(typeof(Grid2DMono))]
    public class Grid2DMonoEditor : Editor
    {
        private Grid2DMono _target;

        private bool setDirty;

        public override void OnInspectorGUI()
        {
            _target = (Grid2DMono)target;

            GUILayout.BeginHorizontal();
            GUILayout.Label("W", GUILayout.Width(20));
            var width = EditorGUILayout.IntField(_target.width);
            GUILayout.Label("H", GUILayout.Width(20));
            var height = EditorGUILayout.IntField(_target.height);
            GUILayout.Label("C", GUILayout.Width(20));
            var cellSize = EditorGUILayout.FloatField(_target.cellSize);
            GUILayout.EndHorizontal();

            if (width != _target.width || height != _target.height || cellSize != _target.cellSize)
            {
                _target.width = width;
                _target.height = height;
                _target.cellSize = cellSize;
                setDirty = true;
            }

            if (setDirty)
                EditorUtility.SetDirty(_target);

            setDirty = false;
        }
    }
}