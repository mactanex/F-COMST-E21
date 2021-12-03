using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public static void Play (string name)
    {
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found");
            return;
        }
        s.source.Play();
    }

    public static void DelayAndPlay(string name, float time)
    {
        instance.PlayWithDelay(name, time);
    }

    private void PlayWithDelay(string name, float time)
    {
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + "not found");
            return;
        }
        StartCoroutine(DelayedPlay(time, s));
    }

    private IEnumerator DelayedPlay(float time, Sound s)
    {
        yield return new WaitForSeconds(time);
        s.source.Play();
    }

    public static AudioSource GetSource(string name)
    {
        Sound s = Array.Find(instance.sounds, sound => sound.name == name);
        
        return s.source;
    }
}
