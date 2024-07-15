using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassReader : MonoBehaviour
{
    //string _myData;

    [SerializeField]
    private ClassDB _classdb;
    //private List<string> entitiesName = new List<string>{};
    
    private void Start() {
        // 실행 시 마다 "BattleStageDB"를 리셋
        _classdb.Entities = new Dictionary<string, ClassDBEntity>();
        _classdb.EntityList = new List<ClassDBEntity>();

        // "WAM_battle_stage.csv" 파일 읽기 
        List<Dictionary<string, object>> EventData = CSVReader.Read("Class");

        // 각 인덱스에 포함된 데이터 가져와서 "BattleStageDB" 에 추가
        foreach(var i in EventData){
            //_eventdb.EntityNames.Add(i["Event_ID"].ToString());
            ClassDBEntity classDBEntity = new ClassDBEntity();
            classDBEntity.Class_Name = i["Class_Name"].ToString();
            classDBEntity.Insight = (int)i["Insight"];
            classDBEntity.Dexerity = (int)i["Dexerity"];
            classDBEntity.Intelligence = (int)i["Intelligence"];
            classDBEntity.Athletic = (int)i["Athletic"];

            _classdb.EntityList.Add(classDBEntity);
            _classdb.Entities.Add(i["Class_ID"].ToString(), classDBEntity);
        }
    }  
}
