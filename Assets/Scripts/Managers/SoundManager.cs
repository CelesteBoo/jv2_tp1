using UnityEngine;

// If unused, remove it.
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.loop = true;
        audioSource.Play();
    }
}
