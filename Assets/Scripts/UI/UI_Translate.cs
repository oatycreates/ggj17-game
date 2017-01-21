using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Moves an UI Image when Triggered
/// </summary>

public class UI_Translate : MonoBehaviour 
{
	private Image myImage;
	private Text myText;
	public bool activate = false;
	public float moveSpeed = 5;

	public Vector3 hiddenPosition;
	public Vector3 shownPosition;
	private RectTransform myRect;

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
	}

	void Start()
	{
		if (activate)
		{
			myRect.localPosition = shownPosition;
		}
		else
		if (!activate)
		{
			myRect.localPosition = hiddenPosition;	
		}
	}


	void Update()
	{
		if (activate)
		{
			myRect.localPosition = Vector3.MoveTowards(myRect.localPosition, shownPosition, Time.deltaTime * moveSpeed);
		}
		else
		if (!activate)
		{
			myRect.localPosition = Vector3.MoveTowards(myRect.localPosition, hiddenPosition, Time.deltaTime * moveSpeed);
		}
	}

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
		CancelInvoke();
		Invoke("EffectOff", time);
	}
}
