using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButton : MonoBehaviour
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private Sounds audioManager;
    [SerializeField] private float scoreMultiplyer;
    [SerializeField] private bool isInteracting;
    [SerializeField] private GameObject effect;

    private GameObject currentCircle;
    private Circle currentCircleSC;
    private Rigidbody2D currentCircleRB;
    private fightWaveButton currentCircleWB;
    float x;
    float y;

    private void Update()
    {
        if(isInteracting)
        {
            if(currentCircleSC.isSlider)
            {
                if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Space))
                {
                    if(currentCircleSC.sliderLength > 0)
                    {
                        currentCircleRB.velocity = Vector3.zero;
                        currentCircleSC.sliderLength -= Time.deltaTime;
                        currentCircleWB.inContactSlider = true;
                        //ADD Point
                    }
                    else
                    {
                        Destroy(currentCircle.gameObject);
                        isInteracting = false;
                    }
                    

                }
                else
                {
                    currentCircleWB.inContactSlider = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space))
                {
                    waveManager.combo++;
                    waveManager.comboAnimator.Play("Combo1");
                    if (x < 3)
                    {
                        x = waveManager.comboText.transform.localScale.x;
                        y = waveManager.comboText.transform.localScale.y;
                        waveManager.comboText.transform.localScale = new Vector2(x + 0.04f, y + 0.04f);
                    }
                    waveManager.comboText.color = waveManager.comboColor.Evaluate(Mathf.Clamp01(waveManager.combo / 50f));


                    audioManager.PlaySound("kick");

                    waveManager.currentWave.damage -= (1 / (float)waveManager.currentWave.circles.Length) * scoreMultiplyer;
                    if (waveManager.currentWave.damage > 1f)
                        waveManager.currentWave.damage = 1;

                    Instantiate(effect, transform.position, Quaternion.identity);
                    Destroy(currentCircle.gameObject);
                    isInteracting = false;
                }
            }
        }
        //if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Space))
        //{
        //    if(isInteracting)
        //    {
        //        if(currentCircleSC.sliderLength > 0)
        //        {

        //        }
        //        else
        //        {
        //            waveManager.combo++;
        //            waveManager.comboAnimator.Play("Combo1");
        //            if (x<3)
        //            {
        //                x = waveManager.comboText.transform.localScale.x;
        //                y = waveManager.comboText.transform.localScale.y;
        //                waveManager.comboText.transform.localScale = new Vector2(x + 0.04f, y + 0.04f);
        //            }
        //            waveManager.comboText.color = waveManager.comboColor.Evaluate(Mathf.Clamp01(waveManager.combo/50f));


        //            audioManager.PlaySound("kick");

        //            waveManager.currentWave.damage -= (1/(float)waveManager.currentWave.circles.Length) * scoreMultiplyer;
        //            if (waveManager.currentWave.damage > 1f)
        //                waveManager.currentWave.damage = 1;

        //            Instantiate(effect, transform.position, Quaternion.identity);
        //            Destroy(currentCircle.gameObject);
        //            isInteracting = false;
        //        }
        //    }
        //}
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
            scoreMultiplyer = -0.1f;
            isInteracting = true;
            currentCircle = collision.gameObject.transform.parent.gameObject;
        }
        if (collision.tag == "WaveButtonS")
        {
            scoreMultiplyer = 0f;
            isInteracting = true;
            currentCircle = collision.gameObject.transform.parent.parent.gameObject;
        }
        currentCircleSC = currentCircle.GetComponent<fightWaveButton>().circle;
        currentCircleRB = currentCircle.GetComponent<Rigidbody2D>();
        currentCircleWB = currentCircle.GetComponent<fightWaveButton>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "WaveButtonF")
        {
            currentCircle = collision.gameObject.transform.parent.parent.parent.gameObject;
            currentCircleRB = currentCircle.GetComponent<Rigidbody2D>();
            currentCircleSC = currentCircle.GetComponent<fightWaveButton>().circle;
            currentCircleWB = currentCircle.GetComponent<fightWaveButton>();
        }
        if (collision.tag == "WaveButtonD")
        {
            currentCircle = collision.gameObject;
            currentCircleRB = currentCircle.GetComponent<Rigidbody2D>();
            currentCircleSC = currentCircle.GetComponent<fightWaveButton>().circle;
            currentCircleWB = currentCircle.GetComponent<fightWaveButton>();
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
            scoreMultiplyer = -0.1f;
        }
        if (collision.tag == "WaveButtonS")
        {
            scoreMultiplyer = 0f;
        }
    }
}
