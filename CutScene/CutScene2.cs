using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene2 : CutSceneBase
{
    // Global variables needed.
    private Cases cases;
    [SerializeField] private int caseNum;
    [SerializeField] private EventController eventController;
    [SerializeField] private DialogController dialogController;
    [SerializeField] private CAMSController camsController;

    // Condition which needs to be satisfied for events.
    private bool Condition_Satisfied = true;

    public override void Enter()
    {
        //Condition_Satisfied = true;
        cases = eventController.EVENTS[caseNum].GetComponent<Cases>();
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 조건 없이 무조건 수행하려면 Enter() 부분에 true 설정.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }


        bool is_Completed = cases.is_finished();

        // 완료 조건

        if(is_Completed && dialogController.CurSituation == null){
            Debug.Log("CutScene2 Finished");
            controller.SetNextCutScene();
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }
}
