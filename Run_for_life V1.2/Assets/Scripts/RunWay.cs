using UnityEngine;
using System.Collections;

public class RunWay : MonoBehaviour 
{

	// Use this for initialization
	public GameObject[] obstacles;
	public float startLength = 50; //Obstacle start Position  N/A
	public int minLength = 200;//Obstacle
	public int maxLength = 300;//Obstacle
	public float rotateSpeed = 10.0f;

	private Transform guide;
	private GuidePoints guidePoint;
	private int targetWayPointIndex;
	private EnvGenerator envGenerator;
	private Transform body;
	private Transform m_transform;
	private float rotateAngle = 0.0f;

	void Awake()
	{
		m_transform = this.transform;

		guide = GameObject.FindGameObjectWithTag (Tags.guide).transform;
		guidePoint = transform.Find ("GuidePoint").GetComponent<GuidePoints> ();
		body = transform.Find ("Line002").transform;
		targetWayPointIndex = guidePoint.points.Length - 2;
		envGenerator = Camera.main.GetComponent<EnvGenerator> ();
	}

	void Start()
	{
		GenerateObstacle ();
	}


	// Update is called once per frame
	void Update () 
	{

		if(GameController.gamestate == GameState.Playing)
		{
			rotateAngle += rotateSpeed *Input.acceleration.x;//Input.acceleration.x;
			Debug.Log(Input.acceleration.x);
			m_transform.rotation = Quaternion.Euler(0,90,rotateAngle);
		}
	}


	void GenerateObstacle()
	{
		float startZ = transform.position.z;
		float endZ = startZ + 3000;
		float z = startZ;

		while (true) 
		{
			z += Random.Range(minLength,maxLength);
			if(z > endZ)
			{
				break;
			}
			else
			{
				Vector3 center = GetWayPosiByZ(z);
				float R = 93;
				//生成障碍物的角度
				int obsAngle = Random.Range(30,300);
				//转换成弧度
				float obsAngleR = obsAngle * 3.14f/180;
				int obsIndex = Random.Range(0,obstacles.Length);
				//在position创建障碍物
				Vector3 position = new Vector3(center.x - Mathf.Sin(obsAngleR) * R,center.y - Mathf.Cos(obsAngleR) * R,center.z);
				GameObject go = GameObject.Instantiate(obstacles[obsIndex],position,Quaternion.Euler(0,-180,obsAngle)) as GameObject;
				go.transform.parent = body;
			}
		}
	}

	Vector3 GetWayPosiByZ(float z)
	{
		Transform[] points = guidePoint.points;
		int index = 0;


		for(int i = 0;i < points.Length - 1;i++)
		{
			if(z <= points[i].position.z && z >= points[i+1].position.z)
			{
				index = i;
				break;
			}
		}
		//posi: index ~ index+1
		return Vector3.Lerp (points [index + 1].position, points [index].position, (z - points [index + 1].position.z) / (points [index].position.z - points [index + 1].position.z));
	}


	public Vector3 GetNextTargetPoint()
	{
		while(true)
		{
			if(Mathf.Abs(guide.position.z - guidePoint.points[targetWayPointIndex].position.z)< 10) 
			{
				targetWayPointIndex--;
				if(targetWayPointIndex < 0)
				{
					//Camera.main.SendMessage("GenerateRunWay");
					envGenerator.GenerateRunWay();
					Destroy(this.gameObject,2);
					return envGenerator.runway1.GetNextTargetPoint();
				}
			}
			else
			{
				return guidePoint.points[targetWayPointIndex].position;
			}
		}
	}

}











