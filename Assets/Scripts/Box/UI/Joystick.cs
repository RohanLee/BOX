using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 corePosition = new Vector2(200f, 180f), offset;
    private RectTransform rect;

    public static Vector3 JoystickOffset;

    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        rect.anchoredPosition = corePosition;
    }

    void Update()
    {
        //JoystickOffset = (this.transform.position - corePosition).normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 downPos = new Vector2();
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(UICore.FixedRoot.GetComponent<RectTransform>(), eventData.position, UICore.UICamera, out downPos))
        {
            offset = rect.anchoredPosition - downPos;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dragPos = new Vector2();
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(UICore.FixedRoot.GetComponent<RectTransform>(), eventData.position, UICore.UICamera, out dragPos))
        {
            rect.anchoredPosition = dragPos + offset;
            JoystickOffset = (rect.anchoredPosition - corePosition).normalized;
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        rect.anchoredPosition = corePosition;
        JoystickOffset = Vector3.zero;
    }
}
