using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpiCa;

namespace SpiCa{
public class BrushInCanvas : MonoBehaviour
{
    [SerializeField] GameObject listTogglePrefab;
    void MyOnValueChanged(Toggle t, Canvas c){
        foreach(Stroke s in c.Strokes){
            s.LineObject.SetActive(t.isOn);
        }

    }


    void Start () {
    Transform list = this.gameObject.transform;
    if(Painter3DManager.Instance != null)
        {
            foreach(Canvas element in Painter3DManager.Instance.AllCanvases){
                //プレハブからボタンを生成
                GameObject listToggle = Instantiate(listTogglePrefab) as GameObject;
                //Vertical Layout Group の子にする
                listToggle.transform.SetParent(list,false);
                listToggle.transform.FindChild("Label").GetComponent<Text>().text = element.gameObject.name;
                listToggle.SetActive(true);
                listToggle.GetComponent<Toggle>().isOn = true;

                //以下、追加---------
                //引数に何番目のボタンかを渡す
                listToggle.GetComponent<Toggle>().onValueChanged.AddListener(delegate {
                MyOnValueChanged(listToggle.GetComponent<Toggle>(),element);
                });
            }
        }
    }
}
}

    
