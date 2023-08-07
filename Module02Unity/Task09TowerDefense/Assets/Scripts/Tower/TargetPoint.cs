using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    public Vector3 Position => transform.position;
    public Enemy Enemy { get; private set; }

    public float ColliderSize { get; private set; }

    private void Awake()
    {
        Enemy = transform.root.GetComponent<Enemy>();

        ColliderSize = GetComponent<BoxCollider>().size.x * transform.localScale.x;
    }
}