using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIAttack : MonoBehaviour {

	private AIMovement move;
	private Animator anim;
	[SerializeField]
	private Rigidbody2D arrow;
	[SerializeField]
	private Transform firePoint;
	private float arrowSpeed;
	private float reloadTime = 2f;
	private float fireTime;

	[SerializeField]
	private Slider arrowPower;
	[SerializeField]
	private Image powerImage;
	[SerializeField]
	private float aimDegrees = 0f;

	private bool chargingArrow = false;

	void Start () {
		anim = GetComponent<Animator> ();
		move = GetComponent<AIMovement> ();
		fireTime = Time.time;
		arrowPower.value = 0;
	}

	void Update () {
		if (!move.blocking) {
			Aim ();
			FireArrow ();
		}
	}

	void FireArrow(){

		if (Mathf.Abs (move.enemy.position.x - transform.position.x) < 10 && Time.time > fireTime) {
			float random = move.ReturnRandom ();
			if (random > 98 || chargingArrow) {
				chargingArrow = true;
				anim.SetBool ("DrawArrow", true);
				move.firing = true;
				arrowPower.value++;
				arrowSpeed = arrowPower.value;
				powerImage.color = Color.Lerp (Color.yellow, Color.red, arrowPower.value / arrowPower.maxValue);
			}
		}
		if(arrowPower.value == arrowPower.maxValue){
			anim.SetTrigger ("ReleaseArrow");
			anim.SetBool ("DrawArrow", false);
			move.firing = false;
			fireTime = Time.time + reloadTime;
			arrowPower.value = arrowPower.minValue;
			chargingArrow = false;
		}
	}

	void  Aim(){

		Vector3 relativePos = move.enemy.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		Quaternion newRotation = new Quaternion (0f, 0f, rotation.z, rotation.w);
		arrowPower.transform.rotation = newRotation;
		if (move.facingRight) {
			aimDegrees = arrowPower.transform.eulerAngles.z;
			} else {
			aimDegrees =360 - arrowPower.transform.eulerAngles.z;
		}
	}
	public void ArrowOne(){
		if (move.facingRight) {
			Rigidbody2D arrowIns = Instantiate (arrow, firePoint.position, Quaternion.Euler (new Vector3 (0, 0, -90 + aimDegrees))) as Rigidbody2D;
			arrowIns.velocity = new Vector2 (Mathf.Cos(aimDegrees * Mathf.Deg2Rad),
				Mathf.Sin(aimDegrees * Mathf.Deg2Rad)) * arrowSpeed;
		} else {
			Rigidbody2D arrowIns = Instantiate (arrow, firePoint.position, Quaternion.Euler (new Vector3 (0, 0, 90 - aimDegrees))) as Rigidbody2D;
			arrowIns.velocity = new Vector2 (-Mathf.Cos(aimDegrees * Mathf.Deg2Rad),
				Mathf.Sin(aimDegrees * Mathf.Deg2Rad)) * arrowSpeed;		
		}
	}

}
