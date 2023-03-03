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
    public bool canPhone = false;
    public bool canBackpack = false;
    public bool canTool = false;

    [ReadOnly] public float money = 0;
    [ReadOnly] public int backpackSize = 5;
    public List<ToolObjects> toolsOwned;
    [ReadOnly] public ToolObjects currentTool;
    [ReadOnly] public List<OrganObject> organsInventory;

    public bool isBackpackFull;

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

        backpackCanvas.transform.Find("BackpackSize").GetComponent<TextMeshProUGUI>().SetText(backpackCanvas.GetComponent<BackpackUI>().organList.Count.ToString() + "/" + backpackSize);

        if (backpackCanvas.GetComponent<BackpackUI>().organList.Count > backpackSize -1)
        {
            isBackpackFull = true;
        }
        else if (backpackCanvas.GetComponent<BackpackUI>().organList.Count < backpackSize)
        {
            isBackpackFull = false;
        }
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
            AddMoney(-toolObject.price);
        }
        else
        {
            Debug.Log("Poverty moment");
        }
    }

    public void AddOrgan(GameObject obj)
    {
        if (!isBackpackFull)
        {
            backpackCanvas.GetComponent<BackpackUI>().AddOrgan(obj);
        }
    }

    public void AddMoney(float addedMoney)
    {
        money += addedMoney;
        phoneCanvas.transform.Find("BankPage").Find("MoneyNumber").GetComponent<TextMeshProUGUI>().SetText(money.ToString());
    }

    public void StatCashOut()
    {
        float organCount = backpackCanvas.GetComponent<BackpackUI>().OrganCount();
        float totalPrice = backpackCanvas.GetComponent<BackpackUI>().CashOut();
        BankUI.transaction transaction = new BankUI.transaction();
        transaction.organCount = organCount;
        transaction.sellPrice = totalPrice;
        transaction.taxMultiplier = 0.7f;
        transaction.smuggleFee = 1000;
        transaction.bankFee = 0.4f;
        phoneCanvas.transform.Find("BankPage").GetComponent<BankUI>().CashOut(transaction);
    }

    public bool SetBackpackSize(float price)
    {
        bool enoughMoney = false;
        if (price < money)
        {
            enoughMoney = true;
        }
        else
        {
            enoughMoney = false;
        }

        if (price < 1001 && enoughMoney)
        {
            backpackSize = 10;
            AddMoney(-price);
        }
        else if (price < 5000 && enoughMoney)
        {
            backpackSize = 25;
            AddMoney(-price);
        }
        else if (price < 50000 && enoughMoney)
        {
            backpackSize = 50;
            AddMoney(-price);
        }
        else if (price < 100000 && enoughMoney)
        {
            backpackSize = 80;
            AddMoney(-price);
        }
        else
        {
            Debug.Log("haha, poor");
        }

        return enoughMoney;
    }
}
