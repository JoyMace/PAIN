using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenus : MonoBehaviour
{
    /// <summary>
    /// Goes back to the main menu
    /// </summary>
    public void GoBack()
    {
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.MenuButtonClick);
    }

    public void QuitGame()
    {
        AudioManager.Play(AudioClipName.MenuButtonClick);
        Application.Quit();
    }
}
