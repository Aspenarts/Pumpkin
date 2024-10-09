using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinPopIn : MonoBehaviour
{
    public SpriteMask spritemask;
    public float fadeTime = 0.25f;

    SpriteRenderer spriteRenderer;
    bool onScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (onScreen)
        {
            //spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, 1), Mathf.PingPong(Time.fixedTime, 1));
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, 1), fadeTime);
        }
        else
        {
            //spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, 0), Mathf.PingPong(Time.fixedTime, 1));
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, new Color(1, 1, 1, 0), fadeTime);
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
