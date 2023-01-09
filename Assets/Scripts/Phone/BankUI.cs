using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankUI : MonoBehaviour
{
    public Transform itemsContainer;
    public Transform itemTemplate;
    public List<TransactionObject> transactionObjects;

    public Color positiveColor;
    public Color negativeColor;

    private bool isExpanded = false;
    void Start()
    {
        foreach (TransactionObject obj in transactionObjects)
        {
            CreateItemButton(obj, transactionObjects.IndexOf(obj));
        }
        itemTemplate.gameObject.SetActive(false);
        Hide();

    }


    private void CreateItemButton(TransactionObject transactionObject, int positionIndex)
    {
        Transform transactionItemTransform = Instantiate(itemTemplate, itemsContainer);
        RectTransform transItemRectTransform = transactionItemTransform.GetComponent<RectTransform>();


        float shopItemHeight = 70f;
        transItemRectTransform.anchoredPosition = new Vector2(itemTemplate.transform.localPosition.x, itemTemplate.transform.localPosition.y + shopItemHeight * positionIndex);


        transactionItemTransform.Find("TransactionID").GetComponent<TextMeshProUGUI>().SetText("Transaction " + transactionObject.transactionID.ToString());
        transactionItemTransform.Find("MoneyEarned").GetComponent<TextMeshProUGUI>().SetText("Money Earned: " + transactionObject.TotalEarnings().ToString());
        transactionItemTransform.Find("MoneyEarnedColor").GetComponent<Image>().color = (transactionObject.TotalEarnings() > 0) ? positiveColor : negativeColor; ;
        transactionItemTransform.Find("Expand").gameObject.SetActive(false);

        transactionItemTransform.GetComponent<Button>().onClick.AddListener(() =>
        {
            Expand(transactionItemTransform, transactionObject);
        });
    }

    private void Expand(Transform item, TransactionObject transactionObject)
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

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
