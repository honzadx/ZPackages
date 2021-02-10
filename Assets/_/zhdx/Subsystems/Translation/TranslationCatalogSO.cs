using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace zhdx.Subsystems.Translation
{
    [System.Serializable]
    public struct Translation
    {
        public string id;
        public List<LanguageText> languageTexts;

        public void RemoveLanguage(string lang)
        {
            for(int i = 0; i < languageTexts.Count; i++)
            {
                var langText = languageTexts[i];

                if(langText.language == lang)
                {
                    languageTexts.RemoveAt(i);
                }
            }
        }

        public void AddLanguage(string lang)
        {
            if(languageTexts == null) languageTexts = new List<LanguageText>();

            var newLangText = new LanguageText();
            newLangText.language = lang;

            languageTexts.Add(newLangText);
        }
    }

    [System.Serializable]
    public struct LanguageText
    {
        public string language;
        public string text;
    }

    [CreateAssetMenu(menuName = "zhdx/Translation/Translation Catalog")]
    public class TranslationCatalogSO : ScriptableObject
    {
        public List<string> languages;
        public string[] GetLanguages() => languages.ToArray();
        public int GetLanguageIndex(string lang) 
        {
            for (int i = 0; i < languages.Count; i++) 
            {
                if (lang == languages[i]) return i;  
            } 
            return 0;
        }
        public List<Translation> translations;

        private Dictionary<string, string> keyToText;

        #region ONPLAY_FUNCTIONS
        public void PlayInit()
        {
            keyToText = new Dictionary<string, string>();

            for(int i = 0; i < translations.Count;i++)
            {
                var translation = translations[i];
                for(int k = 0;k < translation.languageTexts.Count;k++)
                {
                    var langText = translation.languageTexts[k];
                    keyToText.Add(GetKey(translation.id, langText.language), langText.text);
                }
            }
        }

        public string Translate(string key)
        {
            if (keyToText == null)
                PlayInit();

            return keyToText[key];
        }

        public string GetKey(string id, string lang)
        {
            return $"id_{id},lang_{lang}";
        }

        public bool KeyExists(string key)
        {
            if (keyToText == null)
                PlayInit();

            return keyToText.ContainsKey(key);
        }
        #endregion //ONPLAY_FUNCTIONS

        #region EDITOR_FUNCTIONS
        internal void EditorLazyInstantiate()
        {
            if (languages == null)
                languages = new List<string>();
            if (translations == null)
                translations = new List<Translation>();
        }

        internal void EditorRemoveLanguageAt(int index)
        {
            var lang = languages[index];

            for (int i = 0; i < translations.Count; i++)
            {
                var translation = translations[i];
                translation.RemoveLanguage(lang);
            }
            languages.RemoveAt(index);
        }

        internal bool EditorAddLanguage(string newLang)
        {
            if (languages.Contains(newLang))
                return false;

            languages.Add(newLang);

            foreach (var translation in translations)
            {
                translation.AddLanguage(newLang);
            }
            return true;
        }

        internal void EditorRemoveTranslationAt(int index)
        {
            translations.RemoveAt(index);
        }

        internal bool EditorAddTranslation(string id)
        {
            for(int i = 0; i< translations.Count;i++)
            {
                var translationCheck = translations[i];
                if (translationCheck.id == id)
                    return false;
            }

            var newTranslation = new Translation();
            newTranslation.id = id;

            foreach (string lang in languages)
            {
                newTranslation.AddLanguage(lang);
            }

            translations.Add(newTranslation);
            return true;
        }

        internal void EditorInsertTranslation(Translation translation, int index)
        {
            if (index >= translations.Count || index < 0)
                return;

            var lastIndex = translations.IndexOf(translation);

            translations.RemoveAt(lastIndex);
            translations.Insert(index, translation);
        }

        internal void EditorExportCSV()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < languages.Count; ++i)
            {
                var lang = languages[i];
                stringBuilder.Append($",{lang}");
            }
            stringBuilder.Append("\n");
            for (int i = 0; i < translations.Count; ++i)
            {
                var translation = translations[i];
                stringBuilder.Append($"{translation.id}");
                for (int k = 0; k < translation.languageTexts.Count; ++k)
                {
                    var translationText = translation.languageTexts[k];
                    stringBuilder.Append($",{translationText.text}");
                }
                stringBuilder.Append("\n");
            }
            stringBuilder.Append("\n");

            var path = GetExportPath();
            
            using(var file = File.Create(path))
            {
                using (var sw = new StreamWriter(file, Encoding.UTF8))
                {
                    sw.Write(stringBuilder.ToString());
                }
            }
            
            Debug.Log($"Export finished to {path}");
        }

        internal void EditorImportCSV()
        {
            translations = new List<Translation>();
            languages = new List<string>();
            keyToText = new Dictionary<string, string>();

            bool readLanguages = false;
            using(StreamReader sr = new StreamReader(GetExportPath(), true))
            {
                string[] langArray = null;
                while (sr.Peek() >= 0)
                {
                    if(!readLanguages)
                    {
                        langArray = sr.ReadLine().Split(',');
                        for (int i = 1; i < langArray.Length; ++i)
                        {
                            var lang = langArray[i];
                            EditorAddLanguage(lang);
                        }
                        readLanguages = true;
                    }
                    else
                    {
                        Translation translation = new Translation();
                        var translationArray = sr.ReadLine().Split(',');
                        var id = translationArray[0];
                        translation.id = id;
                        translation.languageTexts = new List<LanguageText>();
                        for (int i = 1; i < translationArray.Length; ++i)
                        {
                            LanguageText langText = new LanguageText();
                            
                            var lang = langArray[i];
                            var text = translationArray[i];
                            langText.language = lang;
                            langText.text = text;

                            translation.languageTexts.Add(langText);
                        }
                        translations.Add(translation);
                        readLanguages = true;
                    }
                }
            }
        }

        private string GetExportPath() => Application.dataPath + "/Export/translation.csv";
        #endregion //EDITOR_FUNCTIONS
    }
}

