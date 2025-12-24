using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private float startMusicVolume;
    private float startSFXVolume;

    [Header("---------- Audio Source ----------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip backgroundMusic;
    public AudioClip stage1Music;
    public AudioClip clickEffect;
    public AudioClip jumpSound;
    public AudioClip fruitCollectedSound;
    public AudioClip playerDamageSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        startMusicVolume = PlayerPrefs.GetFloat("musicVolume");
        startSFXVolume = PlayerPrefs.GetFloat("SFXVolume");
    }

    private void Start()
    {
        MusicVolume(startMusicVolume);
        SFXVolume(startSFXVolume);
        musicSource.clip = backgroundMusic;
        PlayMusic();
    }

    public void ChangeMusic(AudioClip newClip)
    {
        if (musicSource.clip == newClip) return; // prevent restart same music

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();
    }

    public void RestartMusic()
    {
        if (musicSource.clip == null) return;

        musicSource.Stop();
        musicSource.time = 0f;   // rewind to start
        musicSource.Play();
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }
}
