using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingManagerQ : MonoBehaviour
{

    [SerializeField] GameObject playerOne;
    [SerializeField] GameObject playerTwo;
    [SerializeField] TextMeshProUGUI playerOneText;
    [SerializeField] TextMeshProUGUI playerTwoText;

    void Start()
    {
        TerritoryCalculator territoryCalculator = TerritoryCalculator.instance;
        territoryCalculator.drawGizmo = false;

        if (territoryCalculator.Player1Percent > territoryCalculator.Player2Percent)
        {
            playerOne.transform.GetChild(0).gameObject.SetActive(true);
            playerTwo.transform.GetChild(1).gameObject.SetActive(true);
            playerOneText.enabled = true;
        }
        else
        {
            playerOne.transform.GetChild(1).gameObject.SetActive(true);
            playerTwo.transform.GetChild(0).gameObject.SetActive(true);
            playerTwoText.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
