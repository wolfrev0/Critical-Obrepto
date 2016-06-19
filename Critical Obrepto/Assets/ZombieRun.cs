using UnityEngine;
using System.Collections;

public class ZombieRun : StateMachineBehaviour
{
    NavMeshAgent nma = null;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(nma==null)
            nma = animator.GetComponent<NavMeshAgent>();
        nma.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nma.SetDestination(PlayerHandler.instance.transform.position);

        if (Vector3.Distance(PlayerHandler.instance.transform.position, animator.transform.position) >= 25 || PlayerHandler.instance.died)
        {
            animator.SetBool("Trace", false);
        }
        if (Vector3.Distance(PlayerHandler.instance.transform.position, animator.transform.position) < 1.5f && PlayerHandler.instance.died==false)
        {
            animator.SetBool("Attack", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        nma.enabled = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
