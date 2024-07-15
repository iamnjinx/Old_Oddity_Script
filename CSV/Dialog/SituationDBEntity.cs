using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SituationDBEntity
{
    public string Situation_ID;
    public List<string> KorText = new List<string>{};
    public List<string> CharID = new List<string>{};
}
