using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiCa;

namespace SpiCa{
    //各キャンバスにstrokeを紐づけ
public class Canvas : MonoBehaviour
{
    // キャンバスに含まれるストローク
    public List<Stroke> m_Strokes = new List<Stroke>();
    public List<Stroke> Strokes { get { return m_Strokes; } }

    void Start()
    {
        Painter3DManager.Instance.ActiveCanvas = this;

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

    //キャンバス情報の保存
    
}
}
