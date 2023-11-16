using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] GameObject brick;

    private bool isPassed = false;
    private void OnTriggerEnter(Collider other)
    {
        if(!isPassed && other.gameObject.tag == "Player")
        {
            other.GetComponent<Player>().AddBrick();
            isPassed = true;
            SoundManager.instance.PlayClip(AudioType.FX_Pickup);

            Despawn();
        }
    }

    private void Despawn()
    {
        brick.SetActive(false);
    }
}
