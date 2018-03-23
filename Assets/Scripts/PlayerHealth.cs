using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	private float currentHealth;
	private float maxHealth = 10;
	private float lastHitTime;
	private float repeatDamagePeriod = 2f;

	private Animator anim;
	private Rigidbody2D rb;

	[SerializeField]
	private Slider healthBar;
	[SerializeField]
	private Image healthColor;
	[SerializeField]
	private Transform respawnPoint;


	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.value = currentHealth;
		healthColor.color = Color.Lerp (Color.red, Color.green, currentHealth / maxHealth);
	}

	void OnCollisionEnter2D(Collision2D col){

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

		StartCoroutine (Respawn ());
	}

	IEnumerator Respawn(){
		yield return new WaitForSeconds (2f);

		currentHealth = maxHealth;
		transform.position = respawnPoint.position;
		anim.Play ("Archer1Idle");
		transform.GetComponent<PlayerMovement> ().enabled = true;
		transform.GetComponent<PlayerAttack> ().enabled = true;
	}

}
