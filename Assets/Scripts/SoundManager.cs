using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundClip
{
	public string clipID;
	[Range(0f,1f)]
	public float volume;

	[SerializeField]
	AudioClip clip;
	public AudioClip Clip
	{
		get {
			if(clip == null) {
				clip = Resources.Load ("Sounds/" + clipID) as AudioClip;
			}
			return clip;
		}
	}
}

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance;
	void Awake()
	{ 
		if(Instance != null) {
			Destroy (this.gameObject);
			return;
		}

		DontDestroyOnLoad (this.gameObject);
		Instance = this;
	}

	public AudioClip fullClip;
	public AudioClip[] musicClips;

	public SoundClip[] soundClips;
	Dictionary<string, SoundClip> soundDict = new Dictionary<string, SoundClip> ();

	public GameObject sourceObject;
	Stack<AudioSource> availableSources = new Stack<AudioSource> ();


	AudioSource menuMusicSource;
	List<AudioSource> musicSources = new List<AudioSource>();

	void Start () {
		soundDict = new Dictionary<string, SoundClip> ();
		for(int i=0;i<soundClips.Length;i++) {
			if(!string.IsNullOrEmpty(soundClips[i].clipID))
				soundDict.Add (soundClips [i].clipID, soundClips [i]);
		}

		availableSources = new Stack<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)) {
			AddNextMusicTrack ();
		}
	}

	public void StartMainGame()
	{
		StopAllCoroutines ();
		if(menuMusicSource != null)
			menuMusicSource.Stop ();

		foreach (AudioSource source in musicSources)
			Destroy (source);
		musicSources = new List<AudioSource> ();

		AddNextMusicTrack ();
	}

	public void StartMenuMusic()
	{
		foreach (AudioSource source in musicSources)
			Destroy (source);
		musicSources = new List<AudioSource> ();

		if (menuMusicSource == null) {
			menuMusicSource = this.gameObject.AddComponent<AudioSource> ();
			menuMusicSource.clip = fullClip;
			menuMusicSource.loop = true;
			menuMusicSource.Play ();
			StartCoroutine ("FadeInVolume", menuMusicSource);
		} else {
			menuMusicSource.volume = 1.0f;
			menuMusicSource.Play ();
		}
	}

	public void AddNextMusicTrack()
	{
		if (musicSources.Count == musicClips.Length)
			return;

		AudioSource source = this.gameObject.AddComponent<AudioSource> ();
		source.clip = musicClips [musicSources.Count];
		source.volume = 0.0f;
		source.loop = true;

		if(musicSources.Count > 0) {
			source.time = musicSources [0].time;
		}

		source.Play ();
		StartCoroutine (FadeInVolume (source));
		musicSources.Add (source);
	}

	IEnumerator FadeInVolume(AudioSource source)
	{
		source.volume = 0.0f;
		while (source.volume < 1.0f) {
			source.volume += 0.005f;
			yield return new WaitForSeconds (0.01f);
		}
	}

	public SoundClip GetClipForID(string id)
	{
		SoundClip sound = null;
		soundDict.TryGetValue (id, out sound);

		if(sound == null) {
			Debug.LogError ("Could not find sound clip with id " + id);
			return null;
		}

		return sound;
	}

	public void PlaySoundEffect(string clipID, bool randomPitch = false)
	{
		SoundClip sound = GetClipForID (clipID);
		if(sound == null || sound.Clip == null) {
			return;
		}

		AudioSource soundSource = null;
		if(availableSources.Count == 0) {
			soundSource = sourceObject.AddComponent<AudioSource> ();
		} else {
			soundSource = availableSources.Pop ();
		}

		soundSource.loop = false;

		if (randomPitch)
			soundSource.pitch = Random.Range (0.8f, 1.2f);
		else
			soundSource.pitch = 1.0f;

		StartCoroutine (PlaySound (soundSource, sound));
	}

	IEnumerator PlaySound(AudioSource source, SoundClip sound)
	{
		source.volume = sound.volume;
		source.clip = sound.Clip;
		source.Play ();
		yield return null;
		while(source.isPlaying) {
			yield return null;
		}
		source.Stop ();
		availableSources.Push (source);
	}
}