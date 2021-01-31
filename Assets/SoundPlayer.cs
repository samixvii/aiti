using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource start;
    public AudioSource loop;

    // Start is called before the first frame update
    void Start()
    {
        start.Play();
        Invoke(nameof(PlayLoop), start.clip.length);
    }

    void PlayLoop()
    {
        loop.Play();
    }

}
