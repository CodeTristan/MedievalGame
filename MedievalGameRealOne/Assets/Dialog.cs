using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogSprite
{
    public string assetName;
    public Sprite sprite;
}
[System.Serializable]
public class Dialog
{
    public string name;
    public List<string> sentences;
    public DialogSprite dialogSprite;
    public Dialog()
    {
        sentences = new List<string>();
        dialogSprite = new DialogSprite();
    }

}
