using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class BumpMesh : MonoBehaviour {

    public Texture2D normalMap;
    public float bumpScale = 1f;

    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;
    private Vector2[] uv;

    void Start () {
        mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        vertices = mesh.vertices;
        normals = mesh.normals;
        uv = mesh.uv;

		for (int i = 0; i < vertices.Length; i++) {
		    Vector3 vertex = transform.TransformPoint(vertices[i]);
		    Vector3 normal = transform.TransformDirection(normals[i]);
		    Vector2 texcoord = uv[i];

		    // Sample the normal map at the vertex position
		    Vector3 tangent = Vector3.Cross(Vector3.forward, normal);
		    Vector3 bitangent = Vector3.Cross(normal, tangent);
		    Color worldNormalc = normalMap.GetPixelBilinear(texcoord.x, texcoord.y) * 2f - new Color(1f, 1f, 1f, 0f);
			Vector3 worldNormal = new Vector3(worldNormalc.r, worldNormalc.g, worldNormalc.b);
		    Vector3 bumpNormal = tangent * worldNormal.x + bitangent * worldNormal.y + normal * worldNormal.z;

		    // Bump the vertex position based on the normal map
		    vertex += bumpNormal * bumpScale;

		    // Transform the vertex back to local space
		    vertices[i] = transform.InverseTransformPoint(vertex);
		    normals[i] = transform.InverseTransformDirection(bumpNormal.normalized); // Convert to a Vector3
		}

		// Update the mesh with the modified vertices and normals
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.RecalculateBounds();
		mesh.RecalculateTangents();
    }
}