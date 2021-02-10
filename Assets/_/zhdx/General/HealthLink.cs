using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace zhdx.General
{
    public class HealthLink : MonoBehaviour
    {
        [SerializeField]
        private Health healthComponent = null;
        [SerializeField]
        private string format = "0.#";

        private UITextType textType;

        private void OnEnable()
        {
            if (gameObject.GetComponent<TextMeshProUGUI>())
            {
                textType = UITextType.TextMeshProUGUI;
            }
            else if (gameObject.GetComponent<TextMeshPro>())
            {
                textType = UITextType.TextMeshPro;
            }

            healthComponent.healthValueChanged += UpdateText;
            UpdateText(healthComponent.health);
        }

        private void OnDisable()
        {
            if(healthComponent != null)
                healthComponent.healthValueChanged -= UpdateText;
        }

        private void UpdateText(float health)
        {
            if(this != null)
            {
                var text = health.ToString(format);
                switch (textType)
                {
                    case UITextType.TextMeshPro:
                        GetComponent<TextMeshPro>().text = text;
                        break;
                    case UITextType.TextMeshProUGUI:
                        GetComponent<TextMeshProUGUI>().text = text;
                        break;
                }
            }
        }
    }
}
