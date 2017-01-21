using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{
    /// <summary>
    /// Wave height.
    /// </summary>
    public float fWaveVertSize = 0.025f;

    /// <summary>
    /// First waveform's speed.
    /// </summary>
    public float fFirstWaveSpeed = 0.007f;

    /// <summary>
    /// Second waveform's speed.
    /// </summary>
    public float fSecondWaveSpeed = 0.001f;

    /// <summary>
    /// Water mesh size.
    /// </summary>
    private Vector3 _v3WaterWorldSize;

    /// <summary>
    /// Absolute minumum water mesh size.
    /// </summary>
    private const float MINMESHSIZE = 5.0f;

    /// <summary>
    /// Engine capped maxiumum number of vertices (larger than a short?).
    /// </summary>
    private const int MAXVERTICES = 64999;

    /// <summary>
    /// Number of sides in a triangle.
    /// </summary>
    private const int TRISIDES = 3;

    /// <summary>
    /// Halved size of a patch.
    /// </summary>
    private const float VERTEXSIZE = 0.5f;

    /// <summary>
    /// Called when the class is initialised
    /// </summary>
    void Start()
    {
        // Set up default properties
        gameObject.name = "Water";
        GetComponent<Collider>().isTrigger = true;

        // Grab the size of the world mesh
        _v3WaterWorldSize=this.transform.localScale;

        // Store the size
        _v3WaterWorldSize.x=Mathf.Abs(_v3WaterWorldSize.x);
        _v3WaterWorldSize.y=Mathf.Abs(_v3WaterWorldSize.y);
        _v3WaterWorldSize.z=Mathf.Abs(_v3WaterWorldSize.z);

        // If below the minimum size, return
        if(_v3WaterWorldSize.x < MINMESHSIZE || _v3WaterWorldSize.z < MINMESHSIZE)
            return;

        // Calculate the number of patches in the mesh
        int iPatchHCount = Mathf.RoundToInt(_v3WaterWorldSize.x / 2);
        int iPatchVCount = Mathf.RoundToInt(_v3WaterWorldSize.z / 2);

        // Calculate the number of triangles and vertices needed (2 triangles per patch)
        int iNumTriangles = (iPatchHCount * iPatchVCount) * (TRISIDES * 2);
        int iNumVertices = (iPatchHCount + 1) * (iPatchVCount + 1);

        // If above maximum engine number of vertices, stop
        if (iNumVertices > MAXVERTICES)
        {
            Debug.LogError(gameObject.name + " has too many vertices(" + iNumVertices + " of 65000), make it smaller.");
            return;
        }

        // Variables to hold the new water mesh data
        Vector3[] v3NewVertices = new Vector3[iNumVertices];
        Vector2[] v2NewUVs = new Vector2[iNumVertices];
        Vector4[] v4NewTangents = new Vector4[iNumVertices];
        int[] iNewTriangles =new int[iNumTriangles];

        // Mesh index variable
        int iMeshIndex = 0;

        // Calculate various scales
        float fMeshScale = (_v3WaterWorldSize.x + _v3WaterWorldSize.z) / MINMESHSIZE;
        float fUVx = 1.0f / iPatchHCount * fMeshScale;
        float fUVy = 1.0f / iPatchVCount * fMeshScale;
        float fVertexScaleX = 1.0f / iPatchHCount;
        float fVertexScaleY = 1.0f / iPatchVCount;

        // Coordinate variables
        float fXPos = 0.0f;
        float fYPos = 0.0f;

        // Construct the vertices
        for(fYPos = 0; fYPos < (iPatchVCount + 1); fYPos++){
            for(fXPos = 0; fXPos < (iPatchHCount + 1); fXPos++){
                // Add each vertex
                v3NewVertices[iMeshIndex] =new Vector3(-VERTEXSIZE + (fXPos * fVertexScaleX), VERTEXSIZE,-VERTEXSIZE + (fYPos * fVertexScaleY));

                // Add a UV for the new vertex
                v2NewUVs[iMeshIndex] = new Vector2(fXPos*fUVx, fYPos*fUVy);

                // Default tangent for this new vertex (increments the index counter)
                v4NewTangents[iMeshIndex++] = new Vector4(0,1,0,1);
            }
        }

        // Reset the index counter
        iMeshIndex=0;

        // Create the mesh faces
        for(fYPos=0; fYPos < iPatchVCount; fYPos++){
            for(fXPos=0; fXPos < iPatchHCount; fXPos++){
                // Construct the 6 triangles to make up the cell
                iNewTriangles[iMeshIndex] = Mathf.RoundToInt((fYPos * (iPatchHCount + 1)) + fXPos);
                iNewTriangles[iMeshIndex+1] = Mathf.RoundToInt(((fYPos+1) * (iPatchHCount + 1)) + fXPos);
                iNewTriangles[iMeshIndex+2] = Mathf.RoundToInt((fYPos * (iPatchHCount + 1)) + fXPos + 1);
                iNewTriangles[iMeshIndex+3] = Mathf.RoundToInt(((fYPos + 1) * (iPatchHCount + 1)) + fXPos);
                iNewTriangles[iMeshIndex+4] = Mathf.RoundToInt(((fYPos + 1) * (iPatchHCount + 1)) + fXPos + 1);
                iNewTriangles[iMeshIndex+5] = Mathf.RoundToInt((fYPos * (iPatchHCount + 1)) + fXPos + 1);
                
                // Increment the index counter as such
                iMeshIndex += 6;
            }
        }

        // Create a new empty mesh to store the generated mesh
        Mesh meshNewWater = new Mesh ();

        // Clear the existing mesh
        GetComponent<MeshFilter>().mesh = meshNewWater;

        // Clear all mesh data
        meshNewWater.Clear();

        // Assign the new vertices
        meshNewWater.vertices = v3NewVertices;

        // Assign UV data
        meshNewWater.uv = v2NewUVs;

        // Assign triangles
        meshNewWater.triangles = iNewTriangles;

        // Assign tangents
        meshNewWater.tangents = v4NewTangents;
    }

 
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update () {
        if (GetComponent<Renderer>().enabled)
        {
            // Retrieve mesh component
            Mesh meshWaterMesh = GetComponent<MeshFilter>().mesh;

            // Retrieve mesh vertices
            Vector3[] v3WaterVertices = meshWaterMesh.vertices;

            // Calculate scale (average of X and Z plane sizes)
            float fWaterScale = (_v3WaterWorldSize.x + _v3WaterWorldSize.z) / 2.0f;

            // Represents the vertex's vertical offset
            float fYOffset = 0.0f;

            // Apply the wave to each vertex
            for (int i = 0; i < v3WaterVertices.Length; i++)
            {
                // Reset the offset
                fYOffset = 0.0f;

                // Apply first waveform
                fYOffset += Mathf.Sin(((Time.time * fFirstWaveSpeed + (v3WaterVertices[i].x + v3WaterVertices[i].z) * VERTEXSIZE) % (Mathf.PI / 2)) * fWaterScale);

                // Apply second waveform
                fYOffset += Mathf.Sin((Time.time * fSecondWaveSpeed + (v3WaterVertices[i].z)) * fWaterScale);

                // Apply combined offset to the vertex
                v3WaterVertices[i].y = VERTEXSIZE + fYOffset * fWaveVertSize;
            }

            // Apply vertices
            meshWaterMesh.vertices = v3WaterVertices;

            // Optimize the water mesh
            ;

            // Fix up vertex normals
            meshWaterMesh.RecalculateNormals();
        }
    } 

    /// <summary>
    /// Called when an entity enters the water.
    /// </summary>
    /// <param name="other">Object entering the water.</param>
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name + " has entered the water!");
    }

    /// <summary>
    /// Called when an entity enters the water.
    /// </summary>
    /// <param name="other">Object entering the water.</param>
    void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name + " has exited the water!");
    }

    /// <summary>
    /// Calculates and returns the mesh's height at the input position.
    /// </summary>
    /// <param name="fXPos">World X position.</param>
    /// <param name="fYPos">World Y position.</param>
    /// <returns>Vertex map height at the input position.</returns>
    public float HeightAtPosition(float fXPos, float fYPos)
    {
        // Distance finding variables
        float fClosestDist = float.MaxValue;
        Vector3 v3ClosestVert = Vector3.zero;

        // Pseudo vector to store input coordinates
        Vector3 v3InpVect = new Vector3(fXPos, 0, fYPos);

        // Find the closest vertex to the input coordinates
        foreach (Vector3 v3TempVert in GetComponent<MeshFilter>().mesh.vertices)
        {
            // Move the vector into world space
            Vector3 v3WorldVert = transform.TransformPoint(new Vector3(v3TempVert.x, 0, v3TempVert.z));

            // Calculate the horizontal distance between the world space vertex and the input point
            float fDist = Vector3.Distance(v3WorldVert, v3InpVect);

            // If closer
            if (fDist < fClosestDist)
            {
                // Store the distance and world vector
                fClosestDist = fDist;
                v3ClosestVert = v3WorldVert;
            }
        }

        // Return mesh height if possible (if not, return transform height)
        return v3ClosestVert != Vector3.zero ? v3ClosestVert.y : transform.position.y;
    }
}
