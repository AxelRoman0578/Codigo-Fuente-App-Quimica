using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shownfo : MonoBehaviour
{
    public GameObject niquelObject; 
    public GameObject hidrogenoObject; 
    public GameObject barraObject; 
    public GameObject panelInfo;
    public GameObject btn_options;

    public void ShowPanelInfo_Niquel()
    {
        niquelObject.SetActive(false);
        hidrogenoObject.SetActive(false);
        barraObject.SetActive(false);
        btn_options.SetActive(false);
        panelInfo.SetActive(true);
    }

    public void QuitPanelInfo_Niquel()
    {
        niquelObject.SetActive(true);
        hidrogenoObject.SetActive(true);
        barraObject.SetActive(true);
        btn_options.SetActive(true);
        panelInfo.SetActive(false);
    }
}
