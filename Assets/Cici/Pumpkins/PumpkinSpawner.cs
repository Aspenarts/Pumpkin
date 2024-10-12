using System.Collections.Generic;
using UnityEngine;

public class PumpkinSpawner : MonoBehaviour
{
    public int startCount = 20;
    public GameObject background;
    public GameObject pumpkin;

    Renderer bgRenderer;
    Bounds bgBounds;
    Vector3 bgSize;

    // A list to hold the spawned pumpkins
    public List<GameObject> spawnedPumpkins = new List<GameObject>();

    void Start()
    {
        bgRenderer = background.GetComponent<Renderer>();
        bgBounds = bgRenderer.bounds;
        bgSize = bgBounds.extents;

        for (int i = 0; i < startCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-bgSize.x, bgSize.x), Random.Range(-bgSize.y, bgSize.y), 0);

            // Instantiate the pumpkin and add it to the list
            GameObject spawnedPumpkin = Instantiate(pumpkin, position, Quaternion.identity);
            spawnedPumpkins.Add(spawnedPumpkin); // Add to the list
        }

        // Find the AIController in the scene and pass the entire list of pumpkins
        AIController aiController = FindObjectOfType<AIController>();  // Find the AIController in the scene
        if (aiController != null && spawnedPumpkins.Count > 0)
        {
            aiController.SetPumpkinTargets(spawnedPumpkins); // Pass the full list of pumpkins
        }

    }
}
