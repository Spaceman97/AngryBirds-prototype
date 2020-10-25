using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
	CapsuleCollider capsCol;
	bool death;
	NavMeshAgent navAgent;
	Player player;
	//MovementAnimator moveAnim;

	Animator animator;
    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<Player>();
		navAgent = GetComponent<NavMeshAgent>();
		capsCol = GetComponent<CapsuleCollider>();
		animator = GetComponentInChildren<Animator>();
		//moveAnim = GetComponent<MovementAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
		if(death)
			return;
		navAgent.SetDestination(player.transform.position);
    }

	public void Kill(){
		
		if(!death){
			death=true;
			Destroy(capsCol);
			//Destroy(navAgent);
			animator.SetTrigger("dead");

		}
	}
}
