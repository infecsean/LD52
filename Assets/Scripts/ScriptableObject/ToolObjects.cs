using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Tool Object")]
public class ToolObjects : ScriptableObject
{
    public Sprite toolSprite;
    public string toolName;
    public bool owned;
    public float sharpness;
    public float quality;
    public float ergonomics;
    public int price;
}
