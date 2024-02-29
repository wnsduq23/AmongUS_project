using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameIntroUI : MonoBehaviour
{
    [SerializeField]
    private GameObject shhh;

    [SerializeField]
    private GameObject crewmate;

    [SerializeField]
    private Text playerType;

    [SerializeField]
    private Image gradientImg;

    [SerializeField]
    private IntroCharacter myCharacter;

    [SerializeField]
    private List<IntroCharacter> otherCharacters = new List<IntroCharacter>();

    [SerializeField]
    private Color crewColor;

    [SerializeField]
    private Color imposterColor;

    [SerializeField]//������Ʈ �Ʒ�UI���� �ٷ�� ����
    private CanvasGroup canvasGroup;

    //��Ʈ�� ������ �����ִ� �Լ�
    public IEnumerator ShowIntroSequence()
    {
        shhh.SetActive(true);
        yield return new WaitForSeconds(3f);
        shhh.SetActive(false);

        ShowPlayerType();
        crewmate.SetActive(true);
    }

    //��Ʈ�� Ÿ�� ���� �Լ�
    public void ShowPlayerType()
    {
        var players = GameSystem.Instance.GetPlayerList();
        
        IngameCharacterMover myPlayer = null;

        foreach (var player in players)
        {
            if (player.hasAuthority)
            {
                myPlayer = player;
                break;
            }
        }

        myCharacter.SetIntroCharacter(myPlayer.nickname, myPlayer.playerColor);

        //�ڽ��� �������Ͱ�
        if (myPlayer.playerType == EPlayerType.Imposter)
        {
            playerType.text = "��������";
            playerType.color = gradientImg.color = imposterColor;

            int i = 0;

            foreach (var player in players)
            {
                //�ٸ������ ���������̸�
                if (!player.hasAuthority && player.playerType == EPlayerType.Imposter)
                {
                    otherCharacters[i].SetIntroCharacter(player.nickname, player.playerColor);
                    otherCharacters[i].gameObject.SetActive(true);
                    i++;
                }
            }
        }

        else //�ڽ��� ũ����� ���
        {
            playerType.text = "ũ���";
            playerType.color = gradientImg.color = crewColor;

            int i = 0;

            foreach (var player in players)
            {
                //�ڽ� ���� 
                if (!player.hasAuthority)
                {
                    otherCharacters[i].SetIntroCharacter(player.nickname, player.playerColor);
                    otherCharacters[i].gameObject.SetActive(true);
                    i++;
                }
            }
        }
    }

    public void Close()
    {
        StartCoroutine(FadeOut());
    }

    //canvas�� ���� �������� �Լ�
    private IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            yield return null;
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer);
        }

        gameObject.SetActive(false);
    }
}
