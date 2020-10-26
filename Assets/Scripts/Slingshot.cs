using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    static private Slingshot S;
	[Header ("Статические переменные")]
    
	public GameObject prefabProjectile;
    public float velocityMult = 8.0f;

	[Header ("Динамические переменные")]
	public GameObject launchPoint;
	public Vector3 launchPos;
	public GameObject projectile; 
	public bool aimingMode;

    public Rigidbody projectileRigidbody;

    static public Vector3 LAUNCH_POS {
        get {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }

	void Awake(){
        S = this;
		Transform launchPointTrans = transform.Find("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
		launchPos = launchPointTrans.position;

	}
    // Start is called before the first frame update
    void Start()
    {
         
    }
	void OnMouseEnter() {

		launchPoint.SetActive( true ); 
	}

	void OnMouseExit() {
		//print("Slingshot:OnMouseExit()");
		launchPoint.SetActive( false ); 
	}

	void OnMouseDown() {
		aimingMode = true;
		projectile = Instantiate (prefabProjectile) as GameObject;
		projectile.transform.position = launchPos;
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
	}
    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return;

        //получаем текущую позицию мыщи на экране с помощью Input
        Vector3 mousePos2D = Input.mousePosition;
        //смотрим,как глубоко нажатие в трехмерном изм.
        mousePos2D.z = -Camera.main.transform.position.z;
        //преобразовываем точку на двумерной плоскости экрана в трехмерные координаты 
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);


        Vector3 mouseDelta = mousePos3D - launchPos;
        //если наше нажатие больше радиуса сферы
        float maxMagnitude = GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //Передвигаем снаряд в новую позицию

        Vector3 projectilePos = launchPos + mouseDelta;
        projectile.transform.position = projectilePos;
        if (Input.GetMouseButtonUp(0)) {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;
            
        }

    }
}
