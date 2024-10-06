using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        bgRenderer = background.GetComponent<Renderer>();
        bgBounds = bgRenderer.bounds;
        bgSize = bgBounds.extents;

        for (int i = 0; i < startCount; i++)
        {
            Vector3 position = new Vector3(Random.Range(-bgSize.x, bgSize.x), Random.Range(-bgSize.y, bgSize.y), 0);
            Instantiate(pumpkin, position, Quaternion.identity);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(bgBounds.center, bgBounds.extents * 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
