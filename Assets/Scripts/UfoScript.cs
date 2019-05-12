using UnityEngine;

public class UfoScript : MonoBehaviour
{
    private float shootTime = 1f;
    private float nextShoot;
    private float speed = 150f;
    private GameObject player;
    private Rigidbody rb;
    private ScreenBounds screenBounds;
    private AudioManager audioManager;

   

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = FindObjectOfType<ScreenBounds>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnEnable()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        rb.AddForce((player.transform.position - transform.position).normalized * speed);
    }
    private void Start()
    {
        nextShoot = Time.time + shootTime;
        
    }

    private void Update()
    {
        if (Time.time > nextShoot)
        {
            GameObject eBullet;
            eBullet = ObjectPooler.SharedInstance.GetPooledObject("UfoBullet");
            if (eBullet != null)
            {
                eBullet.transform.position = transform.position;
                eBullet.transform.rotation = transform.rotation;
                nextShoot = Time.time + shootTime;
                eBullet.SetActive(true);
                audioManager.PlaySound(3);
                eBullet.GetComponent<Rigidbody>().AddForce((player.transform.position - transform.position).normalized * 500);
            }
        }

        transform.position = screenBounds.CheckBounds(transform);
    }
}