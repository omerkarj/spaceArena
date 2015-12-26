﻿using UnityEngine;
using System.Collections;
using System;

public class Enemy2Movement : MonoBehaviour {

    // Push parameters
    public float MinForce = 40f;
    public float MaxForce = 100f;
    public float DirectionChangeInterval = 1f;
    public int healthCounter = 8;
    public GameObject Child;
    public int damageReward = 10;
    public int killReward = 100;
    public Transform explosionParticles;
    public Transform hitParticles;

    private float x;
    private float y;
    private bool movement = false;

    // Use this for initialization
    void Start () {
        StartCoroutine(initialMovement());
        
        StartCoroutine(couroutineThatWaits());
        StartCoroutine(movementCoroutine());
    }

    private IEnumerator initialMovement()
    {
        iTween.MoveBy(gameObject, new Vector3(-4, 0, 0), 2f);
        yield return new WaitForSeconds(0);
    }

    IEnumerator movementCoroutine()
    {
        while (true)
        {
            if (transform.position.y > 3) { 
                iTween.MoveBy(gameObject, new Vector3(0, UnityEngine.Random.Range(-9f, -4f), 0), 4f);
            }
            else 
            {
                iTween.MoveBy(gameObject, new Vector3(0, UnityEngine.Random.Range(3f,7f), 0), 2f);
            }
            yield return new WaitForSeconds(4f);

        }

    }
    IEnumerator couroutineThatWaits()
    {
        yield return new WaitForSeconds(3.8f);
        movement = true;
        while (true)
        {
            Instantiate(Child, transform.position + new Vector3(-1.5f, 0, 0), new Quaternion());
            yield return new WaitForSeconds(3.8f);
        }
        
        

    }
        
	
	// Update is called once per frame
	void Update ()
    {
        if (movement)
        { 
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.1f, 0.9f);
        pos.y = Mathf.Clamp(pos.y, 0.1f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        }

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "PlayerShot")
        {
            
            healthCounter--;
            Instantiate(hitParticles, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            addScore(damageReward);

        }

        // Check if enemy is dead
        if (healthCounter == 0)
        {
            Destroy(gameObject);
            Instantiate(explosionParticles, other.transform.position, Quaternion.identity);
            addScore(killReward);
        }
        if (other.gameObject.tag == "Missile")
        {
            Destroy(gameObject);
            addScore(killReward);
        }
    }

    void addScore(int value)
    {
        PlayerScore ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
        ps.updateScore(value);
    }
}
