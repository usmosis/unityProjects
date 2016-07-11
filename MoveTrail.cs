using UnityEngine;
using System.Collections;

public class MoveTrail : MonoBehaviour {

	public int moveSpeed = 230;
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.right * Time.deltaTime* moveSpeed); //transform.translate is used to move an object over time. 
		//Make sure to * by moveSpeed to move the bullet!
		//Vector3.right is in other words Vector3 (1,0,0)
		//hover mouse over for description
		//Time.deltaTime. Time gets time information from Unity. deltaTime retrieves the time taken to complete the last frame. 
		Destroy(gameObject, 1);
	
	}
}
