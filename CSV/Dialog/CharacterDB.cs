using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Characters", menuName = "Scriptable Object/Characters", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class CharacterDB : ScriptableObject
{
    public List<CharacterDBEntity> EntityList;
    public Dictionary<string, CharacterDBEntity> Entities;
}
