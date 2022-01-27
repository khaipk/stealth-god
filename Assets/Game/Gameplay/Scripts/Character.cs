using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AnimateIdle()
    {
        animator.CrossFade("Idle", 0.1f);
    }

    internal void AnimateWalk()
    {
        animator.CrossFade("Sneak", 0.1f);
    }

    internal void AnimateRun()
    {
        animator.CrossFade("Run", 0.1f);
    }

    internal void AnimateAttack()
    {
        animator.CrossFade("Attack", 0.1f);
    }
}
