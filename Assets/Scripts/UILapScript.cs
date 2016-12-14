using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UILapScript : MonoBehaviour
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
        int content = Mathf.Min(nave.GetComponent<MoveShip>().currentLap(), 3);
        text.text = content + "/" + ((int)nave.GetComponent<MoveShip>().laps).ToString();
    }
    // Update is called once per frame
}
