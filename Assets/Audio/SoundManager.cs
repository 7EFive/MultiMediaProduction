using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] AudiClip;
    public AudioSource audioSource;
    public void PlaySound(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

   // public void playFirst()
   // {
   //     audioSource.clip = AudioClip[0];
   //     audioSource.PlayOneShot();  
   // }
}
