using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hider : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject canvas;
    private GameObject image;
    private Color32 CanvasMotoColor;
    private Shader imageMotoShader;
    void Start(){
        canvas = GameObject.FindGameObjectWithTag("3dcanvas");
        image = GameObject.FindGameObjectWithTag("inputimage");
        if (canvas != null)
        {
            CanvasMotoColor= canvas.GetComponent<MeshRenderer> ().material.color;
        }
        if (image != null)
        {
        imageMotoShader = image.GetComponent<MeshRenderer> ().material.shader;
        }

    }    
    
 
    //Toggle用のフィールド
    public Toggle canvastoggle;
    public Toggle cullingtoggle;
    
    
	
    public void OnCanvasToggleChanged(){
        
        if (canvas != null)
        {
            canvas.GetComponent<MeshRenderer> ().material.color = canvastoggle.isOn ? new Color32(0,0,0,0) : CanvasMotoColor;
        }
        
    }
    public void OnCullingToggleChanged(){
        
        if (image != null)
        {
            image.GetComponent<MeshRenderer> ().material.shader = cullingtoggle.isOn ? Shader.Find("Unlit/Transparent") : imageMotoShader;
        }
        
    }
    public void OnCTClick() {
        Destroy(image);
  
        SceneManager.LoadScene("imageload");
    }
    
}
