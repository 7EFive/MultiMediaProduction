using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    //Tryin to do some musik
    public AudioClip[] AudioClip;
    public AudioSource audioSource;
    public static PlayerSoundManager instance;
    public void firstPunch()
    {
        audioSource.clip = AudioClip[0];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void secondPunch()
    {
        audioSource.clip = AudioClip[1];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void thirdPunch()
    {
        audioSource.clip = AudioClip[2];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void chargeSound()
    {
        audioSource.clip = AudioClip[3];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void chargeSoundStop()
    {
        //audioSource.clip = AudioClip[3];
        audioSource.Stop();
    }
}
