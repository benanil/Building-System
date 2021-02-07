
using AnilTools;
using AnilTools.AsyncUpdates;
using UnityEngine;

public class Torna : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private ParticleSystem part;

    [SerializeField] private Transform �ubuk;
    [SerializeField] private float �ubukSpeed;

    [SerializeField] private GameObject finishMenu;

    private void Start()
    {
        this.Delay(1f, () => trigerEnter()).Add(() => { Debug2.Log("okudu"); }, 2f)
                                           .Add(() => { Debug2.Log("okudu1"); }, 3f);
    }

    void Update()
    {
        if (InputPogo.MouseHold)
        {
            �ubuk.localPosition -= Vector3.right * Time.deltaTime * �ubukSpeed;
            if (!part.isPlaying)
            {
                part.Play();
            }
        }

        if (InputPogo.MouseUp)
        {
            part.Stop();
        }
     
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }

    public void trigerEnter()
    {
        finishMenu.SetActive(true);
    }

}
