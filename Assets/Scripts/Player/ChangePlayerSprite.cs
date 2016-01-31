using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ChangePlayerSprite : MonoBehaviour {

	public Sprite assaultSprite;
	public Sprite tankSprite;
	public Sprite sniperSprite;

	public Image img;
	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
	}

	public void ChangeSprite(ClassType classType) {
		if (classType == ClassType.Assault)
			img.sprite = assaultSprite;
		else if (classType == ClassType.Tank)
			img.sprite = tankSprite;
		else
			img.sprite = sniperSprite;
	}
}
