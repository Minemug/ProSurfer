using System;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Fragsurf.Movement;
using UnityEngine.SceneManagement;
public class PlayerAiming : MonoBehaviour
{
	[Header("References")]
	public Transform bodyTransform;

	[Header("Sensitivity")]
	public float sensitivityMultiplier = 1f;
	public float horizontalSensitivity = 1f;
	public float verticalSensitivity = 1f;

	[Header("Restrictions")]
	public float minYRotation = -90f;
	public float maxYRotation = 90f;

	//The real rotation of the camera without recoil
	private Vector3 realRotation;

	[Header("Aimpunch")]
	[Tooltip("bigger number makes the response more damped, smaller is less damped, currently the system will overshoot, with larger damping values it won't")]
	public float punchDamping = 9.0f;

	[Tooltip("bigger number increases the speed at which the view corrects")]
	public float punchSpringConstant = 65.0f;

	[HideInInspector]
	public Vector2 punchAngle;

	[HideInInspector]
	public Vector2 punchAngleVel;
	public GameObject player;
	public List<PointInTime> pointsInTime;
	public bool isPlaying = false;
	private void Start()
	{
		// Lock the mouse
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		if(MainManager.Instance == null)
        {
			Debug.Log("bez managera");
        }
        else
        {
			sensitivityMultiplier = MainManager.Instance.sensivity;
			Debug.Log("sens ustawiony na " + MainManager.Instance.sensivity);
        }
		pointsInTime = new List<PointInTime>();
		
	}

	private void Awake()
	{
		
	}

	private void Update()
	{
		// Fix pausing
		if (Mathf.Abs(Time.timeScale) <= 0)
			return;

		DecayPunchAngle();

		CalculateRotation();

		if (Input.GetKeyDown(KeyCode.H) && SceneManager.GetActiveScene().buildIndex == 1)
		{
			//SaveSystem.SaveReplay(this);
			PlayerReplay replay = SaveSystem.LoadReplay();
			if (replay != null)
			{
				pointsInTime = replay.pointsInTime;
			}
			StartPlaying();
		}

		if (Input.GetKeyUp(KeyCode.H) && SceneManager.GetActiveScene().buildIndex == 1)
			StopPlaying();
	}
	private void CalculateRotation()
	{
		if (!isPlaying)
		{
			// Input
			float xMovement = Input.GetAxisRaw("Mouse X") * horizontalSensitivity * sensitivityMultiplier;
			float yMovement = -Input.GetAxisRaw("Mouse Y") * verticalSensitivity * sensitivityMultiplier;

			// Calculate real rotation from input
			realRotation = new Vector3(Mathf.Clamp(realRotation.x + yMovement, minYRotation, maxYRotation),
				realRotation.y + xMovement, realRotation.z);
			realRotation.z = Mathf.Lerp(realRotation.z, 0f, Time.deltaTime * 3f);
			//Apply real rotation to body
			bodyTransform.eulerAngles = Vector3.Scale(realRotation, new Vector3(0f, 1f, 0f));

			//Apply rotation and recoil
			Vector3 cameraEulerPunchApplied = realRotation;
			cameraEulerPunchApplied.x += punchAngle.x;
			cameraEulerPunchApplied.y += punchAngle.y;

			transform.eulerAngles = cameraEulerPunchApplied;
		}
	}

	public void ViewPunch(Vector2 punchAmount)
	{
		//Remove previous recoil
		punchAngle = Vector2.zero;

		//Recoil go up
		punchAngleVel -= punchAmount * 20;
	}

	private void DecayPunchAngle()
	{
		if (punchAngle.sqrMagnitude > 0.001 || punchAngleVel.sqrMagnitude > 0.001)
		{
			punchAngle += punchAngleVel * Time.deltaTime;
			float damping = 1 - (punchDamping * Time.deltaTime);

			if (damping < 0)
				damping = 0;

			punchAngleVel *= damping;

			float springForceMagnitude = punchSpringConstant * Time.deltaTime;
			punchAngleVel -= punchAngle * springForceMagnitude;
		}
		else
		{
			punchAngle = Vector2.zero;
			punchAngleVel = Vector2.zero;
		}
	}
	private void StopPlaying()
	{
		isPlaying = true;
	}

	private void StartPlaying()
	{
		isPlaying = false;

	}

	private void FixedUpdate()
	{

		if (isPlaying)
		{
			Play();
		}
		else
			Record();
        
        
	}

	private void Play()
	{
		
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
			player.transform.position = pointInTime.position;
			bodyTransform.transform.eulerAngles = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		}
		else
		{
			isPlaying = false;
		}
	}

	void Record()
	{
		pointsInTime.Add(new PointInTime(player.transform.position, bodyTransform.transform.eulerAngles));
	}
}


