using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip audioClip; // Assign your audio clip in the Unity Editor
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            // If there is no AudioSource component on the GameObject, add one.
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set the AudioClip for the AudioSource
        audioSource.clip = audioClip;
        // Set loop to true to make the audio loop
        audioSource.loop = true;
    }

    // Call this method to play the audio
    public void PlayAudio()
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.Play();
        }
    }
}