using UnityEngine;

public class SceneAnimManager : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

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
        switch (state)
        {
            case StateType.Init:
                _anim.SetTrigger("Init");
                break;

            case StateType.Loaded:
                _anim.SetTrigger("Scene1");
                break;

            case StateType.Scene1:
                break;

            case StateType.Scene2:
                _anim.SetTrigger("Scene2");
                break;

            default:
                break;
        }
    }
}
