using UnityEngine;
using System.Collections;

public class enemy_cannon : MonoBehaviour {

//	private Enemy_Ship playerTracking;
	public Transform m_muzzle;
	public GameObject m_shotPrefab;

	float timer = 0;
//	bool timerReached = false;


	// Use this for initialization
	void Start()
	{
//		playerTracking = GetComponent<Enemy_Ship>();
	}

	// Update is called once per frame
	void Update()
	{
//		playerTracking.enabled = true;
		if (timer < .1f)
			timer += .1f*Time.deltaTime;

		if (timer > .1f)
		{
//			Debug.Log("Done waiting");
			shoot(3);

			//Set to false so that We don't run this again
//			timerReached = true;
			timer = 0;
		}
	}

	private void shoot(int amount)
	{
			StartCoroutine(FireShots(amount));
		}

	private IEnumerator FireShots(int numShots){
		for (int i = 0; i < numShots; i++){
			GameObject go = GameObject.Instantiate(m_shotPrefab, m_muzzle.position, m_muzzle.rotation) as GameObject;
			GameObject.Destroy(go, 3f);
			yield return new WaitForSeconds(.3f);
//			}
		}
	}
}
