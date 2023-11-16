using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UIPanel
{
    WinUI = 0,
    LoseUI = 1,
    SettingUI = 2,
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //public static UIManager instance
    // {
    //     get
    //     {
    //         if(instance == null)
    //         {
    //             instance = FindAnyObjectByType<UIManager>();
    //         }

    //         return instance;
    //     }
    // }

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Text coinText;

    [SerializeField] private List<GameObject> panels;

    public void SetCoin(int coin)
    {
        coinText.text = coin.ToString();
    }

    public void OpenPanel(UIPanel type)
    {
        //CloseAllPanel();
        int index = (int)type;
        if (index >= 0 && index < panels.Count)
        {
            panels[index].SetActive(true);
        }
    }

    public void OpenPanelWithIndex(int index)
    {
        if (index >= 0 && index < panels.Count)
        {
            panels[index].SetActive(true);
        }
    }
    public void ClosePanelWithIndex(int index)
    {
        if (index >= 0 && index < panels.Count)
        {
            panels[index].SetActive(false);
        }
    }

    public void ClosePanel(UIPanel type)
    {
        int index = (int)type;
        if (index >= 0 && index < panels.Count)
        {
            panels[index].SetActive(false);
        }
    }

    public void CloseAllPanel()
    {
        for(int i = 0; i < panels.Count; i++)
        {
            ClosePanel((UIPanel)i);
        }
    }
}
