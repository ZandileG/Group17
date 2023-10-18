using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent animationHit, animationHit2;

    public void TriggerAttack()
    {
        animationHit?.Invoke();
    }

    public void TiggerAttack2()
    {
        animationHit2?.Invoke();
    }
}
