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
    [SerializeField] private int darknessSpawnCircleCount;
    
    public GameObject prefab;
    public ColorChange ch;
    public Gradient fade;
    public Image lightout;
    [SerializeField] private Sounds soundManager;
    [SerializeField] private GameObject LightGameObject;
    public Transform spawnPoint;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI comboText;
    public Wave[] waves;
    public Wave currentWave;
    public bool CanSpawnWave;
    public int combo;
    public Gradient comboColor;

    private float currentDarknessTimer;
    private Light2D lightSc;
    private Animator lightAnimator;
    [HideInInspector] public Animator comboAnimator;
    private bool lightOn = true;
    private DialogManager dialogManager;
    private int currentCircleCount;
    private void Start()
    {
        dialogManager = FindObjectOfType<DialogManager>();
        lightSc = LightGameObject.GetComponent<Light2D>();
        comboAnimator = comboText.gameObject.GetComponent<Animator>();
        lightAnimator = LightGameObject.GetComponent<Animator>();
        currentWave = waves[0];
        currentDarknessTimer = currentWave.darkTimeTimer;
        currentWave.damage = 1;
        if (CanSpawnWave)
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

        if(combo > 5)
        {
            comboText.text = combo.ToString();
        }
        else
        {
            comboText.text = "";
        }
        damageText.text = "%"+ (currentWave.damage * 100).ToString();

        currentDarknessTimer -= Time.deltaTime;
        if (currentDarknessTimer < 1 && lightout.color.a == 0)
        {
            ch.StopAllCoroutines();
            ch.ChangeColorStart(2, fade, lightout, null, true);
        }
        if (currentDarknessTimer < 0 && currentCircleCount < darknessSpawnCircleCount)
        {
            if(lightOn == false && currentWave.darknessFakeCount > 0)
            {
                lightAnimator.SetBool("LightIn", false);
                lightAnimator.SetBool("LightFake",true);
                currentDarknessTimer = currentWave.darkTimeTimer + 5;
                ch.StopAllCoroutines();
                ch.ChangeColorStart(5, fade, lightout, null, true);
                if (currentWave.darknessFakeCount < 1)
                    lightAnimator.SetBool("LightFake", false);
                lightAnimator.Play("LightFake");

                lightAnimator.SetBool("LightIn", true);
                lightAnimator.SetBool("LightFake", false);
                currentWave.darknessFakeCount--;
            }
            if (lightOn == true)  //Means: light is on, turn it off
            {
                lightOn = false;
                lightSc.intensity = 0;
                lightAnimator.SetBool("LightIn", false);
                lightAnimator.SetBool("LightOut", true);
                ch.StopAllCoroutines();
                ch.ChangeColorStart(2, fade, lightout, null, false);
                currentDarknessTimer = currentWave.darknessTime;
            }
            else
            {
                lightOn = true;
                lightSc.intensity = 1;
                lightAnimator.SetBool("LightOut", false);
                lightAnimator.SetBool("LightIn", true);
                currentDarknessTimer = currentWave.darkTimeTimer;

            }
        }
    }
    public IEnumerator SpawnWave(Wave wave)
    {
        //Add Music
        yield return new WaitForSeconds(1);
        soundManager.PlaySound(wave.musicName);

        yield return new WaitForSeconds(firstCircleDelay);

        currentDarknessTimer = wave.darkTimeTimer;
        for (int i = 0; i < wave.circles.Length; i++)
        {
            Instantiate(wave.circles[i].prefab, spawnPoint.position, Quaternion.identity).gameObject.GetComponent<fightWaveButton>().waveManager = this;
            currentCircleCount = GameObject.FindGameObjectsWithTag("WaveButtonD").Length;
            yield return new WaitForSeconds(wave.circles[i].nextCircleSpawnDelay);
        }

        yield return new WaitForSeconds(3);
        currentDarknessTimer = 100000;
        lightAnimator.SetBool("LightIn", true);
        dialogManager.GetDialogs(dialogManager.FindPath(DialogName));
    }
}
