using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class Investigators : MonoBehaviour
{
    public InvestigatorDB investigatorDB;

    private NavMeshAgent agent;

    private Vector3 previousVelocity = Vector3.zero;

    [SerializeField] private Transform particleTransform;
    private float particle_first_y;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();  
    }

    private void Start() {
        agent.destination = transform.position;
        particle_first_y = particleTransform.position.y;
        StartCoroutine(moveParticle());
    }
    
    public void setTargetPosition(Vector3 t_pos, float waitTime){
        StartCoroutine(setTP(t_pos, waitTime));
    }

    private IEnumerator moveParticle(){
        while(true){
            particleTransform.DOMoveY(particle_first_y + .25f, 1f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(1.1f);
            particleTransform.DOMoveY(particle_first_y - .25f, 1f).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(1.1f);
        }
    }

    private IEnumerator setTP(Vector3 t_pos, float waitTime = 0){
        if(waitTime > 0){
            yield return new WaitForSeconds(waitTime);
        }
        //Debug.Log("move");
        agent.destination = t_pos;
    }

    public void setStoppingDistance(float dist){
        agent.stoppingDistance = dist;
    }

    // 조사자 AI 이동 일시정지.
    public void pauseAI(){
        previousVelocity = agent.velocity;
        agent.velocity = Vector3.zero; 
        agent.isStopped = true;
        // 위 문장 노란 밑줄 그어져서 일단은 주석 처리. 노란 밑줄이 있어도 구동에는 전혀 이상 없음.
    }

    // 조사자 AI 이동 재개.
    public void resumeAI(){
        agent.velocity = previousVelocity;
        agent.isStopped = false;
        // 위 문장 노란 밑줄 그어져서 일단은 주석 처리. 노란 밑줄이 있어도 구동에는 전혀 이상 없음.
    }
}
