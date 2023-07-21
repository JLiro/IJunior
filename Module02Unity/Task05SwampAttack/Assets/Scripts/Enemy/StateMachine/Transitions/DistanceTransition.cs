using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;
    [SerializeField] private float _rengetSpread;

    private void Start() => _transitionRange += Random.Range(-_rengetSpread, _rengetSpread);

    private void Update()
    {
        if(Target != null && Vector2.Distance(transform.position, Target.transform.position) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}
