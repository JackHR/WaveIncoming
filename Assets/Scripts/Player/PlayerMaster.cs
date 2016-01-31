using UnityEngine;
using System.Collections;

public class PlayerMaster : MonoBehaviour {

	public GameObject AssaultPlayerP1;
	public GameObject TankPlayerP1;
	public GameObject SniperPlayerP1;

	public GameObject AssaultPlayerP2;
	public GameObject TankPlayerP2;
	public GameObject SniperPlayerP2;

	public Transform spawnPoint;

	void Awake () {

		switch(MainMenu.P1_Class)
		{
		case ClassType.Tank:
			Instantiate (TankPlayerP1, spawnPoint.position - Vector3.right, spawnPoint.rotation);
			break;
		case ClassType.Sniper:
			Instantiate (SniperPlayerP1, spawnPoint.position - Vector3.right, spawnPoint.rotation);
			break;
		default:
			Instantiate (AssaultPlayerP1, spawnPoint.position - Vector3.right, spawnPoint.rotation);
			break;
		}

		if(MainMenu.SelectedGameMode == GameMode.Multiplayer) {

			switch(MainMenu.P2_Class)
			{
			case ClassType.Tank:
				Instantiate (TankPlayerP2, spawnPoint.position + Vector3.right, spawnPoint.rotation);
				break;
			case ClassType.Sniper:
				Instantiate (SniperPlayerP2, spawnPoint.position + Vector3.right, spawnPoint.rotation);
				break;
			default:
				Instantiate (AssaultPlayerP2, spawnPoint.position + Vector3.right, spawnPoint.rotation);
				break;
			}
		}
	}
}
