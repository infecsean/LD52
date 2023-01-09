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

    void OnEnable()
    {
        Lua.RegisterFunction("SetPhone", this, SymbolExtensions.GetMethodInfo(() => SetPhone()));
        Lua.RegisterFunction("SetBackpack", this, SymbolExtensions.GetMethodInfo(() => SetBackpack()));
        Lua.RegisterFunction("SetTool", this, SymbolExtensions.GetMethodInfo(() => SetTool()));
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("SetPhone");
        Lua.UnregisterFunction("SetBackpack");
        Lua.UnregisterFunction("SetTool");
        
    }
}
