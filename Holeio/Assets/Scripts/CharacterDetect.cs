using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ICharacter>()!= null)
            other.GetComponent<ICharacter>().OnDeathTrigger();
    }
}
