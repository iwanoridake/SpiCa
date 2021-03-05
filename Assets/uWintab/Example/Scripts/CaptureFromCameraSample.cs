using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
 
public class CaptureFromCameraSample : MonoBehaviour
{
    [SerializeField] private static readonly string CAPUTURED_PICTURE_SAVE_DIRECTORY = "/";
    public Camera _captureCamera;
 
    void Start(){
        
    }
    public IEnumerator Capture()
    {
        //_captureCameraに写るものを800*600でPNG画像として保存
        Debug.Log(_captureCamera.targetTexture);
        var coroutine = StartCoroutine(CaptureFromCamera(1000, 1000, _captureCamera));
        
        yield return coroutine;
    }
 
    /// <summary>
    /// CaptureMain
    /// </summary>
    /// <param name="width">横解像度</param>
    /// <param name="height">縦解像度</param>
    private IEnumerator CaptureFromCamera(int width, int height, Camera camera)
    {
        //var d_width = camera.targetTexture.width;
        //var d_height = camera.targetTexture.height;
 
        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
 
        if (camera.targetTexture != null)
            camera.targetTexture.Release();
 
        camera.targetTexture = new RenderTexture(width, height, 24);
 
        
        yield return new WaitForEndOfFrame();
        
 
        RenderTexture.active = camera.targetTexture;
        tex.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        tex.Apply();
 
        byte[] bytes = tex.EncodeToPNG();
        string savePath = Application.streamingAssetsPath + CAPUTURED_PICTURE_SAVE_DIRECTORY + "EXAMPLE_NAME" + ".png";
        File.WriteAllBytes(savePath, bytes);
        
 
        Destroy(tex);
 
        if (camera.targetTexture != null)
            camera.targetTexture.Release();
 
        yield break;
    }
    public void OnCaptureClick() {
        StartCoroutine("Capture");
        
    }
}