using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightWaveButton : MonoBehaviour
{
    [HideInInspector] public Wave currentWave;
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
        currentWave.damage -= (1 / (float)currentWave.circles.Length) * 2;
        Destroy(gameObject);
    }
}
