using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerShip")
        {
            return;
        }
else if(other.tag == "Laser"){
//		other.gameObject.SetActive (false);
        Destroy(other.gameObject);
        Destroy(gameObject);
}
    }
}
