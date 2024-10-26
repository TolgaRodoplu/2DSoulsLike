using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour, IDataPersistance
{
    [SerializeField] Button newGameButton, continueButton, exitButton;
    int scaneToContinue;

    private void Start()
    {
        if(!DataPersistenceManager.instance.HasGameData()) 
        {
            continueButton.gameObject.SetActive(false);
        }
        
    }
    public void NewGameButton()
    {
        DataPersistenceManager.instance.NewGame();

        SceneManager.LoadSceneAsync("Forest");
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(scaneToContinue);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueButton.interactable = false;
        exitButton.interactable = false;
    }

    public void LoadData(ref GameData data)
    {
        scaneToContinue = data.litBonfires[data.lastBonfire];
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
