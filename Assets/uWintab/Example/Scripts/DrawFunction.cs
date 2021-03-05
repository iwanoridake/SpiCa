using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
namespace uWintab
{
[RequireComponent(typeof(TrailRenderer))]
public class DrawFunction : MonoBehaviour
{
    public float width = 1.0f;
    public float timeout = 0.05f;
    TrailRenderer m_tr;
    Tablet tablet_;
    bool drawingnow=false;
    private float timeElapsed=0;
    int kaisu = 0;
    

    void Reset()
    {
        m_tr = this.gameObject.GetComponent<TrailRenderer>();
        m_tr.time = Mathf.Infinity;
        m_tr.widthMultiplier = 0.01f;
        m_tr.minVertexDistance = 0.01f;
    }

    void Start()
    {
        tablet_ = FindObjectOfType<Tablet>();
        m_tr = this.gameObject.GetComponent<TrailRenderer>();
        drawingnow=true;
    }

    void LateUpdate()
    {
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
                    Vector3 indexTipPos = hit.point;
                    print(hit.point);
                    this.gameObject.transform.position = indexTipPos;
                    m_tr.emitting = true;
                }
            }
            else
            {
                m_tr.emitting = false;
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
                Vector3 penpoint = new Vector3(tablet_.x*Screen.currentResolution.width, tablet_.y*Screen.currentResolution.height,0);
                //print(penpoint);
                Ray ray = Camera.main.ScreenPointToRay (penpoint);
                RaycastHit hit;
                if (Physics.Raycast (ray, out hit)&& hit.collider.gameObject.layer == 8) {
                    Vector3 indexTipPos = hit.point;
                    //print(hit.point);
                    this.gameObject.transform.position = indexTipPos;
                    m_tr.emitting = true;
                    
                    timeElapsed += Time.deltaTime;
                    if(timeElapsed>timeout)
                    {
                        var pressure = tablet_.pressure;
                        AnimationCurve curve = m_tr.widthCurve;
                        curve.AddKey(kaisu*timeout, 1.0f*pressure);
                        m_tr.widthCurve = curve;
                        m_tr.widthMultiplier = width;
                        kaisu++;
                        timeElapsed = 0;
                    }
                }
            }
            else
            {
                m_tr.emitting = false;
            }
        }

        if (tablet_.pressure==0)
        {
            drawingnow=false;
        }
    }
}
}