using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    AudioSource alertSource;
    public AudioClip Alert;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        alertSource = GetComponent<AudioSource>();
        alertSource.clip = Alert;
    }

    public void PlayAlert()
    {
        if (!alertSource.isPlaying)
        {
            alertSource.Play();
        }
    }

    public void StopAlert()
    {
        if (alertSource.isPlaying)
        {
            alertSource.Stop();
        }
    }
}