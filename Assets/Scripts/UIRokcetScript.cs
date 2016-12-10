using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRokcetScript : MonoBehaviour
{

    // Use this for initialization
    public GameObject nave;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = ((int)nave.GetComponent<MoveShip>().currentRockets).ToString() + "/" + ((int)nave.GetComponent<MoveShip>().maxRockets).ToString();
    }
    // Update is called once per frame

}