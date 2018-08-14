using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class GameController : MonoBehaviour 
{
	public static GameController instance;

	public Camera UICamera;
	public Canvas UICanvas;
	public EventSystem eventSystem;

	[HideInInspector] public bool isGamePaused = false;

	public List<GameObject> GameScreens = new List<GameObject>();
	[HideInInspector] public GameObject LastScreen = null;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		if (instance == null) {
			instance = this;
			return;
		}
		Destroy (gameObject);
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start()
	{
		Application.targetFrameRate = 60;
		LastScreen = SpawnUIScreen ("MainScreen");
	}
	
	/// <summary>
	/// Fades the in user interface screen.
	/// </summary>
	/// <param name="thisScreen">This screen.</param>
	public void FadeInUIScreen(GameObject thisScreen)
	{
		if (thisScreen.GetComponent<CanvasGroup> ()) 
		{
			StartCoroutine (FadeInCanvasGroup (thisScreen.GetComponent<CanvasGroup>()));
		}
	}

	/// <summary>
	/// Spawns the user interface screen.
	/// </summary>
	/// <returns>The user interface screen.</returns>
	/// <param name="name">Name.</param>
	public GameObject SpawnUIScreen(string name)
	{
		GameObject thisScreen = null;

		thisScreen = GameScreens.Where(obj => obj.name == name).SingleOrDefault();

		if (thisScreen == null) 
		{
			thisScreen = (GameObject)Instantiate (Resources.Load ("Prefabs/UIScreens/" + name.ToString ()));
			thisScreen.name = name;
			thisScreen.transform.SetParent (UICanvas.transform);
			thisScreen.transform.localPosition = Vector3.zero;
			thisScreen.transform.localScale = Vector3.one;
			thisScreen.GetComponent<RectTransform> ().sizeDelta = Vector3.zero;
		}
		thisScreen.Init ();
		thisScreen.OnWindowLoad ();
		thisScreen.SetActive (true);
		LastScreen = thisScreen;
		return thisScreen;
	}

	/// <summary>
	/// Gets the user interface screen.
	/// </summary>
	/// <returns>The user interface screen.</returns>
	/// <param name="name">Name.</param>
	GameObject GetUIScreen(string name)
	{
		GameObject thisScreen = null;
		thisScreen = GameScreens.Where(obj => obj.name == name).SingleOrDefault();
		return thisScreen;
	}

	/// <summary>
	/// Raises the back button pressed event.
	/// </summary>
	public void OnBackButtonPressed()
	{
		if (LastScreen.name == "MainScreen") {
			SpawnUIScreen ("QuitConfirm");
		} else if (LastScreen.name == "QuitConfirm") {
			LastScreen.OnWindowRemove ();
			LastScreen = GetUIScreen ("MainScreen");
		} else if (LastScreen.name == "GamePlay") {
			PauseGame ();
		} else if (LastScreen.name == "Pause") {
			LastScreen.OnWindowRemove ();
			LastScreen = GetUIScreen ("GamePlay");
		} else if (LastScreen.name == "GameOver") {
			ExitToMainScreenFromGameOver (LastScreen);
			LastScreen = GetUIScreen ("MainScreen");
		}
	}

	/// <summary>
	/// Fades the in canvas group.
	/// </summary>
	/// <returns>The in canvas group.</returns>
	/// <param name="canvasGroup">Canvas group.</param>
	IEnumerator FadeInCanvasGroup(CanvasGroup canvasGroup)
	{
		for(float opacity = 0; opacity <= 1F; opacity += 0.075F)
		{
			yield return new WaitForEndOfFrame();
			canvasGroup.alpha = opacity;
		}
		canvasGroup.alpha = 1F;
	}

	/// <summary>
	/// Fades the out user interface screen.
	/// </summary>
	/// <param name="thisScreen">This screen.</param>
	/// <param name="disableOnFadeOut">If set to <c>true</c> disable on fade out.</param>
	public void FadeOutUIScreen(GameObject thisScreen, bool disableOnFadeOut = false)
	{
		if (thisScreen.GetComponent<CanvasGroup> ()) 
		{
			StartCoroutine (FadeOutCanvasGroup (thisScreen.GetComponent<CanvasGroup> (),disableOnFadeOut));
		}
	}

	/// <summary>
	/// Fades the out canvas group.
	/// </summary>
	/// <returns>The out canvas group.</returns>
	/// <param name="canvasGroup">Canvas group.</param>
	/// <param name="disableOnFadeOut">If set to <c>true</c> disable on fade out.</param>
	IEnumerator FadeOutCanvasGroup(CanvasGroup canvasGroup, bool disableOnFadeOut = false)
	{
		for(float opacity = 1; opacity >= 0F; opacity -= 0.075F)
		{
			yield return new WaitForEndOfFrame();
			canvasGroup.alpha = opacity;
		}
		canvasGroup.alpha = 0F;
		
		if(disableOnFadeOut)
		{
			canvasGroup.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update()
	{
		/// Detected the back button pressed event.
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (InputManager.instance.canInput()) {
				OnBackButtonPressed ();
			}
		}
	}

	/// <summary>
	/// Starts the game play.
	/// </summary>
	/// <param name="currentScreen">Current screen.</param>
	public void StartGamePlay( GameObject currentScreen )
	{
		currentScreen.SetActive (false);
		SpawnUIScreen ("GamePlay");
	}

	/// <summary>
	/// Raises the game over event.
	/// </summary>
	/// <param name="currentScreen">Current screen.</param>
	public void OnGameOver( GameObject currentScreen )
	{
		currentScreen.SetActive (false);
		SpawnUIScreen ("GameOver");
	}

	/// <summary>
	/// Reloads the game.
	/// </summary>
	/// <param name="currentScreen">Current screen.</param>
	public void ReloadGame( GameObject currentScreen )
	{
		currentScreen.SetActive (false);
		SpawnUIScreen ("GamePlay");
	}

	/// <summary>
	/// Resumes the game.
	/// </summary>
	/// <param name="currentScreen">Current screen.</param>
	public void ResumeGame( GameObject currentScreen)
	{
		currentScreen.OnWindowRemove ();
	}

	public void ExitToMainScreenFromPause( GameObject currentScreen)
	{
		currentScreen.OnWindowRemove ();
		GetUIScreen ("GamePlay").SetActive (false);
		SpawnUIScreen ("MainScreen");
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	public void PauseGame()
	{
		SpawnUIScreen ("Pause");
	}

	/// <summary>
	/// Exits to main screen from game over.
	/// </summary>
	/// <param name="currentScreen">Current screen.</param>
	public void ExitToMainScreenFromGameOver( GameObject currentScreen )
	{
		currentScreen.SetActive (false);
		SpawnUIScreen ("MainScreen");
	}
}