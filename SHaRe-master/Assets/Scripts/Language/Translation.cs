using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation {

    public enum AvailableLanguage
    {
        EN,
        DE,
        PT
    }

    static AvailableLanguage _defaultLanguage = AvailableLanguage.EN;
    
    static LanguageFile _languageFile;

    static string _notFoundPlaceholder = "[Translation missing]";

	static Translation()
    {
        if (!LoadLanguage(GameSettings.Language))
        {

            Debug.LogError(string.Format("Language {0} could not be loaded!", GameSettings.Language.ToString()));
        }
    }

    public static void Test()
    {
        Debug.Log("Test");
        LanguageFile l = LanguageFile.Create("en", "English");
        l.Save();
    }

    public static bool LoadLanguage(AvailableLanguage language)
    {
        LanguageFile loaded = LanguageFile.Load(language.ToString().ToLower());
        if (loaded != null)
        {
            _languageFile = loaded;
            return true;
        }
        else
        {
            Debug.LogError("Language could not be loaded: " + language);
            return false;
        }   
    }

    public static string Get(string key)
    {
        string translation;
        if (_languageFile == null || !_languageFile.translations.TryGetValue(key, out translation))
            return _notFoundPlaceholder;

        return translation;
    }

}
