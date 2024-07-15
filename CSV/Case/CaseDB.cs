using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Case", menuName = "Scriptable Object/Case", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class CaseDB : ScriptableObject
{
    public CaseDBEntity caseDBEntity;
}
