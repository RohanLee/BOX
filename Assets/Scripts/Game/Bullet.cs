using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 pos;
    void Awake()
    {
        pos = this.transform.position;
    }
    
    void Update()
    {
        this.transform.Translate(this.transform.forward * 10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Pool.Recycle(this.gameObject, 200, pos);
    }
}
