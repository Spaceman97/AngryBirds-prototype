using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoots : MonoBehaviour
{
	LineRenderer lRend;
	bool visible;
    // Start is called before the first frame update
    void Start()
    {
		lRend=GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(visible)
			visible=false;
		else
			gameObject.SetActive(false);
    }


	public void Show(Vector3 from, Vector3 to)
	{
		lRend.SetPositions(new Vector3[]{from,to}); // выделяем память
		visible = true;
		gameObject.SetActive(true);
	}
}
