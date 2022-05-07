using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
    [SerializeField]private Dropdown modedropdown;
    Material mat;
    //テクスチャのかくのうさき
    private string[] texfilepaths;
    public enum Mode {
        Fade,
        Additive,
        Subtractive,
        Modulate,
    }
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
        //modedropdown.value = 0;

        brushtexChanger();
        //brushmodeChanger();
        
    }
    private void  Update(){
        
        picker.onValueChanged.AddListener( color =>
        {
            //trailRenderer.startColor = color;
            //trailRenderer.endColor = color;
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            //lineRenderer.sharedMaterial.color = color;
            
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
        mat = Resources.Load(System.IO.Path.GetFileNameWithoutExtension(texfilepaths[dropdown.value]),typeof(Material)) as Material;
        
        //trailRenderer.material=mat;
        lineRenderer.material=mat;
    }  

    public void brushmodeChanger() {
        mat = lineRenderer.material;
        if(modedropdown.value==0){
            SetBlendMode(mat,Mode.Fade);
        }else if(modedropdown.value==1){
            SetBlendMode(mat,Mode.Additive);
        }else if(modedropdown.value==3){
            SetBlendMode(mat,Mode.Modulate);
        }else if(modedropdown.value==4){
            SetBlendMode(mat,Mode.Subtractive);
        }
        lineRenderer.material=mat;

    }
    public static void SetBlendMode(Material material, Mode blendMode) {
        material.SetFloat("_Mode", (float)blendMode);  // <= これが必要

        switch (blendMode) {

            case Mode.Fade:
                material.SetOverrideTag("RenderType", "Fade");
                break;

            case Mode.Additive:
                material.SetOverrideTag("RenderType", "Additive");
                break;
            case Mode.Subtractive:
                material.SetOverrideTag("RenderType", "Subtractive");
                break;
            case Mode.Modulate:
                material.SetOverrideTag("RenderType", "Modulate");
                break;
        }
        
    }
}
}
