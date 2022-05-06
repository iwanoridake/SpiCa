using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiCa;
using System.Linq;

namespace SpiCa{
    //各キャンバスにstrokeを紐づけ
public class Canvas : MonoBehaviour
{
    // キャンバスに含まれるストローク
    public List<Stroke> m_Strokes = new List<Stroke>();
    public List<Stroke> Strokes { get { return m_Strokes; } }
    private TransformData prevTransformData;
    private TransformData nowTransformData;

    void Awake()
    {
        if(Painter3DManager.Instance.ActiveCanvas !=null){
            Painter3DManager.Instance.ActiveCanvas.ChangeCanvasFalse();
        }
        
        this.gameObject.name = "Canvas " + Painter3DManager.Instance.canvasAllCount;
        Painter3DManager.Instance.canvasAllCount++;
        Painter3DManager.Instance.ActiveCanvas = this;
        Painter3DManager.Instance.AllCanvases.Add(this);
        
        Painter3DManager.Instance.m_ActiveCanvasIndex = Painter3DManager.Instance.m_AllCanvases.Count-1;

        
        prevTransformData = new TransformData(this.gameObject.transform);
        nowTransformData = new TransformData(this.gameObject.transform);
        this.gameObject.transform.hasChanged = false;
    


    }

    void Update()
    {
        //キャンバスが変形された場合いっしょにブラシも動く
        if (this.gameObject.transform.hasChanged)
        {
            foreach(Stroke element in m_Strokes)
            {
                //nowTransformData = new TransformData(this.gameObject.transform);
                //nowTransformData.DifApplyTo( prevTransformData, element.LineObject.transform);

            }

            
            transform.hasChanged = false;
            prevTransformData = new TransformData(this.gameObject.transform);
        }
    }

    

    //ストローク関連のメソッドたち
    
    public void AddStroke(Stroke s)
        {
            //print("Adding stroke to canvas. Stroke count: " + m_Strokes.Count);

            // Name stroke with index
            s.name = "Stroke " + m_Strokes.Count;
            

            // Add stroke to the list and set parent to canvas
            m_Strokes.Add(s);
            
            s.transform.SetParent(transform);
            
            

            // Turn this on if you want to keep brush stroks consistent to canvas scale rather than world
            //s.transform.localScale = Vector3.one;

            Painter3DManager.Instance.ClearRedo();

            
        }

    public void RemoveUndoStroke(Stroke s)
        {
            m_Strokes.Remove(s);
        }

    public void AddRedoStroke(Stroke s)
        {
            m_Strokes.Add(s);
        }

    public void RemoveAndDestroyStroke( int index )
        {
            Stroke s = m_Strokes[index];
            m_Strokes.RemoveAt(index);
            Destroy(s.gameObject);
        }

    public void RemoveAndDestroyLastStroke()
        {
            RemoveAndDestroyStroke(m_Strokes.Count - 1);
        }
    public void Clear()
        {
            m_Strokes.RemoveAll(item => item == null);

            for (int i = 0; i < m_Strokes.Count; i++)
            {
                if( m_Strokes[i].gameObject != null )
                    Destroy(m_Strokes[i].gameObject);
            }

            m_Strokes.Clear();
        }

    public void ChangeCanvasFalse()
    {
        
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;

        if(this.gameObject.GetComponent<MeshCollider>() != null)
            this.gameObject.GetComponent<MeshCollider>().enabled = false;
        if(this.gameObject.GetComponent<SphereCollider>() != null)
            this.gameObject.GetComponent<SphereCollider>().enabled = false;
        if(this.gameObject.GetComponent<CapsuleCollider>() != null)
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        if(this.gameObject.GetComponent<BoxCollider>() != null)
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            

    }
    public void ChangeCanvasTrue()
    {
        
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        
        if(this.gameObject.GetComponent<MeshCollider>() != null)
            this.gameObject.GetComponent<MeshCollider>().enabled = true;
        if(this.gameObject.GetComponent<SphereCollider>() != null)
            this.gameObject.GetComponent<SphereCollider>().enabled = true;
        if(this.gameObject.GetComponent<CapsuleCollider>() != null)
            this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        if(this.gameObject.GetComponent<BoxCollider>() != null)
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    //キャンバス情報の保存
    
}
}
