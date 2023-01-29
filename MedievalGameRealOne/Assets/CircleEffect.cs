using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEffect : MonoBehaviour
{
    public float waitTime;

    private void Start()
    {
        Die();
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);
    }
}
