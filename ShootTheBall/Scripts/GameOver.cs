using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class GameOver : MonoBehaviour 
{
	// The score text
	public Text txtScore;
	// The Best score text 
	public Text txtBestScore;

	public AudioClip GameOverAudio;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		BGMusicController.instance.StopBGMusic ();
		if (AudioManager.instance.isSoundEnabled) {
			GetComponent<AudioSource>().PlayOneShot(GameOverAudio);
		}

		// Sets score and best score.
		txtBestScore.text = PlayerPrefs.GetInt ("BestScore", 0).ToString();
		txtScore.text = PlayerPrefs.GetInt ("LastScore", 0).ToString();

		#if UNITY_ANDROID || UNITY_IOS
		//If ad is not available, rescue button will be deactivated.
		if (!UnityAds.instance.IsReady()) {
			transform.FindChild("Bottom-Panel/btn-rescue").gameObject.SetActive(false);
		}
		#endif
	}

	/// <summary>
	/// Raises the replay button pressed event.
	/// </summary>
	public void OnReplayButtonPressed ()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();
			GameController.instance.ReloadGame(gameObject);
		}
	}

	/// <summary>
	/// Raises the home button pressed event.
	/// </summary>
	public void OnHomeButtonPressed ()
	{
	    Application.LoadLevel("MainScene");
	    //if (InputManager.instance.canInput ()) {
	    //	InputManager.instance.DisableTouchForDelay ();
	    //	InputManager.instance.AddButtonTouchEffect ();
	    //	GameController.instance.ExitToMainScreenFromGameOver(gameObject);
	    //}
	}

	/// <summary>
	/// Raises the rescue button pressed event.
	/// </summary>
	public void OnRescueButtonPressed()
	{
		if (InputManager.instance.canInput ()) {
			InputManager.instance.DisableTouchForDelay ();
			InputManager.instance.AddButtonTouchEffect ();

			BGMusicController.instance.StopBGMusic ();	

			#if UNITY_ANDROID || UNITY_IOS
			UnityAds.instance.ShowAdsWithResult ("", result => 
			{
				if(result == true)
				{
					//Rescue Successful
					PlayerPrefs.SetInt("isRescued",1);
					GameController.instance.ReloadGame(gameObject);

				}
				else 
				{
					//Rescue Failed
				}
			});
			#endif
		}
	}
}
