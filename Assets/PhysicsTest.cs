﻿using UnityEngine;
using Unity.BEPUphysics;
using Unity.BEPUphysics.Components;
using Unity.Entities;
using System.Collections.Generic;

public class PhysicsTest : MonoBehaviour
{
    private const int MAX_CHANGES_PER_FRAME = 100;
    private EntityManager _entityManager;
    private List<Entity> _entities = new List<Entity>();

    private void Awake()
    {
        _entityManager = World.Active.GetOrCreateManager<EntityManager>();
        var e = _entityManager.CreateEntity();
        _entityManager.AddSharedComponentData(e, new PhysicsObject { });
    }

    private void Update()
    {
        var val = Random.value;
        if (val > 0.75f)
        {
            var changes = Random.Range(0, MAX_CHANGES_PER_FRAME);
            for (int i = 0; i < changes; i++)
            {
                var e = _entityManager.CreateEntity();
                _entities.Add(e);
                PhysicsSystem.Add(e);
            }
        }
        else if (val < 0.25f && _entities.Count > 0)
        {
            var changes = Mathf.Min(_entities.Count, Random.Range(0, MAX_CHANGES_PER_FRAME));
            for (int i = 0; i < changes; i++)
            {
                var index = Random.Range(0, _entities.Count);
                var e = _entities[index];
                PhysicsSystem.Remove(e);
                _entities.RemoveAt(index);
            }
        }
    }
}
