using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hider : MonoBehaviour
{
    // 3Dキャンバスやレイヤを見えなくするためのクラス．
    //ついでにバックフェース火リングとキャンバス変形に戻る時のボタンのメソッドもある．なんで？
    //キャンバス回転いったんここにかくわ　てへぺろ

    //今使ってるキャンバス
    private GameObject canvas;
    //入れてる画像
    private GameObject image;
    //メインカメラ
    [SerializeField] Camera mainC;
    //サブビュー
    [SerializeField] Camera subC;
    // キャンバスの色格納
    private Color32 CanvasMotoColor;
    //画像のシェーダも格納
    private Shader imageMotoShader;
    
    //レイヤーとこのキャンバストグル
    [SerializeField] Toggle canvastoggle;
    //アピアランスパネルのバックフェースカリングのトグル
    [SerializeField] Toggle cullingtoggle;
    [SerializeField]Toggle CanvasAnimationToggle;
    private bool CanvasAnimationOn = false;

    float _period = 5;

    void Start(){
        canvas = GameObject.FindGameObjectWithTag("3dcanvas");
        image = GameObject.FindGameObjectWithTag("inputimage");

        if (image != null)
        {
            //シェーダ覚えておく
        imageMotoShader = image.GetComponent<MeshRenderer> ().material.shader;
        }

    }    
    
    private void Update()
    {
        //アニメーションさせる
        if (CanvasAnimationOn)
        {            
            if (canvas != null)
            {
                
                canvas.transform.Rotate(Vector3.up, 360 / _period * Time.deltaTime);
                
            }
                
            
            
        }

    }

    //キャンバスアニメーション用
    public void OnCanvasAnimationToggleChanged(){
     
        CanvasAnimationOn = CanvasAnimationToggle.isOn;
        
    }
    
 
    
    
	//キャンバストグル用
    public void OnCanvasToggleChanged(){
        
        
        mainC.LayerCullingToggle("3dcanvas");
        subC.LayerCullingToggle("3dcanvas");
        
        
    }
    //レイヤ1用
    public void OnLayer1ToggleChanged(){
        
        mainC.LayerCullingToggle("byoga1");
        subC.LayerCullingToggle("byoga1");
        
    }
    //レイヤ2用
    public void OnLayer2ToggleChanged(){
        
        mainC.LayerCullingToggle("byoga2");
        subC.LayerCullingToggle("byoga2");
        
    }
    //バックフェースカリング用
    public void OnCullingToggleChanged(){
        
        if (image != null)
        {
            image.GetComponent<MeshRenderer> ().material.shader = cullingtoggle.isOn ? Shader.Find("Unlit/Transparent Cutout") : imageMotoShader;
        }
        
    }
    //キャンバス変形に戻る用
    public void OnCTClick() {
        Destroy(image);
  
        SceneManager.LoadScene("imageload");
    }
    
    
}
