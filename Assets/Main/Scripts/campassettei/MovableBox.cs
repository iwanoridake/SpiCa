using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiCa;

namespace SpiCa{
public class MovableBox : MonoBehaviour {

    private Vector3 moveTo;
    
    private float wheelSpeed = 1.05f;
    private float rotateSpeed = 2f;
    private float moveSpeed = 0.3f;
   

    private bool beRay = false;
    private Camera cam;
    private GameObject canvas;
    private Vector3 preMousePosition;

    // Use this for initialization
    void Start () {
        cam =  Camera.main;
    
    }

    // Update is called once per frame
    void Update () {

        if(GameObject.FindGameObjectWithTag("3dcanvas")!=null)
        {
            //canvas = GameObject.FindGameObjectWithTag("3dcanvas");
            canvas = Painter3DManager.Instance.ActiveCanvas.gameObject;
        if (canvas != null)
        {

            
            if (Input.GetMouseButtonDown(0)) {
                preMousePosition = Input.mousePosition;
                RayCheck();
            }
            

            if (beRay) {
                MovePosition();
            }

            if (Input.GetMouseButtonUp(0)) {
                beRay = false;
            }
            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if(scrollWheel != 0.0f)
            {
                MouseWheel(scrollWheel);
            }
            
            if (Input.GetMouseButton(1))
            {
                OnRotate(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
            }
        }
        }
    }

    private void RayCheck() {
        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == canvas.GetComponent<Collider>()) {
            beRay = true;
        } else {
            beRay = false;
        }

    }

    private void MovePosition() {

        Vector3 mousePos = Input.mousePosition;
        mousePos.z =  canvas.transform.position.z- Camera.main.transform.position.z;
        if(Input.mousePosition.x<Screen.width-Screen.width*0.15&&Input.mousePosition.x>Screen.width*0.15){
        moveTo = Camera.main.ScreenToWorldPoint(mousePos);
        canvas.transform.position = moveTo;
        }
        

    }
    /*private void MovePosition(Vector3 mousePosition)
    {
        Vector3 diff = mousePosition - preMousePosition;
        if (diff.magnitude < Vector3.kEpsilon)
        {
            return;
        }
        float d = distance();
        
        
        canvas.transform.Translate(diff * Time.deltaTime * moveSpeed*d);
        Vector3 pos = canvas.transform.position;
        pos.z = canvas.transform.position.z- Camera.main.transform.position.z;
        
        preMousePosition = mousePosition;
        return;
    }
    private float distance()
    {
        return (transform.position - new Vector3(0,0,0)).magnitude;
    }*/
 

   
    private void OnRotate(Vector2 delta)
    {
        // 回転量はドラッグ方向ベクトルの長さに比例する
        float deltaAngle = delta.magnitude * rotateSpeed;

        // 回転量がほぼ0なら、回転軸を求められないので何もしない
        if (Mathf.Approximately(deltaAngle, 0.0f))
        {
            return;
        }

        // ドラッグ方向をワールド座標系に直す
        // 横ドラッグならカメラのright方向、縦ドラッグならup方向ということなので
        // deltaのx、yをright、upに掛けて、2つを合成すればいいはず...
        Transform cameraTransform = Camera.main.transform;
        Vector3 deltaWorld = cameraTransform.right * delta.x + cameraTransform.up * delta.y;

        // 回転軸はドラッグ方向とカメラforwardの外積の向き
        Vector3 axisWorld = Vector3.Cross(deltaWorld, cameraTransform.forward).normalized;

        // Rotateで回転する
        // 回転軸はワールド座標系に基づくので、回転もワールド座標系を使う
        canvas.transform.Rotate(axisWorld, deltaAngle, Space.World);
    }
        

    private void MouseWheel(float delta)//前進/後退
    {
        //canvas.transform.position += canvas.transform.forward * delta * wheelSpeed;
        if(delta>0){
            canvas.transform.localScale *= wheelSpeed;
        } else if (delta<0){
            canvas.transform.localScale /= wheelSpeed;
        }
        
        return;
    }
    
}
}