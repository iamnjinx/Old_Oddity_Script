using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene8 : CutSceneBase
{
    // Global variables needed.
    private Cases cases;
    [SerializeField] private int caseNum;
    [SerializeField] private DialogController dialogController;
    [SerializeField] private EventController eventController;

    // Condition which needs to be satisfied for events.
    private bool Condition_Satisfied = true;

    public override void Enter()
    {
        //CutSceneController.CutScene = true;
        Condition_Satisfied = true;
        cases = eventController.EVENTS[caseNum].GetComponent<Cases>();
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 조건 없이 무조건 수행하려면 Enter() 부분에 true 설정.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }
        
        bool is_Completed = cases.is_finished(null, false);

        if(cases.phase == 2){
            is_Completed = true;
        }
        //Debug.Log(is_Completed);

        // 완료 조건
        if(is_Completed){
            if(cases.phase == 2){
                if(cases.is_finished() && dialogController.CurSituation == null){
                    Debug.Log("CutScene8 Finished");
                    controller.SetNextCutScene();
                }
            }
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }
}
