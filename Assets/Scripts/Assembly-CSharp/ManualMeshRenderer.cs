using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualMeshRenderer : MonoBehaviour
{
    public List<Vector3> vertices;

    public List<Vector3> normals;

    public List<int> triangles;

    public List<Vector2> uvs;

    void Start()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }
}
