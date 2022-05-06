using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpiCa;

namespace SpiCa{
    //各キャンバスにstrokeを紐づけ
public class Painter3DManager : MonoBehaviour
    {
        #region Variables
        public delegate void InBoundsEvent( bool inbounds);
        public InBoundsEvent OnInBoundsEvent;

        public static Painter3DManager Instance;

        //public Painter3DResourceManager m_ResourceManager;

        //public Brush m_ActiveBrush;
        public int m_PaintingLayer = 8;
                
        bool m_UseRedo = true;
        
        public GameObject m_PCCam;

        public GameObject m_EventSystem;

        [Header("Serialization")]
        public int m_SaveIndex = 0;
        public int m_CurrentLoadedIndex = 0;
        public int m_MaxSaveIndex = 0;

        public KeyCode m_ModifierKey = KeyCode.LeftShift;

        #region Canvases
        // Canvas list and active canvas
        public List<Canvas> m_AllCanvases = new List<Canvas>();
        public int m_ActiveCanvasIndex = 0;
        Canvas m_ActiveCanvas;
        public Canvas ActiveCanvas { get { return m_ActiveCanvas; } set { m_ActiveCanvas = value; } }
        public List<Canvas> AllCanvases { get { return m_AllCanvases; } }
        public int canvasAllCount = 1;
        #endregion       

        #region Undo/Redo
        Stack<Stroke> m_ActiveStrokeStack = new Stack<Stroke>();
        Stack<Stroke> m_UndoStack = new Stack<Stroke>();
        #endregion

        // A collider that defines the area you can paint on
        public Collider m_CanvasCollider;

        public int m_UndoCount;
        public int m_ColourCount;
        public int m_StrokeCount;

        //debug
        public bool m_UseStrokeOffset = false;
        
        // Flag for accepting input, ends stroke of input is turned off
        public bool m_InputActive = true;
        
        #endregion
        
        // Use this for initialization
        void Awake()
        {
            // Set the singleton instance
            

            if (Instance == null) {
 
                Instance = this;
                DontDestroyOnLoad (gameObject);
            }
            else {
 
            Destroy (gameObject);
            }
            
            // Set the singleton instance
            //m_CanvasParent = new GameObject("_Canvas parent").transform;
           // m_CanvasParent.SetParent(transform);
           

            // Set the onstroke complete event 
            //m_ActiveBrush.OnStrokeCompleteEvent += ActiveBrush_OnStrokeCompleteEvent;
            
            // Create a new canvas
            //CreateNewCanvas();

            m_MaxSaveIndex = PlayerPrefs.GetInt("m_MaxSaveIndex", 0);
            m_CurrentLoadedIndex = 0;
            m_MaxSaveIndex = m_SaveIndex;

            
        }
        
        
        // Update is called once per frame
        void Update()
        {
            #region Keyboard input
            // Clear all
            //if (Input.GetKey(m_ModifierKey) && Input.GetKeyDown(KeyCode.C))
            //{
                //ClearAll();
            //}

            // Quit application
            if (Input.GetKey(m_ModifierKey) && Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            #endregion
        }
        
        
         
        #region Undo Redo Clear
        private void ActiveBrush_OnStrokeCompleteEvent(Stroke stroke)
        {
            m_StrokeCount++;
            // Push the completed stroke to the active stroke stack
            m_ActiveStrokeStack.Push(stroke);
        }

        public void UndoLastStroke()
        {
            if (m_ActiveStrokeStack.Count > 0)
            {
                // pops the latest stroke from the stack
                Stroke stroke = m_ActiveStrokeStack.Pop();
                m_UndoCount++;

                if (m_UseRedo)
                { 
                    m_UndoStack.Push(stroke);  
                    // Stop stroke rendering
                    //stroke.m_StrokeRenderer.SetRenderState(false);
                    m_ActiveCanvas.RemoveUndoStroke(stroke);
                }
                else
                {
                    m_ActiveCanvas.RemoveAndDestroyLastStroke();
                }
            }
        }

        public void Redo()
        {
            if (m_UndoStack.Count > 0)
            {
                Stroke stroke = m_UndoStack.Pop();
                m_ActiveStrokeStack.Push(stroke);

                m_ActiveCanvas.AddRedoStroke(stroke);

                // Start stroke rendering
                //stroke.m_StrokeRenderer.SetRenderState(true);
            }
        }

        public void ClearRedo()
        {
            foreach (Stroke s in m_UndoStack)
                Destroy(s.LineObject);

            m_UndoStack.Clear();
        }

        public void ClearActiveCanvas()
        {
            foreach(Stroke s in ActiveCanvas.Strokes){
                Destroy(s.gameObject);
            }
            AllCanvases.Remove(ActiveCanvas);
            
            Destroy(ActiveCanvas.gameObject);
            ActiveCanvas =  AllCanvases[AllCanvases.Count-1];
            m_ActiveCanvasIndex = AllCanvases.Count-1;
            ActiveCanvas.ChangeCanvasTrue();
            
        }
        #endregion        
    }
}
