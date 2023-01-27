using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public bool isDefensive;
    public float damage;
    public Circle[] circles;

    //Add music track in here

}

[System.Serializable]
public class Circle
{
    public int nextCircleSpawnDelay;
    public GameObject prefab;
}
