using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//my code is betiful
public class Sounds : MonoBehaviour
{
    public SoundClass[] sounds;

    public Options o;
    public Pause p;
    public AudioSource[] audiosources; 
    public float musicmultiplier;

    private void Update()
    {
        if (Input.GetKey("f") && Input.GetKey("1"))
        {
            SceneManager.LoadScene("Fight1");
        }
        else if (Input.GetKey("f") && Input.GetKey("2"))
        {
            SceneManager.LoadScene("Fight2");
        }
        else if (Input.GetKey("f") && Input.GetKey("3"))
        {
            SceneManager.LoadScene("Fight3");
        }
        else if (Input.GetKey("f") && Input.GetKey("4"))
        {
            SceneManager.LoadScene("Fight4");
        }
        else if (Input.GetKey("f") && Input.GetKey("5"))
        {
            SceneManager.LoadScene("Fight5");
        }
    }
    private void Awake()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;
        }
    }
    public void PlaySound(string soundname)
    {
        SoundClass s = null;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == soundname)
            {
                s = sounds[i];
            }
        }
        if (s!=null)
        {
            s.source.clip = s.clip[Random.Range(0,s.clip.Length)];
            s.source.Play();
        }
        else
        {
            Debug.LogWarning(soundname + " Sound Name Not Valid");
        }
    }

    public void SetVolume()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == "Music1")
            {
                if (p != null)
                {
                    sounds[i].source.volume = sounds[i].volume * JsonUtility.FromJson<OptionsSave>(o.JSON).music * p.musicmultiplier;
                }
                else
                {
                    sounds[i].source.volume = sounds[i].volume * JsonUtility.FromJson<OptionsSave>(o.JSON).music;
                }
            }
            else
            {
                sounds[i].source.volume = sounds[i].volume * JsonUtility.FromJson<OptionsSave>(o.JSON).volume;
            }

        }
    }

}
