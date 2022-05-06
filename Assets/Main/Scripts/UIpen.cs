using UnityEngine;

namespace uWintab
{
namespace SpiCa{
//板タブだとペン位置がよくわからんのでそれ用
public class UIpen : MonoBehaviour 
{

    Tablet tablet_;
    public Vector3 penposition;

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
        //penposition = new Vector3(tablet_.x*Screen.currentResolution.width-(Screen.currentResolution.width-Screen.width), tablet_.y*Screen.currentResolution.height-(Screen.currentResolution.height-Screen.height),0);
        penposition = new Vector3(tablet_.x*Screen.currentResolution.width, (tablet_.y)*Screen.currentResolution.height,0);
        //液タブの時はこれで
        //penposition = new Vector3(tablet_.x*Screen.currentResolution.width-(Screen.currentResolution.width-Screen.width), (tablet_.y)*Screen.currentResolution.height,0);
        transform.position =penposition;
       
    }

   
}
}}