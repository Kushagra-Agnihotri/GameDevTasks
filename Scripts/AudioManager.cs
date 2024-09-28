using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip backgroundMusic;
    public AudioClip jumpSound;
    public AudioClip coinSound;
    public AudioClip crashSound;
    public AudioClip deathSound;


    public float coinVol =0.1f ,jumpVol = 0.5f,CrashVol =0.25f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        audioSource.Stop();
    }

    public void PlayJumpSound()
    {
        PlaySoundEffect(jumpSound, jumpVol);
    }

    public void PlayCoinSound()
    {
        PlaySoundEffect(coinSound, coinVol);
    }

    public void PlayCrashSound()
    {
        PlaySoundEffect(crashSound, CrashVol);
    }
    public void PlayDeathSound()
    {
        PlaySoundEffect(deathSound, 1.0f); 
    }

    private void PlaySoundEffect(AudioClip clip, float volume)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}
