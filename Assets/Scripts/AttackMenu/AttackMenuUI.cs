using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AttackMenuUI : MonoBehaviour
{

    public PlayerInput menuInput;
    public bool isMenuOpen = false;
    public int selection = 0;

    [SerializeField] private GameObject attackMenuUi;
    [SerializeField] private GameObject attackCanvas1;
    [SerializeField] private GameObject attackCanvas2;

    private void Awake(){
        
    }
    // Start is called before the first frame update
    void Start()
    {
        menuInput = GetComponent<PlayerInput>();
        attackMenuUi.SetActive(false);
    }
     void Update()
    {
         if(isMenuOpen){
            attackMenuUi.SetActive(true);
        }else{
            Debug.Log("falso");
            attackMenuUi.SetActive(false);
        } 

        if(selection==0){
            attackCanvas1.GetComponent<Image>().color =  Color.green;
            attackCanvas2.GetComponent<Image>().color =  Color.gray;
        }else{
            attackCanvas1.GetComponent<Image>().color =  Color.gray;
            attackCanvas2.GetComponent<Image>().color =  Color.green;
        }
    }


    private void OnMenu(InputValue value)
    {
        if(value.isPressed){
            isMenuOpen = !isMenuOpen;
        }
    }

    private void OnMenuUp(InputValue value)
    {
        if(value.isPressed && isMenuOpen){
            selection=0;
        }
    }

    private void OnMenuDown(InputValue value)
    {
        if(value.isPressed && isMenuOpen){
            selection=1;
        }
    }
}
