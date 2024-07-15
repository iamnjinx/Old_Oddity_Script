using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReader: MonoBehaviour
{
    //string _myData;

    [SerializeField]
    private CharacterDB _db;
    //private List<string> entitiesName = new List<string>{};
    
    private void Awake() {
        // 실행 시 마다 "_DB"를 리셋
        _db.Entities = new Dictionary<string, CharacterDBEntity>();
        _db.EntityList = new List<CharacterDBEntity>();

        // ".csv" 파일 읽기 
        List<Dictionary<string, object>> _Data = CSVReader.Read("Characters");

        // 각 인덱스에 포함된 데이터 가져와서 "BattleStageDB" 에 추가
        foreach(var i in _Data){
            string mainID = i["CN_ID"].ToString();
            
            CharacterDBEntity _DBEntity = new CharacterDBEntity();
            _DBEntity.Character_ID = mainID;
            _DBEntity.Character_Name = i["CN Name"].ToString();

            _db.EntityList.Add(_DBEntity);
            _db.Entities.Add(mainID, _DBEntity);
        }
    }

}
