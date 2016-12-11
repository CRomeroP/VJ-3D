using UnityEngine;
using System.Collections;

public class ConfiguradorPartida : MonoBehaviour {


    public void pickCircuito(int idCircuito)
    {
        PlayerPrefs.SetInt("circuito", idCircuito);
    }
    public void pickNave(int idNave)
    {
        PlayerPrefs.SetInt("nave", idNave);
    }
	
	
}
