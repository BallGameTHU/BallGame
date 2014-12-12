using UnityEngine;
using System.Collections;

public enum GameState{Menu,Playing,End}

public class GameController : MonoBehaviour 
{
	public static GameState gamestate= GameState.Menu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gamestate == GameState.Menu)
		{
			if(Input.GetMouseButtonDown(0))
			{
				gamestate = GameState.Playing;	
			}
		}
		if(gamestate == GameState.End)
		{
			if(Input.GetMouseButtonDown(0))
			{
				gamestate = GameState.Menu;
				Application.LoadLevel(Application.loadedLevelName);
			}
		}
	}
}
