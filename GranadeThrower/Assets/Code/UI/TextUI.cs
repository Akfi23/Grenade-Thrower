using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _redGrenadeText;
    [SerializeField] private TMP_Text _blueGrenadeText;
    [SerializeField] private GrenadeThrower grenadeThrower;

    private void OnEnable()
    {
        grenadeThrower.RedGrenadesCount += SetRedGrenadesCount;
        grenadeThrower.BlueGrenadesCount += SetBlueGrenadesCount;
    }

    private void OnDisable()
    {
        grenadeThrower.RedGrenadesCount -= SetRedGrenadesCount;
        grenadeThrower.BlueGrenadesCount -= SetBlueGrenadesCount;
    }

    private void SetRedGrenadesCount(int Grenades)
    {
        _redGrenadeText.text = Grenades.ToString();
    }

    private void SetBlueGrenadesCount(int Grenades)
    {
        _blueGrenadeText.text = Grenades.ToString();
    }
}
