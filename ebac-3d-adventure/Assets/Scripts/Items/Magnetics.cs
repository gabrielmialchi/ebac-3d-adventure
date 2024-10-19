using UnityEngine;

public class Magnetics : MonoBehaviour
{
    public float distance = 1f;
    public float coinSpeed = 3f;

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerBase.Instance.transform.position) > distance)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, PlayerBase.Instance.transform.position, Time.deltaTime * coinSpeed);
        }
        if (Vector3.Distance(transform.position, PlayerBase.Instance.transform.position) >= .1f)
        {
        }
    }
}
