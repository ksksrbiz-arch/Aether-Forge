using UnityEngine;
using System.IO;
using System.Collections.Generic;
using AetherForge.World;
using AetherForge.Factory;

namespace AetherForge.Core
{
    /// <summary>
    /// Handles saving and loading the world and factory state.
    /// Uses JSON for simplicity (binary for production).
    /// </summary>
    public class SaveLoadManager : MonoBehaviour
    {
        public static SaveLoadManager Instance { get; private set; }

        public string SaveFileName = "AetherForgeSave.json";
        public WorldManager WorldManager;

        private string SavePath => Path.Combine(Application.persistentDataPath, SaveFileName);

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void SaveGame()
        {
            WorldSaveData data = new WorldSaveData();
            data.SaveTime = Time.time;

            // Save chunks
            foreach (var kvp in WorldManager.loadedChunks) // Assume public or add getter
            {
                var chunk = kvp.Value;
                data.Chunks.Add(new ChunkSaveData(chunk.ChunkPosition, chunk.Voxels));
            }

            // Save machines (demo: find all in scene)
            var machines = FindObjectsOfType<ConveyorBelt>();
            foreach (var m in machines)
            {
                data.Machines.Add(new MachineSaveData
                {
                    Type = "ConveyorBelt",
                    Position = m.transform.position,
                    Rotation = m.transform.rotation
                });
            }

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);
            Debug.Log($"Game saved to {SavePath}");
        }

        public void LoadGame()
        {
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning("No save file found.");
                return;
            }

            string json = File.ReadAllText(SavePath);
            WorldSaveData data = JsonUtility.FromJson<WorldSaveData>(json);

            // Clear current world (simple: destroy chunks)
            foreach (var chunk in WorldManager.loadedChunks.Values)
                Destroy(chunk.gameObject);
            WorldManager.loadedChunks.Clear();

            // Load chunks
            foreach (var chunkData in data.Chunks)
            {
                // Recreate chunk (simplified - in real game use pooling)
                Vector3Int pos = chunkData.Position;
                VoxelChunk newChunk = WorldManager.CreateChunk(pos); // Or custom load
                // Apply voxel data...
            }

            // Load machines
            foreach (var machineData in data.Machines)
            {
                if (machineData.Type == "ConveyorBelt")
                {
                    GameObject go = new GameObject("Conveyor");
                    var conveyor = go.AddComponent<ConveyorBelt>();
                    conveyor.PlaceOnTerrain(machineData.Position, machineData.Rotation);
                }
            }

            Debug.Log("Game loaded.");
        }

        // Call from UI button or on application quit
        private void OnApplicationPause(bool pause) { if (pause) SaveGame(); }
        private void OnApplicationQuit() { SaveGame(); }
    }
}