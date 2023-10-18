using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLogic : MonoBehaviour
{
    [Header("Lamzallamas")]
    public bool lanza_llamas;
    public List<GameObject> pool = new List<GameObject>();
    public GameObject fire;
    public GameObject cabeza;
    private float cronometro2;


    [Header("Jump Attack")]
    public float jump_distance;
    public bool direction_skill;


    [Header("FireBall")]
    public GameObject fire_ball;
    public GameObject point;
    public List<GameObject> pool2 = new List<GameObject>();


    [Header("Boss Values")]
    public int fase = 1;
    public float HP_min;
    public float HP_max;
    public Image barra;
    public AudioSource music;
    public bool muerto;


    [Header("Boss Logic")]
    public int rutina;
    public float cronometro;
    public float time_rutinas;
    public Animator ani;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    public RangoBoss rango;
    public float speed;
    public GameObject[] hit;
    public int hit_select;
    public bool fires = false;
    // Start is called before the first frame update
    void Start()
    {
        ani = transform.GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (fires == true)
        {
            Fire_Ball_Skill();
        }
        //barra.fillAmount = HP_min / HP_max;
        if (HP_min > 0)
        {
            Vivo();
        }
        else
        {
            if (!muerto)
            {
                ani.SetTrigger("dead");
                //music.enabled = false;
                muerto = true; 
            }
        }
    }
    public void Comportamiento()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 15)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            //La variable point es de donde salen las Fire balls
            point.transform.LookAt(target.transform.position);
            //music.enabled = true;

            if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando)
            {
                switch (rutina)
                {
                    case 0:
                        //Caminar
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        ani.SetBool("Walk", false);
                        ani.SetBool("Run", true);
                        
                        if (transform.rotation == rotation)
                        {
                            Debug.Log("PESO");
                            transform.Translate(Vector3.forward * speed * Time.deltaTime);
                        }
                        ani.SetBool("Attack", false);
                        cronometro += 1 * Time.deltaTime;
                        if (cronometro > time_rutinas)
                        {
                            rutina = Random.Range(0, 5);
                            cronometro = 0;
                        }
                        break;
                    case 1:
                        //Correr
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        ani.SetBool("Walk", true);
                        ani.SetBool("Run", false);

                        if (transform.rotation == rotation)
                        {
                            transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);
                        }
                        ani.SetBool("Attack", false);
                        break;
                    case 2:
                        //Lanza llamas
                        ani.SetBool("Walk", false);
                        ani.SetBool("Run", false);
                        ani.SetBool("Attack", true);
                        ani.SetFloat("Skills", 0);
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        rango.GetComponent<CapsuleCollider>().enabled = false;
                        break;
                    case 3:
                        if (fase == 2)
                        {
                            //Ataque de salto
                            jump_distance += 1 * Time.deltaTime;
                            ani.SetBool("Walk", false);
                            ani.SetBool("Run", false);
                            ani.SetBool("Attack", true);
                            ani.SetFloat("Skills", 0);
                            hit_select = 3;
                            rango.GetComponent<CapsuleCollider>().enabled = false;
                            if (direction_skill)
                            {
                                if (jump_distance < 1f)
                                {
                                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                                }
                                transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                            }
                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }
                        break;
                    case 4:
                        //Fire Ball
                        if (fase == 2)
                        {
                            ani.SetBool("Walk", false);
                            ani.SetBool("Run", false);
                            ani.SetBool("Attack", true);
                            ani.SetFloat("Skills", 0);
                            rango.GetComponent<CapsuleCollider>().enabled = false;
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.5f);
                        }
                        else
                        {
                            rutina = 0;
                            cronometro = 0;
                        }
                        break;
                }
            }
        }
        
    }
    public void Final_ani()
    {
        rutina = 0;
        ani.SetBool("Attack", false);
        atacando = false;
        rango.GetComponent<CapsuleCollider>().enabled = true;
        lanza_llamas = false;
        jump_distance = 0;
        direction_skill = false;
    }
    public void Direction_Attack_Start()
    {
        direction_skill = true;
    }
    public void Direction_Attack_Final()
    {
        direction_skill = false;
    }
    //Mele
    public void ColliderWeaponTrue()
    {
        hit[hit_select].GetComponent<SphereCollider>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        hit[hit_select].GetComponent<SphereCollider>().enabled = false;
    }
    //Lanza llamas
    public GameObject GetBala()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return pool[i];
            }
        }
        GameObject obj = Instantiate(fire, cabeza.transform.position, cabeza.transform.rotation) as GameObject;
        pool.Add(obj);
        return obj;
    }
    public void LanzaLlamas_Skill()
    {
        cronometro2 += 1 * Time.deltaTime;
        if (cronometro2 > 0.1f)
        {
            GameObject obj = GetBala();
            obj.transform.position = cabeza.transform.position;
            obj.transform.rotation = cabeza.transform.rotation;
            cronometro2 = 0;
        }
    }
    public void Start_Fire()
    {
        lanza_llamas = true;
    }
    public void Stop_Fire()
    {
        lanza_llamas = false;
    }
    //Fire ball
    public GameObject Get_Fire_Ball()
    {
        for (int i = 0; i < pool2.Count; i++)
        {
            if (!pool2[i].activeInHierarchy)
            {
                pool2[i].SetActive(true);
                return pool2[i];
            }
        }
        GameObject obj = Instantiate(fire_ball, point.transform.position, point.transform.rotation) as GameObject;
        pool2.Add(obj);
        return obj;
    }
    public void Fire_Ball_Skill()
    {
        GameObject obj = Get_Fire_Ball();
        obj.transform.position = point.transform.position;
        obj.transform.rotation = point.transform.rotation;
    }
    public void Vivo()
    {
        if (HP_min < 500)
        {
            fase = 2;
            time_rutinas = 1;
        }
        Comportamiento();
        if (lanza_llamas)
        {
            LanzaLlamas_Skill();
        }
    }
}
