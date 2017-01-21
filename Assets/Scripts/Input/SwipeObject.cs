using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SwipeObject : MonoBehaviour {
#region Variables
	private TouchDrag m_touchDrag = null;
	private Rigidbody m_rigidbody = null;
#endregion

	/// <summary>
    /// Use this for initialization.
    /// </summary>
	void Start () {
		// Register touch event handler
		m_touchDrag = FindObjectOfType<TouchDrag>();
		if (!m_touchDrag) {
			Debug.LogError("Can't find TouchDrag object in the scene!");
		}
		m_touchDrag.OnSwipe += OnSwipe;

		m_rigidbody = GetComponent<Rigidbody>();
		if (m_rigidbody == null) {
			Debug.LogError("Couldn't find RigidBody component!");
		}
	}
	
	/// <summary>
    /// Update is called once per frame
    /// </summary>
	void Update () {
		
	}


	/// <summary>
    /// Called when a swipe action has been detected.
    /// </summary>
    /// <param name="a_swipeStartPos">Where the swipe started.</param>
    /// <param name="a_swipeEndPos">Where the swipe has ended.</param>
    /// <param name="a_swipeDist">How far the swipe travelled.</param>
    /// <param name="a_swipeDuration">How long the swipe took to complete.</param>
	public void OnSwipe(Vector3 a_swipeStartPos, Vector3 a_swipeEndPos, float a_swipeDist, float a_swipeDuration)
	{
		// Add swipe force to RigidBody
		float swipeSpeed = a_swipeDist / a_swipeDuration;
		Vector3 swipeDirection = (a_swipeEndPos - a_swipeStartPos).normalized;
		m_rigidbody.AddForceAtPosition(swipeDirection * swipeSpeed, a_swipeStartPos, ForceMode.Force);

	}
}
