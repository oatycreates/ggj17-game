using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buoyancy : MonoBehaviour {
    /// <summary>
    /// How many points to place within the model's bounding box.
    /// </summary>
    public int numGridPoints = 15;

    /// <summary>
    /// Height of the water level.
    /// </summary>
    public float waterLevelY = 3.0f;

    /// <summary>
    /// Density of this object.
    /// </summary>
    public float objectDensity = 10.0f;
	
	/// <summary>
	/// Overall scaling factor to use when calculating volumetric buoyancy points.
    /// This may help make the grid better fit the object's shape.
	/// </summary>
	public float volumeGridScaleMult = 2.2f;

    /// <summary>
    /// Density of the water this object is floating in.
    /// </summary>
    private const float WATERDENSITY = 1000.0f;

    /// <summary>
    /// Damping factor to use when applying force to each point.
    /// </summary>
	private const float POINTDAMPING = 0.1f;

    /// <summary>
    /// Low resolution point representation of the rigid body
    /// </summary>
    private ArrayList m_buoyancyPoints;

    /// <summary>
    /// Archimedes force to apply per point.
    /// </summary>
    private Vector3 m_archimedesForce = Vector3.zero;

    /// <summary>
    /// Average distance between points on the mesh.
    /// </summary>
    private float m_meshAvgPointDist = 0.0f;

    /// <summary>
    /// Total number of points in the volumetric simulation.
    /// </summary>
    private int m_totalVolumePoints = 0;

    /// <summary>
    /// Used for rendering force gizmos
    /// </summary>
    private List<Vector3[]> m_gizmoForces;

    /// <summary>
    /// Called when the class is initialised
    /// </summary>
    void Start()
    {
        if (GetComponent<Rigidbody>() == null)
            Debug.Log("No rigid body instance attached to buoyancy script!");

        if (numGridPoints % 2 == 0)
        {
            Debug.Log("Use an odd number of points, it gives better results in regards to symmetry.");
        }
		
		// Set the rigid body's centre of mass
		//rigidbody.centerOfMass = new Vector3(0, -collider.bounds.extents.y * 2.0f, 0) + transform.InverseTransformPoint(collider.bounds.center);

        // Initialise points map
        m_buoyancyPoints = new ArrayList();

        // Initialise forces for gizmo rendering
        m_gizmoForces = new List<Vector3[]>();
		
		// Store the previous rotation
		Quaternion prevRot = transform.rotation;
		
		// Zero the rotation
		transform.rotation = Quaternion.Euler(0, 0, 0);

        // Calculate the desired distance between each point
        float pointDistX = GetComponent<Collider>().bounds.size.x * volumeGridScaleMult / numGridPoints;
        float pointDistY = GetComponent<Collider>().bounds.size.y * volumeGridScaleMult / numGridPoints;
        float pointDistZ = GetComponent<Collider>().bounds.size.z * volumeGridScaleMult / numGridPoints;

        // Average the point distances
        m_meshAvgPointDist = (pointDistX + pointDistY + pointDistZ) / 3;

        // Begin generating map of points
        for (int z = 0; z < numGridPoints; z++)
        {
            for (int y = 0; y < numGridPoints; y++)
            {
                for (int x = 0; x < numGridPoints; x++)
                {
                    // Calculate the point
                    Vector3 worldPoint = new Vector3(
                            (x - Mathf.Floor(numGridPoints / 2)) * pointDistX,
                            (y - Mathf.Floor(numGridPoints / 2)) * pointDistY,
                            (z - Mathf.Floor(numGridPoints / 2)) * pointDistZ);

                    // If the point is inside the mesh
                    if (IsInside(GetComponent<Collider>(), transform.TransformPoint(worldPoint)))
                    {
                        // Add the point to the array
                        m_buoyancyPoints.Add(worldPoint);
                    }
                }
            }
        }

        // Store number of points
        m_totalVolumePoints = m_buoyancyPoints.Count;

        // Calculate mesh volume
        float objVolume = GetComponent<Rigidbody>().mass / objectDensity;

        // Calculate archimedes' force per point
        float archimedesForceMagnitude = WATERDENSITY * Mathf.Abs(Physics.gravity.y) * objVolume;
        m_archimedesForce = (Vector3.up * archimedesForceMagnitude) / m_totalVolumePoints;
		
		// Restore the previous rotation
		transform.rotation = prevRot;
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        // Clear the last frame's force gizmos
        m_gizmoForces.Clear();

        // Calculate all points under the water
        foreach (Vector3 point in m_buoyancyPoints)
		{
            // Shift the point into world space
            Vector3 worldPoint = transform.TransformPoint(point);

            //Debug.DrawLine(worldPoint, new Vector3(worldPoint.x, waterLevelY, worldPoint.z), Color.red);

            /*// If more accurate water level data is available, use it
            if (goWaterLevel.GetComponentInChildren<Waves>() != null)
            {
                //waterLevelY = goWaterLevel.GetComponent<Waves>().HeightAtPosition(worldPoint.x, worldPoint.z);
                waterLevelY = goWaterLevel.GetComponent<Waves>().transform.position.y;

                Debug.DrawLine(worldPoint, new Vector3(worldPoint.x, waterLevelY, worldPoint.z), Color.red);
            }
            else
            {
                // Otherwise use the default height
                waterLevelY = goWaterLevel.transform.position.y;
            }*/

            // If the point is below the water level (or close enough)
            if (worldPoint != Vector3.zero && (worldPoint.y <= waterLevelY))
            {
                // Calculate the magnitude provided by point depth
                float distanceMagnitude = ((waterLevelY - worldPoint.y) / m_meshAvgPointDist) + 0.5f;
                distanceMagnitude = Mathf.Clamp(distanceMagnitude, 0, 1);

                // Calculate the damping force on the point
                Vector3 worldVelocity = GetComponent<Rigidbody>().GetPointVelocity(worldPoint);
                Vector3 localDampingForce = -worldVelocity * POINTDAMPING * GetComponent<Rigidbody>().mass;

                // Apply archimedes' force
                Vector3 finalForce = localDampingForce + (Mathf.Sqrt(distanceMagnitude) * m_archimedesForce);

                // Put the force into action
                GetComponent<Rigidbody>().AddForceAtPosition(finalForce * Time.deltaTime, worldPoint);

                // Store the force for rendering the gizmos
                m_gizmoForces.Add(new[] { worldPoint, finalForce });
            }
        }
    }

    /// <summary>
    /// Tests whether the input point is inside the input collider.
    /// </summary>
    /// <param name="collider">Collider to test for collision.</param>
    /// <param name="point">Point to test.</param>
    /// <returns>True if the point is inside the collider, false if not.</returns>
    static public bool IsInside(Collider collider, Vector3 point)
    {
        Vector3 boundsCentre;
        Vector3 rayDir;
        Ray collisionTestRay;
        RaycastHit hitInfo;
        bool didRayHit;

        // Retrieve the centre of the collider (May be inaccurate for planes)
        boundsCentre = collider.bounds.center;

        // Cast a ray from the input point to the center
        if (Vector3.Distance(boundsCentre, point) > 0.001f)
		{
	        rayDir = boundsCentre - point;
	        collisionTestRay = new Ray(point, rayDir);
	        didRayHit = collider.Raycast(collisionTestRay, out hitInfo, rayDir.magnitude);

	        // If the ray hit the collider, the point is outside. So return the inverse of the collision status
	        return !didRayHit && collider.bounds.Contains(point);
        }
        else
        {
        	return true;
        }
    }

    /// <summary>
    /// Draws gizmos.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (m_gizmoForces == null)
        {
            return;
        }
        Gizmos.color = Color.cyan;

        foreach (Vector3[] v3ForceData in m_gizmoForces)
        {
            Gizmos.DrawLine(v3ForceData[0], v3ForceData[0] + (v3ForceData[1] / GetComponent<Rigidbody>().mass));
        }
    }
}
