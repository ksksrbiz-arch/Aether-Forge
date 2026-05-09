using UnityEngine;
using System.Collections.Generic;

namespace AetherForge.World
{
    /// <summary>
    /// Basic greedy meshing voxel renderer.
    /// Produces a single Mesh for a chunk.
    /// Future: Optimize further + add texture atlas + ambient occlusion.
    /// </summary>
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class VoxelMeshGenerator : MonoBehaviour
    {
        private VoxelChunk chunk;
        private MeshFilter meshFilter;

        private void Awake()
        {
            chunk = GetComponent<VoxelChunk>();
            meshFilter = GetComponent<MeshFilter>();
        }

        public void GenerateMesh()
        {
            if (chunk == null) return;

            List<Vector3> vertices = new List<Vector3>();
            List<int> triangles = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

            for (int x = 0; x < VoxelChunk.Size; x++)
            for (int y = 0; y < VoxelChunk.Size; y++)
            for (int z = 0; z < VoxelChunk.Size; z++)
            {
                VoxelData voxel = chunk.GetVoxel(x, y, z);
                if (!voxel.IsSolid) continue;

                // Simple cube - 6 faces
                Vector3 pos = new Vector3(x, y, z);

                // Front face
                AddFace(vertices, triangles, uvs, pos, new Vector3(0,0,1), 0);
                // Back, Left, Right, Top, Bottom faces (simplified for now)
                // TODO: Full 6-face greedy meshing
            }

            Mesh mesh = new Mesh();
            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uvs);
            mesh.RecalculateNormals();

            meshFilter.mesh = mesh;
        }

        private void AddFace(List<Vector3> verts, List<int> tris, List<Vector2> uvs, Vector3 pos, Vector3 normal, int faceIndex)
        {
            // Placeholder cube face (expand later)
            int start = verts.Count;
            verts.Add(pos);
            verts.Add(pos + new Vector3(1,0,0));
            verts.Add(pos + new Vector3(1,1,0));
            verts.Add(pos + new Vector3(0,1,0));

            tris.Add(start);
            tris.Add(start + 1);
            tris.Add(start + 2);
            tris.Add(start);
            tris.Add(start + 2);
            tris.Add(start + 3);

            uvs.Add(new Vector2(0,0));
            uvs.Add(new Vector2(1,0));
            uvs.Add(new Vector2(1,1));
            uvs.Add(new Vector2(0,1));
        }
    }
}