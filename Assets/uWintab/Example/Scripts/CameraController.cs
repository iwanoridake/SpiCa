using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public GameObject CameraTarget;

    [SerializeField, Range(1f, 500f)] private float wheelSpeed = 100f;
    [SerializeField, Range(0.1f, 1f)] private float moveSpeed = 0.3f;
    [SerializeField, Range(0.1f, 1f)] private float rotateSpeed = 1f;
    private Vector3 preMousePosition;
    private Vector3 StartRelativePosition;
    public Toggle rokurotoggle;
    private bool RokuroRotateOn = false;


    private void Start()
    {
        
    
        LookCameraTarget();
        StartRelativePosition = this.transform.position;
    }

    private void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if(scrollWheel != 0.0f)
        {
            MouseWheel(scrollWheel);
        }
 
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            preMousePosition = Input.mousePosition;
        }
        if (RokuroRotateOn)
        {
             //Vector3 diff = new Vector3(1f,0f,0f);
            //float d = distance();
            GameObject canvas = GameObject.FindGameObjectWithTag("3dcanvas");
            float _period = 5;
            if (canvas != null)
            {
                //transform.Translate(-diff * Time.deltaTime * rotateSpeed * d);
                transform.RotateAround(
                    canvas.transform.position,
                    canvas.transform.up,
                    360 / _period * Time.deltaTime
                );
                //LookCameraTarget();
                //transform.position += transform.forward * ((transform.position - CameraTarget.transform.position).magnitude - d);//直線移動と曲線移動の誤差修正
            }
                
            
            
        }

        MouseDrag(Input.mousePosition);
 
        return;
    }
    public void OnButtonClick() {
        this.transform.position =  StartRelativePosition;
        LookCameraTarget();
    }
    public void OnRokuroToggleChanged(){
     
        RokuroRotateOn = rokurotoggle.isOn;
        
    }



    private void MouseWheel(float delta)//前進/後退
    {
        transform.position += transform.forward * delta * wheelSpeed;
        return;
    }
    private void MouseDrag(Vector3 mousePosition)
    {
        Vector3 diff = mousePosition - preMousePosition;
        if (diff.magnitude < Vector3.kEpsilon)
        {
            return;
        }
        float d = distance();
        if (Input.GetMouseButton(1))//回転移動
        {
            transform.Translate(-diff * Time.deltaTime * rotateSpeed * d);
            LookCameraTarget();
            transform.position += transform.forward * ((transform.position - CameraTarget.transform.position).magnitude - d);//直線移動と曲線移動の誤差修正
        }

        preMousePosition = mousePosition;
        return;
    }
    private float distance()
    {
        return (transform.position - CameraTarget.transform.position).magnitude;
    }
    private void LookCameraTarget()
    {
        transform.LookAt(CameraTarget.transform);
        CameraTarget.transform.rotation = transform.rotation;
        return;
    }
}