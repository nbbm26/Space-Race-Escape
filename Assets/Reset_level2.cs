using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset_level2 : MonoBehaviour {

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
        //            if (!GameObject.FindGameObjectWithTag("PlayerShip"))
        //        {
//        if (Input.GetKey(KeyCode.B))
//        {
//            for (int i =0; i < 1000000;i++){
//                if (i > 100000)
//                {
                    Reset();
//                    break;
//                }
//            }
            //        }
//        }
    }
    void Reset(){
        Scene level2 = SceneManager.GetSceneByName ("(Official Ground Level)");
        SceneManager.UnloadSceneAsync ("(Official Ground Level)");
        SceneManager.LoadScene ("(Official Ground Level)");
        SceneManager.SetActiveScene (level2);

    }
}
