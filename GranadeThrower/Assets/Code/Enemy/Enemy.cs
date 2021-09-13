using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private GameObject _damageText;
    public Vector3 StartPosition { get; private set; }


    private void OnDisable()
    {
        _damageText.GetComponent<TMP_Text>().text = null;
    }


    private void Start()
    {
        StartPosition = gameObject.transform.position;
        _damageText.SetActive(false);
    }

    public void TakeDamage(int dmg) 
    {
        _health -= dmg;
        
        StartCoroutine(ShowDamageText(dmg));

        if (_health <= 0)
            Die();
    }

    private void Die() 
    {
        gameObject.SetActive(false);
        _health = 100;
    }

    IEnumerator ShowDamageText(int damage)
    {
        _damageText.GetComponent<TMP_Text>().text = damage.ToString();
        _damageText.SetActive(true);
        yield return new WaitForSeconds(1f);
        _damageText.SetActive(false);
    }
}
