using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;

public class PlayerStats : MonoBehaviour, IPurchaseItem
{
    //public Transform Hud;
    public GameObject pauseCanvas;
    public GameObject phoneCanvas;
    public GameObject backpackCanvas;
    public GameObject toolCanvas;

    public GameObject pauseHud;
    public GameObject phoneHud;
    public GameObject backpackHud;
    public GameObject toolHud;

    // For beginning
    [ReadOnly] public bool canPhone = false;
    [ReadOnly] public bool canBackpack = false;
    [ReadOnly] public bool canTool = false;

    [ReadOnly] public float money = 0;
    [ReadOnly] public int backpackSize = 5;
    public List<ToolObjects> toolsOwned;
    [ReadOnly] public ToolObjects currentTool;
    [ReadOnly] public List<OrganObject> organsInventory;

    

    void Start()
    {
        pauseHud.SetActive(true);
        phoneHud.SetActive(false);
        backpackHud.SetActive(false);
        toolHud.SetActive(false);
        phoneCanvas.SetActive(false);
        currentTool = toolsOwned[0];
    }

    void Update()
    {
        CheckHUD();
        CheckInput();
    }

    void CheckHUD()
    {
        if (canPhone) { phoneHud.SetActive(true); }
        if (canBackpack) { backpackHud.SetActive(true); }
        if (canTool) { toolHud.SetActive(true); }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && canPhone)
        {
            phoneCanvas.SetActive(!phoneCanvas.activeSelf);
            backpackCanvas.SetActive(false);
            toolCanvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.B) && canBackpack)
        {
            backpackCanvas.SetActive(!backpackCanvas.activeSelf);
            toolCanvas.SetActive(false);
            phoneCanvas.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.T) && canTool)
        {
            toolCanvas.SetActive(!toolCanvas.activeSelf);
            phoneCanvas.SetActive(false);
            backpackCanvas.SetActive(false);
        }
    }

    public void PurchaseItem(ToolObjects toolObject)
    {
        if (toolObject.price < money && !toolObject.owned)
        {
            toolCanvas.GetComponent<ToolUI>().toolsOwned.Add(toolObject);
            toolObject.owned = true;
            currentTool = toolObject;
        }
        else
        {
            Debug.Log("Poverty moment");
        }
    }

    public void AddOrgan(OrganObject organObject)
    {
        backpackCanvas.GetComponent<BackpackUI>().CreateOrganItem(organObject, currentTool);
    }

    void AddMoney(float addedMoney)
    {
        money += addedMoney;
        phoneCanvas.transform.Find("BankPage").Find("MoneyNumber").GetComponent<TextMeshProUGUI>().SetText(money.ToString());
    }
}
