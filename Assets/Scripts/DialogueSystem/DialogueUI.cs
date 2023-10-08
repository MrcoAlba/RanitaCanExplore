using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private Transform characterName;
    [SerializeField] private Transform characterImage;
    [SerializeField] private Transform characterText;

    private void Start()
    {
        DialogueManager.Instance.OnDialogueStart += OnDialogueStartDelegate;
        DialogueManager.Instance.OnDialogueNext += OnDialogueNextDelegate;
        DialogueManager.Instance.OnDialogueFinish += OnDialogueFinishDelegate;
        gameObject.SetActive(false);
    }

    private void OnDialogueStartDelegate(Interaction interaction)
    {
        if (interaction!=null){
            gameObject.SetActive(true);
            ShowInteraction(interaction);
        }
    }

    private void OnDialogueNextDelegate(Interaction interaction)
    {
        if (interaction!=null){
            gameObject.SetActive(true);
            ShowInteraction(interaction);
        }
    }

    private void OnDialogueFinishDelegate(){
        gameObject.SetActive(false);
    }

    private void ShowInteraction(Interaction interaction)
    {
        characterImage.GetComponent<Image>().sprite = interaction.characterSprite;
        characterName.GetComponent<TextMeshProUGUI>().text = interaction.characterName;
        characterText.GetComponent<TextMeshProUGUI>().text = interaction.characterText;
    }

}
