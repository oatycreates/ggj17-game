using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger these function to open specific Websites via URL
/// </summary>

public class UI_OpenWebpage : MonoBehaviour 
{
	public void GabbyWebpage()
	{
		Application.OpenURL("http://3dartist.phoenixinteractive.com.au/");
	}

	public void PatrickWebpage()
	{
		Application.OpenURL("https://github.com/patferguson");
	}

	public void RowanWebpage()
	{
		Application.OpenURL("http://www.rowandonaldson.com/");
	}

	public void AbbyWebpage()
	{
		//Leave empty for now
	}


	public void GabbyTwitter()
	{
		//Leave empty for now
	}

	public void PatrickTwitter()
	{
		Application.OpenURL("https://twitter.com/DevPatF");
	}

	public void RowanTwitter()
	{
		Application.OpenURL("https://twitter.com/DevRowan");
	}

	public void AbbyTwitter()
	{
		Application.OpenURL("https://twitter.com/abbysynth");
	}
	
}
