using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {
	private Rigidbody2D rb;
	private float h;
	public float speed = 3f;
	[SerializeField]
	private bool grounded;
	private float jumpForce = 170f;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public bool facingRight = true;
	private Animator anim;

	public bool blocking = false;
	public bool firing = false;

	public Transform enemy;
	[SerializeField]
	private Transform jumpCheck;
	private bool needJump;

	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		grounded = false;

		if (!firing  && !blocking) {
			Jump ();
			Move ();
		}
	}

	void Move(){
		if (Mathf.Abs(enemy.position.x - transform.position.x) > 10) {

			if (enemy.position.x < transform.position.x) {
				h = -1;
			} else if (enemy.position.x > transform.position.x) {
				h = 1;
			} 
		}else {h = 0;}
		rb.velocity = new Vector2 (h * speed, rb.velocity.y);

		if (grounded) {
			anim.SetFloat ("Speed", Mathf.Abs (h));
		}

		if (enemy.position.x > transform.position.x && !facingRight) {
			Flip ();
		} else if (enemy.position.x < transform.position.x && facingRight) {
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

		Collider2D[] frontColliders = Physics2D.OverlapCircleAll(jumpCheck.position, .3f, whatIsGround);
		for (int i = 0; i < frontColliders.Length; i++)
		{
			if (frontColliders[i].gameObject != gameObject)
				needJump = true;
		}

		if (grounded && needJump) {
			grounded = false;
			needJump = false;
			anim.SetTrigger ("Jump");
			anim.SetBool ("isGrounded", false);
			JumpForce ();
		}
	}
	void JumpForce(){
		rb.AddForce (new Vector2 (0f, jumpForce));
	}

	public float ReturnRandom(){

		float random = Random.Range (0, 100);
		return random;
	}
}

