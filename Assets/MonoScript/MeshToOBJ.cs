using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class MeshToOBJ : MonoBehaviour
{
    public Mesh mesh;

    public string savePath;

    public void Start()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Vector3 v in mesh.vertices)
        {
            sb.AppendFormat("v {0} {1} {2}\n", v.x, v.y, v.z);
        }
        foreach (Vector3 v in mesh.normals)
        {
            sb.AppendFormat("vn {0} {1} {2}\n", v.x, v.y, v.z);
        }
        foreach (Vector2 v in mesh.uv)
        {
            sb.AppendFormat("vt {0} {1}\n", v.x, v.y);
        }
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            sb.AppendFormat("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
            mesh.triangles[i] + 1, mesh.triangles[i + 1] + 1, mesh.triangles[i + 2] + 1);
        }
        if (!Directory.Exists(Application.dataPath + "/the"))
        {
            Directory.CreateDirectory(Application.dataPath + "/the");
        }
        File.Create(Application.dataPath + "/the/" + this.name + ".obj").Dispose();
        Debug.LogError(Application.dataPath + "/the/" + this.name + ".obj");
        File.WriteAllText(Application.dataPath + "/the/" + this.name + ".obj", sb.ToString());
    }
}
