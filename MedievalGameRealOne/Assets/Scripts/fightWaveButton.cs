using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightWaveButton : MonoBehaviour
{
    private Wave currentWave;
    [HideInInspector] public WaveManager waveManager;
    public float speed;
    private Rigidbody2D rb;
    private Transform target;
    private Vector3 aim;
    private bool dying;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        aim = target.position - transform.position;
        currentWave = waveManager.currentWave;
    }

    void Update()
    {
        rb.velocity = aim * speed;
        if (transform.position.y < -5 && !dying)
        {
            dying = true;
            kill();
        }
    }

    private void kill()
    {
        currentWave.damage -= (1 / (float)currentWave.circles.Length);
        waveManager.combo = 0;
        Destroy(gameObject);
    }
}
