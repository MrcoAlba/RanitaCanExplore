using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoBoss : MonoBehaviour
{
    public Animator ani;
    public BossLogic boss;
    public int melee;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            melee = Random.Range(0, 4);
            switch (melee)
            {
                case 0:
                    //Golpe 1
                    ani.SetFloat("Skills", 0);
                    boss.hit_select = 0;
                    break;
                case 1:
                    //Golpe 2
                    ani.SetFloat("Skills", 0);
                    boss.hit_select = 1;
                    break;
                case 2:
                    //Salto
                    ani.SetFloat("Skills", 0);
                    boss.hit_select = 2;
                    break;
                case 3:
                    //Fire ball
                    if (boss.fase == 2)
                    {
                        ani.SetFloat("Skills", 0);
                    }
                    else
                    {
                        melee = 0;
                    }

                    break;
            }
            boss.atacando = true;
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
