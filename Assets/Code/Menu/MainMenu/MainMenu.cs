using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject btnMission;
    public GameObject btnSurival;
    public GameObject btnTank;
    public GameObject btnOption;
    public GameObject btnHelp;
    public GameObject btnQuit;

    public GameObject optionMenu;

    void Awake()
    {
        GameController.Instance.loadGame();
        optionMenu.GetComponent<OptionMenuController>().SetVolumeMusic(GameController.Instance.getVolumeMusic());
        optionMenu.GetComponent<OptionMenuController>().SetVolumeSFX(GameController.Instance.getVolumeSFX());
    }
    public void HideButton()
    {
        btnMission.SetActive(false);
        btnSurival.SetActive(false);
        btnTank.SetActive(false);
        btnOption.SetActive(false);
        btnHelp.SetActive(false);
        btnQuit.SetActive(false);
    }

    public void ShowButton()
    {
        btnMission.SetActive(true);
        btnSurival.SetActive(true);
        btnTank.SetActive(true);
        btnOption.SetActive(true);
        btnHelp.SetActive(true);
        btnQuit.SetActive(true);
    }
}
