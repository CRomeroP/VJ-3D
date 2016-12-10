using UnityEngine;
using System.Collections;

public class BaldosaCoheteScript : MonoBehaviour
{

    private float time;
    private GameObject luz, cohete;

    void Start()
    {
        time = 0f;
        luz = gameObject.transform.FindChild("luzCoheteBase").gameObject;
        cohete = gameObject.transform.FindChild("Missil_01").gameObject;

    }

    private void toogleActivate(bool toogle)
    {
        luz.SetActive(toogle);
        cohete.SetActive(toogle);
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
