using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace zhdx.General 
{
    [CustomEditor(typeof(Health))]
    public class HealthEditor : Editor
    {
        private Health _target;

        private SerializedProperty damage;
        private SerializedProperty heal;
        private SerializedProperty death;

        public void OnEnable()
        {
            _target = (Health)target;
            damage = serializedObject.FindProperty("damage");
            heal = serializedObject.FindProperty("heal");
            death = serializedObject.FindProperty("death");
        }

        public override void OnInspectorGUI()
        {
            GUILayout.BeginHorizontal();
            var invulnerable = EditorGUILayout.Toggle(_target.invulnerable, GUILayout.Width(20));
            var maxHealth = EditorGUILayout.FloatField(_target.maxHealth, GUILayout.Width(40));
            var health = EditorGUILayout.Slider(_target.health, 0, _target.maxHealth);
            GUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(damage);
            EditorGUILayout.PropertyField(heal);
            EditorGUILayout.PropertyField(death);

            if(maxHealth != _target.maxHealth || health != _target.health || invulnerable != _target.invulnerable)
            {
                Undo.RecordObject(_target, "(Health) Values changed");

                if(maxHealth != _target.maxHealth)
                {
                    _target.maxHealth = maxHealth;
                    _target.health = maxHealth;
                }
                else
                {
                    _target.health = health;
                }
                _target.healthValueChanged?.Invoke(health);
                _target.invulnerable = invulnerable;

                EditorUtility.SetDirty(_target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}

