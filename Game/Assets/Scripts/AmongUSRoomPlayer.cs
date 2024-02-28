using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Linq;

public class AmongUsRoomPlayer : NetworkRoomPlayer
{
    private static AmongUsRoomPlayer myRoomPlayer;

    //null 값이면 자기 자신플레이어를 반환하는 Property
    public static AmongUsRoomPlayer MyRoomPlayer
    {
        get
        {
            if (myRoomPlayer == null)
            {
                var players = FindObjectsOfType<AmongUsRoomPlayer>();
                foreach (var player in players)
                {
                    if (player.hasAuthority)
                        myRoomPlayer = player;
                }
            }
            return myRoomPlayer;
        }
    }


    [SyncVar]
    public EPlayerColor playerColor;
    [SyncVar]
    public string nickname;

    public CharacterMover lobbyPlayerCharacter;

    private void Start()
    {
        base.Start();

        if (isServer)
        {
            SpawnLobbyPlayerCharacter();
            CmdSetNickname(PlayerSettings.nickname);
            LobbyUIManager.Instance.ActiveStartButton();
        }

        if (isLocalPlayer)
        {
            CmdSetNickname(PlayerSettings.nickname);
		}

        LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
    }

    private void OnDestroy()
    {
        if (LobbyUIManager.Instance != null)
        {
            LobbyUIManager.Instance.GameRoomPlayerCounter.UpdatePlayerCount();
            //LobbyUIManager.Instance.CustomizeUI.UpdateUnselectColorButton(playerColor);
		}
    }

    [Command]
    public void CmdSetNickname(string nick)
    {
        nickname = nick;
        lobbyPlayerCharacter.nickname = nick;
	}
    /*[Command]
    public void CmdSetPlayerColor(EPlayerColor color)
    {
        playerColor = color;

        lobbyPlayerCharacter.playerColor = color;
    }*/

    private void SpawnLobbyPlayerCharacter()
    {
        // as 연산자 : 캐스팅이 안되면 null반환 되면 캐스팅
        // 플레이어의 색상을 랜덤으로 설정
        //대기실에 접속 중인 플레이어를 가져오는 변수 RoomManager의 roomSlots
        var roomSlots = (NetworkManager.singleton as AmongUsRoomManager).roomSlots;
        EPlayerColor color = EPlayerColor.Red;
        for (int i = 0; i < (int)EPlayerColor.Lime + 1; i++)
        {
            bool isFindSameColor = false;
            foreach (var roomPlayer in roomSlots)
            {
                var amongUsRoomPlayer = roomPlayer as AmongUsRoomPlayer;
                //netId 고유식별 network id
                if (amongUsRoomPlayer.playerColor == (EPlayerColor)i && roomPlayer.netId != netId)
                {
                    isFindSameColor = true;
                    break;
                }
            }

            if (!isFindSameColor)
            {
                color = (EPlayerColor)i;
                break;
            }
        }
        playerColor = color;
        //playerColor = GetAvailablePlayerColor();

        Vector3 spawnPos = FindObjectOfType<SpawnPositions>().GetSpawnPosition();

        var playerCharacter = Instantiate(AmongUsRoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).GetComponent<LobbyCharacterMover>();
        //6번째 클라부터는 flip해서 소환
        playerCharacter.transform.localScale = index < 5 ? new Vector3(0.5f, 0.5f, 1f) : new Vector3(-0.5f, 0.5f, 1f);

        //클라이언트들에게 게임오브젝트가 소환됨을 알림
        //connectionToClient와 connectionToServer은 서버와 클라이언트 중 어느 곳에서 호출하느냐 차이
        //서버 측에서 connection을 확인하려고 하면 conntectionToServer를 사용하고 클라이언트 측에서 connection을 확인할 때는 connectionToClient를 사용하면 된다.
        NetworkServer.Spawn(playerCharacter.gameObject, connectionToClient);
        playerCharacter.ownerNetId = netId;
        playerCharacter.playerColor = playerColor;
    }
}