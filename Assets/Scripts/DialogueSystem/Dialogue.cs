using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interaction {
    public string characterName;
    public string characterText;
    public Sprite characterSprite;
}

public class Dialogue : MonoBehaviour
{
    public List<Interaction> interactions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
