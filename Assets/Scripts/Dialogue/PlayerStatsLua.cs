using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PlayerStatsLua : MonoBehaviour
{
    public PlayerStats playerStat;


    public void SetPhone()
    {
        playerStat.canPhone = true;
    }
    public void SetBackpack()
    {
        playerStat.canBackpack = true;
    }
    public void SetTool()
    {
        playerStat.canTool = true;
    }

    public void StatCashOut()
    {
        playerStat.StatCashOut();
    }

    public bool TryBuy(double price)
    {
        return playerStat.SetBackpackSize((float)price);
    }

    void OnEnable()
    {
        Lua.RegisterFunction("SetPhone", this, SymbolExtensions.GetMethodInfo(() => SetPhone()));
        Lua.RegisterFunction("SetBackpack", this, SymbolExtensions.GetMethodInfo(() => SetBackpack()));
        Lua.RegisterFunction("SetTool", this, SymbolExtensions.GetMethodInfo(() => SetTool()));
        Lua.RegisterFunction("StatCashOut", this, SymbolExtensions.GetMethodInfo(() => StatCashOut()));
        Lua.RegisterFunction("TryBuy", this, SymbolExtensions.GetMethodInfo(() => TryBuy((double)0)));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("SetPhone");
        Lua.UnregisterFunction("SetBackpack");
        Lua.UnregisterFunction("SetTool");
        Lua.UnregisterFunction("StatCashOut");
        Lua.UnregisterFunction("TryBuy");
        
    }
}
