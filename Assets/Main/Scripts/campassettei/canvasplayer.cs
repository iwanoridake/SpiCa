using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SpiCa;

namespace SpiCa{
public class canvasplayer : MonoBehaviour
{
    public static GameObject targetobj;
    private Canvas activeCanvas;
    
    // X軸座標を制御する Slider オブジェクトを格納する変数
    public Slider sliderPosX;
    // Y軸座標を制御する Slider オブジェクトを格納する変数
    public Slider sliderPosY;
    // Z軸座標を制御する Slider オブジェクトを格納する変数
    public Slider sliderPosZ;
    // X軸回転を制御する Slider オブジェクトを格納する変数
    public Slider sliderRotX;
    // Y軸回転を制御する Slider オブジェクトを格納する変数
    public Slider sliderRotY;
    // Z軸回転を制御する Slider オブジェクトを格納する変数
    public Slider sliderRotZ;
    //scale(まとめて)
    public Slider sliderScale;
    //scalex
    public Slider sliderscalex;
    //scaley
    public Slider sliderscaley;
    //scalez
    public Slider sliderscalez;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnSphereClick() {
        //Destroy(targetobj);
        GameObject obj = (GameObject)Resources.Load ("Sphere");
        obj.tag = "3dcanvas";
    
        targetobj = Instantiate (obj, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
       
    }
    public void OnCubeClick() {
        //Destroy(targetobj);
        GameObject obj = (GameObject)Resources.Load ("Cube");
        obj.tag = "3dcanvas";
 
        targetobj = Instantiate (obj, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);

    
    }
    public void OnCylinderClick() {
        //Destroy(targetobj);
        GameObject obj = (GameObject)Resources.Load ("Cylinder");
        obj.tag = "3dcanvas";

        targetobj = Instantiate (obj, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);

    }
    public void OnConeClick() {
        //Destroy(targetobj);
        GameObject obj = (GameObject)Resources.Load ("Cone");
        obj.tag = "3dcanvas";

        targetobj = Instantiate (obj, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
   
    }
    public void OnCapsuleClick() {
        //Destroy(targetobj);
        GameObject obj = (GameObject)Resources.Load ("Capsule");
        obj.tag = "3dcanvas";

        targetobj = Instantiate (obj, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);

    }
    public void OnPlaneClick() {
        //Destroy(targetobj);
        GameObject obj = (GameObject)Resources.Load ("Plane");
        obj.tag = "3dcanvas";
        

        targetobj = Instantiate (obj, new Vector3(0.0f,0.0f,0.0f), Quaternion.Euler(-90f, 0f, 0f));
  
    }
    public void OnConfirmClick() {
  
        SceneManager.LoadScene("byoga");
    }
    public void Slider_x() {
        
  
        // オブジェクトの座標を変数 objPos に格納
        Vector3 objPos = targetobj.transform.position;
        // スライダーの値を変数 objPos に格納
        objPos.x = sliderPosX.value;
        // 変数 objPos の値をオブジェクトの座標に格納
        targetobj.transform.position = objPos;
    }
    public void Slider_y() {
        
  
        // オブジェクトの座標を変数 objPos に格納
        Vector3 objPos = targetobj.transform.position;
        // スライダーの値を変数 objPos に格納
        objPos.y = sliderPosY.value;
        // 変数 objPos の値をオブジェクトの座標に格納
        targetobj.transform.position = objPos;
    }
    public void Slider_z() {
        
  
        // オブジェクトの座標を変数 objPos に格納
        Vector3 objPos = targetobj.transform.position;
        // スライダーの値を変数 objPos に格納
        objPos.z = sliderPosZ.value;
        // 変数 objPos の値をオブジェクトの座標に格納
        targetobj.transform.position = objPos;
    }
    public void Slider_Rotx() {
        Vector3 objAng = targetobj.transform.eulerAngles;
        objAng.x = sliderRotX.value;
        targetobj.transform.localEulerAngles = objAng;
    }
    public void Slider_Roty() {
        Vector3 objAng = targetobj.transform.eulerAngles;
        objAng.y = sliderRotY.value;
        targetobj.transform.localEulerAngles = objAng;
    }
    public void Slider_Rotz() {
        Vector3 objAng = targetobj.transform.eulerAngles;
        objAng.z = sliderRotZ.value;
        targetobj.transform.localEulerAngles = objAng;
    }
     public void Slider_Scalex() {
        Vector3 objsc = targetobj.transform.localScale;
        objsc.x = sliderscalex.value;
        targetobj.transform.localScale = objsc;
    }
    public void Slider_Scaley() {
        Vector3 objsc = targetobj.transform.localScale;
        objsc.y = sliderscaley.value;
        targetobj.transform.localScale = objsc;
    }
    public void Slider_Scalez() {
        Vector3 objsc = targetobj.transform.localScale;
        objsc.z = sliderscalez.value;
        targetobj.transform.localScale = objsc;
    }
    /*public void Slider_Scale() {
        Vector3 objsc = new Vector3(sliderScale.value,sliderScale.value,sliderScale.value);
        targetobj.transform.localScale = objsc;
    }*/
  




    // Update is called once per frame
    void Update()
    {
        
        if (targetobj != null)
        {
            Vector3 objPos = targetobj.transform.position;
            Vector3 objAng = targetobj.transform.eulerAngles;
            Vector3 objsc = targetobj.transform.localScale;
            sliderPosX.value = objPos.x;
            sliderPosY.value = objPos.y;
            sliderPosZ.value = objPos.z;
            /*sliderRotX.value = objAng.x;
            sliderRotY.value = objAng.y;
            sliderRotZ.value = objAng.z;*/
            //sliderScale;
            sliderscalex.value = objsc.x;
            sliderscaley.value = objsc.y;
            sliderscalez.value = objsc.z;
        }
        
    }
}
}