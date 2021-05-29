using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int GoodDataChance;
    public int newDataWaitTime;

    public int virusesTerminated;
    public int goodPacketsCollected;

    public bool isAntivirusOn;
    public GameObject serverShields;

    public int antivirusDuration;

    public int newClientThreshold;

    public Client[] allClients;
    public int nextClientToBeAdded;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
       
    }


    // CALLED WHEN PLAYER STOPS A VIRUS 
    public void VirusTerminated()
    {
        virusesTerminated++;
        GameView.Instance.text_VirusesTerminated.text = "Viruses Terminated : " + virusesTerminated.ToString();

        if((virusesTerminated % newClientThreshold) == 0)
        {
            if(nextClientToBeAdded < allClients.Length)
            {
                allClients[nextClientToBeAdded].gameObject.SetActive(true);
                nextClientToBeAdded++;
            }
            
        }
    }

    //CALLED WHEN A GOOD PACKET IS SUCCESSFULLY RECEIVED
    public void GoodPacketCollected()
    {
        goodPacketsCollected++;
        GameView.Instance.text_GoodPacketsCollected.text = "Good Packets: " + goodPacketsCollected.ToString();

        if (!isAntivirusOn)
        {
            GameView.Instance.image_AntivirusPower.fillAmount += 0.1f;

            if (GameView.Instance.image_AntivirusPower.fillAmount == 1)
            {
                StartCoroutine(AntiVirus_());
            }
        }
        
    }

    //CALLED WHEN PLAYER FAILS TO STOP THE VIRUS
    public void VirusAttacked()
    {
        if (!isAntivirusOn)
        {
            GameView.Instance.image_ServerHealth.fillAmount -= 0.1f;

            if (GameView.Instance.image_ServerHealth.fillAmount == 0)
            {
                GameView.Instance.gameOverView.SetActive(true);
                Time.timeScale = 0;
                Debug.Log("Game Over");
            }
        }
        
    }

    //ANTIVIRUS POWER-UP WHEN THE METER IS FULL
    IEnumerator AntiVirus_()
    {
        isAntivirusOn = true;
        serverShields.SetActive(true);

        yield return new WaitForSeconds(antivirusDuration);

        isAntivirusOn = false;
        serverShields.SetActive(false);
        GameView.Instance.image_AntivirusPower.fillAmount = 0;
    }

   
   
}
