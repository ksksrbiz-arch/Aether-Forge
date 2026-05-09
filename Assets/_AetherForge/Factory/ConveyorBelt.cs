using UnityEngine;
using System.Collections.Generic;

namespace AetherForge.Factory
{
    /// <summary>
    /// Basic conveyor belt machine.
    /// Can be placed on terrain. Moves items along forward direction.
    /// DOTS-ready for high-performance scaling (thousands of items).
    /// </summary>
    public class ConveyorBelt : MonoBehaviour
    {
        [Header("Conveyor Settings")]
        public float Speed = 2f;
        public Vector3 Direction = Vector3.forward;
        public float Length = 2f; // Visual length

        private List<Transform> itemsOnBelt = new List<Transform>();

        private void Update()
        {
            // Simple movement for demo (DOTS will replace for scale)
            for (int i = itemsOnBelt.Count - 1; i >= 0; i--)
            {
                if (itemsOnBelt[i] == null)
                {
                    itemsOnBelt.RemoveAt(i);
                    continue;
                }

                itemsOnBelt[i].position += Direction * Speed * Time.deltaTime;

                // Remove if off the end
                if (Vector3.Distance(itemsOnBelt[i].position, transform.position) > Length * 1.5f)
                {
                    // In real game: pass to next machine or despawn
                    Destroy(itemsOnBelt[i].gameObject);
                    itemsOnBelt.RemoveAt(i);
                }
            }
        }

        public void AddItem(Transform item)
        {
            if (!itemsOnBelt.Contains(item))
            {
                itemsOnBelt.Add(item);
                item.SetParent(transform); // Optional: parent for movement
            }
        }

        // Called by placement system (integrate with WorldManager later)
        public void PlaceOnTerrain(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            Direction = transform.forward;
        }
    }
}