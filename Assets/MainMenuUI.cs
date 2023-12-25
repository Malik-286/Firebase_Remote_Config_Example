using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{

    [SerializeField] string gameURL;

    [SerializeField] TextMeshProUGUI updatePanelMessageText;
     

    public GameObject updatePanel;

    public TextMeshProUGUI GetUpdatePanelMessageText()
    { 
        return  updatePanelMessageText; 
    }
    void Start()

    { 
     updatePanel.SetActive(false);
    }

    public void ActivateUpdatePanel()
    {
        updatePanel.SetActive(true);
     }

    public void UpdateGame()
    {
        Application.OpenURL(gameURL);
    }
}
