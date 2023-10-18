using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform BulletSpawnPont;
    public GameObject BulletPrefab;
    public float BulletSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            FireGun();
        }
    }
    public void FireGun()
    {
        var Bullet = Instantiate(BulletPrefab, BulletSpawnPont.position, BulletSpawnPont.rotation);
        Bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPont.forward * BulletSpeed;

    }
}
