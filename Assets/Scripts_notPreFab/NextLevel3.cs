using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel3 : MonoBehaviour {


	void Awake(){
//		GameObject Player = GameObject.FindGameObjectWithTag<"Player"> ();
		SphereCollider sphereCollider = gameObject.GetComponent<SphereCollider> ();
		sphereCollider.isTrigger = true;
	}

	private void OnTriggerEnter(Collider Player) {
//		Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Collider>();
		if (Player.gameObject.tag=="Player"){
			Scene level3 = SceneManager.GetSceneByName ("Level3");
			SceneManager.LoadScene ("Level3");
			SceneManager.SetActiveScene (level3);
			SceneManager.UnloadSceneAsync ("(Official Ground Level)");
		}
	}
	// Use this for initialization
	void Load () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

