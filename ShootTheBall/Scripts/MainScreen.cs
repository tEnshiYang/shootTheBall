using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour {

	public Text txtBest;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		txtBest.text = "最高分 : " + PlayerPrefs.GetInt ("BestScore", 0).ToString("00");
	}

	/// <summary>
	/// Raises the play button pressed event.
	/// </summary>
	public void OnPlayButtonPressed()
	{
		if (InputManager.instance.canInput()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.StartGamePlay(gameObject);
		}
	}

	public void OnReviewButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			Application.OpenURL(Constants.REVIEW_URL);
		}
	}

}
