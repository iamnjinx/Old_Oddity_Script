using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using DG.Tweening;

public class CutScene0 : CutSceneBase
{
    [SerializeField] private GameObject dialogMS;
    [SerializeField] private TextMeshProUGUI LoadingText;
    [SerializeField] private GameObject LogoObj;
    [SerializeField] private GameObject Particle;
    [SerializeField] private AudioSource loadingSound;
    [SerializeField] private GameObject ObserverObj;
    [SerializeField] private Image Fade;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject videoCover;

    // Other Scripts
    [SerializeField] private MainScreenController mainScreenController;
    [SerializeField] private CAMSController camsController;
    //[SerializeField] private AudioController audioController;
    [SerializeField] private TutorialController tutorialController;
    [SerializeField] private EventController eventController;

    //private Sprite selectedSprite = null;


    private bool Condition_Satisfied = false;
    private bool is_Completed1 = false;
    private bool is_Completed2 = false;
    private bool is_Completed3 = false;

    public override void Enter()
    {
        CutSceneController.CutScene = true;
        mainScreenController.changeModeTo(false);
        tutorialController.tutorial_screen.SetActive(true);
        eventController.EVENTS[0].SetActive(false);
        eventController.EVENTS[1].SetActive(false);
        camsController.SM_Dialog.SetActive(false);
        camsController.SM_Health.SetActive(false);
        camsController.SM_Sanity.SetActive(false);
        camsController.SM_Sanity_wave.SetActive(false);
        camsController.SM_Calc.SetActive(false);
        Condition_Satisfied = true;
        StartCoroutine(startVideo());
    
    }

    public override void Execute(CutSceneController controller){
        // 시작 조건
        // 조건 없이 무조건 수행하려면 Enter() 부분에 true 설정.

        // 시작 조건 만족 못하면 진행 x
        if(!Condition_Satisfied){
            return;
        }
        

        if(is_Completed1){
            if(Input.GetKeyDown("space")){
                ObserverObj.SetActive(false);
                is_Completed2 = true;
                is_Completed1 = false;
            }
        }
        //is_Completed1 = selectedSprite != null;

        // 완료 조건
        if(is_Completed2){
            StartCoroutine(LoadingToStart());
            is_Completed2 = false;
        }
        else if(is_Completed3){
            controller.SetNextCutScene();
        }
    }

    public override void Exit(){
        CutSceneController.CutScene = false;
    }

    private IEnumerator startVideo(){
        videoPlayer.Prepare(); 
        while (!videoPlayer.isPrepared){
            Debug.Log("what");
            yield return new WaitForEndOfFrame(); 
        }
        videoPlayer.frame = 0;
        yield return new WaitForSeconds(3f);
        videoPlayer.Play();
        yield return new WaitForSeconds(0.1f);
        videoCover.SetActive(false);

        LogoObj.SetActive(true);
        StartCoroutine(LogoToFade());
    }

    private IEnumerator LogoToFade(){
        yield return new WaitForSeconds(3f);
        RawImage tempImg = LogoObj.transform.GetComponent<RawImage>();
        //LogoObj.SetActive(false);
        for(float i = 1; i >= 0; i -= Time.deltaTime/2){
            Color tempColor1 = Color.HSVToRGB(0,0,i);
            Color tempColor2 = Fade.color;
            tempColor2.a = (1-i) * 0.67f;
            Fade.color = tempColor2;
            tempImg.color = tempColor1;
            yield return new WaitForSeconds(0.01f);
        }
        //LogoObj.SetActive(false);

        yield return new WaitForSeconds(3f);

        string tempString = "연결중";
        for(int i = 0; i < tempString.Length; i++){
            yield return new WaitForSeconds(0.2f);
            AudioManager.instance.PlayUI(AudioManager.UI.Hover);
            LoadingText.text += tempString[i];
        }

        for(int i = 0; i < 3; i++){
            yield return new WaitForSeconds(0.2f);
            tempString = "연결중";
            for(int j = 0; j < 3; j++){
                tempString += ".";
                AudioManager.instance.PlayUI(AudioManager.UI.Hover);
                LoadingText.text = tempString;
                yield return new WaitForSeconds(0.5f);
            }
        }

        LoadingText.text = "";
        string tempString0 = "정보 부족";
        for(int i = 0; i < tempString0.Length; i++){
            yield return new WaitForSeconds(0.2f);
            AudioManager.instance.PlayUI(AudioManager.UI.Hover);
            LoadingText.text += tempString0[i];
        }

        yield return new WaitForSeconds(3f);
        Particle.SetActive(true);
        ObserverObj.SetActive(true);

        is_Completed1 = true;
    }

    private IEnumerator LoadingToStart(){
        LoadingText.text = "";
        StartCoroutine(mainScreenController.Scanning());
        //audioController.audioSource.clip = loadingSound;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Loading);
        string tempString = "신호 감지됨";
        for(int i = 0; i < tempString.Length; i++){
            yield return new WaitForSeconds(0.2f);
            AudioManager.instance.PlayUI(AudioManager.UI.Hover);
            LoadingText.text += tempString[i];
        }

        string tempString2 = "";
        for(int i = 0; i < 3; i++){
            yield return new WaitForSeconds(0.2f);
            tempString2 = "";
            for(int j = 0; j < 3; j++){
                yield return new WaitForSeconds(0.5f);
                tempString2 += ".";
                AudioManager.instance.PlayUI(AudioManager.UI.Hover);
                LoadingText.text = tempString + tempString2;
            }
        }

        camsController.PartyIndicators[0].material = camsController.PI_Mats[0];
        LoadingText.text = "";
        string tempString3 = "조사자 연결됨";
        for(int i = 0; i < tempString3.Length; i++){
            yield return new WaitForSeconds(0.2f);
            AudioManager.instance.PlayUI(AudioManager.UI.Hover);
            LoadingText.text += tempString3[i];
        }
        yield return new WaitForSeconds(3f);
        dialogMS.SetActive(true);
        Transform tempTransform = dialogMS.transform.GetChild(0).GetChild(0);
        tempTransform.localScale = new Vector3(tempTransform.localScale.x, 0, tempTransform.localScale.z);
        tempTransform.DOScaleY(1, 0.3f);
        LoadingText.text = "";
        Particle.SetActive(false);
        is_Completed3 = true;
    }
}
