using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_chasing : MonoBehaviour {


    private enemy_hovercar ShipScript;

	// Use this for initialization
    void Start () {
        ShipScript = this.GetComponent<enemy_hovercar> ();
		
	}
	
	// Update is called once per frame
    void Update () {
        this.enabled = true;
        ShipScript.enabled = true;
		
	}
}
