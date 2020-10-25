using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
	LayerMask lMask;
    // Start is called before the first frame update
    void Start()
    {
		lMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit,1000, lMask))
		{
			transform.position = new Vector3(hit.point.x,transform.position.y, hit.point.z);
		}

    }
}
