using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Doober_smackJOB : MonoBehaviour {

	public Transform target;
	private NavMeshAgent agent;
	// private RaycastHit hitInfo = new RaycastHit();


	// Use this for initialization
	void Start() {
		agent = GetComponent<NavMeshAgent>();
		target = GameObject.FindGameObjectWithTag("Player").transform;

	}

	// Update is called once per frame
	void Update () {


		/*/if(Input.GetMouseButtonDown(0)) 
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                agent.destination = hitInfo.point;
            }
        /
               } */

		agent.SetDestination(target.position);
	}
}
