using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelecter : MonoBehaviour
{
    public GameObject lockedUI;
    public GameObject unlockedUI;
    private void OnEnable()
    {
        CheckForBooleans();
    }

    private void CheckForBooleans()
    {
        for (int i = 1; i <= 3; i++)
        {
            if (PlayerPrefs.GetInt("END" + i) == 0)
                return;
        }

        //If every ending has been seen
        lockedUI.SetActive(false);
        unlockedUI.SetActive(true);
    }

    public void LoadKingRoute()
    {
        SceneManager.LoadScene("KingRoute");
    }
    public void LoadOlgaRoute()
    {
        SceneManager.LoadScene("OlgaRoute");
    }
    public void LoadPaiterRoute()
    {
        SceneManager.LoadScene("PaiterRoute");
    }
    public void LoadVirgiliusRoute()
    {
        SceneManager.LoadScene("VirgiliusRoute");
    }
}
