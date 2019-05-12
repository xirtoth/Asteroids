﻿using UnityEngine;

public class UfoBulletScript : MonoBehaviour
{
    private Rigidbody rb;
    private ScreenBounds screenBounds;
    private GameObject player, ufo;
    private float bulletSpeed = 500f;
    private float aliveTime = 3f;
    private float timeToDestroy;

    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        screenBounds = FindObjectOfType<ScreenBounds>();
        ufo = GameObject.FindGameObjectWithTag("Ufo");
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Start()
    {
       
      
        timeToDestroy = Time.time + aliveTime;
    }

    private void OnEnable()
    {

        
   /*     if (ufo == null)
        {
            return;
        } */

       
       
        timeToDestroy = aliveTime + Time.time;
       
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
        if(other.tag == "Player")
        {
            player.GetComponent<PlayerScript>().Die();
        }
    }
}