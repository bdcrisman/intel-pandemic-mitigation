using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LoadingManager : MonoBehaviour
{
    public EventHandler Finished = (s, e) => { };

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Deactivate()
    {
        _anim.SetTrigger("Deactivate");
    }

    public void FinishedLoading()
    {
        Finished(this, EventArgs.Empty);
    }
}
