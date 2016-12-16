using UnityEngine;
using System.Collections;

public class InstanciadorNave : MonoBehaviour
{

    // Use this for initialization
    public Object navePlayer;
    void Start()
    {
        Vector3 pos = new Vector3(0f,0f,0f);
        if (PlayerPrefs.GetInt("circuito") == 2)
        {
            pos = new Vector3(-346.7f, 187.2f, 112.4f);
        }

        //Vector3 pos = new Vector3(5f, 8f, -150f);
        Vector3 rot = new Vector3(0f, 0f, 0f);
        GameObject nave = (GameObject)Instantiate(navePlayer, pos, Quaternion.Euler(rot));
        nave.GetComponent<MoveShip>().laps = 3;
        nave.GetComponent<MoveShip>().engineForceStep = 1500;
        nave.GetComponent<MoveShip>().checkPointsPerLap = 5;

        Debug.Log("Mapa: " + PlayerPrefs.GetInt("circuito") + ", Nave " + PlayerPrefs.GetInt("nave") + ", " + nave != null);
        GameObject uiIndicators = GameObject.Find("Canvas/MainMenuPanel");
        uiIndicators.GetComponentInChildren<UISpeedScript>().nave = nave;
        uiIndicators.GetComponentInChildren<UIEnergyScript>().nave = nave;
        uiIndicators.GetComponentInChildren<UIRokcetScript>().nave = nave;
        uiIndicators.GetComponentInChildren<UILapScript>().nave = nave;
        uiIndicators.SetActive(true);
    }

}
