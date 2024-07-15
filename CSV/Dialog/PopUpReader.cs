using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpReader: MonoBehaviour
{
    //string _myData;

    [SerializeField]
    private PopUpDB _db;
    //private List<string> entitiesName = new List<string>{};
    
    private void Awake() {
        // 실행 시 마다 "_DB"를 리셋
        _db.Entities = new Dictionary<string, PopUpDBEntity>();
        _db.EntityList = new List<PopUpDBEntity>();

        // ".csv" 파일 읽기 
        List<Dictionary<string, object>> _Data = CSVReader.Read("Pop_Ups");

        // 각 인덱스에 포함된 데이터 가져와서 "BattleStageDB" 에 추가
        foreach(var i in _Data){
            string mainID = i["PopUp_ID"].ToString();
            
            PopUpDBEntity _DBEntity = new PopUpDBEntity();
            _DBEntity.PopUpID = mainID;
            _DBEntity.CharID = i["Char_Name"].ToString();
            _DBEntity.KORText = i["Kor_Text"].ToString();
            _DBEntity.KORText = _DBEntity.KORText.Replace("^", ",");

            _db.EntityList.Add(_DBEntity);
            _db.Entities.Add(mainID, _DBEntity);
        }
    }

}
