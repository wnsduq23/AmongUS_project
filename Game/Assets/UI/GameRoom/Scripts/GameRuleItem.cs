using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ȣ��Ʈ�� �ƴ� Ŭ���̾�Ʈ�� ���� ��Ģ ������ �� �ٲٵ��� ���� ��ũ��Ʈ
public class GameRuleItem : MonoBehaviour
{
    [SerializeField]
    private GameObject inactiveObject;

    void Start()
    {
        //ȣ��Ʈ �̸�
        if (!AmongUsRoomPlayer.MyRoomPlayer.isServer)
            inactiveObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
