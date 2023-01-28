using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private Sounds soundManager;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private Image charImage;
    [SerializeField] private Image fullScreenImage;
    [SerializeField] private GameObject dialogScreen;
    [SerializeField] private float typeDelay;

    public Sprite[] AllSprites;
    public string[] AllDialogPaths;
    public Sprite[] AllFullScreenImages;
    public List<Dialog> dialogs;

    private StreamReader reader;
    private Queue<string> sentences;
    private int dialogIndex;
    private string sentence;
    [SerializeField] private bool showFirstDialogDebug;
    private Sprite FullSCreenImageToShow;
    private string currentDialogPath;
    private string fightLevelName;
    private void Start()
    {
        dialogs = new List<Dialog>();
        sentences = new Queue<string>();
        currentDialogPath = "StartDialog";
        if (showFirstDialogDebug)
            GetDialogs(FindPath(currentDialogPath));
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && dialogScreen.activeSelf)
        {
            if (dialogText.text != sentence)
            {
                StopAllCoroutines();
                dialogText.text = sentence;
            }
            else
            {
                DisplayNextSentence();
            }
        }
    }
    public IEnumerator TypeLetter(string sentence)
    {
        dialogText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typeDelay);
        }
    }

    public void DisplayNextSentence()
    {
        if (dialogIndex == dialogs.Count)
        {
            EndDialog();
            return;
        }
        if(sentences.Count == 0)
        {
            dialogIndex++;
            StartDialog();
        }
        else
        {
            sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeLetter(sentence));
        }
    }

    public void StartDialog()
    {
        if(dialogIndex == dialogs.Count)
        {
            EndDialog();
            return;
        }
        dialogScreen.SetActive(true);
        Dialog currentDialog = dialogs[dialogIndex];
        foreach (string sentence in currentDialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //Renaming speakers name
        nameText.text = currentDialog.name;

        //Checking if there are character sprite
        if(currentDialog.dialogSprite == null)
        {
            charImage.gameObject.SetActive(false);
        }
        else
        {
            charImage.gameObject.SetActive(true);
            charImage.sprite = currentDialog.dialogSprite;
        }

        //Checking if there are fullscreen images
        if (FullSCreenImageToShow == null)
        {
            fullScreenImage.gameObject.SetActive(false);
        }
        else
        {
            fullScreenImage.gameObject.SetActive(true);
            fullScreenImage.sprite = FullSCreenImageToShow;
        }

        //Checking if there are sound effects
        if (currentDialog.soundName != null)
        {
            soundManager.PlaySound(currentDialog.soundName);
        }

        DisplayNextSentence();
    }

    public void EndDialog()
    {
        if(fightLevelName != null && fightLevelName != "")
        {
            SceneManager.LoadScene(fightLevelName);
            return;
        }
        if(currentDialogPath == null)
        {
            dialogScreen.SetActive(false);
        }
        else
        {
            GetDialogs(currentDialogPath);
        }
    }
    public void GetDialogs(string path)
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
        dialogs.Clear();
        dialogIndex = 0;
        sentences.Clear();
        FullSCreenImageToShow = null;
        fightLevelName = null;
        currentDialogPath = null;

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
            // & means next dialog to display
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
                if (reader.Peek() == '#')
                {
                    reader.Read(); // Skips '#' char
                    tempDialog.soundName = reader.ReadLine();  //Reads sound name.
                }
                else
                {
                    tempDialog.soundName = null;
                }

                //Full screen Image to play
                if (reader.Peek() == '$')
                {
                    reader.Read(); // Skips '$' char
                    FullSCreenImageToShow = FindFullScreenImageSprite(reader.ReadLine());
                }

                //Next Dialog to display
                if (reader.Peek() == '&')
                {
                    reader.Read(); // Skips '&' char
                    currentDialogPath = FindPath(reader.ReadLine());
                }

                // '/' is for using special characters in first line so it skips it
                if(reader.Peek() == '/')
                {
                    reader.Read();  //Skips '/' char
                    tempDialog.sentences.Add(reader.ReadLine());
                }

                // '%' is for battlefield level name
                if (reader.Peek() == '%')
                {
                    reader.Read();  //Skips '%' char
                    fightLevelName = reader.ReadLine();
                }


            }
            else
            {
                if (reader.Peek() == '/')
                {
                    reader.Read();  //Skips '/' char
                    tempDialog.sentences.Add(reader.ReadLine());
                }

                tempDialog.sentences.Add(reader.ReadLine());
            }

        }

        //Debugs

        //Debug.Log(dialogs.Count);

        //for (int i = 0; i < dialogs.Count; i++)
        //{
        //    Debug.Log("Name: " + dialogs[i].name);

        //    if (dialogs[i].dialogSprite != null)
        //        Debug.Log("Sprite: " + dialogs[i].dialogSprite.name);
        //    else
        //        Debug.Log("Sprite: null");

        //    for (int j = 0; j < dialogs[i].sentences.Count; j++)
        //    {
        //        Debug.Log(dialogs[i].sentences[j]);
        //    }

        //    Debug.Log("-------");
        //}

        Debug.Log(fightLevelName);
        //Closes the StreamReader
        reader.Close();

        //TO-DO: Connect UI with code.
        StartDialog();
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
