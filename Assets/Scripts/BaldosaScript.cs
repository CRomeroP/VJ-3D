using UnityEngine;
using System.Collections;

public class BaldosaScript : MonoBehaviour {

    private float time;
    private ParticleSystem luz;
    void Start()
    {
        time = 0f;
        luz = transform.FindChild("animacion").GetComponent<ParticleSystem>();
    }
    public bool usar()
    {
        if (time <= 0f)
        {
            luz.Stop();
            time = 4f;
            return true;
        }
        return false;
    }
	void Update () {
        time -= Time.deltaTime;
        if (!luz.isPlaying && time <= 0f) luz.Play();
	}
}
