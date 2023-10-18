using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoss : MonoBehaviour
{
    public float damamge;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider    other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Quiatr Vida al juagdor
            // Debug.Log("Aqui se debe quitar la vida al jugador");
            other.transform.GetComponent<PlayerMovement>().Damage(damamge);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
