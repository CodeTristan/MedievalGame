using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class DialogManager : MonoBehaviour
{
    public Sprite[] AllSprites;
    public string[] AllDialogPaths;
    public Sprite[] AllFullScreenImages;
    public List<Dialog> dialogs;

    private StreamReader reader;
    private void Start()
    {
        dialogs = new List<Dialog>();
        StartDialog(FindPath("text"));
    }

    public void StartDialog(string path)
    {

        //Tries to open file in given path.
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
            //This means its the start of dialog. After the - comes name of the speaker.
            // - means speaker's name
            // * means speaker's sprite
            // # means sound effect to play
            // $ means full screen picture to show
            if (reader.Peek() == '-') 
            {
                if (tempDialog.sentences.Count > 0)
                {
                    dialogs.Add(tempDialog);
                }
                tempDialog = new Dialog();

                //Speaker's name
                reader.Read(); // Skips '-' char
                tempDialog.name = reader.ReadLine();   //Reads name

                //Speaker's sprite
                if(reader.Peek() == '*')
                {
                    reader.Read(); // Skips '*' char
                    tempDialog.dialogSprite = FindSprite(reader.ReadLine());  //Reads spriteName and sends it to FindSprite function.
                }
                else
                {
                    tempDialog.dialogSprite = null;
                }
                

                //Sound Effect
                //Add sound effect thing when sound manager is ready
                //Add sound material to dialog

                //Full screen Image to play
                if(reader.Peek() == '$')
                {
                    reader.Read(); // Skips '$' char
                    tempDialog.fullScreenImageSprite = FindFullScreenImageSprite(reader.ReadLine());
                }
                else
                {
                    tempDialog.fullScreenImageSprite = null;
                }
                

            }
            else
            {
                tempDialog.sentences.Add(reader.ReadLine());
            }

        }

        //Debugs

        Debug.Log(dialogs.Count);

        for (int i = 0; i < dialogs.Count; i++)
        {
            Debug.Log("Name: " + dialogs[i].name);

            if (dialogs[i].dialogSprite != null)
                Debug.Log("Sprite: " + dialogs[i].dialogSprite.name);
            else
                Debug.Log("Sprite: null");

            for (int j = 0; j < dialogs[i].sentences.Count; j++)
            {
                Debug.Log(dialogs[i].sentences[j]);
            }

            Debug.Log("-------");
        }

        //Closes the StreamReader
        reader.Close();

        //TO-DO: Connect UI with code.

    }

    // Finds and returns sprite in AllSprites array according to given string spriteName
    private Sprite FindSprite(string spriteName)
    {
        if (spriteName == null)  //This is end indicator. This doesnt effect dialogs so in order to prevent unneccesary error log. It returns here.
            return null;

        for (int i = 0; i < AllSprites.Length; i++)
        {
            if (spriteName == AllSprites[i].name)
            {
                return AllSprites[i]    ;
            }
        }

        Debug.Log("ERROR: " + spriteName + " named sprite couldn't found.");
        return null;
    }

    //Finds the given text file's path in assets.
    private string FindPath(string name)
    {
        for (int i = 0; i < AllDialogPaths.Length; i++)
        {
            if(AllDialogPaths[i] == name)
            {
                return "Assets/Dialogs/" + AllDialogPaths[i] + ".txt";
            }
        }

        //If file path couldn't found returns error string
        return "NoPathErrorInFindPathFunction";
    }

    //Finds the given named sprite in AllFullScreenImages array
    private Sprite FindFullScreenImageSprite(string spriteName)
    {
        for (int i = 0; i < AllFullScreenImages.Length; i++)
        {
            if(AllFullScreenImages[i].name == spriteName)
            {
                return AllFullScreenImages[i];
            }
        }

        //If none sprite is found. It returns null
        return null;
    }
}
