using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choice", menuName = "Scriptable Object/Choice", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class ChoiceDB : ScriptableObject
{
    public List<ChoiceDBEntity> EntityList;
    public Dictionary<string, ChoiceDBEntity> Entities;
}
