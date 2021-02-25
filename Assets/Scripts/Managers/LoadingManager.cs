using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LoadingManager : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void Deactivate()
    {
        _anim.SetTrigger("Deactivate");
    }
}
