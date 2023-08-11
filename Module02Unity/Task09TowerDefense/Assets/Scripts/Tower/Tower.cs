using UnityEngine;

public class Tower : TileContent
{
    [SerializeField, Range(1f, 12f)] private float _targetingRage   = 1f;
    [SerializeField, Range(1f, 50f)] private float _damagePerSecond = 10f;

    private TargetPoint _target;

    [SerializeField] private Transform _turret;
    [SerializeField] private Transform _laserBeam;
    private Vector3 _laserBeamScale;

    private const int enemyLayerMask = 1 << 9;

    private void Awake()
    {
        _laserBeamScale = _laserBeam.localScale;
    }

    public override void GameUpdate()
    {
        if (IsTargetTracked() || IsAcquireTarget())
        {
            Shoot();
        }
        else
        {
            _laserBeam.localScale = Vector3.zero;
        }
    }

    private void Shoot()
    {
        float beamOffset = .5f;

        var point = _target.Position;

        _turret.LookAt(point);
        _laserBeam.localRotation = _turret.localRotation;

        var distance = Vector3.Distance(_turret.position, point);
        _laserBeamScale.z = distance;
        _laserBeam.localScale = _laserBeamScale;
        _laserBeam.localPosition = _turret.localPosition + beamOffset * distance * _laserBeam.forward;

        _target.Enemy.TakeDamage(_damagePerSecond * Time.deltaTime);
    }

    private bool IsAcquireTarget()
    {
        Collider[] targets = Physics.OverlapBox(transform.localPosition, new Vector3(_targetingRage, _targetingRage, _targetingRage), Quaternion.identity, enemyLayerMask);

        if (targets.Length > 0)
        {
            _target = targets[0].GetComponent<TargetPoint>();

            return true;
        }

        _target = null;

        return false;
    }

    private bool IsTargetTracked()
    {
        if (_target == null)
        {
            return false;
        }

        Vector3 myPosition = transform.localPosition;
        Vector3 targetPosition = _target.Position;

        if (Vector3.Distance(myPosition, targetPosition) > _targetingRage)
        {
            _target = null;

            return false;
        }

        return true;
    }
}
