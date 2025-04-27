using UnityEngine;

public class RootMotionMover : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator == null) return;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.IsTag("RootMotion"))
        {
            animator.applyRootMotion = true;
        }
        else
        {
            animator.applyRootMotion = false;
        }
    }

    public void OnAnimatorMoveRelay(Animator animator)
    {
        // Called from the child's OnAnimatorMove()
        transform.position += animator.deltaPosition;
        transform.rotation *= animator.deltaRotation;
    }
}
