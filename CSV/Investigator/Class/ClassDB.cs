using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Class", menuName = "Scriptable Object/Class", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class ClassDB : ScriptableObject
{
    public List<ClassDBEntity> EntityList;
    public Dictionary<string, ClassDBEntity> Entities;
}
