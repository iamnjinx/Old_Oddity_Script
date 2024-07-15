using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene3 : CutSceneBase
{
    // Global variables needed.
    [SerializeField] private DialogController dialogController;
    [SerializeField] private CAMSController camsController;
    [SerializeField] private MeshRenderer Cash;
    [SerializeField] private string situationID = "S_3_Cutscene";
    [SerializeField] private EventController eventController;
    [SerializeField] private GameObject nextPingobj;
    [SerializeField] private GameObject CashNoise;


    // Condition which needs to be satisfied for events.
    private bool Condition_Satisfied = false;
    bool is_Completed2 = false;
    bool is_noise = false;

    public override void Enter()
    {
        CutSceneController.CutScene = true;
        Condition_Satisfied = true;
        eventController.EVENTS[1].SetActive(true);
        dialogController.startDialog(situationID);
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 조건 없이 무조건 수행하려면 Enter() 부분에 true 설정.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }

        bool is_Completed1 = dialogController.CurSituation == null;

        if(dialogController.CurDialogID == 33){
            // 노이즈 넣기
            if(!is_noise){
                StartCoroutine(cashNoiseOn());
                camsController.SM_Calc.SetActive(true);
                is_noise = true;
            }
        }

        // 완료 조건

        if(is_Completed1){
            DialogController.is_DIALOG = false;
            TutorialController.tutorial_scan = true;
        }

        if(nextPingobj.activeSelf){
            is_Completed2 = true;
            DialogController.is_DIALOG = true;
            TutorialController.tutorial_playerMove = true;
        }

        if(is_Completed2){
            Debug.Log("CutScene3 Finished");
            controller.SetNextCutScene();
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }

    IEnumerator cashNoiseOn(){
        CashNoise.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        CashNoise.SetActive(false);
    }
}
