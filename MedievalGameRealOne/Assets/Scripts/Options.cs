using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider soundbar;
    public Slider musicbar;
    public string JSON;
    public Sounds s;

    private void Start()
    {
        OptionsLoad();
    }

    public void OptionsSave()
    {
        OptionsSave save =  optionssave();
        string json = JsonUtility.ToJson(save);
        StreamWriter sw = new StreamWriter(Application.dataPath + "/JSONdata.text");
        sw.Write(json);
        sw.Close();
        OptionsLoad();
    }
    public void OptionsLoad()
    {
        OptionsSave save = optionssave();
        string json = JsonUtility.ToJson(save);
        StreamReader sr = new StreamReader(Application.dataPath + "/JSONdata.text");
        json = sr.ReadToEnd();
        sr.Close();
        OptionsSave saved = JsonUtility.FromJson<OptionsSave>(json);
        if (soundbar!=null)
        {
            soundbar.value = saved.volume;
            musicbar.value = saved.music;
        }     
        JSON = json;
        s.SetVolume();

    }
    private OptionsSave optionssave()
    {
        OptionsSave save = new OptionsSave();
        save.volume = soundbar.value;
        save.music = musicbar.value;

        return save;
    }
}
