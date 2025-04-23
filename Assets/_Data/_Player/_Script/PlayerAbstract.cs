using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// not attached to an object, used for loading needed components   
public abstract class PlayerAbstract : Load
{
    [SerializeField] protected ThirdPersonMovement thirdPersonMovement;
    [SerializeField] protected Animator playerAnimator;

    protected override void LoadComponent()
    {
        this.LoadThirdPersonMovement(); 
        this.LoadPlayerAnimator();
    }

    protected virtual void LoadThirdPersonMovement()
    {

    }

    protected virtual void LoadPlayerAnimator()
    {

    }
}
