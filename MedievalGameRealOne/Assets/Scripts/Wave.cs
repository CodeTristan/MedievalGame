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
    public List<float> SpawnTime;
    public Circle[] circles;

    public Wave()
    {
        SpawnTime = new List<float>();
    }
}

[System.Serializable]
public class Circle
{
    public float nextCircleSpawnDelay;
    public GameObject prefab;
}
