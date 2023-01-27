using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public bool isDefensive;
    public float damage;
    public float darkTimeTimer;
    public float darknessTime;
    public int darknessFakeCount;
    public string musicName;
    public Circle[] circles;

}

[System.Serializable]
public class Circle
{
    public float nextCircleSpawnDelay;
    public GameObject prefab;
}
