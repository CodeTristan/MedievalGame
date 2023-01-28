using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{

    [SerializeField] private string DialogName;
    [SerializeField] private float firstCircleDelay;
    public GameObject prefab;
    public ColorChange ch;
    public Gradient fade;
    public Image lightout;
    [SerializeField] private Sounds soundManager;
    [SerializeField] private GameObject LightGameObject;
    public Transform spawnPoint;
    public TextMeshProUGUI damageText;
    public Wave[] waves;
    public Wave currentWave;
    public bool CanSpawnWave;

    private float currentDarknessTimer;
    private Light2D lightSc;
    private Animator lightAnimator;
    private bool lightOn = true;
    private DialogManager dialogManager;
    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        lightSc = LightGameObject.GetComponent<Light2D>();
        lightAnimator = LightGameObject.GetComponent<Animator>();
        currentWave = waves[0];
        if(CanSpawnWave)
            StartCoroutine(SpawnWave(currentWave));
    }

    int clicknumber = -1;
    float elapsedtime;
    private void Update()
    {
        elapsedtime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(clicknumber+" "+elapsedtime);
            if (clicknumber!=-1)
            {
                waves[0].circles[clicknumber].nextCircleSpawnDelay = elapsedtime;
                waves[0].circles[clicknumber].prefab = prefab;
            }
            clicknumber++;
            elapsedtime = 0;
        }


        damageText.text = currentWave.damage.ToString();

        currentDarknessTimer -= Time.deltaTime;
        if (currentDarknessTimer < 1 && lightout.color.a == 0)
        {
            ch.StopAllCoroutines();
            ch.ChangeColorStart(2, fade, lightout, null, true);
        }
        if (currentDarknessTimer < 0)
        {
            if(lightOn == false && currentWave.darknessFakeCount > 0)
            {
                currentWave.darknessFakeCount--;
                lightAnimator.SetTrigger("LightFake");
                ch.StopAllCoroutines();
                ch.ChangeColorStart(5, fade, lightout, null, true);
                //lightAnimator.ResetTrigger("LightFake");
                currentDarknessTimer = currentWave.darkTimeTimer + 5;
            }
            if (lightOn == true)  //Means: light is on, turn it off
            {
                lightOn = false;
                lightSc.intensity = 0;
                lightAnimator.SetTrigger("LightOut");
                ch.StopAllCoroutines();
                ch.ChangeColorStart(2, fade, lightout, null, false);
                //  lightAnimator.ResetTrigger("LightOff");
                currentDarknessTimer = currentWave.darknessTime;
            }
            else
            {
                lightOn = true;
                lightSc.intensity = 1;
                lightAnimator.SetTrigger("LightIn");
                //   lightAnimator.ResetTrigger("LightIn");
                currentDarknessTimer = currentWave.darkTimeTimer;
            }
        }
    }
    public IEnumerator SpawnWave(Wave wave)
    {
        //Add Music
        soundManager.PlaySound(wave.musicName);

        yield return new WaitForSeconds(firstCircleDelay);

        currentDarknessTimer = wave.darkTimeTimer;
        for (int i = 0; i < wave.circles.Length; i++)
        {
            Instantiate(wave.circles[i].prefab, spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(wave.circles[i].nextCircleSpawnDelay);
        }

        yield return new WaitForSeconds(3);
        dialogManager.GetDialogs(dialogManager.FindPath(DialogName));
    }
}
