using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinPopIn : MonoBehaviour
{
    public SpriteMask spritemask;
    public float fadeTime = 0.25f;

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
        if (onScreen)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, 1), fadeTime);
            transform.localScale = Vector3.Lerp(transform.localScale, startScale, fadeTime / 2);
            spriteRenderer.sortingLayerName = "Default";
        }
        else
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, 0), fadeTime);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), fadeTime / 2);
            spriteRenderer.sortingLayerName = "BehindBG";
        }
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
