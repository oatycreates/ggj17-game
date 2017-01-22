using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SwipeObject : MonoBehaviour {
#region Variables
	public float waterLevelY = -5.0f;
	public float airSwipeMult = 0.75f;
	public float waterSwipeMult = 1.5f;

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
    /// Draws gizmos.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    	Gizmos.DrawLine(new Vector3(-100, waterLevelY, 0), new Vector3(100, waterLevelY, 0));
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
		float swipeForceMult = CalculateSwipeForceMult(a_swipeStartPos, a_swipeEndPos, a_swipeDist, a_swipeDuration);

		// Add swipe force to RigidBody
		float swipeSpeed = a_swipeDist / a_swipeDuration;
		Vector3 swipeDirection = (a_swipeEndPos - a_swipeStartPos).normalized;

		//Sorry Patrick- this is a quick fix
		if (Time.timeScale > 0)
		{
			m_rigidbody.AddForceAtPosition(swipeDirection * swipeSpeed * swipeForceMult, a_swipeStartPos, ForceMode.Force);
		}
		else
		{
			Debug.LogWarning("Trying to Move Marleen while game is paused"); 
		}

	}

    private float CalculateSwipeForceMult(Vector3 a_swipeStartPos, Vector3 a_swipeEndPos, float a_swipeDist, float a_swipeDuration)
    {
		// If below water, increase force
		if (m_rigidbody.position.y <= waterLevelY)
		{
			return waterSwipeMult;
		}
		else
		{
			return airSwipeMult;
		}
    }
}
