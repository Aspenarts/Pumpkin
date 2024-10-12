using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopIn : MonoBehaviour
{
    public SpriteMask spritemask;
    public int interpolationFramesCount = 5; // Number of frames to completely interpolate between the 2 positions
    
    int elapsedFrames = 0;
    SpriteRenderer spriteRenderer;
    bool onScreen = false;
    Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
    }

    void FixedUpdate()
    {
        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

        if (onScreen)
        {
            if (transform.localScale.x < startScale.x) {
                spriteRenderer.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), interpolationRatio);
                transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), startScale, interpolationRatio);
                spriteRenderer.sortingLayerName = "Default";
            }
        }
        else
        {
            if (transform.localScale.x > 0) {
                spriteRenderer.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), interpolationRatio);
                transform.localScale = Vector3.Lerp(startScale, new Vector3(0, 0, 0), interpolationRatio);
                spriteRenderer.sortingLayerName = "BehindBG";
            }
        }

        elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Mask") {
            onScreen = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Mask") {
            onScreen = false;
        }
    }
}
