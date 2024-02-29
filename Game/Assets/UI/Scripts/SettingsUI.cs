using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private Button MouseControlButton;

    [SerializeField]
    private Button KeyboardMouseControlButton;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //���ӿ�����Ʈ�� Ȱ��ȭ �� �� ȣ��Ǵ� �Լ�
    private void OnEnable()
    {
        //null error
        //AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = false;

        switch (PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                MouseControlButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;

            case EControlType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    //�Ű����� ���� ���� ���� �÷��̾����s�� ��Ʈ��Ÿ���� �������ִ� �Լ�
    public void SetControlMode(int controlType)
    {
        PlayerSettings.controlType = (EControlType)controlType;
        switch (PlayerSettings.controlType)
        {
            case EControlType.Mouse:
                MouseControlButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;

            case EControlType.KeyboardMouse:
                MouseControlButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    //������ �ִ� ����� ������Ʈ ��Ȱ��ȭ
    public virtual void Close()
    {
        //null error
        //AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = true;
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("close");//Ʈ���� �����Լ�
    }
}
