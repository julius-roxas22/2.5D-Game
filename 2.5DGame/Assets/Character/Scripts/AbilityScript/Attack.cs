using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    [CreateAssetMenu(fileName = "New Ability Data", menuName = "IndieGamePractice/Create/Ability/Attack")]
    public class Attack : StateData
    {
        public float _StartAttackTime;
        public float _EndAttackTime;
        public List<string> _ColliderNames = new List<string>();
        public bool _MustCollide;
        public bool _MustFaceAttacker;
        public float _AttackRange;
        public int _MaxHits;
        public List<RuntimeAnimatorController> _DeathAnimators = new List<RuntimeAnimatorController>();
        private List<AttackInfo> _FinishedAttack = new List<AttackInfo>();

        public override void _OnEnterAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            animator.SetBool(TransitionParameters.Attack.ToString(), false);

            GameObject obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;

            AttackInfo info = obj.GetComponent<AttackInfo>();

            info._ResetAttackInfo(this, characterStateBase._GetCharacterControl(animator));

            if (!AttackManager._GetInstance._CurrentAttacks.Contains(info))
            {
                AttackManager._GetInstance._CurrentAttacks.Add(info);
            }
        }

        public override void _OnUpdateAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            registerAttack(characterStateBase._GetCharacterControl(animator), animatorStateInfo);
            deRegisterAttack(animatorStateInfo);
        }

        public override void _OnExitAbility(CharacterStateBase characterStateBase, Animator animator, AnimatorStateInfo animatorStateInfo)
        {
            clearAttacks();
        }

        private void registerAttack(CharacterControl control, AnimatorStateInfo animatorStateInfo)
        {
            if (_StartAttackTime <= animatorStateInfo.normalizedTime && _EndAttackTime > animatorStateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager._GetInstance._CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info._AttackAbility && !info._IsRegistered)
                    {
                        info._RegisterAttackInfo(this);
                    }
                }
            }
        }

        private void deRegisterAttack(AnimatorStateInfo animatorStateInfo)
        {
            if (animatorStateInfo.normalizedTime >= _EndAttackTime)
            {
                foreach (AttackInfo info in AttackManager._GetInstance._CurrentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info._AttackAbility && !info._IsFinished)
                    {
                        info._IsFinished = true;
                        Destroy(info.gameObject);
                    }
                }
            }
        }

        private void clearAttacks()
        {
            _FinishedAttack.Clear();

            foreach (AttackInfo info in AttackManager._GetInstance._CurrentAttacks)
            {
                if (this == info._AttackAbility && info._IsFinished)
                {
                    _FinishedAttack.Add(info);
                }
            }

            foreach (AttackInfo info in _FinishedAttack)
            {
                if (AttackManager._GetInstance._CurrentAttacks.Contains(info))
                {
                    AttackManager._GetInstance._CurrentAttacks.Remove(info);
                }
            }
        }

        public RuntimeAnimatorController _GetDeathAnimator()
        {
            int index = Random.Range(0, _DeathAnimators.Count);
            return _DeathAnimators[index];
        }
    }
}


