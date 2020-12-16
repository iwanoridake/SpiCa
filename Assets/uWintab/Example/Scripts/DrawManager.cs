using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DrawManager : MonoBehaviour {

    //変数を用意
    //SerializeFieldをつけるとInspectorウィンドウからゲームオブジェクトやPrefabを指定できます。
    [SerializeField] GameObject LineObjectPrefab;

    //現在描画中のLineObject;
    private GameObject CurrentLineObject = null;

   

    // Use this for initialization
    void Start () {

    }


    // Update is called once per frame
    void Update () {
        // ここから追加コード
        
        //マウスおす間
        if (Input.GetMouseButton (0)) 
		{
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
            if (Physics.Raycast (ray, out hit)) {
				
				if(CurrentLineObject == null)
                {
                //PrefabからLineObjectを生成
                CurrentLineObject = Instantiate(LineObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
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
}