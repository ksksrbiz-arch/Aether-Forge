using UnityEngine;
using System.Collections.Generic;

namespace AetherForge.Factory
{
    /// <summary>
    /// Inserter arm: pulls items from input (conveyor/machine) and pushes to output.
    /// Place between machines for automation chains.
    /// DOTS-ready for high item throughput.
    /// </summary>
    public class Inserter : MonoBehaviour
    {
        [Header("Inserter Settings")]
        public float Speed = 1.5f;
        public Transform InputPosition;   // Where it picks up
        public Transform OutputPosition;  // Where it drops off

        private Transform heldItem;
        private float cooldown = 0f;

        private void Update()
        {
            if (cooldown > 0) { cooldown -= Time.deltaTime; return; }

            if (heldItem == null)
            {
                // Try to pull from input area (simple overlap check for demo)
                Collider[] hits = Physics.OverlapSphere(InputPosition.position, 0.5f);
                foreach (var hit in hits)
                {
                    if (hit.CompareTag("Item") && hit.transform != heldItem)
                    {
                        heldItem = hit.transform;
                        heldItem.SetParent(transform);
                        heldItem.localPosition = Vector3.zero;
                        cooldown = 0.5f;
                        break;
                    }
                }
            }
            else
            {
                // Move to output and drop
                heldItem.position = Vector3.MoveTowards(heldItem.position, OutputPosition.position, Speed * Time.deltaTime);

                if (Vector3.Distance(heldItem.position, OutputPosition.position) < 0.2f)
                {
                    // Drop item (in real game: add to conveyor or machine input)
                    heldItem.SetParent(null);
                    if (OutputPosition.GetComponent<ConveyorBelt>() != null)
                        OutputPosition.GetComponent<ConveyorBelt>().AddItem(heldItem);

                    heldItem = null;
                    cooldown = 0.3f;
                }
            }
        }

        public void PlaceOnTerrain(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
        }
    }
}