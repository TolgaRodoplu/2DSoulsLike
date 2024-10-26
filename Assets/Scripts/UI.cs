using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{ 
    [SerializeField] GameObject mainInv;
    [SerializeField] GameObject interactUI;
    [SerializeField] TextMeshProUGUI interactText;
    [SerializeField] GameObject teleportMenu;
    [SerializeField] Transform teleportList;
    [SerializeField] GameObject teleportButtonPrefab;
    [SerializeField] GameObject gameSavedText;
    [SerializeField] Image healthBar;
    [SerializeField] GameObject deathScreen;
    float gameSavedTimer = 2f;

    
    private void Start()
    {
        EventSystem.instance.toggleInventory += ToggleInv;
        EventSystem.instance.toggleInteractText += ToggleInteractView;
        EventSystem.instance.changeInteractText += ChangeInteractText;
        EventSystem.instance.gameSaved += GameSaved;
        EventSystem.instance.openTeleportMenu += OpenTeleportMenu;
        EventSystem.instance.editHealthUI += EditHealthUI;
        EventSystem.instance.death += ActivateDeath;
        EventSystem.instance.EditHealth(0);
    }

    void ActivateDeath()
    {
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(3f);
        deathScreen.SetActive(true);
        yield return new WaitForSeconds(3f);
        EventSystem.instance.LoadScane(DataPersistenceManager.instance.GetData().litBonfires[DataPersistenceManager.instance.GetData().lastBonfire]);
    }


    void EditHealthUI(object sender, float leftHealth)
    {
        healthBar.fillAmount = leftHealth;
    }


    void ToggleInv(object sender, bool active) 
    {
        if (mainInv != null) 
            mainInv.SetActive(active);

        EventSystem.instance.SetPlayer(!active);
        
    }

    void GameSaved()
    {
        StartCoroutine(GameSavedRoutine());
    }

    private IEnumerator GameSavedRoutine()
    {
        gameSavedText.SetActive(true);
        yield return new WaitForSeconds(gameSavedTimer);
        gameSavedText.SetActive(false);
    }

    void ToggleInteractView(object sender, bool active)
    {
        interactUI.SetActive(active);
    }

    void ChangeInteractText(object sender, string text)
    {
        interactText.text = "Press F to " + text;
    }

    

    void OpenTeleportMenu()
    {
        EventSystem.instance.SetPlayer(false);
        GameData data = DataPersistenceManager.instance.GetData();

        if (data != null) 
        {
            teleportMenu.SetActive(true);

            for (int i = 0; i < teleportList.childCount; i++)
                Destroy(teleportList.GetChild(i).gameObject);

            foreach (var item in data.litBonfireNames)
            {
                string id = item.Key;
                string name = item.Value;
                int scene;
                data.litBonfires.TryGetValue(item.Key, out scene);
                Vector3 pos;
                data.litBonfireLocations.TryGetValue(item.Key, out pos);
                GameObject newButton = Instantiate(teleportButtonPrefab, teleportList);
                newButton.GetComponent<TravelButton>().TravelButtonInit(item.Key,item.Value, scene, pos);
            }
        }
    }

    public void CloseTeleportMenu()
    {
        EventSystem.instance.SetPlayer(true);
        teleportMenu.SetActive(false);
    }

    
}
