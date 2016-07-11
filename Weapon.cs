using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	public float fireRate = 0f;
	public float damage = 10f;
	public LayerMask whatToHit;

	public Transform bulletTrailPrefab;
	public Transform muzzleFlashPrefab;

	float timeToSpawnEffect;
	public float effectSpawnRate = 10;
	float timeToFire =0;
	Transform firePoint; //the child of Pistol in unity

	// Use this for initialization
	void Awake () 
	{
		firePoint = transform.FindChild ("FirePoint"); //find position of child of pistol called FirePoint. 
		if (firePoint == null) 
		{
			Debug.LogError ("No Fire Point!");
		}

	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (fireRate == 0f) 
		{
			if (Input.GetButtonDown ("Fire1")) ///GetButtonDown means to press once. 
			{ 
				Shoot ();
			} 
		}else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) //GetButton means to hold. GetButtonDown means to press. 
			{
				timeToFire = Time.time + 1/fireRate; //Time.time means current time. 
				//timeToFire will be the next time we want to fire, it will be the current time + the time it takes to fire, ie the delay. 
				Shoot ();
			}
		}
			
	}

	void Shoot()
	{
		// Camera.main targets the first enabled camera tagged mainCamera. ScreenToWorldPoint transforms Screen co-ords to World co-ords. 
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		//firePointPosition is the starting point of our shooting. It starts at the position of the child in Unity named FirePoint. 
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		//1-origin, 2-direction (simple mechanics b-a is direction AB), 3-distance, 4-layerMask. 
		RaycastHit2D Hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatToHit);

		if (Time.time > timeToSpawnEffect) //if the current time > timeToSpawnEffect
		{
			Effect ();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate; //see above description of timeToFire.
		}
		//Debug.Drawline draws a line between two points. firePointPosition is origin, (b-a direction) 
		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition)*100, Color.cyan);

		if (Hit.collider != null) //if not not hit, in other words if we hit. 
		{
			//start point is child "FirePoint", end point is Hit.point (point at which the ray hits a collider)
			Debug.DrawLine (firePointPosition, Hit.point, Color.red); //point is the point in the world where the ray hit the collider's surface.
			Debug.Log ("We hit " + Hit.collider.name + " And did " + damage + " Damage");
		}
	}
	void Effect() //IEnumerator instead of void allows yield, but we would have to change Effect(); to StartCoroutine("Effect");
	{
		Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation); //object, origin, rotation. .rotation is stored as a quaternion. 
		Transform clone = Instantiate(muzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform; //same as above, instantiate the muzzleFlash. We want to store this in a variable
		clone.parent = firePoint;//set parent of clone to firePoint with clone.parent.
		float size = Random.Range (0.6f, 0.9f); //min and max
		clone.localScale = new Vector3 (size, size, size); //localScale is the scale of the transform relative to the parent. Z component doesn't really matter but whatever. 
		//yield return 0; Would only use this if we had IEnumerator
		Destroy (clone.gameObject, 0.2f);//destroy clone after 0.2f //Whenever you want to destroy a transform we need .gameObject
	}


}
