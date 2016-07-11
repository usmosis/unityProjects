using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
 
	public static GameMaster gm;
	void Start()
	{
		if (gm == null)
		{
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>(); //GameObject big G is the base class for all entities in Unity. More general
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint; 
	public Transform spawnPrefab; //particle system 

	public int spawnDelay =2;

	public IEnumerator RespawnPlayer()
	{
		GetComponent<AudioSource>().Play(); //Dont forget the () after Play
		yield return new WaitForSeconds (spawnDelay);
		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject spawnParticles = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject; //or Quaternion.Identity. GameObject and as Gameobject make it so we can destroy these things with Destroy(object, time)
		Destroy(spawnParticles, 3f); //stops spawn particles from staying in the scene. 
	}
	public static void KillPlayer(Player player) //static so that we don't have to reference from the GameMaster every time. Player is reference to the Player class
	{
		Destroy (player.gameObject); //the game object that this class is attached to is gameObject. small g
		gm.StartCoroutine(gm.RespawnPlayer()); //don't forget the () and gm before both StartCoroutine and RespawnPlayer. 
	}


}