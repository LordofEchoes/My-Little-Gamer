using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static int limit = 20;
    public int counter = 0;
    public int index = 0;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Start()
    {
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void PlayLooped(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
        s.source.loop = true;
    }

    IEnumerator PlayLoop()
    {
        System.Random Generator = new System.Random();
        while(counter < limit)
        {
            index = Generator.Next(0,sounds.Length);
            sounds[index].source.Play();
            yield return new WaitForSeconds(sounds[index].clip.length);
            counter++;
        }
    }
}
