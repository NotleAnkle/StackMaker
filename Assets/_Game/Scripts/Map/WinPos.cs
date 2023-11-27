using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
     

        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().RemoveAllBrick();
            SoundManager.instance.PlayClip(AudioType.FX_Pickup);
        }
    }
}
