using UnityEngine;

public class TileCheck : MonoBehaviour
{
    public bool whiteTile;

    public bool blackTile;

    public LevelManager levelManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("White"))
            whiteTile = true;
        if (other.CompareTag("Black"))
            blackTile = true;

        if (levelManager && levelManager.settings.infiniteLevel)
        {
            if (levelManager.settings.levelObjects[levelManager.settings.levelObjects.Count - 10].gameObject == other.gameObject)
            {
                levelManager.CreateConnectLevel();
            }        
        }       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("White"))
            whiteTile = false;
        if (other.CompareTag("Black"))
            blackTile = false;
    }
}
