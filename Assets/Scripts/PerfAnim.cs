using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfAnim : MonoBehaviour
{
    [SerializeField]
    private List<Animation> _perfAnims;

    
    public void Animate(bool isLeft)
    {
        _perfAnims[isLeft ? 0 : 1].Play();
    }
}
