using UnityEngine;

namespace uWintab
{

//板タブだとペン位置がよくわからんのでそれ用
public class UIpen : MonoBehaviour 
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