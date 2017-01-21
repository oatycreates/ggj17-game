using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDrag : MonoBehaviour {
#region Variables
	public float minSwipeDist = 60.0f;
	private bool m_isSwiping = false;
	private Vector3 m_swipeStartPos = Vector3.zero;
	private float m_swipeStartTime = -1.0f;
#endregion

#region Touch events
	/// <summary>
    /// Called when a swipe action has been detected.
    /// </summary>
    /// <param name="a_swipeStartPos">Where the swipe started.</param>
    /// <param name="a_swipeEndPos">Where the swipe has ended.</param>
    /// <param name="a_swipeDist">How far the swipe travelled.</param>
    /// <param name="a_swipeDuration">How long the swipe took to complete.</param>
	public delegate void OnSwipeFunc(Vector3 a_swipeStartPos, Vector3 a_swipeEndPos, float a_swipeDist, float a_swipeDuration);
	public event OnSwipeFunc OnSwipe;
#endregion

	/// <summary>
    /// Use this for initialization 
    /// </summary>
	void Start () {
		// Treat touch and mouse events as the same thing to make testing easier
		Input.simulateMouseWithTouches = true;
	}
	
	/// <summary>
    /// Update is called once per frame.
    /// </summary>
	void Update () {
		HandleInput();
	}

	/// <summary>
    /// Processes a single tick of input and triggers any relevant events.
    /// </summary>
    private void HandleInput()
    {
		Vector3 inputPos = Input.mousePosition;
		
		if (Input.GetButtonDown("Fire1")) {
			m_swipeStartPos = inputPos;
			m_isSwiping = true;
			m_swipeStartTime = Time.time;
		} else if (Input.GetButtonUp("Fire1")) {
			m_isSwiping = false;
			m_swipeStartTime = -1.0f;
		}

		if (m_isSwiping) {
			// Determine if the user has swiped far enough
			float swipeDist = Vector3.Distance(m_swipeStartPos, inputPos);
			float swipeDuration = Time.time - m_swipeStartTime;
			if (swipeDist > minSwipeDist) {
				TriggerSwipe(m_swipeStartPos, inputPos, swipeDist, swipeDuration);
			}
		}
    }

	/// <summary>
    /// Called when a swipe action has been detected.
    /// </summary>
    /// <param name="a_swipeStartPos">Where the swipe started.</param>
    /// <param name="a_swipeEndPos">Where the swipe has ended.</param>
    /// <param name="a_swipeDist">How far the swipe travelled.</param>
    /// <param name="a_swipeDuration">How long the swipe took to complete.</param>
    private void TriggerSwipe(Vector3 a_swipeStartPos, Vector3 a_swipeEndPos, float a_swipeDist, float a_swipeDuration)
    {
		if (OnSwipe != null)
		{
			OnSwipe(a_swipeStartPos, a_swipeEndPos, a_swipeDist, a_swipeDuration);
		}
		m_isSwiping = false;
    }
}
