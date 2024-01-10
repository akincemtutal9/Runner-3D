using UnityEngine;
using System.Collections;
using UniRx;
public class PlayerGem : MonoBehaviour
{
    private CompositeDisposable subscriptions = new CompositeDisposable();
    [HideInInspector] public int currentGem;

    private void OnEnable()
    {
        StartCoroutine(Subscribe());
    }

    private IEnumerator Subscribe()
    {
        yield return new WaitUntil(() => GameEvents.instance != null);

        GameEvents.instance.gemCollected.ObserveEveryValueChanged(x => x.Value)
            .Subscribe(value =>
            {
                currentGem = value;
            })
            .AddTo(subscriptions);

        GameEvents.instance.gameWon.ObserveEveryValueChanged(x => x.Value)
           .Subscribe(value =>
           {
               if (value)
                {
                    GameEconomy.instance.GrantGem(currentGem);
                }
           })
           .AddTo(subscriptions);
    }
    private void OnDisable()
    {
        subscriptions.Clear();
    }

    

}