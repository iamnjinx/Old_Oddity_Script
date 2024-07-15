using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP : MonoBehaviour
{
    public Transform PP_Target;
    private void Awake() {
        if(transform.childCount > 0){
            PP_Target.position = transform.GetChild(0).position;
        }
    }
}
