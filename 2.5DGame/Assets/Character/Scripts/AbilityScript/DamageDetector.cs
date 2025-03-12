using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public class DamageDetector : MonoBehaviour
    {
        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        void Update()
        {
            if (AttackManager._GetInstance._CurrentAttacks.Count > 0)
            {
                checkAttack();
            }
        }

        private void checkAttack()
        {
            foreach (AttackInfo info in AttackManager._GetInstance._CurrentAttacks)
            {
                if (null == info)
                {
                    continue;
                }

                if (!info._IsRegistered)
                {
                    continue;
                }

                if (info._IsFinished)
                {
                    continue;
                }

                if (info._CurrentHits >= info._MaxHits)
                {
                    continue;
                }

                if (info._Attacker == control)
                {
                    return;
                }

                if (info._MustCollide)
                {
                    if (isCollided(info))
                    {
                        takeDamage(info);
                    }
                }
            }
        }

        private bool isCollided(AttackInfo info)
        {
            foreach (TriggerDetector trigger in control._GetAllTriggers())
            {
                foreach (Collider col in trigger._CollidingParts)
                {
                    foreach (string colNames in info._ColliderNames)
                    {
                        if (colNames == col.name)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void takeDamage(AttackInfo info)
        {
            Debug.Log(control.name + " hit by " + info._Attacker.name);
            control._SkinnedMesh.runtimeAnimatorController = info._AttackAbility._GetDeathAnimator();
            info._CurrentHits++;
            control.GetComponent<BoxCollider>().enabled = false;
            control._GetRigidBody.useGravity = false;
        }
    }
}


