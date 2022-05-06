using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiCa{
public class Stroke : MonoBehaviour
{
    public GameObject LineObject=null;
    LineRenderer m_tr;
    AnimationCurve Motocurve;

    public void Addvalue(int value){
        LineObject = this.gameObject;
        
        LineObject.layer = 9 + value;
    }

    public void AnimationValue()
    {
        m_tr = this.gameObject.GetComponent<LineRenderer>();
        

        AnimationCurve curve = m_tr.widthCurve;
        Keyframe[] applykeys = new Keyframe[curve.length]; 
        for(int i=0;i<curve.length;i++){

            if(i==0){
                //applykeys[i] = curve.keys[curve.length-1];
                curve.MoveKey(i,new Keyframe(m_tr.widthCurve.keys[i].time,m_tr.widthCurve.keys[curve.length-1].value));
            }else{
                //applykeys[i] = curve.keys[i-1];
                curve.MoveKey(i,new Keyframe(m_tr.widthCurve.keys[i].time,m_tr.widthCurve.keys[i-1].value));
                
            }
                                    
        }
        
        m_tr.widthCurve = curve;
        
        
    }
    public void MotoCurveIn()
    {
        m_tr = this.gameObject.GetComponent<LineRenderer>();
        

        Motocurve = m_tr.widthCurve;
        
        
        
    }
    public void MotoCurveOut()
    {
        m_tr = this.gameObject.GetComponent<LineRenderer>();
        

        m_tr.widthCurve = Motocurve;
        
        
        
    }

}}
