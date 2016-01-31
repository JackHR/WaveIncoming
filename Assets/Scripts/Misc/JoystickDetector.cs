using UnityEngine;
using System.Collections;

public class JoystickDetector : MonoBehaviour {

	public static JoystickDetector Instance;
	//Added precaution to avoid twitching on release of the right stick
	public static float StickDeadZone = 0.8f;

	[Tooltip("Whether moving the mouse should activate mouse movement and vice versa")]
	public bool allowInputOverride  = true;
	public bool JoystickPresent { get { return joystickPresent; } }
	public bool ForceCursorOn {
		get { return cursorForcedOn; }
		set {
#if !UNITY_EDITOR
			cursorForcedOn = value;
			
			if(value && !Cursor.visible) {
				SetCursorOn(true);
			}
			else if(!value && joystickPresent) {
				SetCursorOn(false);
			}
#endif
		}
	}
	public bool DetectionEnabled {
		get { return detectionEnabled; }
		set {
			detectionEnabled = value;
			if(value && !detectionEnabled) {
				StartCoroutine(Strings.DetectJoystick);
			}
			else if(!value && detectionEnabled) {
				StopCoroutine(Strings.DetectJoystick);
			}
		}
	}

	public delegate void JoystickDetectionHandler(bool joystickDetected);
	public event JoystickDetectionHandler OnJoystickDetection;

	private bool joystickPresent = false;
	private bool detectionEnabled = true;

#if UNITY_EDITOR
	private bool cursorForcedOn = true;
#else
	private bool cursorForcedOn = false;
#endif

	
	private void SetCursorOn(bool on) {	
#if !UNITY_EDITOR
		Cursor.lockState = on ? CursorLockMode.None : CursorLockMode.Locked;
		Cursor.visible = on;
#endif
	}

	// Use this for initialization
	void Awake () {
		if(Instance != null && Instance != this) {
			Destroy(gameObject);
		}
		else {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}

		if(detectionEnabled)
			StartCoroutine(Strings.DetectJoystick);
	}

	void Update() {

		if(allowInputOverride) {

			if(cursorForcedOn && Input.GetButtonDown(Strings.Joystick_On_P1)) {
				ForceCursorOn = false;
			}
			else {
				Vector2 mouseMove = new Vector2(Input.GetAxis(Strings.MouseX_P1), Input.GetAxis(Strings.MouseY_P1));
				if(mouseMove.sqrMagnitude > 1f)
					ForceCursorOn = true;
			}
		}
	}

	IEnumerator DetectJoystick() {

		while(detectionEnabled) {

			string[] joysticks = Input.GetJoystickNames();

			if(joysticks != null && joysticks.Length > 0) {
				// only trigger event for a change, not a repeat
				if(!joystickPresent) {
					joystickPresent = true;
					if(OnJoystickDetection != null) {
						OnJoystickDetection(true);
					}
					if(!cursorForcedOn)
						SetCursorOn(false);
				}
			}
			// only trigger event for a change, not a repeat
			else if(joystickPresent) {
				joystickPresent = false;
				if(OnJoystickDetection != null) {
					OnJoystickDetection(false);
				}
				SetCursorOn(true);
			}

			yield return new WaitForSeconds(1f);
		}
	}

}
