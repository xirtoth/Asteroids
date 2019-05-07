using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private Rigidbody rb;
    private ScreenBounds screenBounds;
    private ObjectPooler objectPooler;
    private ScoreManager scoreManager;
    private AsteroidSpawner asteroidSpawner;

    private int mediumToSpawn = 2;
    private int smallToSpawn = 4;
    private float minRandomSpeed = 50f;
    private float maxRandomSpeed = 200f;
    private int largeScore = 100;
    private int mediumScore = 175;
    private int smallScore = 250;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        screenBounds = FindObjectOfType<ScreenBounds>();
        scoreManager = FindObjectOfType<ScoreManager>();
        asteroidSpawner = FindObjectOfType<AsteroidSpawner>();
    }

    private void OnEnable()
    {
    }

    private void Start()
    {
        Vector3 euler = transform.eulerAngles;
        euler.z = Random.Range(0, 360f);
        transform.eulerAngles = euler;
        rb.AddForce(transform.right * Random.Range(minRandomSpeed, maxRandomSpeed));
    }

    private void FixedUpdate()
    {
        transform.position = screenBounds.CheckBounds(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.sharedInstance.PlaySound(2);
            other.GetComponent<PlayerScript>().Die();
            // this.gameObject.SetActive(false);
        }

        if (other.tag == "PlayerBullet" && this.transform.tag == "AsteroidLarge")
        {
            for (int i = 0; i < mediumToSpawn; i++)
            {
                GameObject newAsteroid = ObjectPooler.SharedInstance.GetPooledObject("AsteroidMedium");
                Vector3 euler = newAsteroid.transform.eulerAngles;
                euler.z = Random.Range(0f, 360f);

                newAsteroid.transform.position = this.transform.position;
                newAsteroid.transform.eulerAngles = euler;
                newAsteroid.SetActive(true);
                asteroidSpawner.IncreaseAsteroids(newAsteroid);
            }
            AudioManager.sharedInstance.PlaySound(1);
            scoreManager.IncreaseScore(largeScore);
            asteroidSpawner.DecreaseAsteroids(this.gameObject);
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
        if (other.tag == "PlayerBullet" && this.transform.tag == "AsteroidMedium")
        {
            for (int i = 0; i < smallToSpawn; i++)
            {
                GameObject newAsteroid = ObjectPooler.SharedInstance.GetPooledObject("AsteroidSmall");
                Vector3 euler = newAsteroid.transform.eulerAngles;
                euler.z = Random.Range(0f, 360f);
                newAsteroid.transform.position = this.transform.position;
                newAsteroid.transform.eulerAngles = euler;
                newAsteroid.SetActive(true);
                asteroidSpawner.IncreaseAsteroids(newAsteroid);
            }
            AudioManager.sharedInstance.PlaySound(1);
            scoreManager.IncreaseScore(mediumScore);
            asteroidSpawner.DecreaseAsteroids(this.gameObject);
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
        if (other.tag == "PlayerBullet" && this.transform.tag == "AsteroidSmall")
        {
            AudioManager.sharedInstance.PlaySound(1);
            scoreManager.IncreaseScore(smallScore);
            asteroidSpawner.DecreaseAsteroids(this.gameObject);
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}