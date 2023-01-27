using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButton : MonoBehaviour
{

    [SerializeField] private bool scoreD;
    [SerializeField] private bool scoreB;
    [SerializeField] private bool scoreS;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "WaveButtonD")
        {
            scoreD = true;
        }
        if (collision.tag == "WaveButtonB")
        {
            scoreB = true;
        }
        if (collision.tag == "WaveButtonS")
        {
            scoreS = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "WaveButtonD")
        {
            scoreD = false;
        }
        if (collision.tag == "WaveButtonB")
        {
            scoreB = false;
        }
        if (collision.tag == "WaveButtonS")
        {
            scoreS = false;
        }
    }
}
