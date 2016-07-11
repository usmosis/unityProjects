using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	[System.Serializable]//Makes playerStats available in Unity
	public class PlayerStats //This is a class within a class, great to avoid confusion because Player class can include many different things
	{
		public int Health = 100;
	}

	//Taking the definition of how the class should look, PlayerStats, and then instantiating it in a variable playerStats and calling it new PlayerStats();
	public PlayerStats playerStats = new PlayerStats(); 
	public int fallBoundary = -20;

	void Update ()
	{
		if (transform.position.y <= -20)
			DamagePlayer (99999);
	}

	public void DamagePlayer (int damage)
	{
		playerStats.Health -= damage; 
		if (playerStats.Health <= 0)
		{
			GameMaster.KillPlayer (this); //can use transform or gameObject
		}
	}




}
