﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {


	void Awake(){
		Destroy (gameObject, 2f);
	}
}
