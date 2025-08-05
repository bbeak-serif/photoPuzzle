using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PieceGenerator : MonoBehaviour {
    private LoadGallery load;

    private const float PIECE_SIZE = 1.0f;
    private const float PROTRUSION_DEPTH = 0.2f;
    private const float PROTRUSION_WIDTH_RATIO = 0.3f;
    private string folderPath;
    private int puzzleSize;
    private Texture2D sourceImage;
    private Material sourceUV;
    internal MeshRenderer meshRenderer;
    internal MeshFilter meshFilter;

    private void Awake() {
        load = gameObject.AddComponent<LoadGallery>(); 
        folderPath = Application.persistentDataPath + "/SourceImage";

        string imageName = "Origin";
        string imagePath = Path.Combine(folderPath, imageName + ".png");
        if (!File.Exists(imagePath)) {
            Debug.LogError($"이미지 파일이 존재하지 않습니다: {imagePath}");
            load.OnClickButton();
        }
        sourceImage = NativeGallery.LoadImageAtPath(imagePath);
        sourceUV = new Material(Shader.Find("Unlit/Texture"));
        sourceUV.mainTexture = sourceImage;
        sourceUV.SetFloat("_Mode", 0);
        if (sourceUV.mainTexture == null) {
            Debug.LogError("머티리얼 텍스처 할당 실패");
        }
    }

    public void SetPuzzleSize(int size) {
        puzzleSize = size;
    }

    public GameObject GeneratePieceMesh(int bottomType, int leftType, int topType, int rightType, Vector2 position) {
        GameObject obj = new GameObject();

        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uv = new List<Vector2>();

        vertices = GetOuterMesh(bottomType, leftType, topType, rightType);
        triangles = GetTriangles(vertices.Count);
        uv = GetUV(bottomType, leftType, topType, rightType, position);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uv.ToArray();
        mesh.RecalculateNormals();

        MeshRenderer meshRenderer = obj.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = obj.AddComponent<MeshFilter>();

        meshFilter.mesh = mesh;
        meshRenderer.material = sourceUV;

        return obj;
    }

    #region Mesh
    private List<Vector3> GetOuterMesh(int bottomType, int leftType, int topType, int rightType) {
        List<Vector3> vertices = new List<Vector3>();

        Vector3 center = new Vector3(PIECE_SIZE / 2, PIECE_SIZE / 2);
        vertices.Add(center);


        Vector3 bottomLeft = new Vector3(0, 0, 0);
        vertices.Add(bottomLeft);

        Vector3 leftMiddle = Vector3.zero;
        if (leftType == 0) {
            leftMiddle = new Vector3(0, PIECE_SIZE / 2, 0);
        } else if (leftType == 1) {
            leftMiddle = new Vector3(-PROTRUSION_DEPTH, PIECE_SIZE / 2, 0);
        } else if (leftType == 2) {
            leftMiddle = new Vector3(PROTRUSION_DEPTH, PIECE_SIZE / 2, 0);
        }
        vertices.Add(leftMiddle);

        Vector3 TopLeft = new Vector3(0, PIECE_SIZE, 0);
        vertices.Add(TopLeft);

        Vector3 TopMiddle = Vector3.zero;
        if (topType == 0) {
            TopMiddle = new Vector3(PIECE_SIZE / 2, PIECE_SIZE, 0);
        } else if (topType == 1) {
            TopMiddle = new Vector3(PIECE_SIZE / 2, PIECE_SIZE + PROTRUSION_DEPTH, 0);
        } else if (topType == 2) {
            TopMiddle = new Vector3(PIECE_SIZE / 2, PIECE_SIZE - PROTRUSION_DEPTH, 0);
        }
        vertices.Add(TopMiddle);

        Vector3 TopRight = new Vector3(PIECE_SIZE, PIECE_SIZE, 0);
        vertices.Add(TopRight);

        Vector3 rightMiddle = Vector3.zero;
        if (rightType == 0) {
            rightMiddle = new Vector3(PIECE_SIZE, PIECE_SIZE / 2, 0);
        } else if (rightType == 1) {
            rightMiddle = new Vector3(PIECE_SIZE + PROTRUSION_DEPTH, PIECE_SIZE / 2, 0);
        } else if (rightType == 2) {
            rightMiddle = new Vector3(PIECE_SIZE - PROTRUSION_DEPTH, PIECE_SIZE / 2, 0);
        }
        vertices.Add(rightMiddle);

        Vector3 rightBottom = new Vector3(PIECE_SIZE, 0, 0);
        vertices.Add(rightBottom);

        Vector3 bottomMiddle = Vector3.zero;
        if (bottomType == 0) {
            bottomMiddle = new Vector3(PIECE_SIZE / 2, 0, 0);
        } else if (bottomType == 1) {
            bottomMiddle = new Vector3(PIECE_SIZE / 2, -PROTRUSION_DEPTH, 0);
        } else if (bottomType == 2) {
            bottomMiddle = new Vector3(PIECE_SIZE / 2, +PROTRUSION_DEPTH, 0);
        }
        vertices.Add(bottomMiddle);


        return vertices;
    }

    private List<int> GetTriangles(int _verticesCount) {
        List<int> triangles = new List<int>();

        int verticesCount = _verticesCount - 1;
        for (int i = 0; i < verticesCount; i++){
            int p1 = i + 1;
            int p2 = (i + 1) % verticesCount + 1;

            triangles.Add(0);
            triangles.Add(p1);
            triangles.Add(p2);
        }

        return triangles;
    }

    private List<Vector2> GetUV(int bottomType, int leftType, int topType, int rightType, Vector2 position) {
        List<Vector2> UVs = new List<Vector2>();
        float n = 1f / (puzzleSize * 1f);
        float depthRatio = PROTRUSION_DEPTH / PIECE_SIZE;

        Vector2 topLeft = new Vector2(0, 1) + new Vector2(n * position.x, -n * position.y);

        Vector2 center = topLeft + new Vector2(n/2, -n/2);

        Vector2 bottomLeft = topLeft + new Vector2(0,-n);
        Vector2 topRight = topLeft + new Vector2(n, 0);
        Vector2 bottomRight = topLeft + new Vector2(n, -n);
        

        UVs.Add(center);

        UVs.Add(bottomLeft);
        if (leftType == 0)
            UVs.Add(bottomLeft + new Vector2(0, n/2));
        else if(leftType == 1)
            UVs.Add(bottomLeft + new Vector2(0, n / 2) + new Vector2(-depthRatio * n, 0));
        else if(leftType == 2)
            UVs.Add(bottomLeft + new Vector2(0, n / 2) + new Vector2(depthRatio * n, 0));

        UVs.Add(topLeft);
        if(topType == 0)
            UVs.Add(topLeft + new Vector2(n/2, 0));
        else if (topType == 1)
            UVs.Add(topLeft + new Vector2(n / 2, 0) + new Vector2(0, depthRatio * n));
        else if (topType == 2)
            UVs.Add(topLeft + new Vector2(n / 2, 0) + new Vector2(0, -depthRatio * n));

        UVs.Add(topRight);
        if(rightType == 0)
            UVs.Add(topRight + new Vector2(0, -n/2));
        else if (rightType == 1)
            UVs.Add(topRight + new Vector2(0, -n / 2) + new Vector2(depthRatio * n, 0));
        else if (rightType == 2)
            UVs.Add(topRight + new Vector2(0, -n / 2) + new Vector2(-depthRatio * n, 0));

        UVs.Add(bottomRight);
        if(bottomType == 0)
            UVs.Add(bottomRight + new Vector2(-n/2, 0));
        else if (bottomType == 1)
            UVs.Add(bottomRight + new Vector2(-n / 2, 0) + new Vector2(0, -depthRatio * n));
        else if (bottomType == 2)
            UVs.Add(bottomRight + new Vector2(-n / 2, 0) + new Vector2(0, depthRatio * n));

        return UVs;
    }
    #endregion
}
