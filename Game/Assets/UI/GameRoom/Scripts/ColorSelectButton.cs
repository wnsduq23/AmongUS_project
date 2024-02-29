using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�� ���� ���� ��ư�� �޾��� ��ũ��Ʈ
public class ColorSelectButton : MonoBehaviour
{
    [SerializeField]
    private GameObject x;

    public bool isInteractable = true;
    
    //SetInteractable ���� true�̸�, x������Ʈ : SetActive(false)
    public void SetInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
        x.SetActive(!isInteractable);
    }
}
