using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject uiPauseMenu;
    private GameObject mainMenu;
    private GameObject optionsMenu;

    private void Start()
    {
        mainMenu = uiPauseMenu.transform.Find("PauseMenu").gameObject;
        optionsMenu = uiPauseMenu.transform.Find("OptionsMenu").gameObject;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        uiPauseMenu.SetActive(!uiPauseMenu.activeSelf);
        Time.timeScale = gameIsPaused ? 1f : 0f;
        gameIsPaused = !gameIsPaused;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ReturnToMainPauseMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void GoToOptionsPauseMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
}
