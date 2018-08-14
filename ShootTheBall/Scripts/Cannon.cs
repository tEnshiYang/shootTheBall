using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour 
{
	/// <summary>
	/// The instance.
	/// </summary>
	public static Cannon instance;

	/// The cannon point.
	public GameObject CannonPoint;

	GameObject FiringBall;

	//Travelling speed of the fired ball.
	public float travelSpeed = 15F;

	//Rotation speed of the cannon, will be constant for ever.
	public float rotateSpeed = 150.0F;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		EGTween.Init (gameObject);
		StartRotation ();
	}

	/// <summary>
	/// Updates the speed.
	/// </summary>
	void UpdateSpeed()
	{
		EGTween.Stop (gameObject);
		StartRotation ();
	}

	/// <summary>
	/// Starts the rotation.
	/// </summary>
	public void StartRotation()
	{
		EGTween.RotateBy (gameObject, EGTween.Hash ("z", -1.0F, "speed",(rotateSpeed), "easeType", EGTween.EaseType.linear, "loopType", EGTween.LoopType.loop));
	}

	/// <summary>
	/// Ends the rotation.
	/// </summary>
	public void EndRotation()
	{
		EGTween.Stop (gameObject);
	}

	/// <summary>
	/// Fires the ball.
	/// </summary>
	public void FireBall()
	{
		FiringBall = FirePool.instance.GetNextBall ();
		FiringBall.SetActive (true);
		Vector2 direction = (CannonPoint.transform.position - transform.position).normalized;
		FiringBall.GetComponent<Rigidbody2D>().AddForce ((direction * travelSpeed), ForceMode2D.Impulse);
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		EGTween.Stop (gameObject);
	}
}
