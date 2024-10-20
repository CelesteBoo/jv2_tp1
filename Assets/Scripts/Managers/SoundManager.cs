using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip gameWonMusic;
    private AudioSource audioSource;

    private void Awake()
    {
        var eventChannels = Finder.EventChannels;
        eventChannels.OnGameWon += OnGameWon;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }

    private void OnGameWon()
    {
        audioSource.Stop();
        audioSource.clip = gameWonMusic;
        audioSource.loop = false;
        audioSource.Play();
    }
}
