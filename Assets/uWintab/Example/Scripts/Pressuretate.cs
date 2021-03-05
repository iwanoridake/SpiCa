using UnityEngine;

namespace uWintab
{

public class Pressuretate : MonoBehaviour 
{
    [SerializeField]
    Transform pen;

    Tablet tablet_;

    void Start()
    {
        tablet_ = FindObjectOfType<Tablet>();
    }

    void LateUpdate()
    {
        transform.localPosition = pen.localPosition;

        var pressure = tablet_.pressure;
        print(pressure);
        transform.localScale = new Vector3(pressure, pressure, 1f);
    }
}

}