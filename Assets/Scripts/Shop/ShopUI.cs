using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUI : MonoBehaviour
{
    public Transform itemsContainer;
    public Transform itemTemplate;
    public List<ToolObjects> toolObjects;

    private IPurchaseItem customer;

    void Start()
    {
        foreach (ToolObjects obj in toolObjects)
        {
            CreateItemButton(obj, toolObjects.IndexOf(obj));
        }
        itemTemplate.gameObject.SetActive(false);
        Hide();

    }


    private void CreateItemButton(ToolObjects toolObject, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(itemTemplate, itemsContainer);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();


        float shopItemHeight = 70f;
        shopItemRectTransform.anchoredPosition = new Vector2(itemTemplate.transform.localPosition.x, itemTemplate.transform.localPosition.y - shopItemHeight * positionIndex);

        shopItemTransform.Find("ToolIcon").GetComponent<Image>().sprite = toolObject.toolSprite;
        shopItemTransform.Find("ToolName").GetComponent<TextMeshProUGUI>().SetText(toolObject.toolName);
        shopItemTransform.Find("ToolSharpness").GetComponent<TextMeshProUGUI>().SetText(toolObject.sharpness.ToString());
        shopItemTransform.Find("ToolQuality").GetComponent<TextMeshProUGUI>().SetText(toolObject.quality.ToString());
        shopItemTransform.Find("ToolErgo").GetComponent<TextMeshProUGUI>().SetText(toolObject.ergonomics.ToString());
        shopItemTransform.Find("ToolPrice").GetComponent<TextMeshProUGUI>().SetText(toolObject.price.ToString());

        shopItemTransform.Find("BuyButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            TryBuyUpgrade(toolObject);
        });
    }

    private void TryBuyUpgrade(ToolObjects toolObject)
    {
        customer.PurchaseItem(toolObject);
    }

    public void Show(IPurchaseItem customer)
    {
        this.customer = customer;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
