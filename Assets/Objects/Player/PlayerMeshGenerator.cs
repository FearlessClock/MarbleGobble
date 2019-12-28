using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class PlayerMeshGenerator : MonoBehaviour
{
    [SerializeField] private EntrancePointsHolder entrancePointHolder = null;
    [SerializeField] private float width = 0.5f;
    private Vector3[] vertices;
    private MeshCollider meshCollider = null;

    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        entrancePointHolder.OnValueChanged += GenerateMesh;
    }

    public void GenerateMesh()
    {
        vertices = new Vector3[entrancePointHolder.ListOfEntrancePoints.Count * 4];
        for (int i = 0; i < entrancePointHolder.ListOfEntrancePoints.Count; i++)
        {
            EntrancePoint curPoint = entrancePointHolder.ListOfEntrancePoints[i];
            vertices[0 + i * 4] = curPoint.exitPoint + (Vector2)( Quaternion.Euler(0, 0, curPoint.angleFromRight) * Vector2.down).normalized * width;
            vertices[1 + i * 4] = curPoint.exitPoint + (Vector2)(Quaternion.Euler(0, 0, curPoint.angleFromRight) * Vector2.up).normalized * width;
            vertices[2 + i * 4] = curPoint.entrancePoint + (Vector2)(Quaternion.Euler(0, 0, curPoint.angleFromRight) * Vector2.down).normalized * width;
            vertices[3 + i * 4] = curPoint.entrancePoint + (Vector2)(Quaternion.Euler(0, 0, curPoint.angleFromRight) * Vector2.up).normalized * width;
        }
        Mesh mesh;
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        meshCollider.sharedMesh = mesh;
        mesh.name = "Procedural Grid";

		mesh.vertices = vertices;
        int[] triangles = new int[entrancePointHolder.ListOfEntrancePoints.Count * 6];
        for (int i = 0; i < entrancePointHolder.ListOfEntrancePoints.Count; i++)
        {
            triangles[0 + i * 6] = 0 + i * 4;
            triangles[1 + i * 6] = 1 + i * 4;
            triangles[2 + i * 6] = 2 + i * 4;
            triangles[3 + i * 6] = 1 + i * 4;
            triangles[4 + i * 6] = 3 + i * 4;
            triangles[5 + i * 6] = 2 + i * 4;
        }
        mesh.triangles = triangles;
    }
    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.color = new Color(0, i * 0.05f, 0);
            Gizmos.DrawSphere(this.transform.rotation * vertices[i], 0.1f);
        }
    }
}
