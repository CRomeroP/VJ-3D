using UnityEngine;
using System.Collections;

public class InstanciadorNave : MonoBehaviour {

    // Use this for initialization
    public Object navePlayer;
	void Start () {
        Vector3 pos = new Vector3(5f, 8f, -150f);
        Vector3 rot = new Vector3(0f, 0f, 0f);
        GameObject nave = (GameObject)Instantiate(navePlayer, pos,Quaternion.Euler(rot));
        Debug.Log("Mapa: " + PlayerPrefs.GetInt("circuito") + ", Nave " + PlayerPrefs.GetInt("nave") + ", " + nave != null);
        GameObject uiIndicators = GameObject.Find("Canvas/MainMenuPanel");
        uiIndicators.GetComponentInChildren<UISpeedScript>().nave = nave;
        uiIndicators.GetComponentInChildren<UIEnergyScript>().nave = nave;
        uiIndicators.GetComponentInChildren<UIRokcetScript>().nave = nave;
        uiIndicators.SetActive(true);
    }
	
}
