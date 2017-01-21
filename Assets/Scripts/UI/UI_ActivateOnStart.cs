using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activate a Particular UI Element on Start
/// </summary>
public class UI_ActivateOnStart : MonoBehaviour 
{
	private UI_Translate trans;
	private UI_Visible vis;
	private UI_Shake shake;
	private UI_Scale scale;

	public float delayTime = 0;

	void Awake()
	{
		trans = gameObject.GetComponent<UI_Translate>();
		vis = gameObject.GetComponent<UI_Visible>();
		shake = gameObject.GetComponent<UI_Shake>();
		scale = gameObject.GetComponent<UI_Scale>();
	}

	void Start()
	{
		Invoke("StartEffect", delayTime);
	}

	void StartEffect()
	{
		if (trans != null)
		{
			trans.activate = true;
		}

		if (vis != null)
		{
			vis.activate = true;
		}

		if (shake != null)
		{
			shake.activate = true;
		}

		if (scale != null)
		{
			scale.activate = true;
		}
	}
}
