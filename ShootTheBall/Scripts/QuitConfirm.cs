using UnityEngine;
using System.Collections;

public class QuitConfirm : MonoBehaviour {

	/// <summary>
	/// Raises the cancel button pressed event.
	/// </summary>
	public void OnCancelButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
		}
	}

	/// <summary>
	/// Raises the quit button pressed event.
	/// </summary>
	public void OnQuitButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			Application.Quit();
		}
	}
}
