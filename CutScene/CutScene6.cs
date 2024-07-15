using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene6 : CutSceneBase
{
    // Global variables needed.

    // Condition which needs to be satisfied for events.
    private bool Condition_Satisfied = false;

    [SerializeField] private DialogController dialogController;
    [SerializeField] private EventController eventController;
    [SerializeField] private MainScreenController mainScreenController;
    [SerializeField] private InvestigatorController investigatorController;
    [SerializeField] private EModeController eModeController;
    [SerializeField] private GameObject advice_instruction;
    [SerializeField] private string situationID = "S_T1_Cutscene_2";
    [SerializeField] private string adviceSituationID = "S_Advice_Elvin_1";
    [SerializeField] private Cases targetCase;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private GameObject adviceBlock;
    private bool is_Completed1 = false;
    private bool is_Completed2 = false;

    public override void Enter()
    {
        CutSceneController.CutScene = true;
        Condition_Satisfied = true;
        dialogController.startDialog(situationID);
        mainScreenController.changeModeTo(true);
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 조건 없이 무조건 수행하려면 Enter() 부분에 true 설정.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }

        if(dialogController.CurSituation == situationID){
            if(dialogController.CurDialogID == 4){
                // 모드 변경을 해야함
                DialogController.is_DIALOG = false;
                TutorialController.tutorial_changeMode = true;
                if(MainScreenController.MODE){
                    Debug.Log(MainScreenController.MODE);
                    return;
                }
                DialogController.is_DIALOG = true;
                dialogController.nextDialog();
            }

            else if(dialogController.CurDialogID == 5){
                // 벽 너머의 빛을 눌러야함.
                DialogController.is_DIALOG = false;
                if(eventController.Cases_Selected_EMode != targetCase){
                    return;
                }
                DialogController.is_DIALOG = true;
                dialogController.nextDialog();
            }

            else if(dialogController.CurDialogID == 6){
                DialogController.is_DIALOG = false;
                if(eModeController.FloatedEvidenceBlocks.Count <= 0){
                    return;
                }
                DialogController.is_DIALOG = true;
                dialogController.nextDialog();
            }

            else if(dialogController.CurDialogID == 9){
                DialogController.is_DIALOG = false;
                if(!adviceBlock.activeSelf){
                    return;
                }
                DialogController.is_DIALOG = true;
                dialogController.nextDialog();
            }
        }
        else if(dialogController.CurSituation == adviceSituationID){
            advice_instruction.SetActive(false);
            is_Completed1 = true;
        }
        else{
            if(!is_Completed1){
                advice_instruction.SetActive(true);
            }
        }

        if(dialogController.CurSituation == null && is_Completed1){
            mainScreenController.changeModeTo(true);
            investigatorController.PP_A.GetComponent<PP>().PP_Target.position = targetPosition;
            investigatorController.SetInvestigatorTargetTransform(investigatorController.PP_A.GetComponent<PP>().PP_Target, true);
            is_Completed2 = true;
        }

        // 완료 조건

        // 슈뢰딩거가 콘솔 쪽으로 이동했을 경우.
        Debug.Log((investigatorController.I_A.position, targetPosition));
        if(is_Completed2 && Vector3.Distance(investigatorController.I_A.position, targetPosition) < 2){
            Debug.Log("CutScene6 Finished");
            controller.SetNextCutScene();
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }
}
