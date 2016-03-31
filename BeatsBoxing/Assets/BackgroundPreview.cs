using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BackgroundPreview : MonoBehaviour {

    public int Width, Height;
    public Sprite TileSprite;
    public bool ToUpdate = false;
    // Use this for initialization
    void OnValidate()
    {
        Width = Mathf.Max(1, Width);
        Height = Mathf.Max(1, Height);

        if (TileSprite == null) { return; }

        if (ToUpdate)
        {
            //deletes all current children
            var children = new List<GameObject>();
            foreach (Transform child in transform) { children.Add(child.gameObject); }
            children.ForEach(child => UnityEditor.EditorApplication.delayCall += () =>
                {
                    DestroyImmediate(child.gameObject);
                });

            //create new children in the correct locations
            float deltaX = transform.position.x - 0.5f * (Width - 1) * TileSprite.bounds.size.x;
            float deltaY = transform.position.y - 0.5f * (Height - 1) * TileSprite.bounds.size.y;
            for (int r = 0; r < Width * 3; ++r)
            {
                for (int c = 0; c < Height; ++c)
                {
                    GameObject tile = new GameObject("Sprite " + r + ", " + c);
                    SpriteRenderer renderer = tile.AddComponent<SpriteRenderer>();
                    renderer.sprite = TileSprite;
                    renderer.sortingOrder = -10;
                    tile.transform.position = new Vector3(deltaX + r * TileSprite.bounds.size.x, deltaY + c * TileSprite.bounds.size.y);
                    tile.transform.parent = this.transform;

                }
            }
        }
        /**/
    }
}
