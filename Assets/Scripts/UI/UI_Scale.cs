using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scales the UI element in local space when activated
/// </summary>

public class UI_Scale : MonoBehaviour 
{
	private Image myImage;
	private Text myText;
	private RectTransform myRect;

	private Vector3 rememberScale;

	public bool activate = false;
	public float scaleSpeed = 5;
	public bool pingPong = false;

	public Vector3 maxScale;

	void Awake()
	{
		myImage = gameObject.GetComponent<Image>();

		if (myImage == null)
		{
			myText = gameObject.GetComponent<Text>();
		}

		if (myImage != null)
		{
			myRect = myImage.rectTransform;
		}
		else
		{
			myRect = myText.rectTransform;
		}

		RememberScale();
	}

	void Update()
	{
		if (activate)
		{
			if (!pingPong)
			{
				myRect.transform.localScale = Vector3.MoveTowards(myRect.transform.localScale, maxScale, Time.deltaTime *scaleSpeed);
			}
			else
			if (pingPong)
			{
				Vector3 pingPongScale = new Vector3 (PingPong(rememberScale.x, maxScale.x), PingPong(rememberScale.y, maxScale.y), rememberScale.z);
				myRect.transform.localScale = pingPongScale;
			}
		}
		else
		if (!activate)
		{
			myRect.transform.localScale = Vector3.MoveTowards( myRect.transform.localScale, rememberScale, Time.deltaTime * scaleSpeed);
		}
	}

	void RememberScale()
	{
		rememberScale = myRect.transform.localScale;
		print(rememberScale);
	}

	float PingPong ( float a_Min, float a_Max)
	{
		float bounce = 0;

		bounce = Mathf.PingPong(Time.time * scaleSpeed, a_Max - a_Min) + a_Min;

		return bounce;
	}

	//PUblic Functions
	public void EffectOn()
	{
		activate = true;
		pingPong = false;	
	}

	public void EffectOff()
	{
		activate = false;
		pingPong = false;
	}

	public void EffectOn_PingPong()
	{
		activate = true;
		pingPong = true;
	}

	public void EffectTime( float time, bool usePingPong)
	{
		if (!usePingPong)
		{
			EffectOn();
		}
		else
		{
			EffectOn_PingPong();
		}

		CancelInvoke();

		Invoke("EffectOff", time);
	}
}
