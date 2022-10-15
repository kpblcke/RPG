using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] NavMeshAgent _navMeshAgent;
    [SerializeField] Animator _animator;

    private Ray lastRay;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveToCursor();
        }

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = _navMeshAgent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float forwardSpeed = localVelocity.z;
        _animator.SetFloat("forwardSpeed", forwardSpeed);
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if (hasHit)
        {
            _navMeshAgent.destination = hit.point;
        }
    }
}
