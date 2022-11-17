using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class AnimatingUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float yPosition;
    TextMeshProUGUI buttonText;
    private void Awake() {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void MoveText()
    {
        //buttonText.transform.position = new Vector2(buttonText.transform.position.x, buttonText.transform.position.y + yPosition);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonText.transform.position = new Vector2(buttonText.transform.position.x, buttonText.transform.position.y + yPosition);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonText.transform.position = new Vector2(buttonText.transform.position.x, buttonText.transform.position.y - yPosition);
    }
}
