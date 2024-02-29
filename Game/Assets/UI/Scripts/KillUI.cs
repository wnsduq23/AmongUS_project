using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillUI : MonoBehaviour
{
    [SerializeField]
    private Image imposterImg;

    [SerializeField]
    private Image crewmateImg;

    [SerializeField]
    private Material material;

    public void Open(EPlayerColor imposter, EPlayerColor crewmate)
    {
        AmongUsRoomPlayer.MyRoomPlayer.myCharacter.isMoveable = false;

        Material inst1 = Instantiate(material);
        imposterImg.material = inst1;

        Material inst2 = Instantiate(material);
        crewmateImg.material = inst2;

        gameObject.SetActive(true);//KillUI Ȱ��ȭ

        imposterImg.material.SetColor("_PlayerColor", PlayerColor.GetColor(imposter));
        crewmateImg.material.SetColor("_PlayerColor", PlayerColor.GetColor(crewmate));

        //���� �Լ�
        Invoke("Close", 3f);
    }

    public void Close()
    {
        gameObject.SetActive(false);


        //killUI�� ������ �� Report�� Ghost�� ȸ�� �� �����̴� ������ �߻�
        //AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = true;

        //UI�� �ϳ��� ���������� false
        AmongUsRoomPlayer.MyRoomPlayer.myCharacter.isMoveable =
            !(IngameUIManager.Instance.ReportUI.gameObject.activeSelf || 
            IngameUIManager.Instance.MeetingUI.gameObject.activeSelf);
    }
}
