using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButton : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private float scoreMultiplyer;
    [SerializeField] private bool isInteracting;

    private GameObject currentCircle;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space))
        {
            if(isInteracting)
            {
                Debug.Log("Killed");
                waveManager.currentWave.damage += 5 * scoreMultiplyer;
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
            scoreMultiplyer = -2f;
            isInteracting = true;
            currentCircle = collision.gameObject;
        }
        if (collision.tag == "WaveButtonD")
        {
            scoreMultiplyer = -0.5f;
            isInteracting = true;
            currentCircle = collision.gameObject;
        }
        if (collision.tag == "WaveButtonB")
        {
            scoreMultiplyer = 1f;
            isInteracting = true;
        }
        if (collision.tag == "WaveButtonS")
        {
            scoreMultiplyer = 2f;
            isInteracting = true;
            //currentCircle = collision.GetComponentInParent<GameObject>();
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
            scoreMultiplyer = 1f;
        }
        if (collision.tag == "WaveButtonS")
        {
            scoreMultiplyer = -0.5f;
        }
    }
}
