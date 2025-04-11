using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class SpringStretching : MonoBehaviour
{
    [Header("Anchor Points")]
    public Transform anchorPoint1;
    public Transform anchorPoint2;

    [Header("Spring Settings")]
    public float thickness = 0.5f;
    public int segments = 20;

    private Mesh originalMesh;
    private Mesh stretchedMesh;
    private Vector3[] originalVertices;
    private Vector3[] stretchedVertices;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogError("MeshFilter missing or no mesh assigned!");
            enabled = false;
            return;
        }

        // Store reference to original mesh
        originalMesh = meshFilter.sharedMesh;

        // Create a new mesh for stretching
        stretchedMesh = new Mesh();
        stretchedMesh.name = "StretchedSpring";

        // Manually copy necessary data from original mesh
        originalVertices = originalMesh.vertices;
        stretchedVertices = new Vector3[originalVertices.Length];
        stretchedMesh.vertices = originalVertices;
        stretchedMesh.triangles = originalMesh.triangles;
        stretchedMesh.uv = originalMesh.uv;

        // Assign the new mesh to the MeshFilter
        meshFilter.mesh = stretchedMesh;
    }

    void Update()
    {
        if (anchorPoint1 == null || anchorPoint2 == null)
        {
            Debug.LogWarning("Anchor points not assigned!");
            return;
        }

        Vector3 direction = anchorPoint2.position - anchorPoint1.position;
        float distance = direction.magnitude;
        direction.Normalize();

        float originalLength = originalMesh.bounds.size.z;
        float scaleFactor = distance / originalLength;

        StretchMesh(scaleFactor, direction);
    }

    private void StretchMesh(float scaleFactor, Vector3 stretchDirection)
    {
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, stretchDirection);
        Vector3 centerPos = (anchorPoint1.position + anchorPoint2.position) / 2f;

        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            vertex.z *= scaleFactor;
            vertex = rotation * vertex;
            vertex += centerPos - transform.position;
            stretchedVertices[i] = vertex;
        }

        // Assign vertices and recalculate
        stretchedMesh.vertices = stretchedVertices;
        stretchedMesh.RecalculateNormals();
        stretchedMesh.RecalculateBounds();
    }

    void OnDestroy()
    {
        // Clean up the dynamically created mesh
        if (stretchedMesh != null)
        {
            Destroy(stretchedMesh);
        }
    }
}
