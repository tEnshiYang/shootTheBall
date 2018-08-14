using UnityEngine;
using System.Collections;

public class Paused : MonoBehaviour {

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		BGMusicController.instance.StopBGMusic ();
		GameController.instance.isGamePaused = true;
		EGTween.Pause(GamePlay.instance.gameObject,true);
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		BGMusicController.instance.StartBGMusic ();
		EGTween.Resume(GamePlay.instance.gameObject,true);
		GameController.instance.isGamePaused = false;
	}

	/// <summary>
	/// Raises the resume button pressed event.
	/// </summary>
	public void OnResumeButtonPressed()
	{
		if (InputManager.instance.canInput()) 
		{
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.ResumeGame(gameObject);
		}
	}

	/// <summary>
	/// Raises the exit button pressed event.
	/// </summary>
	public void OnExitButtonPressed()
	{
		if (InputManager.instance.canInput()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.ExitToMainScreenFromPause(gameObject);
		}
	}
}
