using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InvestigatorInputController : MonoBehaviour
{
    // playerInput 관련
    PlayerInput playerInput;
    [SerializeField] List<InputAction> i_Action;
    //float actionID = 0;

 
    private bool Sbutton_isPressed = false;
    private float SbuttonElapsedTime = 0;
    [SerializeField] private float tapTime;
    [SerializeField] private float holdTime;

    InvestigatorController investigatorController;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        investigatorController = FindObjectOfType<InvestigatorController>();
    }

    private void Start() {
        i_Action.Add(playerInput.actions.FindAction("I1"));
        i_Action.Add(playerInput.actions.FindAction("I2"));
        i_Action.Add(playerInput.actions.FindAction("I3"));
        i_Action.Add(playerInput.actions.FindAction("Gather"));
    }

    // Update is called once per frame
    void Update()
    {
        changeInvestigatorA();
        
        if(MainScreenController.MODE){
            DivideOrGatherInvestigator();
        }
    }

    // 십자키(아래 키 제외) 혹은 WAD를 누를 시, 해당하는 값의 조사자로 조사자A 변경.
    private void changeInvestigatorA(){
        if(i_Action[0].ReadValue<float>() != 0 && investigatorController.currentInvestigatorNum >= 1){
            investigatorController.changeInvestigatorA(0);
        }
        else if(i_Action[1].ReadValue<float>() != 0 && investigatorController.currentInvestigatorNum >= 2){
            investigatorController.changeInvestigatorA(1);
        }
        else if(i_Action[2].ReadValue<float>() != 0 && investigatorController.currentInvestigatorNum >= 3){
            investigatorController.changeInvestigatorA(2);
        }
    }

    // 아래 십자키 / S -> 분리(탭) or 집합(홀드)
    private void DivideOrGatherInvestigator(){
        // 버튼이 눌려있는 상태 + 눌린 시간이 holdTime 이상이라면.
        if(SbuttonElapsedTime >= holdTime && Sbutton_isPressed){
            investigatorController.gatherInvestigators();
            Sbutton_isPressed = false;
        }
        // 버튼을 누르고 있음.
        else if(i_Action[3].ReadValue<float>() != 0){
            // 눌린 시간이 0이라면, Sbutton_isPressed = True
            if(SbuttonElapsedTime == 0) Sbutton_isPressed = true;
            // Sbutton_isPressed = True 라면, 눌린 시간 계속 증가.
            if(Sbutton_isPressed)SbuttonElapsedTime += Time.deltaTime;
        }
        // Sbutton_isPressed 상태에서 버튼을 땜 + 버튼을 누른 시간이 0 초과, tapTime 이하라면.
        else if(SbuttonElapsedTime <= tapTime && SbuttonElapsedTime > 0 && Sbutton_isPressed){
            investigatorController.divideInvestigator();
            SbuttonElapsedTime = 0;
        }
        // 걍 버튼을 누르고 있지 않는 상황이라면, Sbutton_isPressed = false, 누른 시간 = 0.
        else{
            Sbutton_isPressed = false;
            SbuttonElapsedTime = 0;
        }
        // */
    }
}
