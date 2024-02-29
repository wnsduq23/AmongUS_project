using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeetingPlayerPanel : MonoBehaviour
{

    [SerializeField]
    private Image characterImg;

    [SerializeField]
    private Text nicknameText;

    [SerializeField]
    private GameObject deadPlayerBlock;

    [SerializeField]
    private GameObject reportSign;

    [SerializeField]
    private GameObject voteButtons;

    public IngameCharacterMover targetPlayer;

    [SerializeField]
    private GameObject voteSign;

    [SerializeField]
    private GameObject voterPrefab;

    [SerializeField]
    private Transform voterParentTransform;

    public void UpdatePanel(EPlayerColor voterColor)
    {
        var voter = Instantiate(voterPrefab, voterParentTransform).GetComponent<Image>();

        voter.material = Instantiate(voter.material);
        voter.material.SetColor("_PlayerColor", PlayerColor.GetColor(voterColor));
    }

    //��ǥ�� ��� Ȱ��ȭ
    public void OpenResult()
    {
        voterParentTransform.gameObject.SetActive(true);
    }

    //��ǥ�ߴٴ� Sign ǥ�����ִ� �Լ�
    public void UpdateVoteSign(bool isVoted)
    {
        voteSign.SetActive(isVoted);
    }

    //�÷��̾� �ǳ� ����
    public void SetPlayer(IngameCharacterMover target)
    {
        Material inst = Instantiate(characterImg.material);
        characterImg.material = inst;

        targetPlayer = target;
        characterImg.material.SetColor("_PlayerColor", PlayerColor.GetColor(targetPlayer.playerColor));
        nicknameText.text = target.nickname;

        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as IngameCharacterMover;

        if (((myCharacter.playerType & EPlayerType.Imposter) == EPlayerType.Imposter)
            && ((targetPlayer.playerType & EPlayerType.Imposter) == EPlayerType.Imposter))
        {
            nicknameText.color = Color.red;
        }

        bool isDead = (targetPlayer.playerType & EPlayerType.Ghost) == EPlayerType.Ghost;

        //������ ����ε� ó�� img
        deadPlayerBlock.SetActive(isDead);
        //������ ��ư ��ȣ�ۿ� �Ұ����ϰ�
        GetComponent<Button>().interactable = !isDead;
        reportSign.SetActive(targetPlayer.isReporter);
    }

    public void OnClickPlayerPanel()
    {
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as IngameCharacterMover;

        if (myCharacter.isVote) return;

        //��Ʈ�� �ƴҰ�쿡�� Panel��ư�۵�
        if ((myCharacter.playerType & EPlayerType.Ghost) != EPlayerType.Ghost)
        {
            IngameUIManager.Instance.MeetingUI.SelectPlayerPanel();
            voteButtons.SetActive(true);
        }
    }

    public void Select()
    {
        var myCharacter = AmongUsRoomPlayer.MyRoomPlayer.myCharacter as IngameCharacterMover;
        myCharacter.CmdVoteEjectPlayer(targetPlayer.playerColor);
        Unselect();
    }

    public void Unselect()
    {
        voteButtons.SetActive(false);
    }
}
