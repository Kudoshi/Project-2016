

using System;
using UnityEngine;

public class CountdownController : MonoBehaviour
{
    [SerializeField] private GameObject _container;

    private void Awake()
    {
        _container.SetActive(false);
    }
    private void OnEnable()
    {
        GameManager.OnChangeGameState += OnChangeGameState;
    }

    private void OnDisable()
    {
        GameManager.OnChangeGameState -= OnChangeGameState;

    }

    private void OnChangeGameState(GameState state)
    {
        if (state == GameState.COUNTDOWN)
        {
            TriggerCountdown();
        }
    }

    private void TriggerCountdown()
    {
        _container.SetActive(true);

        SoundManager.Instance.PlaySound("sfx_three");

        Util.WaitForSeconds(this, () =>
        {
            SoundManager.Instance.PlaySound("sfx_two");

        }, .95f);

        Util.WaitForSeconds(this, () =>
        {
            SoundManager.Instance.PlaySound("sfx_one");

        }, 1.95f);



        Util.WaitForSeconds(this, ()=>
        {
            _container.SetActive(false);
            SoundManager.Instance.PlaySound("sfx_zero");
            GameManager.Instance.ChangeGameState(GameState.GAME);
        }, 2.95f);

    }
}
