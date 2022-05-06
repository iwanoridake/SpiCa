using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiCa;

namespace SpiCa{
public class TransformData
{
    public Vector3 LocalPosition = Vector3.zero;
    public Quaternion LocalRotation = Quaternion.identity;
    public Vector3 LocalScale = Vector3.one;

    // Unity requires a default constructor for serialization
    public TransformData() { }

    public TransformData(Transform transform)
    {
        LocalPosition = transform.localPosition;
        LocalRotation = transform.localRotation;
        LocalScale = transform.localScale;
    }

    public void ApplyTo(Transform transform)
    {
        transform.localPosition = LocalPosition;
        transform.localRotation = LocalRotation ;
        transform.localScale = LocalScale;
    }

    public void DifApplyTo(TransformData prev, Transform Applysaki)
    {
        Applysaki.localPosition += LocalPosition - prev.LocalPosition;

        
        
        //拡大縮小
        Vector3 Gyakusu = new Vector3(1/prev.LocalScale.x, 1/prev.LocalScale.y, 1/prev.LocalScale.z);
        Applysaki.localScale = Vector3.Scale(Applysaki.localScale,Vector3.Scale(LocalScale,Gyakusu));

        //回す
        Quaternion KakeruGyoretsu = LocalRotation * Quaternion.Inverse(prev.LocalRotation);
        Applysaki.localRotation = KakeruGyoretsu * Applysaki.localRotation ;

        
        
        

    }
}
}