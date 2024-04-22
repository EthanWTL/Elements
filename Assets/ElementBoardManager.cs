using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementBoardManager : MonoBehaviour
{
    public Text fireNumber;
    public Text waterNumber;
    public Text lighteningNumber;
    public Text greenNumber;
    public Text rockNumber;
    
    public Text buffAdder;
    public Text buffMultiplier;

    public PlayerElement playerElement;

    private void Start()
    {
        playerElement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerElement>();
    }

    public void refreshBoard()
    {
        fireNumber.text = playerElement.fireNumber.ToString();
        waterNumber.text = playerElement.waterNumber.ToString();
        lighteningNumber.text = playerElement.lighteningNumber.ToString();
        greenNumber.text = playerElement.greenNumber.ToString();
        rockNumber.text = playerElement.rockNumber.ToString();
        buffMultiplier.text = "x" + playerElement.buffMultiplier.ToString("F2");
        buffAdder.text = playerElement.buffAdder.ToString();
    }

}
