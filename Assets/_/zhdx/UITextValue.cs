﻿using UnityEngine;
using TMPro;

namespace zhdx
{
    [ExecuteAlways]
    public class UITextValue : MonoBehaviour
    {
        public static int clearChar = 8203;

        private UITextType type = UITextType.NONE;

        private TextMeshPro tmp;
        private TextMeshProUGUI tmpUGUI;

        public void OnEnable()
        {
            if (GetComponent<TextMeshPro>())
            {
                type = UITextType.TextMeshPro;
                tmp = GetComponent<TextMeshPro>();
            }
            else if (GetComponent<TextMeshProUGUI>())
            {
                type = UITextType.TextMeshProUGUI;
                tmpUGUI = GetComponent<TextMeshProUGUI>();
            }
        }

        public string GetValue()
        {
            string text;

            switch (type)
            {
                case UITextType.TextMeshPro:
                    text = tmp.text;
                    break;
                case UITextType.TextMeshProUGUI:
                    text = tmpUGUI.text;
                    break;
                case UITextType.NONE:
                default:
                    throw new System.Exception($"GameObject '{gameObject.name}' is not a text");
            }

            return text;
            
        }

        public string GetValueCleared()
        {
            var text = GetValue();
            var cleared = text;
            if (text[text.Length - 1] == clearChar)
            {
                cleared = text.Remove(text.Length - 1);
            }
             
            return cleared;
        }
    }
}
