using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class HealVolume : MonoBehaviour, IHealer<int>
{
    [SerializeField] protected float healIntervalSeconds = 1f;
    [SerializeField] protected int healAmount = 1;
    private Dictionary<Collider, Coroutine> coroutines = new Dictionary<Collider, Coroutine>();

    public event IHealEvent<int> OnHealCallback;

    private void OnTriggerEnter(Collider other)
    {
        var healable = other.GetComponentInParent<IHealable<int>>();
        var isHealable = healable != null;
        if (!isHealable)
            return;

        if (!coroutines.ContainsKey(other))
        {
            var healRoutine = StartCoroutine(CoHeal(healable));
            coroutines.Add(other, healRoutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (coroutines.ContainsKey(other))
        {
            Assert.IsTrue(coroutines[other] != null);
            StopCoroutine(coroutines[other]);
            coroutines.Remove(other);
        }
    }

    private IEnumerator CoHeal(IHealable<int> healable)
    {
        while (healable != null)
        {
            if (healable.IsHealable)
            {
                healable.Heal(healAmount);
                OnHealCallback?.Invoke(healable.Health);
            }
            yield return new WaitForSeconds(healIntervalSeconds);
        }
    }
}
