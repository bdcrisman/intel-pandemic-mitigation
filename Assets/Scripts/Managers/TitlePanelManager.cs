using UnityEngine;

public class TitlePanelManager : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        DemoControl.StateUpdated += OnStateUpdated;
    }

    private void OnDisable()
    {
        DemoControl.StateUpdated -= OnStateUpdated;
    }

    private void OnStateUpdated(object sender, StateType state)
    {
        switch(state)
        {
            case StateType.Loaded:
                _anim.SetTrigger("Activate");
                break;

            default:
                break;
        }
    }
}
