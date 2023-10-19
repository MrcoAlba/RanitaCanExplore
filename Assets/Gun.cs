using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform BulletSpawnPont;
    public GameObject BulletPrefab;
    public float BulletSpeed = 5;
    public Transform BulletSpawnPoint2;
    public PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FireGun()
    {
        if (Player.ToRotate == 1)
        {
            var Bullet = Instantiate(BulletPrefab, BulletSpawnPont.position, BulletSpawnPont.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPont.forward * BulletSpeed;
        }
        else if (Player.ToRotate == -1)
        {
            var Bullet = Instantiate(BulletPrefab, BulletSpawnPoint2.position, BulletSpawnPoint2.rotation);
            Bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint2.forward * BulletSpeed;
        }
        

    }
}
