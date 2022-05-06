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
            if(Painter3DManager.Instance != null)
            {
                foreach(Canvas element in Painter3DManager.Instance.AllCanvases)
                {
                    Canvasdropdown.options.Add(new Dropdown.OptionData { text = element.gameObject.name });
                
                }
            Canvasdropdown.value=Painter3DManager.Instance.m_ActiveCanvasIndex;
            }

        }
       
        //キャンバスドロップダウン用メソッド

        public void CanvasChanger() {
            if(Painter3DManager.Instance != null){
                Painter3DManager.Instance.ActiveCanvas.ChangeCanvasFalse();
            }
            Painter3DManager.Instance.ActiveCanvas = Painter3DManager.Instance.m_AllCanvases[Canvasdropdown.value];
            Painter3DManager.Instance.ActiveCanvas.ChangeCanvasTrue();

        
             Painter3DManager.Instance.m_ActiveCanvasIndex = Canvasdropdown.value;

        }  
}
}