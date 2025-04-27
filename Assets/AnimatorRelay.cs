using UnityEngine;

public class AnimatorRelay : MonoBehaviour
{
    private RootMotionMover rootMotionMover;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rootMotionMover = GetComponentInParent<RootMotionMover>();
    }

    void OnAnimatorMove()
    {
        if (rootMotionMover != null && animator.applyRootMotion)
        {
            rootMotionMover.OnAnimatorMoveRelay(animator);
        }
    }
}
