using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private ObjectPooler objectPooler;
    public GameObject playerExplosionParticle;
    public Transform playerShip;

    public float maxSpeed = 1;
    public float speed = 10;
    public float speedMultiplier = 1;
    public float rotationSpeed;
    public float rotationMultiplier = 2;

    public Transform shootPoint;
    public ScreenBounds screenBounds;

    public void Die()
    {
        GameObject go = Instantiate(playerExplosionParticle, transform.position, Quaternion.identity) as GameObject;
        go.GetComponent<ParticleSystem>().Play();
        Destroy(go, 2);
        //  Invoke("Respawn", 3);
        this.gameObject.transform.position = Vector3.zero;

        rb.velocity = Vector3.zero;
    }

    private void Respawn()
    {
        transform.position = Vector3.zero;
        this.gameObject.SetActive(true);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        screenBounds = FindObjectOfType<ScreenBounds>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        speed = Input.GetAxis("Vertical");
        if (speed < 0)
        {
            speed = 0;
        }
        rotationSpeed = Input.GetAxis("Horizontal");
        transform.position = screenBounds.CheckBounds(transform);
    }

    private void Shoot()
    {
        GameObject bullet;

        bullet = ObjectPooler.SharedInstance.GetPooledObject("PlayerBullet");

        if (bullet != null)
        {
            bullet.transform.position = shootPoint.position;
            bullet.transform.rotation = transform.rotation;
            bullet.transform.forward = -transform.forward;
            bullet.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(rotationSpeed * rotationMultiplier, 0, 0));

        rb.AddForce(-transform.forward * speed * speedMultiplier);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}