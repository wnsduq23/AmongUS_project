using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightWire : MonoBehaviour
{
    public EWireColor WireColor { get; private set; }

    public bool IsConnected { get; private set; }

    [SerializeField]
    private List<Image> mWireImages;

    [SerializeField]
    private Image mLightImage;

    [SerializeField]
    private List<LeftWire> mConnectedWires = new List<LeftWire>();

    public void SetWireColor(EWireColor wireColor)
    {
        WireColor = wireColor;
        Color color = Color.black;

        switch (WireColor)
        {
            case EWireColor.Red:
                color = Color.red;
                break;

            case EWireColor.Blue:
                color = Color.blue;
                break;

            case EWireColor.Yellow:
                color = Color.yellow;
                break;

            case EWireColor.Magenta:
                color = Color.magenta;
                break;
        }

        //base, body, rubber ��� ������
        foreach (var image in mWireImages)
        {
            image.color = color;
        }
    }

    //�������� leftwire�� ����� �� ����
    public void ConnectWire(LeftWire leftWire)
    {
        if (mConnectedWires.Contains(leftWire)) return ;

        mConnectedWires.Add(leftWire);

        //����� ���̾ �Ѱ��̸鼭 ������ ������
        if (mConnectedWires.Count == 1 && leftWire.WireColor == WireColor)
        {
            mLightImage.color = Color.yellow;
            IsConnected = true;
        }
        else
        {
            mLightImage.color = Color.gray;
            IsConnected = false;
        }
    }

    public void DisconnectWire(LeftWire leftWire)
    {
        mConnectedWires.Remove(leftWire);

        if (mConnectedWires.Count == 1 && mConnectedWires[0].WireColor == WireColor)
        {
            mLightImage.color = Color.yellow;
            IsConnected = true;
        }
        else
        {
            mLightImage.color = Color.gray;
            IsConnected = false;
        }
    }
}
