using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public GameObject CameraTarget;

    [SerializeField, Range(1f, 500f)] private float wheelSpeed = 100f;
    [SerializeField, Range(0.1f, 1f)] private float moveSpeed = 0.3f;
    [SerializeField, Range(0.1f, 1f)] private float rotateSpeed = 1f;
    private Vector3 preMousePosition;
    private Vector3 StartRelativePosition;
    private Quaternion StartRelativeRotation;
    private Vector3 StartTargetPosition;
    private Quaternion StartTargetRotation;
    public Toggle rokurotoggle;
    public Toggle reversetoggle;
    private bool RokuroRotateOn = false;
    float _period = 5;
    public Slider hpSlider;

    private void Start()
    {
        
        StartTargetRotation = CameraTarget.transform.rotation;
        StartTargetPosition = CameraTarget.transform.position;
        StartRelativeRotation = this.transform.rotation;
        StartRelativePosition = this.transform.position;
        LookCameraTarget();
        hpSlider.value=_period;
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
            
            if (canvas != null)
            {
                //transform.Translate(-diff * Time.deltaTime * rotateSpeed * d);
                
                transform.RotateAround(
                    canvas.transform.position,
                    canvas.transform.up,
                    reversetoggle.isOn?(-360 / _period * Time.deltaTime):(360 / _period * Time.deltaTime)
                    
                    
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
        this.transform.rotation =  StartRelativeRotation;
        CameraTarget.transform.rotation=StartTargetRotation;
        CameraTarget.transform.position=StartTargetPosition;
        LookCameraTarget();
    }
    public void OnRokuroToggleChanged(){
     
        RokuroRotateOn = rokurotoggle.isOn;
        
    }
    public void Sppedchange()
    {
 
        _period=hpSlider.value;
 
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
        else if (Input.GetMouseButton(0))//平行移動
        {
            if(Input.mousePosition.x<Screen.width-600&&Input.mousePosition.x>600){
                transform.Translate(-diff * Time.deltaTime * moveSpeed * d);
                CameraTarget.transform.Translate(-diff * Time.deltaTime * moveSpeed * d);
                LookCameraTarget();
            }
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