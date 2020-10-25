using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveAnimator : MonoBehaviour
{
	NavMeshAgent navAgent;
	Animator animator;
    // Start is called before the first frame update
    void Start()
    {
		navAgent = GetComponent<NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		animator.SetFloat("speed", navAgent.velocity.magnitude);
    }
}
