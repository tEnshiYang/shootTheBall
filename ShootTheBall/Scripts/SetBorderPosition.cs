using UnityEngine;
using System.Collections;

/// <summary>
/// Set border position of the wall colliders also sets its hight based on the device size.
/// </summary>
public class SetBorderPosition : MonoBehaviour {

	float CameraOtrhoSize = 5F;
	public GameObject borderLeft;
	public GameObject borderRight;
	public GameObject borderTop;
	public GameObject borderBottom;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		CameraOtrhoSize = Camera.main.orthographicSize;
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		UpdateBorderPosition ();
	}

	/// <summary>
	/// Updates the border position.
	/// </summary>
	void UpdateBorderPosition()
	{
		float screenHeight = CameraOtrhoSize * 2F;
		float screenWidth = ((((float) Screen.width) / ((float) Screen.height)) * screenHeight);

		borderLeft.transform.position = new Vector3 (-(screenWidth / 2F), 0, 0);
		borderRight.transform.position = new Vector3 ((screenWidth / 2F), 0, 0);
		borderTop.GetComponent<BoxCollider2D> ().size = new Vector2 (screenWidth, 0.1F);
		borderBottom.GetComponent<BoxCollider2D> ().size = new Vector2 (screenWidth, 0.1F);
	}
}
