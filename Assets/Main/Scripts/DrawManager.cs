using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
namespace uWintab
{
    public class DrawManager : MonoBehaviour {
        //ブラシや消しゴムを管理するクラス．
        //あとマウスで描くとき用のコードも格納．

        
        //
        [SerializeField] GameObject LineObjectPrefab;
        [SerializeField] GameObject TrailObjectPrefab;
        [SerializeField] private Dropdown layerdropdown;
        [SerializeField] private Dropdown tooldropdown;
        
        

        //現在描画中のLineObject;
        private GameObject CurrentLineObject = null;
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
        
        

    

        // Use this for initialization
        void Start () {
            
            tablet_ = FindObjectOfType<Tablet>();
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
                     Undo.DestroyObjectImmediate(kesubrush);
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
        



        private void brushmanger() {//マウス，LineRendererによる描画
            if (Input.GetMouseButton (0)) 
            {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)&& hit.collider.gameObject.layer == 8) {
                    
                    if(CurrentLineObject == null)
                    {
                    //PrefabからLineObjectを生成
                    CurrentLineObject = Instantiate(LineObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    CurrentLineObject.layer = 9 + layerdropdown.value;
                    Undo.RegisterCreatedObjectUndo (CurrentLineObject, "line");
                    }
                    //ゲームオブジェクトからLineRendererコンポーネントを取得
                    LineRenderer render = CurrentLineObject.GetComponent<LineRenderer>();

                    //LineRendererからPositionsのサイズを取得
                    int NextPositionIndex = render.positionCount;

                    //LineRendererのPositionsのサイズを増やす
                    render.positionCount = NextPositionIndex + 1;

                    //LineRendererのPositionsに現在のコントローラーの位置情報を追加
                    render.SetPosition(NextPositionIndex, hit.point);
                } 
            }
            else if (!Input.GetMouseButton (0))//人差し指のトリガーを離したとき
            {
                if(CurrentLineObject != null)
                {
                    //現在描画中の線があったらnullにして次の線を描けるようにする。
                    CurrentLineObject = null;
                }
                    
                
                
            }
        }

        private void brushmanger2() {//マウス，TrailRendererによる描画
            
            if (Input.GetMouseButtonDown (0)) 
            {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                //print(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)&& hit.collider.gameObject.layer == 8) {
                    
                    if(CurrentLineObject == null)
                    {
                    //PrefabからLineObjectを生成
                    CurrentLineObject = Instantiate(TrailObjectPrefab, hit.point, Quaternion.identity);
                    CurrentLineObject.layer = 9 + layerdropdown.value;
                    Undo.RegisterCreatedObjectUndo (CurrentLineObject, "tablet");
                    }
                    
                } 
            }
            else if (Input.GetMouseButtonUp (0))
            {
                if(CurrentLineObject != null)
                {
                    
                    //現在描画中の線があったらnullにして次の線を描けるようにする。
                    CurrentLineObject = null;
                }
                    
                
                
            }
        }
        private void brushmanger3() {//タブレット，TrailRendererによる描画
            
            if (tablet_.pressure!=0) 
            {
                Vector3 penpoint = new Vector3(tablet_.x*Screen.currentResolution.width, tablet_.y*Screen.currentResolution.height,0);
                //print(penpoint);
                Ray ray = Camera.main.ScreenPointToRay (penpoint);
                
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)&& hit.collider.gameObject.layer == 8) {
                    //print(hit.point);
                    if(CurrentLineObject == null)
                    {
                    //PrefabからLineObjectを生成
                    CurrentLineObject = Instantiate(TrailObjectPrefab, hit.point, Quaternion.identity);
                    CurrentLineObject.layer = 9 + layerdropdown.value;
                    

        
                    
                    }
                    
                } 
            }
            if (tablet_.pressure==0)
            {
                if(CurrentLineObject != null)
                {
                    
                    Undo.IncrementCurrentGroup();
                    Undo.RegisterCreatedObjectUndo (CurrentLineObject, "line");
                    //現在描画中の線があったらnullにして次の線を描けるようにする。
                    CurrentLineObject = null;
                }
                    
                
                
            }
        }
        
        private void brushmanger4() { //タブレット，LineRendererによる描画
            
            if (tablet_.pressure!=0) 
            {
                //上が液タブの時・下は板タブの時
                Vector3 penpoint =  new Vector3(tablet_.x*Screen.currentResolution.width-(Screen.currentResolution.width-Screen.width), (tablet_.y)*Screen.currentResolution.height,0);
                
                
                Ray ray = Camera.main.ScreenPointToRay (penpoint);
                
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)&& hit.collider.gameObject.layer == 8) {
                    if(CurrentLineObject == null)
                    {
                    //PrefabからLineObjectを生成
                    CurrentLineObject = Instantiate(LineObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    CurrentLineObject.transform.parent = hit.collider.gameObject.transform;
                    CurrentLineObject.layer = 9 + layerdropdown.value;
                    
                    }
                    
                    
                } 
            }
            if (tablet_.pressure==0)
            {
                if(CurrentLineObject != null)
                {
                    //アンドゥできるようにしておく
                    Undo.IncrementCurrentGroup();
                    Undo.RegisterCreatedObjectUndo (CurrentLineObject, "line");
                    
                    //現在描画中の線があったらnullにして次の線を描けるようにする。
                    CurrentLineObject = null;
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