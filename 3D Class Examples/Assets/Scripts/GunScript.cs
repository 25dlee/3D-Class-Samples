using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public float gunRange;
    public LayerMask gunMask;
    public float bulletImpactForce;
    public GameObject bulletSFX;
    public GameObject gunDecal;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.lockState = CursorLockMode.;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    void Shoot()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Instantiate(bulletSFX, transform.position, Quaternion.identity);

        if (Physics.Raycast(ray, out RaycastHit hit, gunRange, gunMask))
        {
            Quaternion hitRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            GameObject decal = Instantiate(gunDecal, hit.point, hitRotation, hit.transform);
            decal.transform.parent = hit.transform;
            

            Debug.Log(hit.collider.gameObject);
            if(hit.rigidbody != null)
            {
                Vector3 forceDirection = (hit.point - transform.position).normalized;
                hit.rigidbody.AddForceAtPosition(forceDirection * bulletImpactForce, hit.point);
            }


        }
    }
}
