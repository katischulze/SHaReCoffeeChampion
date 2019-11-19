using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static string configPath = "Assets/Resources/Music/Clips.music";
    private static string musicPath = "Music/BackgroundMusic/";
    private AudioClip guitarStart;
    private AudioClip pianoStart;
    private AudioClip bassStart;
	private AudioClip drumStart;
    private static LinkedList<string> pianoFiles = new LinkedList<string>();
    private static LinkedList<string> guitarFiles = new LinkedList<string>();
    private static LinkedList<string> bassFiles = new LinkedList<string>();
	private static LinkedList<string> drumFiles = new LinkedList<string>();
    private LinkedList<AudioClip> guitarClips = new LinkedList<AudioClip>();
    private LinkedList<AudioClip> pianoClips = new LinkedList<AudioClip>();
    private LinkedList<AudioClip> bassClips = new LinkedList<AudioClip>();
	private LinkedList<AudioClip> drumClips = new LinkedList<AudioClip>();

    [SerializeField]
    AudioSource guitarSource;
    [SerializeField]
    AudioSource pianoSource;
    [SerializeField]
    AudioSource bassSource;
	[SerializeField]
    AudioSource drumSource;
    public static MusicManager instance = null;
    //public float lowPitchRange = .95f;
    //public float highPitchRange = 1.05f;


	// Use this for initialization
	void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        LoadClips();
        FirstClip();
	}

    private void Update()
    {
        if (!guitarSource.isPlaying)
        {
            RandomizeClips();
        }
    }

    private void FirstClip()
    {
        guitarSource.clip = guitarStart;
        pianoSource.clip = pianoStart;
        bassSource.clip = bassStart;
		drumSource.clip = drumStart;
        guitarSource.Play();
        pianoSource.Play();
        bassSource.Play();
		drumSource.Play();
    }

    //public AudioClip PickRandomClip(params AudioClip[] clips)
    //{
    //    int randomIndex = Random.Range(0, clips.Length);

    //    return clips[randomIndex];
    //}

    public void RandomizeClips()
    {
        //guitar
        LinkedList<AudioClip>.Enumerator en = guitarClips.GetEnumerator();
        int randomIndex = Random.Range(0, guitarClips.Count);
        for (int i = 0; i <= randomIndex; i++)
        {
            en.MoveNext();
        }
        guitarSource.clip = en.Current;
        en.Dispose();

		//piano
        en = pianoClips.GetEnumerator();
        randomIndex = Random.Range(0, pianoClips.Count);
        for (int i = 0; i <= randomIndex; i++)
        {
            en.MoveNext();
        }
        pianoSource.clip = en.Current;
        en.Dispose();

		//bass
        en = bassClips.GetEnumerator();
        randomIndex = Random.Range(0, bassClips.Count);
        for (int i = 0; i <= randomIndex; i++)
        {
            en.MoveNext();
        }
        bassSource.clip = en.Current;
        en.Dispose();
		
		//drum
        en = drumClips.GetEnumerator();
        randomIndex = Random.Range(0, drumClips.Count);
        for (int i = 0; i <= randomIndex; i++)
        {
            en.MoveNext();
        }
        drumSource.clip = en.Current;
        en.Dispose();

        guitarSource.Play();
        pianoSource.Play();
        bassSource.Play();
		drumSource.Play();
    }

    private void LoadClips()
    {
        AudioClip clip;
        string path = "";

        Parse();
        while (guitarFiles.Count != 0)
        {
            path = musicPath + guitarFiles.Last.Value;
            clip = Resources.Load(path) as AudioClip;

            guitarClips.AddLast(clip);
            guitarFiles.RemoveLast();
        }
        while (pianoFiles.Count != 0)
        {
            path = musicPath + pianoFiles.Last.Value;
            clip = Resources.Load<AudioClip>(path);

            pianoClips.AddLast(clip);
            pianoFiles.RemoveLast();
        }
        while (bassFiles.Count != 0)
        {
            path = musicPath + bassFiles.Last.Value;
            clip = Resources.Load<AudioClip>(path);

            bassClips.AddLast(clip);
            bassFiles.RemoveLast();
        }
		while (drumFiles.Count != 0)
        {
            path = musicPath + drumFiles.Last.Value;
            clip = Resources.Load<AudioClip>(path);

            drumClips.AddLast(clip);
            drumFiles.RemoveLast();
        }

        guitarStart = Resources.Load<AudioClip>(musicPath + "Dm7-G7-C7M-C6_guitar_1");
        pianoStart = Resources.Load<AudioClip>(musicPath + "Dm7-G7-C7M-C6_piano_1");
        bassStart = Resources.Load<AudioClip>(musicPath + "Dm7-G7-C7M-C6_empty");
		bassStart = Resources.Load<AudioClip>(musicPath + "Dm7-G7-C7M-C6_empty");
    }

    private void Parse()
    {
        System.IO.StreamReader reader = System.IO.File.OpenText(configPath);
        string line = "";
        string path = "";

        while ((line = reader.ReadLine()) != null)
        {
            if (line.Contains("#Bass"))
            {
                path = line.Substring(6);
                bassFiles.AddLast(path);
            }
            else if (line.Contains("#Piano"))
            {
                path = line.Substring(7);
                pianoFiles.AddLast(path);
            }
            else if (line.Contains("#Guitar"))
            {
                path = line.Substring(8);
                guitarFiles.AddLast(path);
            }
			else if (line.Contains("#Drum"))
            {
                path = line.Substring(6);
                drumFiles.AddLast(path);
            }
        }
    }
}
