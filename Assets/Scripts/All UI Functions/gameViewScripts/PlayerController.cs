using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody rb;

	// Use this for initialization
	void Start () {

        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        Speedometer.ShowSpeed(rb.velocity.magnitude,0,150);
	}
}
