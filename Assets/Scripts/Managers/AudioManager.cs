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
    private void Start()
    {
        alertSource = GetComponent<AudioSource>();
        alertSource.clip = Alert;
    }

    private void Update()
    {
        // ESC를 눌렀을 때 게임종료
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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