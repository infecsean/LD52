using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Custom/Organ Object")]
public class OrganObject : ScriptableObject
{
    public Sprite organSprite;
    public string organName;
    public float preserveTime;
    public int marketPrice;
    public float successRate;
}
