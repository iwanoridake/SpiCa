using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiCa{
public class AnimatedGif : MonoBehaviour
{

    //入力画像のフレームレート
    public float framerate = 6;
    //Resoucesフォルダからのパス
    
    
    //ファイル名のprefix
    public string prefixName = "loop_";
    //合計コマ数
    public int length = 100;
    //ループ再生設定
    public bool isLoop = true;

    private Material targetMaterial;
    private int currentFrame;
    private float frameTime;
    public bool isPlaying = false;

    /// <summary>
    /// 再生
    /// </summary>
    public void Play()
    {
        isPlaying = true;
    }

    /// <summary>
    /// 一時停止
    /// </summary>
    public void Pause()
    {
        isPlaying = false;
    }

    /// <summary>
    /// リセット
    /// </summary>
    public void Reset()
    {
        currentFrame = 1;
        frameTime = 0.0f;
    }


    private void Start()
    {
        targetMaterial = gameObject.GetComponent<Renderer>().material;
        this.Reset();
        
    }

    private void Update()
    {
        
        if (!isPlaying) return;
        // フレーム時間更新
        frameTime += Time.deltaTime;
        
        // フレームに更新がある場合
        if (frameTime > 1f / framerate)
        {
            frameTime = 0.0f;
            currentFrame++;
            // ループ再生の場合は最後のコマで最初に戻る
            if (currentFrame > length && isLoop) currentFrame = 1;
            // ループ再生でない場合は最後のコマで再生終了
            if (currentFrame >= length && !isLoop) isPlaying = false;
            // 現在のフレームを取得して表示
            string filename;
            if(currentFrame<10){
            filename = "Renban/" + prefixName +"00"+ currentFrame ;
            }else if(currentFrame<100){
            filename = "Renban/" + prefixName +"0"+ currentFrame ;
            }else{
            filename = "Renban/" + prefixName + currentFrame ;
            }
               
            Texture2D tex = Resources.Load(filename) as Texture2D;
            //if(tex==null)Debug.Log("tex");            
            
            targetMaterial.mainTexture = tex;
        }
    }

}
}