using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Animator buttonanim;

    private void Start()
    {
        buttonanim = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonanim.Play("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonanim.Play("UnHover");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy==false)
        {
            GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        }
    }
}
