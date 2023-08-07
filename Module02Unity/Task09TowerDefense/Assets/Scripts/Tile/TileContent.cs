using UnityEngine;

[SelectionBase]
public class TileContent : MonoBehaviour
{
    [SerializeField] private TileContentType _type;

    public TileContentType Type => _type;

    public TileContentFactory OriginFactory { get; set; }

    public bool IsBlockingPath => Type == TileContentType.Wall || Type == TileContentType.Tower;

    public void Recycle()
    {
        OriginFactory.Reclaim(this);
    }

    public virtual void GameUpdate()
    {

    }
}