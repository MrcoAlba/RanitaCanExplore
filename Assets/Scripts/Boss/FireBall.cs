using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update
    private float cronometro;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 6 * Time.deltaTime);
        transform.localScale += new Vector3(0.5f, 0.5f, 0.5f) * Time.deltaTime;
        
        cronometro += 1 * Time.deltaTime;

        if (cronometro > 1f)
        {
            transform.localScale = new Vector3(0.0005f, 0.0005f, 0.0005f);
            gameObject.SetActive(false);
            cronometro = 0;
        }
    }
}
