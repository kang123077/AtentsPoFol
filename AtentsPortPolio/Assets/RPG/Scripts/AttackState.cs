using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class AttackState : StateMachineBehaviour
{
    // StateMachineBehaviour를 상속했기 때문에, 컴포넌트로는 쓸 수 없음

    public bool isMachine = false;

    // 해당 상태로 진입 할 때 사용하는 함수
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isMachine)
        {
            animator.SetBool("isAttacking", true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!isMachine)
        {
            animator.SetBool("isAttacking", false);
        }
        
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        animator.SetBool("isAttacking", true);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        animator.SetBool("isAttacking", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
