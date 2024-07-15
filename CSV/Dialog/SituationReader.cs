using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SituationReader: MonoBehaviour
{
    //string _myData;

    [SerializeField]
    private SituationDB _db;
    //private List<string> entitiesName = new List<string>{};
    
    private void Awake() {
        // 실행 시 마다 "_DB"를 리셋
        _db.Entities = new Dictionary<string, SituationDBEntity>();
        _db.EntityList = new List<SituationDBEntity>();

        // ".csv" 파일 읽기 
        List<Dictionary<string, object>> _Data = CSVReader.Read("Situations");

        // 각 인덱스에 포함된 데이터 가져와서 "BattleStageDB" 에 추가

        List<string> names = new List<string>();

        SituationDBEntity dBEntity = new SituationDBEntity();

        foreach(var i in _Data){
            string mainID = i["Situation_ID"].ToString();

            if(mainID != ""){
                if(names.Count > 0){
                    _db.EntityList.Add(dBEntity);
                    _db.Entities.Add(dBEntity.Situation_ID, dBEntity);
                    dBEntity = new SituationDBEntity();
                }
                names.Add(mainID);
                dBEntity.Situation_ID = mainID;
            }
            dBEntity.KorText.Add(i["Dialogue_KOR"].ToString().Replace("^", ","));
            dBEntity.CharID.Add(i["Char_Name"].ToString());
        }
    }
}
