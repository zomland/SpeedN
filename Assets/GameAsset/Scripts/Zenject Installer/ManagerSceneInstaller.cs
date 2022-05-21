using System.Collections;
using System.Collections.Generic;
using FirebaseHandler;
using UnityEngine;
using Zenject;

namespace Runtime.ZenjectInstaller
{
    public class ManagerSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<FirebaseAuthHandler>().AsSingle();
            Container.Bind<FirebaseDatabaseHandler>().AsSingle();
        }
    }
}

