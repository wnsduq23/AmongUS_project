using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ȸ�� ����
public enum EMeetingState
{
    None,
    Meeting,
    Vote
}

public class MeetingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPanelPrefab;

    //Player Panel���� ��� Parent������Ʈ
    [SerializeField]
    private Transform playerPanelsParent;

    [SerializeField]
    private GameObject voterPrefab;

    [SerializeField]
    private GameObject skipVoteButton;

    [SerializeField]
    private GameObject skipVotePlayers;

    [SerializeField]
    private Transform skipVoteParentTransform;

    //�ð� ��� Text
    [SerializeField]
    private Text meetingTimeText;

    private EMeetingState meetingState;

    //������ �÷��̾� �г��� ������ List
    private List<MeetingPlayerPanel> meetingPlayerPanels = new List<MeetingPlayerPanel>();

    public void Open()
    {
        //�ڽ� �÷��̾� ���� �߰�
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as IngameCharacterMover;
        var myPanel = Instantiate(playerPanelPrefab, playerPanelsParent).GetComponent<MeetingPlayerPanel>();
        myPanel.SetPlayer(myCharacter);
        meetingPlayerPanels.Add(myPanel);

        gameObject.SetActive(true);

        //������ �÷��̾� �߰�
        var players = FindObjectsOfType<IngameCharacterMover>();

        foreach (var player in players)
        {
            if (player != myCharacter)
            {
                var panel = Instantiate(playerPanelPrefab, playerPanelsParent).GetComponent<MeetingPlayerPanel>();
                panel.SetPlayer(player);
                meetingPlayerPanels.Add(panel);
            }
        }
    }

    //���� ���� ���� �Լ�
    public void ChangeMeetingState(EMeetingState state)
    {
        meetingState = state;
    }

    //�ٸ� �ǳ� ��Ȱ��ȭ �Լ�
    public void SelectPlayerPanel()
    {
        foreach (var panel in meetingPlayerPanels)
        {
            panel.Unselect();
        }
    }

    public void UpdateVote(EPlayerColor voterColor, EPlayerColor ejectColor)
    {
        foreach (var panel in meetingPlayerPanels)
        {
            //��ǥ���� �÷��̾��
            if (panel.targetPlayer.playerColor == ejectColor)
            {
                panel.UpdatePanel(voterColor);
            }

            //��ǥ�� �÷��̾��
            if (panel.targetPlayer.playerColor == voterColor)
            {
                panel.UpdateVoteSign(true);
            }
        }
    }

    public void UpdateSkipVotePlayer(EPlayerColor skipVotePlayerColor)
    {
        //skip�ϸ� sign ǥ��
        foreach (var panel in meetingPlayerPanels)
        {
            if (panel.targetPlayer.playerColor == skipVotePlayerColor)
            {
                panel.UpdateVoteSign(true);
            }
        }

        //
        var voter = Instantiate(voterPrefab, skipVoteParentTransform).GetComponent<Image>();
        voter.material = Instantiate(voter.material);
        voter.material.SetColor("_PlayerColor", PlayerColor.GetColor(skipVotePlayerColor));

        //��ư ��Ȱ��ȭ
        //skipVoteButton.SetActive(false); (����)
    }

    public void OnClickSkipVoteButton()
    {
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as IngameCharacterMover;

        //��ǥ�� ���߰� �����ʾ����� ��ŵ �����ϰ�
        /*if (!myCharacter.isVote && !((myCharacter.playerType & EPlayerType.Ghost) == EPlayerType.Ghost))
        {
            myCharacter.CmdSkipVote();
            SelectPlayerPanel();
        }*/
    }

    //��ǥ ���� ��
    public void CompleteVote()
    {
        foreach (var panel in meetingPlayerPanels)
        {
            panel.OpenResult();
            panel.Unselect();
        }

        skipVoteButton.SetActive(false);
        skipVotePlayers.SetActive(true);
    }

    private void Update()
    {
        //Mathf.Clamp(value, min, max) : value�� �������� ������ min��ȯ, max���� ũ�� max��ȯ 
        if (meetingState == EMeetingState.Meeting)
        {
            meetingTimeText.text = string.Format("ȸ�ǽð� : {0}s", (int)Mathf.Clamp(GameSystem.Instance.remainTime, 0f, float.MaxValue));
        }

        else if (meetingState == EMeetingState.Vote)
        {
            meetingTimeText.text = string.Format("��ǥ�ð� : {0}s", (int)Mathf.Clamp(GameSystem.Instance.remainTime, 0f, float.MaxValue));
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
