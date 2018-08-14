using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WindowTransition : MonoBehaviour 
{
	/// Should window transit or not when loading.
	public bool doAnimateOnLoad = true;

	/// Should window transit or not when destroys.
	public bool doAnimateOnDestroy = true;

	/// Should windows fade on loading
	public bool doFadeInBackLayOnLoad = true;

	/// Should window fade out when destroys or despawn.
	public bool doFadeOutBacklayOnDestroy = true;

	/// Should window destroy or despawn when removing from screen.
	public bool DestroyOnFinish = false;

	public Image BackLay;
	public GameObject WindowContent;

	public float TransitionDuration = 0.35F;

	Vector3 initialPosition = Vector3.zero;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake()
	{
		if (WindowContent) {

		}
	}
	/// <summary>
	/// Raises the window added event.
	/// </summary>
	public void OnWindowAdded()
	{
		if(doAnimateOnLoad && (WindowContent != null))
		{
			initialPosition = WindowContent.transform.localPosition;
			WindowContent.MoveFrom(EGTween.Hash("x",-600,"easeType",EGTween.EaseType.easeOutBack,"time",TransitionDuration,"islocal",true,"ignoretimescale",true));
		}

		if(doFadeInBackLayOnLoad)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From",0F,"To",TransitionDuration,"Time",0.5F,"onupdate","OnOpacityUpdate","ignoretimescale",true));
		}
	}

	/// <summary>
	/// Raises the window remove event.
	/// </summary>
	public void OnWindowRemove()
	{
		if((doAnimateOnDestroy && (WindowContent != null)))
		{
			WindowContent.MoveTo(EGTween.Hash("x",600F,"easeType", EGTween.EaseType.easeInBack, "time",TransitionDuration, "islocal",true,"ignoretimescale",true ));

			if(doFadeOutBacklayOnDestroy)
			{
				BackLay.gameObject.ValueTo(EGTween.Hash("From",TransitionDuration,"To",0F,"Time",TransitionDuration,"onupdate","OnOpacityUpdate","ignoretimescale",true));
			}

			Invoke("OnRemoveTransitionComplete",0.5F);
		}
		else
		{
			if(doFadeOutBacklayOnDestroy)
			{
				BackLay.gameObject.ValueTo(EGTween.Hash("From",TransitionDuration,"To",0F,"Time",TransitionDuration,"onupdate","OnOpacityUpdate"));
				Invoke("OnRemoveTransitionComplete",0.5F);
			}
			else
			{
				OnRemoveTransitionComplete();
			}
		}

	}
		
	/// <summary>
	/// Animates the window on load.
	/// </summary>
	public void AnimateWindowOnLoad()
	{
		if(doAnimateOnLoad && (WindowContent != null))
		{
			WindowContent.MoveFrom(EGTween.Hash("x",600,"easeType",EGTween.EaseType.easeOutBack,"time",TransitionDuration,"islocal",true));
		}

		FadeInBackLayOnLoad ();
	}

	/// <summary>
	/// Animates the window on destroy.
	/// </summary>
	public void AnimateWindowOnDestroy()
	{
		if(doAnimateOnDestroy && (WindowContent != null))
		{
			WindowContent.MoveTo(EGTween.Hash("x",-600F,"easeType",EGTween.EaseType.easeInBack,"time",TransitionDuration,"islocal",true));
		}

		FadeOutBacklayOnDestroy ();
	}

	/// <summary>
	/// Fades the in back lay on load.
	/// </summary>
	public void FadeInBackLayOnLoad()
	{
		if(doFadeInBackLayOnLoad)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From",0F,"To",0.5F,"Time",TransitionDuration,"onupdate","OnOpacityUpdate"));
		}
	}

	/// <summary>
	/// Fades the out backlay on destroy.
	/// </summary>
	public void FadeOutBacklayOnDestroy()
	{
		if(doFadeOutBacklayOnDestroy)
		{
			BackLay.gameObject.ValueTo(EGTween.Hash("From",0.5F,"To",0F,"Time",TransitionDuration,"onupdate","OnOpacityUpdate"));
		}
	}

	/// <summary>
	/// Raises the opacity update event.
	/// </summary>
	/// <param name="Opacity">Opacity.</param>
	void OnOpacityUpdate(float Opacity)
	{
		BackLay.color = new Color (BackLay.color.r, BackLay.color.g, BackLay.color.b, Opacity);
	}

	/// <summary>
	/// Raises the remove transition complete event.
	/// </summary>
	void OnRemoveTransitionComplete()
	{
		if (DestroyOnFinish) {
			Destroy (gameObject);
		} else {
			WindowContent.transform.localPosition = initialPosition;
			gameObject.SetActive(false);
		}
	}
}
