using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sounds : MonoBehaviour
{
    public Options o;
    public Pause p;
    public AudioSource clickaudio;
    public AudioSource musicsource;
    public AudioClip[] clips;
    public AudioClip[] musicclips;
    AudioClip music;
    AudioClip prevmusic;
    public float musicmultiplier;

    private void Update()
    {
        if (musicsource.isPlaying==false&& p != null)
        {
            Sound("music");
        }
    }

    public void Sound(string voicetype)
    {
        if (voicetype=="click")
        {
            clickaudio.pitch = Random.Range(1f, 1.3f);
            clickaudio.PlayOneShot(clips[Random.Range(0, 3)]);
        }
        else if (voicetype=="music")
        {
            music = musicclips[Random.Range(0, 2)];
            if (music!=prevmusic)
            {
                musicsource.PlayOneShot(music);
                prevmusic = music;
            }
        }
    }
    public void SetVolume()
    {
        clickaudio.volume= JsonUtility.FromJson<OptionsSave>(o.JSON).volume;
        if (p!=null)
        {
            musicsource.volume = JsonUtility.FromJson<OptionsSave>(o.JSON).music * p.musicmultiplier;
        }
        else
        {
            musicsource.volume = JsonUtility.FromJson<OptionsSave>(o.JSON).music;
        }
    }

}
