using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
namespace HSVPicker
{
public class LineColorChanger : MonoBehaviour
{
    //色変えをする．ついでにブラシテクスチャも変えられる
    //trailを使うときは入れ替える
    public ColorPicker picker;
    [SerializeField] private GameObject objTemp;
    public static GameObject _prefab;

    //TrailRenderer trailRenderer;
    LineRenderer lineRenderer;
    //ブラシテクスチャのドロップダウン
    [SerializeField]private Dropdown dropdown;
    //テクスチャのかくのうさき
    private string[] texfilepaths;

    private void Awake()
    {
        _prefab = objTemp;
        //trailRenderer = _prefab.GetComponent<TrailRenderer>();
        lineRenderer = _prefab.GetComponent<LineRenderer>();
        //こっからテクスチャドロップダウンの起動
        ReadFiles();
        for(int i=0; i<texfilepaths.Length; i++)
        {
            dropdown.options.Add(new Dropdown.OptionData { text = System.IO.Path.GetFileNameWithoutExtension(texfilepaths[i]) });
        }
        dropdown.value=9;
        brushtexChanger();
        
    }
    private void  Update(){
        
        picker.onValueChanged.AddListener( color =>
        {
            //trailRenderer.startColor = color;
            //trailRenderer.endColor = color;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            
        } );
    }
    //texファイル名の取得
    private void ReadFiles()
    {
        string path = Application.dataPath + "/" + "Main" + "/" + "Resources";
        texfilepaths = System.IO.Directory.GetFiles(path, "*.mat", System.IO.SearchOption.AllDirectories);
        
    } 
    //dropdownchangeの取得
    public void brushtexChanger() {
        Material mat = Resources.Load(System.IO.Path.GetFileNameWithoutExtension(texfilepaths[dropdown.value]),typeof(Material)) as Material;
        
        //trailRenderer.material=mat;
        lineRenderer.material=mat;
    }  
}
}
