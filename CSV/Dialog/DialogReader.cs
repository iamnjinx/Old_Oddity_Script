using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogReader: MonoBehaviour
{
    //string _myData;

    [SerializeField]
    private DialogDB _db;
    //private List<string> entitiesName = new List<string>{};
    
    private void Awake() {
        // 실행 시 마다 "_DB"를 리셋
        _db.Entities = new Dictionary<string, DialogDBEntity>();
        _db.EntityList = new List<DialogDBEntity>();

        // ".csv" 파일 읽기 
        List<Dictionary<string, object>> _Data = CSVReader.Read("Dialog");

        // 각 인덱스에 포함된 데이터 가져와서 "BattleStageDB" 에 추가

        List<string> names = new List<string>();

        foreach(var i in _Data){
            string mainID = i["Dialogue_ID"].ToString();

            if(names.Contains(mainID)){
                continue;
            }
            else{
                names.Add(mainID);
            }
            //_eventdb.EntityNames.Add(i["Event_ID"].ToString());

            DialogDBEntity dBEntity = new DialogDBEntity();

            dBEntity.Dialog_ID = mainID;
            dBEntity.Char_ID = i["Character_Name"].ToString();
            string tempText = i["KOR_Text"].ToString();
            tempText = tempText.Replace("^", ",");
            dBEntity.KOR_Text = tempText;

            _db.EntityList.Add(dBEntity);
            _db.Entities.Add(mainID, dBEntity);
        }
    }

}
