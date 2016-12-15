using UnityEngine;
using System.Collections;

public class BaldosaTurboScript : MonoBehaviour {
    private float time;
    private GameObject luz, seta;

    void Start()
    {
        time = 0f;
        luz = gameObject.transform.FindChild("luzCoheteBase").gameObject;
        seta = gameObject.transform.FindChild("Mushrooms").gameObject;

    }

    private void toogleActivate(bool toogle)
    {
        luz.SetActive(toogle);
        seta.SetActive(toogle);
    }
    public bool usar()
    {
        if (time <= 0f)
        {

            time = 4f;
            toogleActivate(false);
            return true;
        }
        return false;
    }
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0f) toogleActivate(true);
    }
}

