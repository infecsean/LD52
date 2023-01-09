using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Custom/Test Transaction Object")]
public class TransactionObject : ScriptableObject
{
    public int transactionID;
    public List<OrganObject> organObjects;
    public float taxMultiplier;
    public int smuggleFee;
    public float bankFee;

    public float TotalEarnings()
    {
        float moneyEarned = 0;
        foreach (OrganObject organObject in organObjects) // sum of organs at market price
        {
            moneyEarned += organObject.marketPrice;
        }
        //Debug.Log("Sum market price: " + moneyEarned);
        moneyEarned *= (1 - taxMultiplier);
        //Debug.Log("Money after tax: " + moneyEarned);
        moneyEarned *= 1 - bankFee;
        //Debug.Log("Money after bank: " + moneyEarned);
        moneyEarned -= smuggleFee * organObjects.Count;
        //Debug.Log("Money after smuggle: " + moneyEarned);
        return moneyEarned;
    }
}
