using UnityEngine;
using System.Collections;

public class PlayerCollider : MonoBehaviour 
{
	
	void OnTriggerStay(Collider other)
	{
		//StartCoroutine(Test());
		Vector3 obs = other.gameObject.transform.position;
		Vector3 player = this.transform.position;

		if(other.tag == Tags.obstacles && GameController.gamestate == GameState.Playing && Vector3.Distance(obs,player) < 30.0f)
		{

			Debug.Log ("You lose" + other.gameObject.transform.position + other.gameObject.transform.rotation);
			GameController.gamestate = GameState.End;

		}
	}
}
