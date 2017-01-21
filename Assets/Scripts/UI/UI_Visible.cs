using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Changes Visibility when Triggered
/// </summary>
public class UI_Visible : MonoBehaviour 
{
	private Image myImage;
	private Text myText;

	public bool activate = false;
	public float alphaFloat = 0;
	public float fadeSpeed = 2;

	void Awake()
	{
		myImage = gameObject.GetComponent<Image>();

		if (myImage == null)
		{
			myText = gameObject.GetComponent<Text>();
		}

		//Do I start invisible or not
		if (activate)
		{
			alphaFloat = 1;
		}
		else
		{
			alphaFloat = 0;
		}
	}

	void Update()
	{
		if (activate)
		{
			alphaFloat = Mathf.MoveTowards(alphaFloat, 1, fadeSpeed * Time.deltaTime);
		}
		else
		{
			alphaFloat = Mathf.MoveTowards(alphaFloat, 0, fadeSpeed * Time.deltaTime);
		}

		alphaFloat = Mathf.Clamp(alphaFloat, 0, 1);

		if (myImage != null)
		{
			myImage.color = new Color( myImage.color.r, myImage.color.g, myImage.color.b, alphaFloat);
		}
		else
		{
			myText.color = new Color( myText.color.r, myText.color.g, myText.color.b, alphaFloat);
		}
	}

	//PUblic triggers
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
		//Remember to cancel Invoke before using another one.
		CancelInvoke();
		Invoke("EffectOff", time);
	}
}
