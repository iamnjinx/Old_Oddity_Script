using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceReader: MonoBehaviour
{
    //string _myData;

    [SerializeField]
    private ChoiceDB _db;
    //private List<string> entitiesName = new List<string>{};
    
    private void Awake() {
        // 실행 시 마다 "_DB"를 리셋
        _db.Entities = new Dictionary<string, ChoiceDBEntity>();
        _db.EntityList = new List<ChoiceDBEntity>();

        // ".csv" 파일 읽기 
        List<Dictionary<string, object>> _Data = CSVReader.Read("Choices");

        // 각 인덱스에 포함된 데이터 가져와서 "BattleStageDB" 에 추가
        foreach(var i in _Data){
            string mainID = i["Choice_ID"].ToString();
            
            ChoiceDBEntity _DBEntity = new ChoiceDBEntity();
            _DBEntity.Choice_ID = mainID;
            _DBEntity.type = int.Parse(i["Type"].ToString());
            _DBEntity.Base_Text = i["Base_Text"].ToString();
            _DBEntity.Success = i["Success"].ToString();
            _DBEntity.Failure = i["Failure"].ToString();
            _DBEntity.Hover_Text = i["Hover_Text"].ToString();

            _db.EntityList.Add(_DBEntity);
            _db.Entities.Add(mainID, _DBEntity);
        }
    }

}
