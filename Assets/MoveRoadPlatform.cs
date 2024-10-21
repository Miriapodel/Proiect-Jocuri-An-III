using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRoadPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -20) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeactivatePlatforms"))
        {
            // Dezactivez sectiunea de drum     --->     Parca asa a zis la curs ca vrea sa facem ca altfel o distrugeam :)))
            gameObject.SetActive(false);
            
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Obstacle"))
                {
                    child.parent = null;
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}
