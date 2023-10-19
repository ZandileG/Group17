using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAnimationHelper : MonoBehaviour
{
    public UnityEvent animationAttack1, animationAttack2, animationAttack3, animationAttack4, animationAttack5, animationAttack6;

    public void TriggerAttack1()
    {
        animationAttack1?.Invoke();
    }
    public void TriggerAttack2()
    {
        animationAttack2?.Invoke();
    }
    public void TriggerAttack3()
    {
        animationAttack3?.Invoke();
    }
    public void TriggerAttack4()
    {
        animationAttack4?.Invoke();

    }
    public void TriggerAttack5()
    {
        animationAttack5?.Invoke();
    }
    public void TriggerAttack6()
    {
        animationAttack6?.Invoke();
    }
}

