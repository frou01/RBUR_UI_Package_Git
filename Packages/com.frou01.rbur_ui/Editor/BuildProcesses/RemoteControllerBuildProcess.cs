using frou01.RigidBodyTrain;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace frou01.RBUR_UI.Editor
{
    public class RemoteControllerBuildProcess : IProcessSceneWithReport
    {
        public int callbackOrder => -1;

        public void OnProcessScene(Scene scene, BuildReport report)
        {
            TrainManager trainManager = null;
            foreach (GameObject obj in scene.GetRootGameObjects())
            {
                trainManager = obj.GetComponent<TrainManager>();
                if (trainManager != null) break;
            }
            List<Train> trains = new List<Train>();
            List<RemoteController> remoteControllers = new List<RemoteController>();
            foreach (GameObject obj in scene.GetRootGameObjects())
            {
                trains.AddRange(obj.GetComponentsInChildren<Train>(true));
                remoteControllers.AddRange(obj.GetComponentsInChildren<RemoteController>(true));
            }
            foreach (RemoteController remoteController in remoteControllers)
            {
                if (remoteController.trainManager == null)
                {
                    remoteController.trainManager = trainManager;
                }
            }
            if(remoteControllers.Count > 0)
            {
                foreach (Train train in trains)
                {
                    if (!train.gameObject.GetComponent<ConstantForce>()) train.gameObject.AddComponent<ConstantForce>();
                }
            }
        }
    }
}
