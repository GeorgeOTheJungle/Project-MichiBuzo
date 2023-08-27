using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioClip mainMenuSong;
    [SerializeField] private AudioClip gameSong;
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] audios;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlaySong(mainMenuSong);
    }

    public void PlaySong(bool mainMenu)
    {
        PlaySong(mainMenu ? mainMenuSong : gameSong);
    }

    public void StopSong()
    {
        audioSource.Stop();
    }

    private void PlaySong(AudioClip song)
    {
        audioSource.clip = song;
        audioSource.Play();
    }

    public void PlaySfx(int id)
    {
        sfxSource.clip = audios[id];
        sfxSource.Play();
    }
}
