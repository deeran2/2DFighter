using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

	public float damage = 5;

	void Awake(){
		Destroy (gameObject, 2f);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.transform.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
