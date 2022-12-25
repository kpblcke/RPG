using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Combat;
using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{

    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform _target;
        [SerializeField] NavMeshAgent _navMeshAgent;
        [SerializeField] Animator _animator;
        [SerializeField] float maxSpeed = 6f;
        Health health;

        private Ray lastRay;

        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            _navMeshAgent.enabled = health.IsAlive();
            UpdateAnimator();
        }
        
        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = maxSpeed * speedFraction;
            _navMeshAgent.isStopped = false;
        }
        
        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float forwardSpeed = localVelocity.z;
            _animator.SetFloat("forwardSpeed", forwardSpeed);
        }

        public object CaptureState()
        {
            return new SerializableTransform(transform);
        }

        public void RestoreState(object state)
        {
            if (state is SerializableTransform posRot)
            {
                transform.rotation = posRot.ToTransform().Rotation;
                _navMeshAgent.Warp(posRot.ToTransform().Position);
            } 
        }
    }
}