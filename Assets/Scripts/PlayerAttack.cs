using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	private PlayerMovement move;
	private Animator anim;
	[SerializeField]
	private Rigidbody2D arrow;
	[SerializeField]
	private Transform firePoint;
	public float speed = 20f;

	void Start () {
		anim = GetComponent<Animator> ();
		move = GetComponent<PlayerMovement> ();
	}
	
	void Update () {
		FireArrow ();
	}

	void FireArrow(){
		if (Input.GetButtonDown ("Fire1")) {
			anim.SetTrigger ("FireArrow");


		}
	}
	public void ArrowOne(){
		if (move.facingRight) {
			Rigidbody2D arrowIns = Instantiate (arrow, firePoint.position, Quaternion.Euler (new Vector3 (0, 0, -90))) as Rigidbody2D;
			arrowIns.velocity = new Vector2 (speed, 0);
		} else {
			Rigidbody2D arrowIns = Instantiate (arrow, firePoint.position, Quaternion.Euler (new Vector3 (0, 0, 90))) as Rigidbody2D;
			arrowIns.velocity = new Vector2 (-speed, 0);
		}
	}

}
