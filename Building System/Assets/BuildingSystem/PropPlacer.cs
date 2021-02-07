
using AnilTools;
using AnilTools.Move;
using Assets.BuildingSystem;
using UnityEngine;

namespace BuildingSystem
{
    public class PropPlacer : MonoBehaviour
    {
        [SerializeField] private float PlaceDistance = 2.5f;
        [SerializeField] private float rotateSpeed = 10;

        public PropData[] propData;

        [ReadOnly] public Prop CurrentProp;

        private bool _placeMod;
        private bool placeMod
        {
            get
            {
                return _placeMod;
            }
            set
            {
                CurrentProp.gameObject.SetActive(value);
                _placeMod = value;
            }
        }
        
        private Transform _Camera;
        private Vector3 CurrentGridPoint;

        private const float GridLength = 1f;

        private byte propIndex = 0;

        private Vector3 lastRotation;

        private void Start()
        {
            _Camera = Camera.main.transform;
            CurrentProp = PoolManager.instance.RequestProp(propData[0]);
            Debug2.Log("pres f to activate place mod");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                placeMod = !placeMod;
            }


            if (!placeMod)
                return;
            
            for (byte i = 0; i < propData.Length; i++)
            {
                if (Input.GetKeyDown((KeyCode)(49 + i))) // 49 = alpha0 , 50 alpha1
                {
                    propIndex = i;
                    lastRotation = CurrentProp.transform.localEulerAngles;
                    CurrentProp.gameObject.SetActive(false);
                    CurrentProp = PoolManager.instance.RequestProp(propData[propIndex]);
                    CurrentProp.transform.localEulerAngles = lastRotation;
                }
            }

            if (Messenger.GetMessage(0) == false)// rotating
            {
                CurrentGridPoint = FindGridPoint(_Camera.position + _Camera.forward * PlaceDistance);
                CurrentProp.transform.position = CurrentGridPoint;

                CurrentProp.material.SetColor(ShaderPool.Color, Placeable() ? Color.green : Color.red);

                if (Input.GetMouseButtonDown(0))
                {
                    if (Placeable())
                    {
                        lastRotation = CurrentProp.transform.localEulerAngles;
                        CurrentProp.Place();
                        CurrentProp = PoolManager.instance.RequestProp(propData[propIndex]);
                        CurrentProp.gameObject.SetActive(true);
                        CurrentProp.transform.localEulerAngles = lastRotation;
                    }
                    else
                    {
                        Debug2.Log("this place is not placeable D");
                    }
                }

                if (Input.GetMouseButtonDown(1))
                {
                    if (Mathmatic.RaycastFromCamera(out RaycastHit hit , Mathmatic.FloatMaxValue))
                    {
                        if (hit.transform.TryGetComponent(out Prop prop))
                        {
                            prop.Remove();
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.E)) // saga rotate
                {
                    Messenger.SendMessage(0); // rotating
                    CurrentProp.transform.RotateTransform(CurrentProp.transform.localEulerAngles + Vector3.up * 90 , rotateSpeed , false , () => Messenger.RemoveMessage(0));
                }

                if (Input.GetKeyDown(KeyCode.Q)) // sola rotate
                {
                    Messenger.SendMessage(0); // rotating
                    CurrentProp.transform.RotateTransform(CurrentProp.transform.localEulerAngles + Vector3.down * 90, rotateSpeed, false, () => Messenger.RemoveMessage(0));
                }

                if (Input.GetAxis("Mouse ScrollWheel") != 0f) // yukarı rotate
                {
                    Messenger.SendMessage(0); // rotating
                    CurrentProp.transform.RotateTransform(CurrentProp.transform.localEulerAngles + Vector3.right * 90, rotateSpeed, false, () => Messenger.RemoveMessage(0));
                }

            }
        }

        private bool Placeable()
        {
            var hits = Physics.OverlapSphere(CurrentProp.transform.position, GridLength / 2.1f, -1 , QueryTriggerInteraction.Ignore);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].transform != CurrentProp.transform)
                {
                    return false;
                }
            }

            return true;
        }

        private Vector3 FindGridPoint(Vector3 pos)
        {
            return pos - new Vector3(pos.x % GridLength, (pos.y % GridLength)  , pos.z % GridLength);            
        }

    }
}