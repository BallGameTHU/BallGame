using UnityEngine;
using System.Collections;

public class EnvGenerator : MonoBehaviour {

	public RunWay runway1;
	public RunWay runway2;

	public int runwayCount = 1;

	public GameObject[] runways;



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void GenerateRunWay()
	{
		runwayCount++;
		int type = Random.Range (0, runways.Length);
		GameObject newForest = GameObject.Instantiate (runways [type], new Vector3 (0, 93, runwayCount * 3000), Quaternion.Euler(0,90,0)) as GameObject;
		runway1 = runway2;
		runway2 = newForest.GetComponent<RunWay> ();

	}
}
