using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_shot : MonoBehaviour {

    public GameObject thisObject;
    void Start(){
        thisObject = this.gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy_Laser") {
            //      other.gameObject.SetActive (false);
            Destroy (other.gameObject);
            if (thisObject.tag == "Player") {
                Destroy (gameObject);
                //            return;
            } 
//            Destroy (gameObject);
        }
    }
}
