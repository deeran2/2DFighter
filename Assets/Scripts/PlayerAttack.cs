using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

	private PlayerMovement move;
	private Animator anim;
	[SerializeField]
	private Rigidbody2D arrow;
	[SerializeField]
	private Transform firePoint;
	private float arrowSpeed;
	private float reloadTime = .5f;
	private float fireTime;

	[SerializeField]
	private Slider arrowPower;
	[SerializeField]
	private Image powerImage;
	[SerializeField]
	private float aimDegrees = 0f;

	void Start () {
		anim = GetComponent<Animator> ();
		move = GetComponent<PlayerMovement> ();
		fireTime = Time.time;
		arrowPower.value = 0;
	}
	
	void Update () {
		Aim ();
		FireArrow ();

	}

	void FireArrow(){

		if(Input.GetButton("Fire1") && Time.time > fireTime){
			anim.SetTrigger ("FireArrow");
			anim.SetBool ("DrawArrow", true);
			move.firing = true;
			arrowPower.value++;
			arrowSpeed = arrowPower.value;
			powerImage.color = Color.Lerp (Color.yellow, Color.red, arrowPower.value / arrowPower.maxValue);
		}
		if(Input.GetButtonUp("Fire1")){
			anim.SetTrigger ("ReleaseArrow");
			anim.SetBool ("DrawArrow", false);
			move.firing = false;
			fireTime = Time.time + reloadTime;
			arrowPower.value = arrowPower.minValue;
		}
	}

	void  Aim(){
		var v = Input.GetAxis ("Vertical");
		aimDegrees += v;
		if (aimDegrees > 60) {
			aimDegrees = 60;
		}else if (aimDegrees < -60){
			aimDegrees = -60;
		}
		if (move.facingRight) {
			arrowPower.transform.rotation = Quaternion.Euler (0f, 0f, aimDegrees);
		} else {
			arrowPower.transform.rotation = Quaternion.Euler (0f, 0f, -aimDegrees);
		}
	}
	public void ArrowOne(){
		if (move.facingRight) {
			Rigidbody2D arrowIns = Instantiate (arrow, firePoint.position, Quaternion.Euler (new Vector3 (0, 0, -90 + aimDegrees))) as Rigidbody2D;
			arrowIns.velocity = new Vector2 (Mathf.Cos(aimDegrees * Mathf.Deg2Rad),
				Mathf.Sin(aimDegrees * Mathf.Deg2Rad)) * arrowSpeed;
			//arrowIns.velocity = new Vector2 (arrowSpeed, 0);
		} else {
			Rigidbody2D arrowIns = Instantiate (arrow, firePoint.position, Quaternion.Euler (new Vector3 (0, 0, 90 - aimDegrees))) as Rigidbody2D;
			arrowIns.velocity = new Vector2 (-Mathf.Cos(aimDegrees * Mathf.Deg2Rad),
				Mathf.Sin(aimDegrees * Mathf.Deg2Rad)) * arrowSpeed;		
		}
	}

}
