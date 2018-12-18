using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset_level3 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update () {
//            if (!GameObject.FindGameObjectWithTag("PlayerShip"))
//        {
//            if (Input.GetKey(KeyCode.B))
//            {
//            for (int i =0; i < 1000000;i++){
//                if (i > 100000)
//                {
                    Reset();
//                    break;
//                }
//            }
//        }
//	}
    }
    void Reset(){
        Scene level3 = SceneManager.GetSceneByName ("Level3");
        SceneManager.UnloadSceneAsync ("Level3");
        SceneManager.LoadScene ("Level3");
        SceneManager.SetActiveScene (level3);

    }
}
