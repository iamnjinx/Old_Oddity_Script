using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene9 : CutSceneBase
{
    // Global variables needed.
    [SerializeField] private DialogController dialogController;
    [SerializeField] private EventController eventController;
    [SerializeField] private InvestigatorController investigatorController;
    [SerializeField] private string situationID = "S_6_Cutscene";
    [SerializeField] private Vector3 targetPos;

    [SerializeField] private DemoScript demoScript;

    [SerializeField] private MB_Door mB_Door;

    [SerializeField] private GameObject portrait;

    public Transform Mysterious;

    // Condition which needs to be satisfied for events.
    private bool Condition_Satisfied = true;

    private bool is_Completed = false;
    private bool is_Completed2 = false;
    private bool is_Completed3 = false;

    public override void Enter()
    {
        //CutSceneController.CutScene = true;
        //Condition_Satisfied = true;
        TutorialController.tutorial_changeMode = false;
        TutorialController.tutorial_playerMove = false;
        TutorialController.tutorial_scan = false;
        FindObjectOfType<MainScreenController>().changeModeTo(true);
        StartCoroutine(next());
    }

    IEnumerator next(){
        mB_Door.openclose(true);
        yield return new WaitForSeconds(3f);
        investigatorController.PP_A.GetComponent<PP>().PP_Target.position = targetPos;
        investigatorController.SetInvestigatorTargetTransform(investigatorController.PP_A.GetComponent<PP>().PP_Target, true);
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 조건 없이 무조건 수행하려면 Enter() 부분에 true 설정.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }

        if(is_Completed3){
            return;
        }


        if(Vector3.Distance(targetPos, investigatorController.I_A.position) < 1 && !is_Completed){
            is_Completed = true;
            dialogController.startDialog(situationID);
        }

        if(is_Completed && dialogController.CurSituation == null){
            AudioManager.instance.PlaySfx(AudioManager.Sfx.gunshot);
            is_Completed2 = true;
        }
        else if(is_Completed && dialogController.CurDialogID == 0){
            portrait.SetActive(false);
        }
        else if(is_Completed && dialogController.CurDialogID == 1){
            portrait.SetActive(true);
        }

        //Debug.Log(is_Completed);

        // 완료 조건
        if(is_Completed2){
            Debug.Log("CutScene9 Finished");
            demoScript.DemoEnded();
            is_Completed3 = true;
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }
}
