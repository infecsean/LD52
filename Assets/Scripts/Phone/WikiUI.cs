using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WikiUI : MonoBehaviour
{
    public Transform itemsContainer;
    public Transform itemTemplate;
    public List<OrganObject> organObjects;

    void Start()
    {
        foreach (OrganObject obj in organObjects)
        {
            CreateItemButton(obj, organObjects.IndexOf(obj));
        }
        itemTemplate.gameObject.SetActive(false);
        Hide();

    }


    private void CreateItemButton(OrganObject organObject, int positionIndex)
    {
        Transform shopItemTransform = Instantiate(itemTemplate, itemsContainer);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();


        float shopItemHeight = 50f;
        shopItemRectTransform.anchoredPosition = new Vector2(itemTemplate.transform.localPosition.x, itemTemplate.transform.localPosition.y - shopItemHeight * positionIndex);

        shopItemTransform.Find("WikiIcon").GetComponent<Image>().sprite = organObject.organSprite;
        shopItemTransform.Find("WikiName").GetComponent<TextMeshProUGUI>().SetText(organObject.organName);
        shopItemTransform.Find("WikiTime").GetComponent<TextMeshProUGUI>().SetText("Preserve Time: " + organObject.preserveTime.ToString());
        shopItemTransform.Find("WikiPrice").GetComponent<TextMeshProUGUI>().SetText("Market Price: " + organObject.marketPrice.ToString());
        shopItemTransform.Find("WikiSuccess").GetComponent<TextMeshProUGUI>().SetText("Success Rate: " + organObject.successRate.ToString());
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
