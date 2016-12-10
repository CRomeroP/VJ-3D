using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIEnergyScript : MonoBehaviour
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
        text.text = ((int)nave.GetComponent<MoveShip>().energy).ToString();
    }
    // Update is called once per frame

}