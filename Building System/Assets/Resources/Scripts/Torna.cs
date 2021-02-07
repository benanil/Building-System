
using AnilTools;
using AnilTools.AsyncUpdates;
using UnityEngine;

public class Torna : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private ParticleSystem part;

    [SerializeField] private Transform Çubuk;
    [SerializeField] private float çubukSpeed;

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
            Çubuk.localPosition -= Vector3.right * Time.deltaTime * çubukSpeed;
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
