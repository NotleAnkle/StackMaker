using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject chestOpen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().Win();
            chestOpen.SetActive(true);

            GameManager.instance.OnWin();

            this.gameObject.SetActive(false);
        }
    }
}
