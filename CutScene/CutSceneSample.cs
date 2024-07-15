using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneSample : CutSceneBase
{
    // Global variables needed.

    // Condition which needs to be satisfied for events.
    private bool Condition_Satisfied = false;

    public override void Enter()
    {
        CutSceneController.CutScene = true;
        //Condition_Satisfied = true;
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 조건 없이 무조건 수행하려면 Enter() 부분에 true 설정.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }

        //bool is_Completed = ;

        // 완료 조건

        /* example

        if(is_Completed){
            controller.SetNextCutScene();
        }

        */
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }
}
