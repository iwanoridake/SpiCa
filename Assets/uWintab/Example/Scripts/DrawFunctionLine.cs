using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
namespace uWintab
{
[RequireComponent(typeof(LineRenderer))]
public class DrawFunctionLine : MonoBehaviour
{
    DrawManager drawManager;
    GameObject player;
    Canvas canvas;
    
    public float width = 1.0f;
    public float timeout = 0.03f;
    public float minDistance = 0.01f;
    LineRenderer m_tr;
    Tablet tablet_;
    bool drawingnow=false;
    private float timeElapsed=0;
    int kaisu = 0;
    static int senhonsu=0;
    public Slider hpSlider;
    
    
    void Reset()
    {
        m_tr = this.gameObject.GetComponent<LineRenderer>();
        m_tr.widthMultiplier = 0.01f;
        //m_tr.minVertexDistance = 0.01f;
    }
    void Start()
    {
        tablet_ = FindObjectOfType<Tablet>();
        player = GameObject.Find ("Player");
        drawManager = player.GetComponent<DrawManager>();
        m_tr = this.gameObject.GetComponent<LineRenderer>();
        canvas=GameObject.Find("mainCanvas").GetComponent<Canvas>();
        hpSlider = canvas.transform.Find("brushcustom").transform.Find("Slider").GetComponent<Slider>();
        drawingnow=true;
        
    }
    void LateUpdate()
    {
        //width=drawManager.width;
        tabletdraw();
    }
    void mousedraw()
    {
        if(drawingnow)
        {
            if (Input.GetMouseButton (0)) 
            {
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)&& hit.collider.gameObject.layer == 8) {
                    //Vector3 indexTipPos = hit.point;
                    //print(hit.point);
                    //LineRendererからPositionsのサイズを取得
                    int NextPositionIndex = m_tr.positionCount;
                    //LineRendererのPositionsのサイズを増やす
                    m_tr.positionCount = NextPositionIndex + 1;
                    //LineRendererのPositionsに現在のコントローラーの位置情報を追加
                    m_tr.SetPosition(NextPositionIndex, hit.point);
                }
            }
            else
            {
                //m_tr.emitting = false;
            }
        }
        if (Input.GetMouseButtonUp (0)) 
        {
            drawingnow=false;
        }
    }
    void tabletdraw()
    {
        if(drawingnow)
        {
            if  (tablet_.pressure!=0)  
            {
                width=hpSlider.value;
                Vector3 penpoint = new Vector3(tablet_.x*Screen.currentResolution.width-(Screen.currentResolution.width-Screen.width), tablet_.y*Screen.currentResolution.height,0);
                //print(penpoint);
                Ray ray = Camera.main.ScreenPointToRay (penpoint);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)&& hit.collider.gameObject.layer == 8) {
                    
                    
                   //LineRendererからPositionsのサイズを取得
                    int NextPositionIndex = m_tr.positionCount;
                    var positions = new Vector3[m_tr.positionCount];
                    int cnt = m_tr.GetPositions(positions);
                    
                    if(cnt>0){
                        if(Vector3.Distance(hit.point, positions[cnt-1])>minDistance){
                            //LineRendererのPositionsのサイズを増やす
                            m_tr.positionCount = NextPositionIndex + 1;
                            //LineRendererのPositionsに現在のコントローラーの位置情報を追加
                            m_tr.SetPosition(NextPositionIndex, hit.point);
                            timeElapsed += Time.deltaTime;
                            if(timeElapsed>timeout)
                            {
                                var pressure = tablet_.pressure;
                                AnimationCurve curve = m_tr.widthCurve;
                                for(int i=1;i<=kaisu;i++){
                                   float x = m_tr.widthCurve.keys[i].time*(((float)kaisu+1.0f)/((float)kaisu+2.0f));
                                    //Debug.Log(x);
                                    curve.MoveKey(i,new Keyframe(x,m_tr.widthCurve.keys[i].value));
                                    //Debug.Log(curve.keys[i].time);
                                }
                        
                                curve.AddKey(((float)kaisu+1.0f)/((float)kaisu+2.0f), 1.0f*pressure);
                                
                                m_tr.widthCurve = curve;
                                m_tr.widthMultiplier = width*0.3f;
                                kaisu++;
                                timeElapsed = 0;
                            }
                    
                        }
                    }else{
                        m_tr.material.renderQueue=m_tr.material.renderQueue+senhonsu;//一個前に
                        //Debug.Log(m_tr.material.renderQueue);
                        senhonsu++;
                        //LineRendererのPositionsのサイズを増やす
                        m_tr.positionCount = NextPositionIndex + 1;
                        //LineRendererのPositionsに現在のコントローラーの位置情報を追加
                        m_tr.SetPosition(NextPositionIndex, hit.point);
                        timeElapsed += Time.deltaTime;
                        if(timeElapsed>timeout){
                            var pressure = tablet_.pressure;
                            AnimationCurve curve = m_tr.widthCurve;
                            for(int i=1;i<=kaisu;i++){
                                float x = m_tr.widthCurve.keys[i].time*(((float)kaisu+1.0f)/((float)kaisu+2.0f));
                                //Debug.Log(x);
                                curve.MoveKey(i,new Keyframe(x,m_tr.widthCurve.keys[i].value));
                                //Debug.Log(curve.keys[i].time);
                            }
                            
                            curve.AddKey(((float)kaisu+1.0f)/((float)kaisu+2.0f), 1.0f*pressure);
                            m_tr.widthCurve = curve;
                            m_tr.widthMultiplier = width*0.3f;
                            kaisu++;
                            timeElapsed = 0;
                        }
                     
                    }
                    
                       
                    
                    
                }
            }
            else
            {
                //m_tr.emitting = false;
                
                kaisu=0;
                drawingnow=false;
                
            }
        }
        /*if (tablet_.pressure==0)
        {
            drawingnow=false;
        }*/
    }
    
}
}