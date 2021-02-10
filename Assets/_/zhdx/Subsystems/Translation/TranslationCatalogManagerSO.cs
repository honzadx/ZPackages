using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zhdx.Subsystems.Translation
{ 
    [CreateAssetMenu(menuName = "zhdx/Translation/Translation Catalog Manager")]

    public class TranslationCatalogManagerSO : ManagerSO
    {
        public static TranslationCatalogManagerSO Instance;
       
        public TranslationCatalogSO translationCatalog;
        public string currentLanguage;

        public System.Action LanguageChanged;

        private string[] finalLanguages;

        public override void Initialize()
        {
            if(AbleToInitialize())
            {
                translationCatalog.PlayInit();
                finalLanguages = translationCatalog.GetLanguages();
                Instance = this;
            }        
        }

        public override bool InitializeOnEditMode() => true;

        public string Translate(string id)
        {
            return translationCatalog.Translate(translationCatalog.GetKey(id, currentLanguage));
        }

        public bool KeyExists(string id)
        {
            return translationCatalog.KeyExists(translationCatalog.GetKey(id, currentLanguage));
        }

        public bool ChangeLanguage(string newLang)
        {
            for(int i = 0; i < finalLanguages.Length; i++)
            {
                string checkLang = finalLanguages[i];

                
                if (string.Equals(checkLang, newLang, System.StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                if (i >= finalLanguages.Length - 1)
                {
                    Debug.LogError($"Couldn't change language to '{newLang}' - len:{newLang.Length}!");
                    return false;
                }
            }

            var oldLanguage = currentLanguage;
            currentLanguage = newLang;
            LanguageChanged?.Invoke();

            Debug.Log($"Language changed from '{oldLanguage}' to '{currentLanguage}'");
            return true;
        }
    }
}
