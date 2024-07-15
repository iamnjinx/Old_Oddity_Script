using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Advice", menuName = "Scriptable Object/Advice", order = int.MaxValue)]
//출처: https://wergia.tistory.com/189 [베르의 프로그래밍 노트:티스토리]
public class AdviceDB : ScriptableObject
{
    public List<string> IO_data;
}

