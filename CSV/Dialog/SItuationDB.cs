using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Situations", menuName = "Scriptable Object/Situations", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class SituationDB : ScriptableObject
{
    public List<SituationDBEntity> EntityList;
    public Dictionary<string, SituationDBEntity> Entities;
}
