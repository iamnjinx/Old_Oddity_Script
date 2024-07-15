using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutSceneBase : MonoBehaviour
{
    public abstract void Enter();

    public abstract void Execute(CutSceneController controller);

    public abstract void Exit();
}
