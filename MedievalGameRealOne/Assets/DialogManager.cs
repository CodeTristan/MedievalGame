using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class DialogManager : MonoBehaviour
{
    public DialogSprite[] AllSprites;
    public List<Dialog> dialogs;

    private StreamReader reader;
    string path = "Assets/text.txt";
    private void Start()
    {

        //Start Dialog Part
        dialogs = new List<Dialog>();

        try
        {
            reader = new StreamReader(path, Encoding.GetEncoding("ISO-8859-9"));
        }
        catch (Exception e)
        {
            Debug.LogError("File Exception occured: " + e.Message);
            return;
        }

        Dialog tempDialog = new Dialog();
        while (reader.EndOfStream == false)
        {
            if (reader.Peek() == '<')
            {
                //This char is for spacing and it means skip this 
                reader.ReadLine();
            }
            if (reader.Peek() == '-')
            {
                if (tempDialog.sentences.Count > 0)
                {
                    dialogs.Add(tempDialog);
                }
                tempDialog = new Dialog();

                reader.Read(); // Skips '-' char
                tempDialog.name = reader.ReadLine();   //Reads name

                reader.Read(); // Skips '*' char
                string spriteName = reader.ReadLine();
                tempDialog.dialogSprite.sprite = FindSprite(spriteName);  //Reads spriteName and sends it to FindSprite function.
                tempDialog.dialogSprite.assetName = spriteName;
            }
            else
            {
                tempDialog.sentences.Add(reader.ReadLine());
            }

        }

        Debug.Log(dialogs.Count);

        for (int i = 0; i < dialogs.Count; i++)
        {
            Debug.Log("Name: " + dialogs[i].name + "  Sprite: " + dialogs[i].dialogSprite.assetName);

            for (int j = 0; j < dialogs[i].sentences.Count; j++)
            {
                Debug.Log(dialogs[i].sentences[j]);
            }

            Debug.Log("-------");
        }


        reader.Close();
    }

    // Finds and returns sprite in AllSprites array according to given string spriteName
    private Sprite FindSprite(string spriteName)
    {
        for (int i = 0; i < AllSprites.Length; i++)
        {
            if (spriteName == AllSprites[i].assetName)
            {
                return AllSprites[i].sprite;
            }
        }

        Debug.Log("ERROR: " + spriteName + " named sprite couldn't found.");
        return null;
    }
}
