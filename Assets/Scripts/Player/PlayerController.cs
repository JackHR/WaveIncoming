using UnityEngine;
using System.Collections;

public enum PlayerNumber { P1, P2 };

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

	public PlayerNumber playerNumber;
	public float speed = 1;
	public ForceMode2D fMode;
	public PlayerStats playerStats;

	private Animator anim;
	private Vector2 moveDirection = new Vector2();
	private string horizontalAxis, verticalAxis;

	void Start () {

		if(playerNumber == PlayerNumber.P1) {
			horizontalAxis = Strings.Horizontal_P1;
			verticalAxis = Strings.Vertical_P1;
		}
		else {
			horizontalAxis = Strings.Horizontal_P2;
			verticalAxis = Strings.Vertical_P2;
		}

		speed = playerStats.Speed;
	
		anim = GetComponentInChildren<Animator>();
		if (anim == null)
			Debug.LogError ("No player animator?!");
	}

	void Update () {
		
		if (Customization.MenuActive) {
			speed = playerStats.Speed;
			return;
		}
	
		moveDirection = new Vector2 (Input.GetAxisRaw(horizontalAxis), Input.GetAxisRaw(verticalAxis));
		moveDirection *= speed;
		//Debug.Log (moveDirection);


		anim.SetFloat (Strings.Speed, Mathf.Abs (moveDirection.x + moveDirection.y));
		
		playerStats.Position = transform.position;
	}
	
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().AddForce (moveDirection, fMode);
	}
}
