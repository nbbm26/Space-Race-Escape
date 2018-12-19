using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Hi, First you need to check the collision between your player and your chechpoint and if it is true it set a static bool to true like: public static bool HaveCheckpoint; void OnTriggerEnter (collision Col) { if (Col.gameObject.tag == “checkpoint”) { HaveCheckpoint = true; } }

Then when you respawn you must say to your player to go to the checkpoint position. After the reloading scene script write: public Transform CheckpointPosition

//after reload scene if (HaveCheckpoint == true) { transform.position = CheckpointPosition.position; }

Assign your checkpoint Transform in the inspector. When you finish your level reset HaveCheckpoint to false.
*/

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
