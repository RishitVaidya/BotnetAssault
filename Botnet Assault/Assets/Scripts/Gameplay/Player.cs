using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Client client = hit.transform.parent.parent.parent.GetComponent<Client>();

                if (!client.isNeutralized)
                {
                    GameManager.Instance.VirusTerminated();
                    client.NeutralizeConnection();
                }
                
            }
        }
    }
}
