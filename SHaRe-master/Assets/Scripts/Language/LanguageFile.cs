using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LanguageFile {

    private static readonly string _savePath = Application.streamingAssetsPath + "/Languages/";

    public string languageShort;

    public string languageLong;

    public Dictionary<string, string> translations;

    [SerializeField]
    private DictionaryEntry[] dictionaryEntries;

    private LanguageFile(string languageShort, string languageLong, Dictionary<string, string> translations)
    {
        this.languageShort = languageShort;
        this.languageLong = languageLong;
        this.translations = translations;
    }

    public static LanguageFile Load(string language)
    {
        using (StreamReader sr = new StreamReader(_savePath + language.ToLower() + ".lang", System.Text.Encoding.UTF8))
        {
            string json = sr.ReadToEnd();
            LanguageFile languageFile = JsonUtility.FromJson<LanguageFile>(json);
            languageFile.translations = new Dictionary<string, string>();
            foreach (DictionaryEntry entry in languageFile.dictionaryEntries)
            {
                languageFile.translations.Add(entry.key, entry.value);
            }
            return languageFile;
        }
    }

    public static LanguageFile Create(string languageShort, string languageLong)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add("Created_At", DateTime.Now.ToLongTimeString());
        return new LanguageFile(languageShort, languageLong, dictionary);
    }

    public void Save()
    {
        if (!Directory.Exists(_savePath))
            Directory.CreateDirectory(_savePath);

        dictionaryEntries = new DictionaryEntry[translations.Count];
        int i = 0;
        foreach (KeyValuePair<string, string> kvp in translations)
        {
            dictionaryEntries[i++] = new DictionaryEntry(kvp.Key, kvp.Value);
        }

        using (StreamWriter sw = new StreamWriter(_savePath + languageShort.ToLower() + ".lang", false, System.Text.Encoding.UTF8))
        {
            string json = JsonUtility.ToJson(this, true);
            sw.Write(json);
        }
        
    }
	
}

[Serializable]
class DictionaryEntry
{
    public string key;
    public string value;

    public DictionaryEntry(string key, string value)
    {
        this.key = key;
        this.value = value;
    }
}
