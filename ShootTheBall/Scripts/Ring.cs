using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This is the rotating ring of the game.
/// </summary>
public class Ring : MonoBehaviour 
{
	public static Ring instance;

	/// The rotate speed.
	public float rotateSpeed = 50.0F;

	/// The minimum speed.
	public float minSpeed = 50.0F;

	/// The max speed.
	public float maxSpeed = 150.0F;

	// how much speed should increase on level up.
	public float speedIncreaseOnLevelUp = 15.0F;

	// 1 level will be increase after given count;
	public int levelUpOnCount = 5;

		/// All the different typed of rings are assigned to this list from the inspector.
	public List<GameObject> Rings = new List<GameObject>();
	GameObject currentRing = null;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
	    rotateSpeed = 80f;

	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		GamePlay.OnScoreUpdatedEvent += OnScoreUpdated;

		if (PlayerPrefs.GetInt ("isRescued", 0) == 1) {
			rotateSpeed =  ((rotateSpeed > minSpeed) ? rotateSpeed : minSpeed);
			if(currentRing == null)
			{
				currentRing = Rings [Random.Range (0, Rings.Count)];
			}
		} else {
			currentRing = Rings [Random.Range (0, Rings.Count)];
			rotateSpeed = minSpeed;
		}
		currentRing.SetActive (true);
		EGTween.Init (gameObject);
		StartRotation ();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		GamePlay.OnScoreUpdatedEvent -= OnScoreUpdated;
		EGTween.Stop (gameObject);
		currentRing.SetActive (false);
	}

	/// <summary>
	/// Raises the score updated event.
	/// </summary>
	/// <param name="score">Score.</param>
	void OnScoreUpdated (int score)
	{
		if (score % levelUpOnCount == 0) {

			rotateSpeed += speedIncreaseOnLevelUp;
			rotateSpeed = Mathf.Clamp(rotateSpeed, minSpeed, maxSpeed);

			UpdateRandomRing();
		}
	}

	/// <summary>
	/// Updates the random ring.
	/// </summary>
	void UpdateRandomRing()
	{
		currentRing.SetActive (false);
		currentRing = Rings [Random.Range (0, Rings.Count)];
		currentRing.SetActive (true);
		StartRotation ();
	}

	/// <summary>
	/// Starts the rotation.
	/// </summary>
	public void StartRotation()
	{
		EGTween.RotateBy (gameObject, EGTween.Hash ("z", 1.0F, "speed",(rotateSpeed), "easeType", EGTween.EaseType.linear, "loopType", EGTween.LoopType.loop));
	}
}
