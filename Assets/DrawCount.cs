using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class DrawCount : MonoBehaviour
{
    EntityQuery _query;

    private void Start()
    {
        _query = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(Bird));
    }

    private void OnGUI()
    {
        // Button so it has a background
        GUI.backgroundColor = Color.black;
        GUILayout.Button($"Bird count: {_query.CalculateEntityCount()}");
    }
}
