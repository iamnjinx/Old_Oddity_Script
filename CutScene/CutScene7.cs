using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene7 : CutSceneBase
{
    // Global variables needed.
    [SerializeField] private DialogController dialogController;
    [SerializeField] private string situationID = "S_5_Cutscene";

    [SerializeField] private CAMSController camsController;

    [SerializeField] private InvestigatorController investigatorController;

    [SerializeField] private GameObject changeInv_instruction1;
    [SerializeField] private Transform Elvin;
    [SerializeField] private Transform Mysterious;
    [SerializeField] private List<GameObject> off_instruction;

    // Condition which needs to be satisfied for events.
    private bool Condition_Satisfied = false;
    bool is_Completed2 = false;

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
        // 34

        bool is_Completed1 = dialogController.CurSituation == null;

        if(dialogController.CurDialogID == 34){
            camsController.PartyIndicators[1].material = camsController.PI_Mats[1];
            camsController.PartyIndicators[2].material = camsController.PI_Mats[1];
        }

        // 완료 조건
        if(is_Completed1){
            changeInv_instruction1.SetActive(true);
            if(!investigatorController.investigators.Contains(Mysterious)){
                investigatorController.addInvestigator(Mysterious, false);
            }
            if(investigatorController.I_A == Elvin){
                return;
            }
            else{
                is_Completed2 = true;
            }
        }

        if(is_Completed2){
            Debug.Log("CutScene7 Finished");
            controller.SetNextCutScene();
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
        dialogController.dialogEnded();
    }
}
