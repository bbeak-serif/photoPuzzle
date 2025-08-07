using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class UIMeshGraphic : Graphic {
    public Mesh mesh;
    public Texture texture;  // Inspector에서 설정하거나 코드로 할당 (sourceImage)

    public override Texture mainTexture {
        get {
            return texture != null ? texture : s_WhiteTexture;  // 텍스처 반환 오버라이드
        }
    }

    protected override void OnPopulateMesh(VertexHelper vh) {
        vh.Clear();
        if (mesh == null) return;

        var vertices = mesh.vertices;
        var uvs = mesh.uv;
        var triangles = mesh.triangles;

        // (이전 스케일링 코드 유지: rect, scaleX/Y, pivotOffset 등)
        Rect rect = rectTransform.rect;
        float width = rect.width;
        float height = rect.height;
        Vector2 pivot = rectTransform.pivot;
        float pivotOffsetX = width * pivot.x;
        float pivotOffsetY = height * pivot.y;
        float scaleX = width / 1.0f;  // PIECE_SIZE=1.0f
        float scaleY = height / 1.0f;

        for (int i = 0; i < vertices.Length; i++) {
            Vector3 pos = vertices[i];
            pos.x = (pos.x * scaleX) - pivotOffsetX;
            pos.y = (pos.y * scaleY) - pivotOffsetY;
            pos.z = 0;

            vh.AddVert(pos, color, uvs.Length > i ? uvs[i] : Vector2.zero);
        }

        for (int i = 0; i < triangles.Length; i += 3) {
            vh.AddTriangle(triangles[i], triangles[i + 1], triangles[i + 2]);
        }
    }

    public void SetMesh(Mesh newMesh) {
        mesh = newMesh;
        SetVerticesDirty();
        SetMaterialDirty();  // Material 변경 시 강제 업데이트
    }

    public void SetTexture(Texture newTexture) {
        texture = newTexture;
        if (material != null) {
            material.mainTexture = texture;  // Material에 직접 바인딩
        }
        SetMaterialDirty();
    }
}