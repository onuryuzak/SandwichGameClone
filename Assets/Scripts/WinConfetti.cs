using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConfetti : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.OnLevelWin.AddListener(PlayConfettiParticle);
    }

    private void OnDisable()
    {
        EventManager.OnLevelWin.RemoveListener(PlayConfettiParticle);
    }

    void PlayConfettiParticle()
    {
        GetComponent<ParticleSystem>().Play();
    }
}
