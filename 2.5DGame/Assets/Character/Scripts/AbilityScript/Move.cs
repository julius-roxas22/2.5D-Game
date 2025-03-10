using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Move")]
    public class Move : StateData
    {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float blockDistance;
        [SerializeField] private AnimationCurve speedGraph;

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            CharacterControl control = characterStateBase._GetCharacterControl(animator);

            if (control._Jump)
            {
                animator.SetBool(TransitionParameters.Jump.ToString(), true);
            }

            if (control._MoveRight && control._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (!control._MoveRight && !control._MoveLeft)
            {
                animator.SetBool(TransitionParameters.Move.ToString(), false);
                return;
            }

            if (control._MoveRight)
            {
                if (!checkFront(control))
                {
                    control.transform.Translate(Vector3.forward * movementSpeed * speedGraph.Evaluate(animatorStateInfo.normalizedTime) * Time.deltaTime);
                }
                control.transform.rotation = Quaternion.Euler(0f, 0, 0f);
            }

            if (control._MoveLeft)
            {
                if (!checkFront(control))
                {
                    control.transform.Translate(Vector3.forward * movementSpeed * speedGraph.Evaluate(animatorStateInfo.normalizedTime) * Time.deltaTime);
                }
                control.transform.rotation = Quaternion.Euler(0f, 180, 0f);
            }
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {

        }

        private bool checkFront(CharacterControl control)
        {
            foreach (GameObject obj in control._FrontSpheres)
            {
                Debug.DrawRay(obj.transform.position, control.transform.forward * blockDistance, Color.red);
                if (Physics.Raycast(obj.transform.position, control.transform.forward, out RaycastHit hit, blockDistance))
                {
                    return true;
                }
            }

            return false;
        }
    }
}


