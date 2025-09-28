using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;  // Global access point

    [Header("Audio Sources")]
    public AudioSource musicSource;   // For background music
    public AudioSource sfxSource;     // For one-shot sound effects

    [Header("Audio Clips")]
    public List<AudioClip> musicClips;
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> sfxDict;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Build lookup dictionaries for quick access
        musicDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in musicClips) musicDict[clip.name] = clip;

        sfxDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in sfxClips) sfxDict[clip.name] = clip;
    }

    // === MUSIC FUNCTIONS ===
    public void PlayMusic(string clipName, bool loop = true)
    {
        if (musicDict.TryGetValue(clipName, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning($"Music clip '{clipName}' not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // === SFX FUNCTIONS ===
    public void PlaySFX(string clipName)
    {
        if (sfxDict.TryGetValue(clipName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"SFX clip '{clipName}' not found!");
        }
    }
}
