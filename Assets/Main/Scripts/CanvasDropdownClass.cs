using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
namespace SpiCa{
public class CanvasDropdownClass : MonoBehaviour
{
    [SerializeField]private Dropdown Canvasdropdown;
     void Start () {
            

            //こっからキャンバスドロップダウンの起動
        
            foreach(Canvas element in Painter3DManager.Instance.AllCanvases)
            {
                Canvasdropdown.options.Add(new Dropdown.OptionData { text = element.gameObject.name });
                
            }
            Canvasdropdown.value=Painter3DManager.Instance.m_ActiveCanvasIndex;

        }
       
        //キャンバスドロップダウン用メソッド

        public void CanvasChanger() {
            if(Painter3DManager.Instance.ActiveCanvas !=null){
                Painter3DManager.Instance.ActiveCanvas.gameObject.SetActive(false);
            }
            Painter3DManager.Instance.ActiveCanvas = Painter3DManager.Instance.m_AllCanvases[Canvasdropdown.value];
            Painter3DManager.Instance.ActiveCanvas.gameObject.SetActive(true);

        
             Painter3DManager.Instance.m_ActiveCanvasIndex = Canvasdropdown.value;

        }  
}
}