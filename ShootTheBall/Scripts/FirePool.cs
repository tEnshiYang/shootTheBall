using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Fire pool.
/// This script is used for recycling balls which is already fired and reuse for the upnext coming fires.
/// Rather destroying ball and loading new instance runtime, this method is quite efficeint for memory.
/// </summary>
public class FirePool : MonoBehaviour 
{
	public static FirePool instance;
	public GameObject ball;

	/// <summary>
	/// The List fire balls.
	/// </summary>
	List<GameObject> FireBalls = new List<GameObject> ();

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
		FillFirePool ();
	}

	/// <summary>
	/// Fills the fire pool.
	/// </summary>
	void FillFirePool()
	{
		foreach (Transform t in transform) {
			if(!FireBalls.Contains(t.gameObject))
			{
				t.gameObject.SetActive(false);
				t.localPosition = Vector3.zero;
				FireBalls.Add(t.gameObject);
			}
		}
	}

	/// <summary>
	/// Gets the next ball.
	/// If current pool is empty, new ball will spawn (Instantiated).
	/// </summary>
	/// <returns>The next ball.</returns>
	public GameObject GetNextBall()
	{
		GameObject ballInstance = null;
		if (FireBalls.Count > 0) {
			ballInstance = FireBalls [FireBalls.Count - 1];
			FireBalls.Remove (ballInstance);
		} 
		else 
		{
			ballInstance = (GameObject) Instantiate (ball) as GameObject;
		}
		return ballInstance;
	}

	/// <summary>
	/// Cools the previous ball.
	/// </summary>
	/// <param name="firedBall">Fired ball.</param>
	public void  CoolPreviousBall(GameObject firedBall)
	{
		firedBall.SetActive (false);
		firedBall.transform.localPosition = Vector3.zero;
		firedBall.transform.localEulerAngles = Vector3.zero;
		if (!FireBalls.Contains (firedBall)) {
			FireBalls.Add(firedBall);
		}
	}
}
