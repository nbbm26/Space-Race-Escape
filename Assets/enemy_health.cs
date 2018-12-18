using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class enemy_health : MonoBehaviour {

	public Image EcurrentHealthBar;
	public float EcurrentHealth = 100;
	public float EmaxHealth = 100;
//	public bool EcanTakeDamage;
//	public float damageDelay = .5f;
	public GameObject EhealthBar;
//	private ufo_AI_script ufo_ship_script;


	void Awake()
	{
//		ufo_ship_script = this.GetComponent<ufo_AI_script> ();
	}

	void Update()
	{
//		this.gameObject.GetComponent<ufo_AI_script> ().enabled = true;
		float ratio = EcurrentHealth / EmaxHealth;
		EcurrentHealthBar.rectTransform.localScale = new Vector3(ratio,1,1);
//		EcanTakeDamage = false;
//		Invoke("ECanTakeDamage", damageDelay);
		if (EcurrentHealth <= 0)
		{
			Destroy (this.gameObject);
	}
	}

	void ETakeDamage()
	{
		EcurrentHealth -= 20;
		Update();

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "enemy_Laser") {
			return;
		}
		if (other.CompareTag("Laser")) {
			//			ETakeDamage();
			EcurrentHealth -= 10;
						return;
		}        
		//		if (EcanTakeDamage)
		//		{
		//			ETakeDamage();
		//		}   
	}

//	void ECanTakeDamage()
//	{
//		EcanTakeDamage = true;
//	}


}
