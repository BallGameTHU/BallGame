using UnityEngine;
using System.Collections;

public class GuidePoints : MonoBehaviour {

	public Transform[] points;
	
	void OnDrawGizmos()
	{
		iTween.DrawPath (points);
	}
}
