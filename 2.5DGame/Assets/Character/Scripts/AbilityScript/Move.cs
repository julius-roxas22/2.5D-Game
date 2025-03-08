using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Move")]
    public class Move : StateData
    {
        [SerializeField] private float movementSpeed;
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

            if (!VirtualInputManager._GetInstance._MoveRight && !VirtualInputManager._GetInstance._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (VirtualInputManager._GetInstance._MoveRight)
            {
                characterStateBase.GetCharacterControl(animator).transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                characterStateBase.GetCharacterControl(animator).transform.rotation = Quaternion.Euler(0f, 0, 0f);
            }

            if (VirtualInputManager._GetInstance._MoveLeft)
            {
                characterStateBase.GetCharacterControl(animator).transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                characterStateBase.GetCharacterControl(animator).transform.rotation = Quaternion.Euler(0f, 180, 0f);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }
    }
}


