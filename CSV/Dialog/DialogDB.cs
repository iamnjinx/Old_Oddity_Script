using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Scriptable Object/Dialog", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class DialogDB : ScriptableObject
{
    public List<DialogDBEntity> EntityList;
    public Dictionary<string, DialogDBEntity> Entities;
}
