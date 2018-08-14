using UnityEngine;
using System.Collections;

/// <summary>
/// Despawn particle.
/// </summary>
public class DespawnParticle : MonoBehaviour 
{
	/// <summary>
	/// The despawn delay.
	/// </summary>
	public float DespawnDelay = 1.5F;

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		Invoke ("Despawn", DespawnDelay);
	}

	/// <summary>
	/// Despawn this instance.
	/// </summary>
	void Despawn()
	{
		ParticlePool.instance.CoolParticleRing (gameObject);
	}
}
