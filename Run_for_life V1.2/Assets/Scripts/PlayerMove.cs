using UnityEngine;
using System.Collections;

public enum TouchDir{
	None,
	Left,
	Right,
}


public class PlayerMove : MonoBehaviour 
{
	public float moveSpeed = 100;       //小球向前运动速度
	public float moveHorizontalSpeed = 5;//小球水平移动运动速度
	public float rotateSpeed = 120;//小球向前滚动的角速度（？）我大物没学好\(^o^)/
	
	private EnvGenerator envgenerator;
	private TouchDir touchdir = TouchDir.None;
	private Vector3 lastMouseDown = Vector3.zero;
	private Transform ball;
	private Transform guide;
	private float angle = 0;


	void Awake()
	{
		envgenerator = Camera.main.GetComponent<EnvGenerator> ();
		guide = GameObject.FindGameObjectWithTag (Tags.guide).transform;
		ball = this.transform.Find ("ball").transform;
	}


	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(GameController.gamestate == GameState.Playing)
		{
			Vector3 targetPos = envgenerator.runway1.GetNextTargetPoint ();
			Vector3 movDir = targetPos - guide.position;
			guide.position += movDir.normalized * moveSpeed * Time.deltaTime;
			transform.position = new Vector3(guide.position.x,0,guide.position.z);
		
			//transform.Translate(0,0,moveSpeed * Time.deltaTime);

			KeepRotate();
			MoveControl();
		}
	}

	private void KeepRotate()
	{
		//transform.Rotate (Vector3.up, rotateSpeed * Time.deltaTime, Space.World);	

		ball.Rotate (rotateSpeed * Time.deltaTime, 120 *Time.deltaTime,0);
	}

	private void MoveControl()
	{
		TouchDir dir = GetTouch ();
		Debug.Log (dir.ToString());


	}

	TouchDir GetTouch()
	{
		if(Input.GetMouseButtonDown(0))
		{
			lastMouseDown = Input.mousePosition;
		}
		if(Input.GetMouseButtonUp(0))
		{
			Vector3 mouseUp = Input.mousePosition;
			Vector3 touchOffset = mouseUp - lastMouseDown;

			if(Mathf.Abs(touchOffset.x) > 50 || Mathf.Abs(touchOffset.y) > 50)
			{

				if(Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x >0)
				{

					return TouchDir.Right;
				}
				else if(Mathf.Abs(touchOffset.x) > Mathf.Abs(touchOffset.y) && touchOffset.x < 0)
				{

					return TouchDir.Left;
				}

			}
		}
		return TouchDir.None;
	}
}















