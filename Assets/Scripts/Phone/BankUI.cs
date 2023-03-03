using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankUI : MonoBehaviour
{
    public Transform itemsContainer;
    public Transform itemTemplate;

    public Color positiveColor;
    public Color negativeColor;

    public PlayerStats stats;

    public List<transaction> transactions;

    private bool isExpanded = false;

    public struct transaction
    {
        public int transactionID;
        public float sellPrice;
        public float organCount;
        public float taxMultiplier;
        public int smuggleFee;
        public float bankFee;
    }

    void Start()
    {
        transactions = new List<transaction>(100);
        itemTemplate.gameObject.SetActive(false);
        Hide();
    }


    // Creates a transaction gameobject below the previous transaction
    private void CreateItemButton(transaction transaction, int positionIndex)
    {
        Transform transactionItemTransform = Instantiate(itemTemplate, itemsContainer);
        RectTransform transItemRectTransform = transactionItemTransform.GetComponent<RectTransform>();


        float shopItemHeight = 70f;
        transItemRectTransform.anchoredPosition = new Vector2(itemTemplate.transform.localPosition.x, itemTemplate.transform.localPosition.y + shopItemHeight * positionIndex);


        transactionItemTransform.Find("TransactionID").GetComponent<TextMeshProUGUI>().SetText("Transaction " + transaction.transactionID.ToString());
        transactionItemTransform.Find("MoneyEarned").GetComponent<TextMeshProUGUI>().SetText("Money Earned: " + TotalEarning(transaction).ToString());
        transactionItemTransform.Find("MoneyEarnedColor").GetComponent<Image>().color = (TotalEarning(transaction) > 0) ? positiveColor : negativeColor; ;
        transactionItemTransform.Find("Expand").gameObject.SetActive(false);

        transactionItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Expand(transactionItemTransform);
        });
    }

    public float TotalEarning(transaction transaction)
    {
        float moneyEarned = 0;
        
        moneyEarned = transaction.sellPrice;
        
        //Debug.Log("Sum market price: " + moneyEarned);
        moneyEarned *= (1 - transaction.taxMultiplier);
        //Debug.Log("Money after tax: " + moneyEarned);
        moneyEarned *= 1 - transaction.bankFee;
        //Debug.Log("Money after bank: " + moneyEarned);
        moneyEarned -= transaction.smuggleFee * transaction.organCount;
        //Debug.Log("Money after smuggle: " + moneyEarned);
        return moneyEarned;
    }

    private void Expand(Transform item)
    {
        
        if (isExpanded)
        {
            isExpanded = false;
            item.Find("Background").GetComponent<RectTransform>().sizeDelta -= new Vector2(0, 1000);
            for (int i = 0; i < item.parent.childCount; i++)
            {
                item.parent.GetChild(i).gameObject.SetActive(true);
            }
            item.parent.GetChild(0).gameObject.SetActive(false);
            item.Find("Expand").gameObject.SetActive(false);
            Debug.Log("Collapsing");
            return;
        }
        
        // This needs to
        // - Enlarge Background size
        // - Disable all other transactions
        // - Display market price for all organs in this transaction, as well as changing the color to green
        // - Display tax fee, change color
        // - Display smuggling fee, change color
        // - Display Bank fee, change color
        // - Display net earning
        item.Find("Background").GetComponent<RectTransform>().sizeDelta += new Vector2(0, 1000);
        for (int i = 0; i < item.parent.childCount; i++)
        {
            if (item.parent.GetChild(i) != item)
            {
                item.parent.GetChild(i).gameObject.SetActive(false);
                //Debug.Log("different game object");
            }
            else
            {
                item.parent.GetChild(i).gameObject.SetActive(true);
                //Debug.Log("Same game object");
            }
        }
        item.Find("Expand").gameObject.SetActive(true);
        Debug.Log("Expanding");
        isExpanded = true;
    }

    public void CashOut(transaction transaction)
    {
        // When other scripts call this function on the bank page,
        // - Add a new transaction to the transaction list
        // - Create a new transaction
        // - Take in a transaction parameter
        // - Change the money on the player stats
        transaction.transactionID = transactions.Count + 1;
        CreateItemButton(transaction, transactions.Count + 1);
        stats.AddMoney(TotalEarning(transaction));
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
