using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name;
    public List<string> sentences;
    public Sprite dialogSprite;
    public string soundName;
    public Dialog()
    {
        sentences = new List<string>();
    }

}
