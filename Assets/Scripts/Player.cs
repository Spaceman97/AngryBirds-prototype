using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
	NavMeshAgent navAgent;
	public float speed;
	Shoots shot;
	Cursor cur;
	public Transform gunBr;
    // Start is called before the first frame update
    void Start()
    {
		cur = FindObjectOfType<Cursor>();
		shot = FindObjectOfType<Shoots>();
		navAgent = GetComponent<NavMeshAgent>();
		navAgent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 dir =Vector3.zero;
		if(Input.GetKey(KeyCode.A))
			dir.z = -1.0f;
		if(Input.GetKey(KeyCode.D))
			dir.z = 1.0f;
		if(Input.GetKey(KeyCode.W))
			dir.x = -1.0f;
		if(Input.GetKey(KeyCode.S))
			dir.x = 1.0f;
	if(Input.GetMouseButtonDown(0)){
			var from = gunBr.position;
			var target = cur.transform.position;
			var to = new Vector3(target.x, from.y,target.z);

			var direction = (to - from).normalized;
			shot.Show(from, to);
			RaycastHit hit;
			if(Physics.Raycast(from,direction, out hit , 100))
			{
				if(hit.transform !=null)
				{
					var zombie = hit.transform.GetComponent<Zombie>();
					if(zombie != null)
						zombie.Kill();
				}
				to = new Vector3(hit.point.x, from.y, hit.point.z);
			}
			else{
				to = from + direction*100;
			}
		}
		navAgent.velocity = dir * speed;

		Vector3 forward = cur.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));
    }
}
