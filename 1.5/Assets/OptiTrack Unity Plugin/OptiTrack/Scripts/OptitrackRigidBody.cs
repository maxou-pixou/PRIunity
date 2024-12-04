/* 
Copyright © 2016 NaturalPoint Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License. 
*/

using System;
using UnityEngine;

/// <summary>
/// Implements live tracking of streamed OptiTrack rigid body data onto an object.
/// </summary>
public class OptitrackRigidBody : MonoBehaviour
{
    [Tooltip("The object containing the OptiTrackStreamingClient script.")]
    public OptitrackStreamingClient StreamingClient;

    [Tooltip("The Streaming ID of the rigid body in Motive")]
    public Int32 RigidBodyId;

    [Tooltip("Subscribes to this asset when using Unicast streaming.")]
    public bool NetworkCompensation = true;

    [Tooltip("Offset to apply to the position.")]
    public Vector3 positionOffset = Vector3.zero; // Permet d'ajuster la position.

    [Tooltip("Offset to apply to the rotation.")]
    public Vector3 rotationOffset = Vector3.zero; // Permet d'ajuster la rotation.

#if UNITY_2017_1_OR_NEWER
    void OnEnable()
    {
        Application.onBeforeRender += OnBeforeRender;
    }

    void OnDisable()
    {
        Application.onBeforeRender -= OnBeforeRender;
    }
#endif

    void Start()
    {
        // If the user didn't explicitly associate a client, find a suitable default.
        if (this.StreamingClient == null)
        {
            this.StreamingClient = OptitrackStreamingClient.FindDefaultClient();

            // If we still couldn't find one, disable this component.
            if (this.StreamingClient == null)
            {
                Debug.LogError(GetType().FullName + ": Streaming client not set, and no " + typeof(OptitrackStreamingClient).FullName + " components found in scene; disabling this component.", this);
                this.enabled = false;
                return;
            }
        }

        this.StreamingClient.RegisterRigidBody(this, RigidBodyId);
    }

    void Update()
    {
        ReceiveOptiTrackData();
        UpdatePose();
    }

#if UNITY_2017_1_OR_NEWER
    void OnBeforeRender()
    {
        UpdatePose();
    }
#endif

    /// <summary>
    /// Updates the position and rotation of the GameObject based on the OptiTrack data, including offsets.
    /// </summary>
    void UpdatePose()
    {
        // Retrieve the latest rigid body state.
        OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId, NetworkCompensation);
        if (rbState != null)
        {
            // Apply the offsets to position and rotation.
            Vector3 adjustedPosition = rbState.Pose.Position + positionOffset;
            Quaternion adjustedRotation = rbState.Pose.Orientation * Quaternion.Euler(rotationOffset);

            // Update the transform of the object.
            transform.localPosition = adjustedPosition;
            transform.localRotation = adjustedRotation;
        }
    }

    /// <summary>
    /// Example method for receiving OptiTrack data and applying offsets.
    /// </summary>
    void ReceiveOptiTrackData()
    {
        // Récupérer l'état du rigid body à partir de l'ID configuré
        OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId, NetworkCompensation);

        if (rbState != null)
        {
            // Appliquer l'offset à la position et la rotation
            Vector3 adjustedPosition = rbState.Pose.Position + positionOffset;
            Quaternion adjustedRotation = rbState.Pose.Orientation * Quaternion.Euler(rotationOffset);

            // Mettre à jour le transform de l'objet
            transform.position = adjustedPosition;
            transform.rotation = adjustedRotation;

            Debug.Log($"Données OptiTrack reçues. Position: {adjustedPosition}, Rotation: {adjustedRotation.eulerAngles}");
        }
        else
        {
            Debug.LogWarning($"Pas de données disponibles pour le Rigid Body ID: {RigidBodyId}");
        }
    }

}
