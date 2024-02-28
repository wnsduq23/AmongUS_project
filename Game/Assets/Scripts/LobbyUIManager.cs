using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Mirror;

public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager Instance;

    /*[SerializeField] private CustomizeUI customizeUI;
    public CustomizeUI CustomizeUI { get { return customizeUI; } }*/
    [SerializeField]
    private GameRoomPlayerCounter gameRoomPlayerCounter;
    public GameRoomPlayerCounter GameRoomPlayerCounter { get { return gameRoomPlayerCounter; } }

    [SerializeField] private Button useButton;
    [SerializeField] private Sprite originalUseButtonSprite;
    [SerializeField] private Button startButton;

    private void Awake()
    {
        Instance = this;
    }

    public void SetUseButton(Sprite sprite, UnityAction action)
    {
        useButton.image.sprite = sprite;
        useButton.onClick.AddListener(action);
        useButton.interactable = true;
    }

    public void UnsetUseButton()
    {
        useButton.image.sprite = originalUseButtonSprite;
        useButton.onClick.RemoveAllListeners();
        useButton.interactable = false;
    }

    //Start 버튼 구현
    public void ActiveStartButton()
    {
        startButton.gameObject.SetActive(true);
	}

    public void SetInteractableButton(bool isInteractable)
    {
        startButton.interactable = isInteractable;
	}

    public void OnClickStartButton()
    {
        /*var players = FindObjectOfType<AmongUsRoomPlayer>();
        for (int i = 0; i < players.Length; i++)
        {
            players[i].readyToBegin = true;
		}

        var manager = NetworkManager.singleton as AmongUsRoomManager;
        manager.ServerChangeScene(manager.GameplayScene);*/

        var manager = NetworkManager.singleton as AmongUsRoomManager;

        //AmongUsRoomManager의 gameRuleData에 저장
        manager.gameRuleData = FindObjectOfType<GameRuleStore>().GetGameRuleData();

        var players = FindObjectsOfType<AmongUsRoomPlayer>();
        //플레이어들을 준비상태로
        foreach (var player in players)
        {
            player.CmdChangeReadyState(true);
        }

        //Scene 전환
        manager.ServerChangeScene(manager.GameplayScene);
    }
}
