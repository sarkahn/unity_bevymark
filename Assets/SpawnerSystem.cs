using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SpawnerSystem : SystemBase
{
    EntityCommandBufferSystem _barrier;

    protected override void OnCreate()
    {
        _barrier = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        if( UnityEngine.Input.GetMouseButton(0))
        {
            var ecb = _barrier.CreateCommandBuffer();
            var dt = Time.DeltaTime;

            Entities.ForEach((Entity entity, int nativeThreadIndex, in BirdSpawner spawner) =>
            {
                int count = (int)((float)spawner.BirdsPerSecond * dt);

                for (int i = 0; i < count; ++i)
                {
                    var rand = Random.CreateFromIndex((uint)nativeThreadIndex);

                    var e = ecb.Instantiate(spawner.BirdPrefab);

                    ecb.SetComponent(e, new Bird
                    {
                        Velocity = new float2(rand.NextFloat(5f, 15f))
                    });

                    ecb.SetComponent(e, new Translation
                    {
                        Value = new float3(0, 2.5f, (float)i * 0.001f)
                    });

                }

            }).Schedule();

            _barrier.AddJobHandleForProducer(Dependency);
        }
    }
}
