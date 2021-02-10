using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.Subsystems.Translation
{
    public class ChangeLanguage : MonoBehaviour
    {
        public void ChangeByUITextValue(UITextValue text)
        {
            Change(text.GetValueCleared());
        }

        public void Change(string language)
        {
            TranslationCatalogManagerSO.Instance.ChangeLanguage(language);
        }
    }
}