using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButton : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Sounds audioManager;
    [SerializeField] private float scoreMultiplyer;
    [SerializeField] private bool isInteracting;

    private GameObject currentCircle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space))
        {
            if(isInteracting)
            {
                audioManager.PlaySound("kick");
                waveManager.currentWave.damage -= (1/(float)waveManager.currentWave.circles.Length) * scoreMultiplyer;
                if (waveManager.currentWave.damage > 1f)
                    waveManager.currentWave.damage = 1;
                //Add animations

                Destroy(currentCircle.gameObject);
                isInteracting = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WaveButtonF")
        {
            scoreMultiplyer = +2f;
            isInteracting = true;
            currentCircle = collision.gameObject.transform.parent.parent.parent.gameObject;

        }
        if (collision.tag == "WaveButtonD")
        {
            scoreMultiplyer = +1f;
            isInteracting = true;
            currentCircle = collision.gameObject;
        }
        if (collision.tag == "WaveButtonB")
        {
            scoreMultiplyer = -0.5f;
            isInteracting = true;
            currentCircle = collision.gameObject.transform.parent.gameObject;
        }
        if (collision.tag == "WaveButtonS")
        {
            scoreMultiplyer = 0f;
            isInteracting = true;
            currentCircle = collision.gameObject.transform.parent.parent.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "WaveButtonF")
        {
            currentCircle = collision.gameObject.transform.parent.parent.parent.gameObject;
        }
        if (collision.tag == "WaveButtonD")
        {
            currentCircle = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaveButtonF")
        {
            isInteracting = false;
        }
        if (collision.tag == "WaveButtonD")
        {
            isInteracting = false;
        }
        if (collision.tag == "WaveButtonB")
        {
            scoreMultiplyer = -0.5f;
        }
        if (collision.tag == "WaveButtonS")
        {
            scoreMultiplyer = 0f;
        }
    }
}
