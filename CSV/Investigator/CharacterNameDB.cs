using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Name", menuName = "Scriptable Object/Character Name", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class CharacterNameDB : ScriptableObject
{
    public List<CharacterNameDBEntity> Entities;
}
