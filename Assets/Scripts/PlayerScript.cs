using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private ObjectPooler objectPooler;

    public GameObject playerExplosionParticle;
    public GameObject gameOverPanel;
    public Transform playerShip;
    private bool isDead = false;
    private float undestructableTime = 2f;
    private int lives = 3;
    public float maxSpeed = 1;
    public float speed = 10;
    public float speedMultiplier = 1;
    public float rotationSpeed;
    public float rotationMultiplier = 2;
    

    private ScoreManager scoreManager;

    public Transform shootPoint;
    public ScreenBounds screenBounds;

    public void Die()
    {
        lives--;
        CheckLives();
        scoreManager.SetLives(lives);
        GameObject go = Instantiate(playerExplosionParticle, transform.position, Quaternion.identity) as GameObject;
        go.GetComponent<ParticleSystem>().Play();
        Destroy(go, 2);
        isDead = true;
        playerShip.GetComponent<Renderer>().enabled = false;
        this.gameObject.transform.position = Vector3.zero;

        rb.velocity = Vector3.zero;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("Respawn", 2f);
    }

    private void CheckLives()
    {
        if (lives <= 0)
        {
            lives = 0;
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void Respawn()
    {
        rb.velocity = Vector3.zero;
        isDead = false;
        playerShip.GetComponent<Renderer>().enabled = true;
        transform.position = Vector3.zero;
        this.gameObject.SetActive(true);
        Invoke("SetColliderActive", undestructableTime);
    }

    private void SetColliderActive()
    {
        GetComponent<BoxCollider>().enabled = true;
    }
    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        rb = GetComponent<Rigidbody>();
        screenBounds = FindObjectOfType<ScreenBounds>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (isDead == false)
        {
            CheckInput();
        }
    }

    private void CheckInput()
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
            AudioManager.sharedInstance.PlaySound(0);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(rotationSpeed * rotationMultiplier, 0, 0));

        rb.AddForce(-transform.forward * speed * speedMultiplier);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
}