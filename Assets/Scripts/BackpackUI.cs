using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackpackUI : MonoBehaviour
{
    public Transform itemsContainer;
    public Transform itemTemplate;
    public List<OrganObject> organObjects;
    public List<OrganItem> organItemList;

    void Start()
    {
        itemTemplate.gameObject.SetActive(false);
        Hide();
    }



    public void CreateOrganItem(OrganObject organObject,ToolObjects tool)
    {
        Transform shopItemTransform = Instantiate(itemTemplate, itemsContainer);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();
        OrganItem organItem = shopItemTransform.GetComponent<OrganItem>();
        organItem.baseOrgan = organObject;
        organItem.toolUsed = tool;

        float shopItemHeight = 50f;
        shopItemRectTransform.anchoredPosition = new Vector2(itemTemplate.transform.localPosition.x, itemTemplate.transform.localPosition.y - shopItemHeight * (organItemList.Count-1));

        shopItemTransform.Find("OrganIcon").GetComponent<Image>().sprite = organObject.organSprite;
        shopItemTransform.Find("OrganName").GetComponent<TextMeshProUGUI>().SetText(organObject.organName);
        shopItemTransform.Find("OrganLife").GetComponent<TextMeshProUGUI>().SetText("Preserve Time: " + organItem.lifeLeft.ToString());
        shopItemTransform.Find("OrganPrice").GetComponent<TextMeshProUGUI>().SetText("Organ Price: " + organItem.priceAppliedTool.ToString());
        shopItemTransform.Find("ToolUsed").GetComponent<TextMeshProUGUI>().SetText("Tool Used: " + tool.toolName);

        shopItemTransform.Find("Drop").GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(shopItemTransform.gameObject, 1);
        });

        if (organItem.lifeLeft < 0)
        {
            Destroy(shopItemTransform.gameObject, 1);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}