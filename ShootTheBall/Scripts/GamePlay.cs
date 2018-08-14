using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GamePlay : MonoBehaviour, IPointerDownHandler
{
	public static GamePlay instance;
	public Text txtScore;
	public SpriteRenderer sp_background;

	public AudioClip SuccessHit;
	public AudioClip RingHit;

	public List<Color> BGColors = new List<Color>();

	[HideInInspector] public int score = 0;
	[HideInInspector] public int bestScore = 0;
	[HideInInspector] public bool isGamePlay; 

	// event for score updation.
	public static event Action<int> OnScoreUpdatedEvent;

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
		BGMusicController.instance.StartBGMusic ();
		bestScore = PlayerPrefs.GetInt ("BestScore", 0);
		isGamePlay = true;

		if (PlayerPrefs.GetInt ("isRescued", 0) == 1) {
			score = PlayerPrefs.GetInt ("LastScore", 0);
		} else {
			SetBackgroundColor ();
			score = 0;
		}

		txtScore.text = score.ToString ("00");
		Invoke ("ResetPrefs", 1F);
	}

	/// <summary>
	/// Resets the prefs.
	/// </summary>
	void ResetPrefs(){
		PlayerPrefs.DeleteKey ("LastScore");
		PlayerPrefs.DeleteKey ("isRescued");
	}

	/// <summary>
	/// Raises the game over event.
	/// </summary>
	public void OnGameOver ()
	{
		PlayerPrefs.SetInt ("LastScore", score);

		if (AudioManager.instance.isSoundEnabled) {
			GetComponent<AudioSource> ().PlayOneShot (RingHit);
		}

		Invoke ("ExecuteGameOver", 1F);
	}

	void ExecuteGameOver()
	{
		GameController.instance.OnGameOver (gameObject);
	}

	/// <summary>
	/// Raises the score updated event.
	/// </summary>
	/// <param name="count">Count.</param>
	public void OnScoreUpdated (int count)
	{
		score += count;
		txtScore.text = score.ToString ("00");
		OnScoreUpdatedEvent.Invoke (score);

		if (score % 5 == 0) {
			SetBackgroundColor();
		}

		if (score > bestScore) {
			bestScore = score;
			PlayerPrefs.SetInt ("BestScore", bestScore);
		}
		if (AudioManager.instance.isSoundEnabled) {
			GetComponent<AudioSource> ().PlayOneShot (SuccessHit);
		}
	}

	/// <summary>
	/// Sets the color of the background from the predefined list if colors randomly.
	/// </summary>
	void SetBackgroundColor()
	{
		sp_background.color = BGColors [UnityEngine.Random.Range (0, BGColors.Count)];;
	}


	#region IPointerDownHandler implementation
	/// <summary>
	/// Raises the pointer down event.
	/// Ball will be fired on pointer down in gameplay mode.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerDown (PointerEventData eventData)
	{
		if (isGamePlay) 
		{
			Cannon.instance.FireBall();	
		}
	}
	#endregion

	/// <summary>
	/// Raises the pause button pressed event.
	/// </summary>
	public void OnPauseButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.PauseGame();
		}
	}
}
