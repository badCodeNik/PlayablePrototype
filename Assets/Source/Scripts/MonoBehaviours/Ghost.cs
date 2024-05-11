using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.Characters;
using Source.Scripts.Ecs.Components;
using Source.Scripts.Ecs.Marks;
using Source.SignalSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.MonoBehaviours
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private float wanderRadius; 
        [SerializeField] private float wanderDelay; 
        [SerializeField] private Signal signal;
        [SerializeField] private Animator animator;
        [SerializeField, ReadOnly] private int entity;

        public int Entity => entity;
        private NavMeshAgent _navMeshAgent;
        private float _timer;

        private void Awake()
        {
            var componenter = EasyNode.EcsComponenter;
            entity = componenter.GetNewEntity();
            componenter.Add<GhostMark>(entity);
            componenter.Add<AttackingData>(entity);
            ref var animatorData = ref componenter.Add<AnimatorData>(entity);
            animatorData.InitializeValues(animator);
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _timer = wanderDelay;
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= wanderDelay)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                _navMeshAgent.SetDestination(newPos);
               
                _timer = 0;
            }
            
            signal.RegistryRaise(new OnEnemyMoveSignal()
            {
                Entity = entity,
                Direction = _navMeshAgent.velocity.normalized
            });
        }

        private Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
        {
            Vector3 randDirection = Random.insideUnitSphere * dist;

            randDirection += origin;

            NavMeshHit navHit;

            NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

            return navHit.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Hero hero))
            {
                Debug.Log("Kek");
                signal.RegistryRaise(new OnHitSignal()
                {
                    EnemyEntity = entity,
                    PlayerEntity = hero.Entity
                });
            }
        }
    }
}