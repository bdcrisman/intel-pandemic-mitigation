using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(CanvasGroup))]
public class StartButtonAnim : MonoBehaviour
{
    [SerializeField]
    private Animation _anim;

    public void Interactable(bool isInteractable)
    {
        GetComponent<Button>().interactable = isInteractable;
    }

    public void Reset()
    {
        GetComponent<CanvasGroup>().alpha = 1;
    }

    /// <summary>
    /// Runs the animation.
    /// </summary>
    public void RunAnim()
    {
        _anim.Play();
    }
}
