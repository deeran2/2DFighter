using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Rigidbody2D rb;
	public float speed = 3f;
	[SerializeField]
	private bool grounded;
	private float jumpForce = 170f;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public bool facingRight = true;
	private Animator anim;
	public bool firing = false;

	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	void FixedUpdate () {
		grounded = false;
			
		if (!firing) {
			Jump ();
			Move ();
		}
	}

	void Move(){
			float h = Input.GetAxis ("Horizontal");
			rb.velocity = new Vector2 (h * speed, rb.velocity.y);

			if (grounded) {
				anim.SetFloat ("Speed", Mathf.Abs (h));
			}

			if (h > 0 && !facingRight) {
				Flip ();
			} else if (h < 0 && facingRight) {
				Flip ();
			}
		
	}

	void Flip(){
		facingRight = !facingRight;

		Vector2 inverse = transform.localScale;
		inverse.x *= -1;
		transform.localScale = inverse;
		}

	void Jump(){
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .1f, whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
				grounded = true;
		}
		anim.SetBool ("isGrounded", grounded);

		if (grounded && Input.GetButton ("Jump")) {
			grounded = false;
			anim.SetTrigger ("Jump");
			anim.SetBool ("isGrounded", false);
			JumpForce ();
		}
	}
	void JumpForce(){
		rb.AddForce (new Vector2 (0f, jumpForce));
	}


}
