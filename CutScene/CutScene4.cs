using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene4 : CutSceneBase
{
    // Global variables needed.
    [SerializeField] private DialogController dialogController;
    [SerializeField] private string situationID = "S_T1_Cutscene";


    // Condition which needs to be satisfied for events.
    private bool Condition_Satisfied = false;

    public override void Enter()
    {
        CutSceneController.CutScene = true;
        Condition_Satisfied = true;
        dialogController.startDialog(situationID);
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 조건 없이 무조건 수행하려면 Enter() 부분에 true 설정.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }

        bool is_Completed = dialogController.CurSituation == null;

        // 완료 조건
       if(is_Completed){
            Debug.Log("CutScene4 Finished");
            controller.SetNextCutScene();
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }
}
