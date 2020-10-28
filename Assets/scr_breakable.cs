using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_breakable : MonoBehaviour
{
    [SerializeField]
    private float _MaxHp;
    private float _CurrHp;

    private Material _Mat;
    private Color _UndamagedColor;
    void Start()
    {
        _CurrHp = _MaxHp;
        _Mat = this.GetComponent<Renderer>().material;
        _UndamagedColor = _Mat.color;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("damage");
        _CurrHp -= damage;
        if (_CurrHp <= 0)
        {
            Destroy(gameObject);
        }
        _Mat.color = Color.Lerp(Color.black, _UndamagedColor, _CurrHp / _MaxHp);

    }
}

