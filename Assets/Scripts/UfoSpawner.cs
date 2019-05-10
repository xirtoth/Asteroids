using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawner : MonoBehaviour
{
    private float spawnTime = 10f;
    private float timeToSpawn;
    private ScreenBounds screenBounds;


    


    private void Awake()
    {
        screenBounds = FindObjectOfType<ScreenBounds>();
    }

    private void Start()
    {
        timeToSpawn = Time.time + spawnTime;
    }

    private void Update()
    {
        if(Time.time > timeToSpawn)
        {
            GameObject ufo;
            ufo = ObjectPooler.SharedInstance.GetPooledObject("Ufo");
            if(ufo != null)
            {
                ufo.transform.position = Random.insideUnitSphere * 20f;
                ufo.SetActive(true);
                timeToSpawn = spawnTime + Time.time;
            }
        }
    }
}
