using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene1 : CutSceneBase
{
    [SerializeField] private DialogController dialogController;
    [SerializeField] private CAMSController camsController;
    [SerializeField] private MainScreenController mainScreenController;
    [SerializeField] private TutorialController tutorialController;
    [SerializeField] private EventController eventController;
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private string situationID = "S_1_Cutscene";

    private bool Condition_Satisfied = false;

    public override void Enter()
    {
        CutSceneController.CutScene = true;
        Condition_Satisfied = true;
        StartScreen.SetActive(true);
        //mainScreenController.changeModeTo(true);
        dialogController.startDialog(situationID);
        eventController.EVENTS[0].SetActive(true);
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 해당 경우에는 조건 없이 무조건 수행.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }

        //Debug.Log("CutScene1");
        bool is_Completed = dialogController.CurSituation == null;

        if(dialogController.CurDialogID == 6 && !MainScreenController.MODE){
            tutorialController.tutorial_screen.SetActive(false);
            StartScreen.SetActive(false);
            camsController.SM_Dialog.SetActive(true);
            camsController.SM_Health.SetActive(true);
            camsController.SM_Sanity.SetActive(true);
            camsController.SM_Sanity_wave.SetActive(true);
            mainScreenController.changeModeTo(true);
        }

        // 완료 조건
        if(is_Completed){
            Debug.Log("CutScene1 Finished");
            controller.SetNextCutScene();
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
        dialogController.dialogEnded();
    }
}
