using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColorChanger : MonoBehaviour
{
    private Tilemap tilemap;
    public TileBase[] backgroundTiles;

    // The time delay in seconds
    public float changeInterval = 2f; 
    private int currentTileIndex = 0;

    void Start()
    {
        tilemap = GetComponent<Tilemap>(); 

        if (tilemap == null)
        {
            Debug.LogError("Tilemap component not found on this GameObject.");
            return;
        }

        if (backgroundTiles == null || backgroundTiles.Length < 2)
        {
            Debug.LogError("Please assign at least two background tiles to the array!");
            return;
        }
        InvokeRepeating(nameof(ChangeTileImage), 0f, changeInterval);
    }

    private void ChangeTileImage()
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (var pos in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                tilemap.SetTile(pos, backgroundTiles[currentTileIndex]);
            }
        }
        
        currentTileIndex = (currentTileIndex + 1) % backgroundTiles.Length; 
    }
}
