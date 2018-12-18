using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel2 : MonoBehaviour {


	void Awake(){
//		GameObject Player = GameObject.FindGameObjectWithTag<"Player"> ();
		SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider> ();
		sphereCollider.isTrigger = true;
	}

	private void OnTriggerEnter(Collider Player) {
//		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider>();
		if (Player.gameObject.tag=="Player"){
			Scene level2 = SceneManager.GetSceneByName ("(Official Ground Level)");
			SceneManager.LoadScene ("(Official Ground Level)");
			SceneManager.SetActiveScene (level2);
			SceneManager.UnloadSceneAsync ("level1_escape");
		}
	}
	// Use this for initialization
	void Load () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
