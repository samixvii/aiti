using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternObject : MonoBehaviour
{

    public Light light;
    public SpriteRenderer spriteRenderer;

    public void BuildFor(Lantern lantern)
    {
        light.color = lantern.color;
        spriteRenderer.sprite = lantern.symbol;
    }

}
