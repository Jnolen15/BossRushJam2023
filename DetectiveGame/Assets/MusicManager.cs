using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource idleMusic;
    [SerializeField] private AudioSource playMusic;

    public void StartTimed()
    {
        idleMusic.Stop();
        playMusic.Play();
    }

    public void StopMusic()
    {
        idleMusic.Stop();
        playMusic.Stop();
    }
}
