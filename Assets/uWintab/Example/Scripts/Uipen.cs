using UnityEngine;

namespace uWintab
{

public class Uipen : MonoBehaviour 
{

    Tablet tablet_;

    void Start()
    {
        tablet_ = FindObjectOfType<Tablet>();
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position = new Vector3(tablet_.x*Screen.currentResolution.width-(Screen.currentResolution.width-Screen.width), (tablet_.y)*Screen.currentResolution.height,0);
    }

}

}