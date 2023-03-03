using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackpackUI : MonoBehaviour
{
    public Transform itemsContainer;
    public Transform itemTemplate;
    public List<GameObject> organList;

    void Start()
    {
        organList = new List<GameObject>(100);
        itemTemplate.gameObject.SetActive(false);
        Hide();
    }

    private void Update()
    {
        
        for (int i = 1; i < itemsContainer.childCount; i++)
        {
            Debug.Log(i);
            Debug.Log("Child Count: "+ itemsContainer.childCount + "Organ size: " + organList.Count + "Child name: " + itemsContainer.GetChild(i).name);
            itemsContainer.GetChild(i).Find("OrganLife").GetComponent<TextMeshProUGUI>().SetText("Preserve Time: " + organList[i-1].GetComponent<OrganItem>().lifeLeft.ToString());

        }
    }

    public void AddOrgan(GameObject obj)
    {
        //Debug.Log("Added object: " + obj.name + "The number of items in inventory is now: " + organList.Count);
        organList.Add(obj);

        Transform shopItemTransform = Instantiate(itemTemplate, itemsContainer);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 50f;
        shopItemRectTransform.anchoredPosition = new Vector2(itemTemplate.transform.localPosition.x, itemTemplate.transform.localPosition.y - shopItemHeight * (organList.Count - 1));

        shopItemTransform.Find("OrganIcon").GetComponent<Image>().sprite = obj.GetComponent<OrganItem>().baseOrgan.organSprite;
        shopItemTransform.Find("OrganName").GetComponent<TextMeshProUGUI>().SetText(obj.GetComponent<OrganItem>().baseOrgan.organName);
        shopItemTransform.Find("OrganLife").GetComponent<TextMeshProUGUI>().SetText("Preserve Time: " + obj.GetComponent<OrganItem>().lifeLeft.ToString());
        shopItemTransform.Find("OrganPrice").GetComponent<TextMeshProUGUI>().SetText("Organ Price: " + obj.GetComponent<OrganItem>().priceAppliedTool.ToString());
        shopItemTransform.Find("ToolUsed").GetComponent<TextMeshProUGUI>().SetText("Tool Used: " + obj.GetComponent<OrganItem>().toolUsed.toolName);

        shopItemTransform.Find("Drop").GetComponent<Button>().onClick.AddListener(() =>
        {
            Discard(shopItemTransform.gameObject, obj);
        });

        if (obj.GetComponent<OrganItem>().lifeLeft < 0)
        {
            Discard(shopItemTransform.gameObject, obj);
        }
    }

    public float CashOut()
    {
        float money = 0;

        for (int i = 1; i < itemsContainer.childCount; i++)
        {
            money += organList[i - 1].GetComponent<OrganItem>().priceAppliedTool;
            organList.RemoveAt(organList.IndexOf(organList[i-1]));

            Destroy(itemsContainer.GetChild(i).gameObject);
            Destroy(organList[i - 1]);
        }

        return money;
    }

    public float OrganCount()
    {
        return organList.Count;
    }

    public void Discard(GameObject obj, GameObject organ)
    {
        organList.RemoveAt(organList.IndexOf(organ));
        Destroy(obj, 0.1f);
        Destroy(organ, 0.1f);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}