using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using SpiCa;
using Canvas = SpiCa.Canvas;

namespace uWintab
{
    namespace SpiCa{
    public class DrawManager : MonoBehaviour {
        //ブラシや消しゴムを管理するクラス．
        //あとマウスで描くとき用のコードも格納．

        
        //
        [SerializeField] GameObject LineObjectPrefab;
        [SerializeField] GameObject TrailObjectPrefab;
        [SerializeField] private Dropdown layerdropdown;
        [SerializeField] private Dropdown tooldropdown;
        [SerializeField] GameObject Pointer;
        
        
        

        //現在描画中のLineObject;
        private Stroke CurrentStroke = null;
        //消しゴム適用
        private GameObject kesubrush = null;
        //選択適用
        private GameObject selectbrush = null;
        //レイヤーはデフォルトで1
        private int layernumber = 1;
        //消す範囲
        private float nearD = 100;
        private Vector3 premousePos;
        Tablet tablet_;
        public Slider hpSlider;

        private float timeElapsed=0;
        public float timeout = 0.05f;
        public float width = 1.0f;
        int kaisu = 0;
        public Vector3 penpoint;
        
        

    

        // Use this for initialization
        void Start () {
            
            tablet_ = FindObjectOfType<Tablet>();

            //ブラシをすべて親から出す
            if(Painter3DManager.Instance != null){
                if(Painter3DManager.Instance.AllCanvases != null){
                    foreach(Canvas c in Painter3DManager.Instance.AllCanvases)
                    {
                        foreach(Stroke element in c.m_Strokes)
                        {             
                            element.transform.parent=null;
                        }
                    }
                }
            }

            

        }
        //レイヤードロップダウン用メソッド
        public void layerChange() {
            if (layerdropdown.value == 0)
            {
                layernumber = 1;
            }
        
            else if (layerdropdown.value == 1)
            {
                layernumber = 2;
            }
        }   
        

        

        void Update () {
            penpoint =  Pointer.GetComponent<UIpen>().penposition;
            //ツールドロップダウンはここで対応
            if (tooldropdown.value == 0)
                brushmanger4();
            else if (tooldropdown.value == 1)
                erasemanger();
            else if (tooldropdown.value == 2)
                selectmanger();

           
            
        }
        //消しゴム
        private void erasemanger() {
            
             if (Input.GetMouseButton (0)) 
             {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)) {
                    
                    kesubrush = searchTag(hit.point, "brush");

                    
                    
                } 
             }
             else if (!Input.GetMouseButton (0))//人差し指のトリガーを離したとき
            {
                if(kesubrush != null)
                {
                    if(nearD < 2)
                    {
                        int Index = Painter3DManager.Instance.ActiveCanvas.m_Strokes.FindIndex(s => s.name == kesubrush.name);
                        Painter3DManager.Instance.ActiveCanvas.RemoveAndDestroyStroke( Index );
                         //Undo.DestroyObjectImmediate(kesubrush);
                    }

                    kesubrush = null;
                    nearD = 100;
                }
                    
                
                
            }

        }
        //選択
        private void selectmanger() {
            
             if (Input.GetMouseButtonDown (0)) 
             {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)) {
                    
                    selectbrush = searchTag(hit.point, "brush");
                    
                    
                    premousePos = Input.mousePosition;
                    if(nearD < 2)
                     MoveLine(selectbrush, Input.mousePosition);
                     
                    selectbrush = null;
                   
                    nearD = 100;
                    
                } 
             }


        }
        //移動
        private void MoveLine(GameObject selectbrush, Vector3 mousepos) {

            //mousepos.z=0;
            //premousePos.z=0;
            LineRenderer line = selectbrush.GetComponent<LineRenderer>();
        
            Vector3 diff = mousepos - premousePos;
            Debug.Log(diff);
            if (Input.GetMouseButton(0))
            {
                
                var positions = new Vector3[line.positionCount];
                int cnt = line.GetPositions(positions);
                Debug.Log(cnt);
        
                for (int i = 0; i < positions.Length; i++)
                {
                    positions[i] += diff * Time.deltaTime;
                }
                line.SetPositions(positions);
            }
            premousePos = mousepos;
            return;


        }   
        


        
        private void brushmanger4() { //タブレット，LineRendererによる描画
            
            if (tablet_.pressure!=0) 
            {
                
                
                
                Ray ray = Camera.main.ScreenPointToRay (penpoint);
                
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)&& hit.collider.gameObject.layer == 8 &&hit.collider.gameObject.GetComponent<Canvas>() == Painter3DManager.Instance.ActiveCanvas) {
                    if(CurrentStroke == null)
                    {
                    //PrefabからLineObjectを生成
                    GameObject CurrentLineObject = Instantiate(LineObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    CurrentStroke = CurrentLineObject.AddComponent<Stroke>();
                    CurrentStroke.Addvalue(layerdropdown.value);
                    
                    Painter3DManager.Instance.ActiveCanvas.AddStroke(CurrentStroke);
                    //CurrentLineObject.transform.parent = hit.collider.gameObject.transform;
                   
                    
                    
                    }
                    
                    
                } 
            }
            if (tablet_.pressure==0)
            {
                if(CurrentStroke != null)
                {
                    //アンドゥできるようにしておく
                    Undo.IncrementCurrentGroup();
                    Undo.RegisterCreatedObjectUndo (CurrentStroke.LineObject, "line");
                    
                     //描き終わったらuse world spaceを切る
                    //CurrentStroke.LineObject.GetComponent<LineRenderer>().useWorldSpace = false;
                    
                    //現在描画中の線があったらnullにして次の線を描けるようにする。
                    CurrentStroke = null;

                   
                }
                    
                
                
            }
        }
        //ブラシ幅のスライダ用
        public void Widthchange()
        {
 
           width=hpSlider.value;
 
        }
        //スライダに現在の太さを返す
        public float GetWidth(){
            return width;
        }

        //消しゴムが近くのブラシ探す用
        GameObject searchTag(Vector3 nowposition, string tagName){
        float tmpDis = 0;           //距離用一時変数
        float nearDis = 0;          //最も近いオブジェクトの距離
        //string nearObjName = "";    //オブジェクト名称
        GameObject targetObj = null; //オブジェクト

        //タグ指定されたオブジェクトを配列で取得する
        foreach (GameObject obj in  GameObject.FindGameObjectsWithTag(tagName)){
            
            LineRenderer line = obj.GetComponent<LineRenderer>();
            //TrailRenderer line = obj.GetComponent<TrailRenderer>();
            
            var positions = new Vector3[line.positionCount];
            int cnt = line.GetPositions(positions);
            foreach (Vector3 objpos in  positions){
                if(obj.layer == layerdropdown.value+9){
                //自身とブラシの距離を取得
                    tmpDis = Vector3.Distance(objpos,nowposition);
                    

                    //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                    //一時変数に距離を格納
                    if (nearDis == 0 || nearDis > tmpDis){
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    targetObj = obj;
                    }
                }
            }

        }
        //最も近かったオブジェクトを返す
        //return GameObject.Find(nearObjName);
        nearD = nearDis;
        return targetObj;
        
    }
    }
}
}