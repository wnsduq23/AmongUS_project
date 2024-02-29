using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SettingsUI�� ��� �޾� �̹� ������ ����� �ٽ� �� �� �ְ� 
public class GameRoomSettinsUI : SettingsUI
{
    public void Open()
    {
        //AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = false;
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        base.Close();
        //AmongUsRoomPlayer.MyRoomPlayer.myCharacter.IsMoveable = true;
    }

    public void ExitGameRoom()
    {
        var manager = AmongUsRoomManager.singleton;

        //�Ŵ��� ��尡 ȣ��Ʈ�̸�
        if (manager.mode == Mirror.NetworkManagerMode.Host)
        {
            manager.StopHost();
        }
        //Ŭ���̾�Ʈ�̸�
        else if (manager.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
    }
}
