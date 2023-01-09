using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganItem : MonoBehaviour
{
    public OrganObject baseOrgan;
    public float lifeLeft = 9999;
    public ToolObjects toolUsed;
    public float priceAppliedTool;

    private void OnEnable()
    {
        lifeLeft = baseOrgan.preserveTime;
    }
    void Update()
    {
        lifeLeft -= Time.deltaTime;
    }
}
