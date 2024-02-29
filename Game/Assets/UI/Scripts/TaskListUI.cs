using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//IPointerClickHandler Ŭ�� �Է��� �޾��� �������̽� ���
public class TaskListUI : MonoBehaviour, IPointerClickHandler
{
    //TaskListUI�� ���� �� transform.x�� �⺻��
    [SerializeField]
    private float offset;

    [SerializeField]
    private RectTransform TaskListUITransform;

    private bool isOpen = true;

    private float timer;

    public void OnPointerClick(PointerEventData eventData)
    {
        //�������� �ڷ�ƾ�Լ� ����
        StopAllCoroutines();
        StartCoroutine(OpenAndHideUI());
    }

    private IEnumerator OpenAndHideUI()
    {
        isOpen = !isOpen;
        if (timer != 0f) timer = 1f - timer;

        while (timer <= 1f)
        {
            timer += Time.deltaTime * 2f;

            float start = isOpen ? -TaskListUITransform.sizeDelta.x : offset;
            float dest = isOpen ? offset : -TaskListUITransform.sizeDelta.x;
            TaskListUITransform.anchoredPosition = new Vector2(Mathf.Lerp(start, dest, timer), TaskListUITransform.anchoredPosition.y);
            yield return null;
        }
    }
}
