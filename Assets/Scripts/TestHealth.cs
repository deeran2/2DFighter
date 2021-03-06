﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHealth : MonoBehaviour {

	private float currentHealth;
	private float maxHealth = 1f;
	private float lastHitTime;
	private float repeatDamagePeriod = 2f;

	private Animator anim;
	private Rigidbody2D rb;
	private PlayerMovement move;

	[SerializeField]
	private Slider healthBar;
	[SerializeField]
	private Image healthColor;
	[SerializeField]
	private Transform respawnPoint;
	private bool isDead = false;

	[SerializeField]
	private GameObject shield;
	[SerializeField]
	private GameObject stars;
	private float maxShieldHealth = 1;
	private float currentShieldHealth;
	private Vector2 shieldSize;
	private bool shieldUp = false;


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;

		currentShieldHealth = maxShieldHealth;
		shieldSize = shield.transform.localScale;

		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		move = GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = currentHealth;
		healthColor.color = Color.Lerp (Color.red, Color.green, currentHealth / maxHealth);

		if (!move.firing && !isDead) {
			Block ();
		}

	}

	void OnCollisionEnter2D(Collision2D col){

		if (shieldUp) {
			if (col.gameObject.tag == "Damager") {
				float damage = col.transform.GetComponent<Arrow> ().damage;
				currentShieldHealth -= damage;
			}
		}
		else 
			if (col.gameObject.tag == "Damager") {
			if(Time.time > lastHitTime + repeatDamagePeriod){
				if (currentHealth > 0) {
					TakeDamage (col.transform);
					lastHitTime = Time.time;
				}
			}
		}
	}

	void TakeDamage(Transform damager){
		rb.AddForce (new Vector2(0f, 100f));

		float damage = damager.GetComponent<Arrow> ().damage;
		currentHealth -= damage;

		if (currentHealth <= 0) {
			Die ();
		} else {
			anim.SetTrigger ("Hurt");
		}
	}

	void Die(){
		anim.SetTrigger ("isDead");
		currentHealth = 0;
		transform.GetComponent<PlayerMovement> ().enabled = false;
		transform.GetComponent<PlayerAttack> ().enabled = false;
		rb.velocity = Vector2.zero;
		isDead = true;

		StartCoroutine (Respawn ());
	}

	IEnumerator Respawn(){
		yield return new WaitForSeconds (2f);

		isDead = false;
		currentHealth = maxHealth;
		transform.position = respawnPoint.position;
		anim.Play ("Archer1Idle");
		transform.GetComponent<PlayerMovement> ().enabled = true;
		transform.GetComponent<PlayerAttack> ().enabled = true;
	}

	void Block(){

			shield.SetActive (true);

			move.blocking = true;
			shieldUp = true;


			anim.SetBool ("BlockBool", true);
			shield.transform.localScale = shieldSize * (currentShieldHealth / maxShieldHealth);

//		} else {
//			shield.SetActive (false);
//
//			move.blocking = false;
//			shieldUp = false;
//
//			currentShieldHealth += 1f * Time.deltaTime;
//
//			anim.SetBool ("BlockBool", false);
//
//			if(currentShieldHealth >= maxShieldHealth){
//				currentShieldHealth = maxShieldHealth;}
//		}

		if(currentShieldHealth <= 0){

			currentShieldHealth = 0;

			shield.SetActive (false);
			stars.SetActive (true);

			isDead = true;
			move.blocking = true;

			anim.SetBool ("BlockBool", false);
			anim.SetTrigger("BlockBroken");

			shieldUp = false;

			StartCoroutine (Recover ());
		}
	}
	IEnumerator Recover(){
		
		yield return new WaitForSeconds (5f);

		anim.SetTrigger ("BlockRecover");

		stars.SetActive (false);

		isDead = false;
		move.blocking = false;
		currentShieldHealth = maxShieldHealth;
			
	}

}
