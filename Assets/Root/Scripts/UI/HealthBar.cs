using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected PlayerEntity player;
    [SerializeField] protected GameObject healthIconPrefab;
    [SerializeField] protected Color healthyColor = Color.red;
    [SerializeField] protected Color damagedColor = new Color(.2f, .2f, .2f, 1f);
    protected HealthIcon[] healthIcons;

    private void OnDamagedUpdateDisplay(DamageableMsg<int> msg)
    {
        if (msg.Damage == 0)
            return;

        var health = msg.healthPostDamage;
        UpdateDisplay(health);
    }

    private void OnHealedUpdateDisplay(int health)
    {
        UpdateDisplay(health);
    }

    private void UpdateDisplay(int health)
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].Image.color = i >= health
                ? damagedColor
                : healthyColor;
        }
    }

    private void GenerateIcons()
    {
        var icons = new List<HealthIcon>();
        for (int i = 0; i < player.Params.MaxHealth; i++)
        {
            var iconObject = Instantiate(healthIconPrefab, this.transform);
            var iconScript = iconObject.GetComponentInChildren<HealthIcon>();
            iconScript.Image.color = healthyColor;
            icons.Add(iconScript);
        }
        healthIcons = icons.ToArray();
    }

    private void Awake()
    {
        GenerateIcons();    
    }

    private void OnEnable()
    {
        player.HurtBox.OnDamageableCallback += OnDamagedUpdateDisplay;
        player.HurtBox.OnHealableCallback   += OnHealedUpdateDisplay;
    }

    private void OnDisable()
    {
        player.HurtBox.OnDamageableCallback -= OnDamagedUpdateDisplay;
        player.HurtBox.OnHealableCallback   -= OnHealedUpdateDisplay;
    }

    private void OnValidate()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerEntity>();
        }
    }
}
