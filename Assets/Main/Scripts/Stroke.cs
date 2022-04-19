using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiCa{
public class Stroke : MonoBehaviour
{
    public GameObject LineObject=null;

    public void Addvalue(GameObject LineObjectPrefab, int value){
        LineObject = Instantiate(LineObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        
        LineObject.layer = 9 + value;
    }

}}
