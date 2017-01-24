using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shakes the UI element in local space when activated
/// </summary>
public class UI_Shake : MonoBehaviour 
{
	private Image myImage;
	private Text myText;

	public bool activate = false;
	public float shakeSpeed = 5;
	public float shakeStrength = 5;

	private RectTransform myRect;
	private Vector3 rememberRot;

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

		//Remember Rotation
		RememberRot();
	}

	void RememberRot()
	{
		rememberRot = myRect.localEulerAngles;
	}

	float PingPong (float a_Min, float a_Max)
	{
		float bounce = 0;

		bounce = Mathf.PingPong( Time.time * shakeSpeed, a_Max - a_Min) + a_Min;

		return bounce;
	}

	void Update()
	{
		if (activate)
		{
			myRect.localEulerAngles = new Vector3(myRect.localEulerAngles.x, myRect.localEulerAngles.y, myRect.localEulerAngles.z + PingPong(-shakeStrength, shakeStrength));
		}
		else
		{
			myRect.localEulerAngles = rememberRot;
		}
	}

	//Public Functions
	public void EffectOn()
	{
		activate = true;
	}

	public void EffectOff()
	{
		activate = false;
	}

	public void EffectTime(float time)
	{
		EffectOn();

		Invoke("EffectOff", time);
	}

	void OnDisable()
	{
		myRect.localEulerAngles = rememberRot;
	}
}
