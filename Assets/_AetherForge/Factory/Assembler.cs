using UnityEngine;
using System.Collections.Generic;
using AetherForge.Kits;

namespace AetherForge.Factory
{
    /// <summary>
    /// Assembler machine: consumes input items, produces output based on recipe.
    /// Links to KitDefinition for modular recipes.
    /// DOTS-ready for parallel crafting.
    /// </summary>
    public class Assembler : MonoBehaviour
    {
        [Header("Assembler Settings")]
        public KitDefinition Recipe; // Assign a KitDefinition SO with inputs/outputs
        public float CraftTime = 2f;

        private float timer = 0f;
        private List<Transform> inputBuffer = new List<Transform>();
        private bool isCrafting = false;

        private void Update()
        {
            if (Recipe == null || isCrafting) return;

            // Simple demo: check if we have "enough" items (expand with real inventory later)
            if (inputBuffer.Count >= 2) // e.g., 2 iron -> 1 plate
            {
                isCrafting = true;
                timer = CraftTime;
            }

            if (isCrafting)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    // Consume inputs
                    foreach (var item in inputBuffer) Destroy(item.gameObject);
                    inputBuffer.Clear();

                    // Produce output (demo: spawn gear)
                    GameObject output = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    output.transform.position = transform.position + transform.forward * 1.5f;
                    output.name = "CraftedItem";
                    output.tag = "Item";

                    // In real game: use Recipe.OutputPrefab or Item type
                    isCrafting = false;
                }
            }
        }

        public void AddInput(Transform item)
        {
            inputBuffer.Add(item);
            item.SetParent(transform);
            item.localPosition = Vector3.zero + Random.insideUnitSphere * 0.2f;
        }

        public void PlaceOnTerrain(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}