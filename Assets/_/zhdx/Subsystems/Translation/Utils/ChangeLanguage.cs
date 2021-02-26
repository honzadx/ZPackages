using UnityEngine;

namespace zhdx
{
    namespace Subsystems
    {
        namespace Translation
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
    }
}