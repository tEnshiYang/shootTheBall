using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	/// <summary>
	/// This method will detect whenter ball is hitted with outside border or ring.
	/// Hitting with ring will result in gameover while hitting with outside border will give point.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other) 
	{
		if (!other.name.Contains ("border")) {
			//Game Over. Shaking GamePlay
			GamePlay.instance.gameObject.GetComponent<ShakeObject> ().StartShake ();
			GamePlay.instance.OnGameOver();
		}
		else 
		{
			//Success. Will add 1 point to score.
			GamePlay.instance.OnScoreUpdated(1);
		}

		// Spawns particle at hitting position.
		GameObject ParticleRing = ParticlePool.instance.GetNewRing ();
		ParticleRing.SetActive (true);
		ParticleRing.transform.localPosition = transform.localPosition;

		gameObject.SetActive (false);
		FirePool.instance.CoolPreviousBall (gameObject);
	}
}
