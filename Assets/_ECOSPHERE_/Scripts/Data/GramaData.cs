using UnityEngine;

[System.Serializable]
public class GramaData
{
    public Vector3 position;

    public GramaData(GameObject grama)
    {
        position = grama.transform.position;
    }
}
