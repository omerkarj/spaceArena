﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject spaceship;
	public bool isPowerupOn=false;
    private WeaponController weaponController;
    
	// Use this for initialization
	void Start () {
        weaponController = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponController>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void FixedUpdate() {
		if(isPowerupOn){

        	if (Input.GetKeyDown(KeyCode.Space)) {
           	 spaceship.GetComponent<missileLauncher>().launchNumber = GameObject.FindGameObjectsWithTag("target").Length;
           	 Debug.Log(spaceship.GetComponent<missileLauncher>().launchNumber + " missiles launched!");
            	spaceship.GetComponent<missileLauncher>().startLaunch = true;
				isPowerupOn=false;
				GameObject player=GameObject.FindGameObjectWithTag("Player");
				player.GetComponent<PlayerScore>().powerUpCounter=0;
				player.GetComponent<SpecialPower>().PowerUp(0,player.GetComponent<PlayerScore>().pointsForPowerUp);
			}
        }
    }

    void OnTriggerEnter (Collider other) {
        switch (other.gameObject.tag) {
            // Weapon pickups
            case "WeaponDrop-LaserGun":
                weaponController.SwitchWeapon(WeaponController.Weapons.laserGun);
                break;
            case "WeaponDrop-AlienWeapon":
                weaponController.SwitchWeapon(WeaponController.Weapons.alienWeapon);
                break;
        }
        if (other.gameObject.tag != "PlayerShot" && other.gameObject.tag != "target")
            Destroy(other.gameObject);
    }
}
