using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ParticleP ool.
/// This script is used for recycling particles which is already used and  can reuse for the upnext coming particle.
/// </summary>
public class ParticlePool : MonoBehaviour 
{
	public static ParticlePool instance;
	public GameObject ParticleRing;
	
	List<GameObject> ParticleRings = new List<GameObject> ();

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
	/// Start this instance.
	/// </summary>
	void Start()
	{
		FillParticlePool ();
	}

	/// <summary>
	/// Fills the particle pool.
	/// </summary>
	void FillParticlePool()
	{
		foreach (Transform t in transform) {
			ParticleRings.Add(t.gameObject);
		}
	}

	/// <summary>
	/// Gets the new ring.
	/// </summary>
	/// <returns>The new ring.</returns>
	public GameObject GetNewRing()
	{
		GameObject particleInstance = null;
		if (ParticleRings.Count > 0) {
			particleInstance = ParticleRings [ParticleRings.Count - 1];
			ParticleRings.Remove (particleInstance);
		} 
		else 
		{
			particleInstance = (GameObject) Instantiate (ParticleRing) as GameObject;
		}
		return particleInstance;
	}

	/// <summary>
	/// Cools the particle ring.
	/// </summary>
	/// <param name="particleRing">Particle ring.</param>
	public void  CoolParticleRing(GameObject particleRing)
	{
		particleRing.SetActive (false);
		particleRing.transform.localPosition = Vector3.zero;
		particleRing.transform.localEulerAngles = Vector3.zero;
		if (!ParticleRings.Contains (particleRing)) {
			ParticleRings.Add(particleRing);
		}
	}
}
