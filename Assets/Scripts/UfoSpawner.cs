using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawner : MonoBehaviour
{
    private float spawnTime = 10;
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
            timeToSpawn = spawnTime + Time.time;
          //  Debug.Log("Spawning UFO");
            GameObject ufo;
            ufo = ObjectPooler.SharedInstance.GetPooledObject("Ufo");
            Debug.Log(ufo);
            if(ufo != null)
            {
                ufo.transform.position = Random.insideUnitSphere * 2000f;
                ufo.SetActive(true);
                
            }
        }
    }
}
