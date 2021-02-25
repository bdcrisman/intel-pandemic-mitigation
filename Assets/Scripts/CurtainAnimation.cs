using UnityEngine;

[RequireComponent(typeof(Animation))]
public class CurtainAnimation : MonoBehaviour
{
    private Animation _anim;

    private void Awake()
    {
        _anim = GetComponent<Animation>();
    }

    /// <summary>
    /// Open the curtains.
    /// </summary>
    public void Open()
    {
        _anim.Play();
    }
}
