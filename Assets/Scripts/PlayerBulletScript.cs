using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private Rigidbody rb;
    private ScreenBounds screenBounds;
    private GameObject player;
    private float bulletSpeed = 350f;
    private float aliveTime = 3f;
    private float timeToDestroy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        screenBounds = FindObjectOfType<ScreenBounds>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        transform.forward = -player.transform.forward;
        rb.AddForce(-player.transform.forward * bulletSpeed);
        timeToDestroy = Time.time + aliveTime;
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (Time.time > timeToDestroy)
        {
            gameObject.SetActive(false);
        }

        transform.position = screenBounds.CheckBounds(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ufo")
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}