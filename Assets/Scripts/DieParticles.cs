using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieParticles : MonoBehaviour
{
    private void OnDestroy()
    {
        UIManagerGame.instance.EndGame();
    }
}
