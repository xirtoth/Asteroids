using UnityEngine;

public class UfoScript : MonoBehaviour
{
    private float shootTime = 2f;
    private float nextShoot;
    private float speed = 3f;
    private GameObject player;
    private Rigidbody rb;
    private ScreenBounds screenBounds;

   

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        screenBounds = FindObjectOfType<ScreenBounds>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        nextShoot = Time.time + shootTime;
        rb.AddForce((Vector3.zero - transform.position) * speed);
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
            }
        }

        transform.position = screenBounds.CheckBounds(transform);
    }
}