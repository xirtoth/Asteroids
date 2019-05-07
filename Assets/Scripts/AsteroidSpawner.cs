using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    private Vector3 spawnPosition = Vector3.zero;
    private ObjectPooler objectPooler;
    private int maxAsteroids = 1;
    private List<GameObject> remainingAsteroids = new List<GameObject>();
    // public GameObject asteroid;

    private void Start()
    {
        SpawnAsteroids();
    }


    public void RestartGame()
    {
        maxAsteroids = 1;
        remainingAsteroids.Clear();

    }

    private void SpawnAsteroids()
    {
        for (int i = 0; i < maxAsteroids; i++)
        {
            GameObject asteroid = ObjectPooler.SharedInstance.GetPooledObject("AsteroidLarge");

            if (asteroid != null)
            {
                asteroid.transform.position = Random.insideUnitSphere * 20f;
                asteroid.transform.position = new Vector3(asteroid.transform.position.x, asteroid.transform.position.y, 0);
                asteroid.SetActive(true);
                IncreaseAsteroids(asteroid);
            }
        }
    }

    public void IncreaseAsteroids(GameObject asteroid)
    {
        remainingAsteroids.Add(asteroid);
        
    }

    public void DecreaseAsteroids(GameObject asteroid)
    {
        remainingAsteroids.Remove(asteroid);
    }

    private void Update()
    {
       if(remainingAsteroids.Count <= 0)
        {
            maxAsteroids++;
            SpawnAsteroids();
        }
    }
}