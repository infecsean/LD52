using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolUI : MonoBehaviour
{
    public Transform itemsContainer;
    public Transform itemTemplate;
    public PlayerStats stats;
    public List<ToolObjects> toolsOwned;

    void Start()
    {
        itemTemplate.gameObject.SetActive(false);
        Hide();
    }



    public void CreateToolItem(ToolObjects tool)
    {
        Transform shopItemTransform = Instantiate(itemTemplate, itemsContainer);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 50f;
        shopItemRectTransform.anchoredPosition = new Vector2(itemTemplate.transform.localPosition.x, itemTemplate.transform.localPosition.y - shopItemHeight * (toolsOwned.Count - 1));

        shopItemTransform.Find("ToolIcon").GetComponent<Image>().sprite = tool.toolSprite;
        shopItemTransform.Find("ToolName").GetComponent<TextMeshProUGUI>().SetText(tool.toolName);
        shopItemTransform.Find("ToolSharpness").GetComponent<TextMeshProUGUI>().SetText("Sharpness: " + tool.sharpness.ToString());
        shopItemTransform.Find("ToolQuality").GetComponent<TextMeshProUGUI>().SetText("Quality: " + tool.quality.ToString());
        shopItemTransform.Find("ToolErgo").GetComponent<TextMeshProUGUI>().SetText("Ergonomics: " + tool.ergonomics.ToString());

        shopItemTransform.Find("Equip").GetComponent<Button>().onClick.AddListener(() =>
        {
            Equip(tool);
        });
    }

    private void Equip(ToolObjects tool)
    {
        stats.currentTool = tool;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
