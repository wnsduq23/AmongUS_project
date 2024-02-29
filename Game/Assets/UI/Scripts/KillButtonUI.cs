using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillButtonUI : MonoBehaviour
{
    [SerializeField]
    private Button killButton;

    [SerializeField]
    private Text cooldownText;

    private IngameCharacterMover targetPlayer;

    //ų��ư Ȱ��ȭ �Լ�
    public void Show(IngameCharacterMover player)
    {
        gameObject.SetActive(true);
        targetPlayer = player;
    }

    private void Update()
    {
        //null error
        if (targetPlayer != null)
        {
            //ų �Ұ��� ��
            if (!targetPlayer.isKillable)
            {
                cooldownText.text = targetPlayer.KillCooldown > 0 ? ((int)targetPlayer.KillCooldown).ToString() : "";
                killButton.interactable = false;
            }
            //ų ���� ��
            else
            {
                cooldownText.text = "";
                killButton.interactable = true;
            }
        }
    }

    public void OnClickKillButton()
    {
        targetPlayer.Kill();
    }
}
