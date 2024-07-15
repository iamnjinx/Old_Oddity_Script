using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene5 : CutSceneBase
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
        //Debug.Log(is_Completed);

        // 완료 조건
        if(cases.phase == 2){
            List<bool> condition1 = new List<bool>{true, false, true, false};
            List<bool> condition2 = new List<bool>{true, false, false, true};
            Debug.Log((cases.phase, cases.is_finished(), cases.is_finished(condition1), cases.is_finished(condition2), dialogController.CurSituation == null));
            if((cases.is_finished() || cases.is_finished(condition1) || cases.is_finished(condition2)) && dialogController.CurSituation == null){
                cases.nextPhase();
                Debug.Log("CutScene5 Finished");
                controller.SetNextCutScene();
            }
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }
}
