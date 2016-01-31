using UnityEngine;
using System.Collections;

public class AimController : MonoBehaviour {

	public PlayerNumber playerNumber;
	public float stickTurnSensitivity = 8.5f;
	private Vector3 turnVector = Vector3.zero;
	private Vector2 stickInput;
	private string aimX, aimY;
	Camera cam;

	// Use this for initialization
	void Start () {

		if(playerNumber == PlayerNumber.P1) {
			aimX = Strings.AimX_P1;
			aimY = Strings.AimY_P1;
		}
		else {
			aimX = Strings.AimX_P2;
			aimY = Strings.AimY_P2;
		}

		cam = GameObject.FindGameObjectWithTag (Strings.MainCamera).GetComponent<Camera>();
		if (cam == null)
			Debug.LogError ("No camera found.");
	}

	// Update is called once per frame
	void Update () {
		if (PauseMenu.MenuActive)
			return;

		if(!JoystickDetector.Instance.ForceCursorOn && JoystickDetector.Instance.JoystickPresent) {

			stickInput = new Vector2(Input.GetAxisRaw(aimX), Input.GetAxisRaw(aimY)).normalized;
			if(stickInput.magnitude > JoystickDetector.StickDeadZone)
			{
				turnVector.z = Mathf.Atan2(stickInput.x, stickInput.y) * Mathf.Rad2Deg;
				
				Quaternion newRotation = new Quaternion();
				newRotation.eulerAngles = turnVector;
				transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * stickTurnSensitivity);
			}
		}
		else if(playerNumber == PlayerNumber.P1) {
			Vector3 pos = cam.WorldToScreenPoint(transform.position);
			Vector3 dir = Input.mousePosition - pos;
			float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
		}
	}
}
