using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowEgg : MonoBehaviour
{
    public GameObject Egg;
    
    public float ForceDelta;
    public float MaxThrowForce;
    public float MinThrowForce;

    private Transform _arm;
    private Slider _throwMeter;

    public float _force = 0;

    void Start()
    {
        _arm = GameObject.Find("Arm").transform;
        _throwMeter = GameObject.Find("ThrowMeter").GetComponent<Slider>();
    }

    GameObject _currentEgg;
    Rigidbody _eggRigi;
    bool _noEgg = true;

    void Update()
    {
        if (_noEgg)
        {
            _noEgg = false;
            _currentEgg = Instantiate(Egg, _arm.transform.position, Quaternion.identity, _arm);
            _eggRigi = _currentEgg.GetComponent<Rigidbody>();
            _eggRigi.useGravity = false;
            _eggRigi.isKinematic = true;
        }

        if (_currentEgg != null)
            _currentEgg.transform.forward = _arm.forward;


        if (Input.GetMouseButtonDown(0))
            _force = MinThrowForce;

        if (Input.GetMouseButton(0) && _force < MaxThrowForce)
        {
            _force += Time.deltaTime * ForceDelta;
            _throwMeter.value = (_force / (MaxThrowForce - MinThrowForce)) - MinThrowForce/MaxThrowForce;
        }
        else if (Input.GetMouseButton(0) && _force >= MaxThrowForce)
        {
            _force = MaxThrowForce;
        }

        if (Input.GetMouseButtonUp(0))
        {
            EggThrow();
        }


    }

    private void EggThrow()
    {
        _currentEgg.transform.parent = null;
        _eggRigi.useGravity = true;
        _eggRigi.isKinematic = false;
        _eggRigi.AddForce(_currentEgg.transform.forward * _force * Time.deltaTime, ForceMode.Impulse);
        _currentEgg = null;

        _noEgg = true;
    }
}
