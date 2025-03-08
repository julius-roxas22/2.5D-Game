using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Idle")]
    public class Idle : StateData
    {
        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            if (VirtualInputManager._GetInstance._MoveRight && VirtualInputManager._GetInstance._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (VirtualInputManager._GetInstance._MoveRight)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
            }

            if (VirtualInputManager._GetInstance._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), true);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


