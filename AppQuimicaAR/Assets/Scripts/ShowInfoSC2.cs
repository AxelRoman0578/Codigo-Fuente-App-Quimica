using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInfoSC2 : MonoBehaviour
{
    public GameObject moleculaObject; 
    public GameObject reproduceoObject; 
    public GameObject btn_options;
    public GameObject panelInfo;

    public void ShowPanelInfo_Sc2()
    {
        moleculaObject.SetActive(false);
        reproduceoObject.SetActive(false);
        btn_options.SetActive(false);
        panelInfo.SetActive(true);
    }

    public void QuitPanelInfo_Sc2()
    {
        moleculaObject.SetActive(true);
        reproduceoObject.SetActive(true);
        btn_options.SetActive(true);
        panelInfo.SetActive(false);
    }
}