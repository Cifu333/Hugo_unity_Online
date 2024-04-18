using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;

    private bool isJumping = false;
    private bool isShooting = false;
    private void Update()
    {

        //if (InputManagers.inputManagerInstance.GetJump() == 0)
        //{
        //    isJumping = true;
        //}
        //else
        //{
        //    isJumping = false;
        //}


        //if (InputManagers.inputManagerInstance.GetShoot() == 0)
        //{
        //    isShooting = true;
        //}
        //else
        //{
        //    isShooting = false;
        //}

      //  m_Animator.SetBool("IsMoving", InputManagers.inputManagerInstance.GetLeftAxisPressed());

        //m_Animator.SetBool("IsJumping", isJumping);

        //m_Animator.SetBool("IsShooting", isShooting);


    }
}