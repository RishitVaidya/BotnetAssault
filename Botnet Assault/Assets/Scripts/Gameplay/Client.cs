using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{

    public bool isNeutralized;

    public Data currentData;
    public float movementSpeed_Data;
    public Transform[] dataPathPoints;

    public MeshRenderer[] allConnectionMeshes;

    private Coroutine c_DataMovement;

    private void Start()
    {
        SendData();
    }

    //SEND DATA
    public void SendData()
    {
        isNeutralized = false;

        currentData = Instantiate(PrefabManager.Instance.prefab_Data, dataPathPoints[0].position, Quaternion.identity, transform).GetComponent<Data>();

        int random = Random.Range(1, 100);

        if (random <= GameManager.Instance.GoodDataChance)
        {
            currentData.SetIsVirus(false);
            SetColorForData(Color.green);
            SetColourForAllConnections(Color.green);
        }
        else
        {
            currentData.SetIsVirus(true);
            SetColorForData(Color.red);
            SetColourForAllConnections(Color.red);
        }
        c_DataMovement = StartCoroutine(DataMovement_());
    }

    //STOP TRANSMISSION, CALLED WHEN DATA IS RECEIVED OR WHEN PLAYER STOPS THE CONNECTION
    public void NeutralizeConnection()
    {
        isNeutralized = true;

        if (c_DataMovement != null)
        {
            StopCoroutine(c_DataMovement);
            c_DataMovement = null;
        }
        

        if(currentData != null)
        {
            Destroy(currentData.gameObject);
            currentData = null;
        }
        
        SetColourForAllConnections(Color.gray);

        StartCoroutine(WaitAndSendNewData_());
    }

    //SET COLOR FOR DATA(SPHERE)
    private void SetColorForData(Color color)
    {
        currentData.GetComponent<MeshRenderer>().material.color = color;
    }

    //SET COLOR FOR CABLES
    private void SetColourForAllConnections(Color color)
    {
        for(int i = 0; i < allConnectionMeshes.Length; i++)
        {
            allConnectionMeshes[i].material.color = color;
        }
    }

 

    //MOVEMENT OF DATA(SHPERE)
    IEnumerator DataMovement_()
    {
        int currentIndex = 1;

        while (true)
        {
            currentData.transform.position = Vector3.MoveTowards(currentData.transform.position, dataPathPoints[currentIndex].transform.position, Time.deltaTime * movementSpeed_Data);

            if(currentData.transform.position == dataPathPoints[currentIndex].transform.position)
            {
                if(currentIndex == dataPathPoints.Length - 1)
                {
                    //Data has reached the lastPoint
                    Debug.Log("Data has reached the destination");
                    
                    

                    if (currentData.isVirus)
                    {
                        GameManager.Instance.VirusAttacked();
                    }
                    else
                    {
                        GameManager.Instance.GoodPacketCollected();
                    }

                    NeutralizeConnection();
                    //Send new Data
                    break;
                }
                else
                {
                    //Move on to the next point
                    currentIndex++;
                }
            }

            yield return null;
        }

        
    }

    IEnumerator WaitAndSendNewData_()
    {
        yield return new WaitForSeconds(GameManager.Instance.newDataWaitTime);

        SendData();
    }
}
