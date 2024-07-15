using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PopUp", menuName = "Scriptable Object/PopUp", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class PopUpDB : ScriptableObject
{
    public List<PopUpDBEntity> EntityList;
    public Dictionary<string, PopUpDBEntity> Entities;
}
