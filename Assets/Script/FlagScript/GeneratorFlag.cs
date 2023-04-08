using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class GeneratorFlag : MonoBehaviour
{
    #region Variables
    [SerializeField] private int _vertexHorizontal;
    [SerializeField] private int _vertexVertical;
    [SerializeField] private float width;
    [SerializeField] private float height;
    [SerializeField] private Material _flagMaterial;

    private Vector2 [] uv;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    #endregion

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        CreateFlag();
        MaterialFlag();
    }

    private void CreateFlag()
    {
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        mesh.vertices = FillArrayVertex();
        mesh.triangles = FillArrayTriagle();
        mesh.RecalculateNormals();
        mesh.uv = uv;
    }

    private Vector3[] FillArrayVertex()
    {
        Vector3[] vertices = new Vector3[(_vertexHorizontal + 1) * (_vertexHorizontal + 1)];
        uv = new Vector2[vertices.Length];

        for (int i = 0, y = 0; y <= _vertexVertical; y++)
        {
            float yPos = (float)y / _vertexVertical * height;
            float v = (float)y / _vertexVertical;
            for (int x = 0; x <= _vertexHorizontal; x++, i++)
            {
                float xPos = (float)x / _vertexHorizontal * width;
                float u = (float)x / _vertexHorizontal;
                vertices[i] = new Vector3(xPos, yPos, 0f);
                uv[i] = new Vector2(u, v);
            }
        }
        return vertices;
    }

    private int[] FillArrayTriagle()
    {
        int[] triangles = new int[_vertexHorizontal * _vertexVertical * 6];
        for (int ti = 0, vi = 0, y = 0; y < _vertexVertical; y++, vi++)
        {
            for (int x = 0; x < _vertexHorizontal; x++, ti += 6, vi++)
            {
                triangles[ti] = vi;
                triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                triangles[ti + 4] = triangles[ti + 1] = vi + _vertexHorizontal + 1;
                triangles[ti + 5] = vi + _vertexHorizontal + 2;
            }
        }
        return triangles;
    }

    private void MaterialFlag() => meshRenderer.material = _flagMaterial;  
}

