using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fightWaveButton : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private Transform target;
    private Vector3 aim;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        aim = target.position - transform.position;
    }

    void Update()
    {
        rb.velocity = aim * speed;
        if (transform.position.y < -10)
            kill();
    }

    private void kill()
    {
        //Add point adjustment
        Destroy(gameObject);
    }
}
