using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowEgg : MonoBehaviour
{
    public GameObject Egg;

    public float InAccuraccyModifier;
    public float ForceDelta;
    public float MaxThrowForce;
    public float MinThrowForce;

    private Transform _arm;
    private Slider _throwMeter;

    public float _force = 0;

    int EggsInInventory;

    void Start()
    {
        _arm = GameObject.Find("Arm").transform;
        _throwMeter = GameObject.Find("ThrowMeter").GetComponent<Slider>();
    }

    GameObject _currentEgg;
    Rigidbody _eggRigi;

    EggBroken _eB;

    bool _noEgg = true;

    void Update()
    {
        if (_noEgg) //SET UP THE EGG
        {
            _noEgg = false;
            _currentEgg = Instantiate(Egg, _arm.transform.position, Quaternion.identity, _arm);
            _eggRigi = _currentEgg.GetComponent<Rigidbody>();
            RigidBodySettings(false);
            _eB = _currentEgg.GetComponent<EggBroken>();
        }

        if (_currentEgg != null)
            _currentEgg.transform.forward = _arm.forward;


        if (Input.GetMouseButtonDown(0))
            _force = MinThrowForce;

        if (Input.GetMouseButton(0) && _force < MaxThrowForce)
        {
            _force += Time.deltaTime * ForceDelta;
            _throwMeter.value = (_force / (MaxThrowForce - MinThrowForce)) - MinThrowForce/MaxThrowForce;

            _eB.CanBrake = (_throwMeter.value > 0.5) ? true : false;

            if (_throwMeter.value > 0.5f)
            {
                float ranInAcc = Random.Range(-InAccuraccyModifier, InAccuraccyModifier);

                Vector3 inAcc = new Vector3(0, ranInAcc, 0);
                _arm.localEulerAngles += inAcc;
            }
            else
            {
                _arm.localEulerAngles = new Vector3(_arm.localEulerAngles.x, 0, _arm.localEulerAngles.z);
            }

        }
        else if (Input.GetMouseButton(0) && _force >= MaxThrowForce)
        {
            _force = MaxThrowForce;
        }

        if (Input.GetMouseButtonUp(0))
        {
            EggThrow();
        }


        //pickup eggs
        Ray ray = new Ray();
        ray.direction = Camera.main.transform.forward;
        ray.origin = Camera.main.transform.position;
        RaycastHit hit;

        if (Input.GetKeyDown("e"))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject != null)
                {
                    if (hit.transform.tag == "EggBasket")
                    {
                        EggBasketScript eggBasket = hit.transform.GetComponent<EggBasketScript>();

                        if (eggBasket.CurrentEggsInBasket > 0)
                            eggBasket.TakeEggsFromBasket(ref EggsInInventory);
                    }
                }
            }
        }

    }

    private void EggThrow()
    {
        _currentEgg.transform.parent = null;
        _eB = null;
        RigidBodySettings(true);
        _eggRigi.AddForce(_currentEgg.transform.forward * _force, ForceMode.Impulse); //YEET
        _force = 0;
        _currentEgg = null;
        _noEgg = true;
    }

    private void RigidBodySettings(bool set)
    {
        _eggRigi.useGravity = set;
        _eggRigi.isKinematic = !set;
    }
}
