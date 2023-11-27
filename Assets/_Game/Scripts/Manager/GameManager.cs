using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    public List<GameObject> levels;
    [SerializeField] private Player player;
    [SerializeField] private Text levelText;
    [SerializeField] private new FollowingCamera camera;

    private GameObject map;
    private int curLevel = 0;

    private void Start()
    {
        OnInit();
    }


    public void OnInit()
    {
        LoadLevel(curLevel);
        //player.OnInit();
        //camera.OnInit();
        levelText.text = "Level " + (curLevel + 1).ToString();
        UIManager.instance.CloseAllPanel();
    }
    public void LoadLevel(int index)
    {
        EventManager.instance.EmitLoadLevelEvent();
        Destroy(map);
        map = Instantiate(levels[index]);
    }

    public void NextLevel()
    {
        EventManager.instance.EmitNextLevelEvent();

        //player.IncreaseCoin(50);

        curLevel++;
        if(curLevel <  levels.Count)
        {
            OnInit();
        }
    }

    public void OnWin()
    {
        EventManager.instance.EmitCompleteLevelEvent();

        SoundManager.instance.PlayClip(AudioType.FX_Win);
        //camera.Win();
        StartCoroutine(openWinPanel());
    }

    public IEnumerator openWinPanel()
    {
        yield return new WaitForSeconds(2f);
        UIManager.instance.OpenPanel(UIPanel.WinUI);
    }

    public void OnLose()
    {
        SoundManager.instance.PlayClip(AudioType.FX_Lose);
        UIManager.instance.OpenPanel(UIPanel.LoseUI);
    }
}
