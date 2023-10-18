using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossBattle : MonoBehaviour
{
    public BossLogic Boss;
    public GameObject Colliders;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartBattle() {
        Boss.EmpezarAtacar = true;
        Colliders.gameObject.SetActive(true);
    }
}
