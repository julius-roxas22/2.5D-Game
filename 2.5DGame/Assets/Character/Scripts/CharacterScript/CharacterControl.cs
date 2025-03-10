using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieGamePractice
{
    public enum TransitionParameters
    {
        Move,
        Jump,
        ForceTransition,
        Grounded
    }

    public class CharacterControl : MonoBehaviour
    {
        public Animator _SkinnedMesh;
        public List<GameObject> _BottomSpheres = new List<GameObject>();
        public List<GameObject> _FrontSpheres = new List<GameObject>();

        public bool _MoveRight;
        public bool _MoveLeft;
        public bool _Jump;

        private Rigidbody rigidBody;

        public Rigidbody _GetRigidBody
        {
            get
            {
                if (null == rigidBody)
                {
                    rigidBody = GetComponent<Rigidbody>();
                }
                return rigidBody;
            }
        }

        private void Awake()
        {
            createSphereEdge();
        }

        private void createSphereEdge()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float top = box.bounds.center.y + box.bounds.extents.y;
            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject topFront = createColliderEdge(new Vector3(0f, top, front));

            GameObject bottomFront = createColliderEdge(new Vector3(0f, bottom, front));
            GameObject bottomBack = createColliderEdge(new Vector3(0f, bottom, back));
            _BottomSpheres.Add(bottomFront);
            _BottomSpheres.Add(bottomBack);
            _FrontSpheres.Add(bottomFront);
            _FrontSpheres.Add(topFront);

            float horSec = (bottomBack.transform.position - bottomFront.transform.position).magnitude / 5f;
            float verSec = (bottomFront.transform.position - topFront.transform.position).magnitude / 9f;

            createSphereEdges(bottomBack.transform.position, Vector3.forward, horSec, 4, _BottomSpheres);
            createSphereEdges(bottomFront.transform.position, Vector3.up, verSec, 8, _FrontSpheres);
        }

        private void createSphereEdges(Vector3 startPos, Vector3 dir, float sec, float iteration, List<GameObject> spheres)
        {
            for (int i = 0; i < iteration; i++)
            {
                Vector3 pos = startPos + dir * (sec * (i + 1));
                GameObject obj = createColliderEdge(pos);
                spheres.Add(obj);
            }
        }

        private GameObject createColliderEdge(Vector3 pos)
        {
            GameObject obj = Instantiate(Resources.Load("ColliderEdge", typeof(GameObject)), pos, Quaternion.identity, transform) as GameObject;
            return obj;
        }
    }
}