using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    private Rigidbody rb;
    private ScreenBounds screenBounds;
    private ObjectPooler objectPooler;

    private int mediumToSpawn = 2;
    private int smallToSpawn = 4;
    private float minRandomSpeed = 50f;
    private float maxRandomSpeed = 200f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        screenBounds = FindObjectOfType<ScreenBounds>();
    }

    private void OnEnable()
    {
    }

    private void Start()
    {
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
            }
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
            }
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
        if (other.tag == "PlayerBullet" && this.transform.tag == "AsteroidSmall")
        {
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
    }
}