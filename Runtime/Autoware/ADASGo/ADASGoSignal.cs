﻿#region License
/******************************************************************************
* Copyright 2019 The AutoCore Authors. All Rights Reserved.
* 
* Licensed under the GNU Lesser General Public License, Version 3.0 (the "License"); 
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
* 
* https://www.gnu.org/licenses/lgpl-3.0.html
* 
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*****************************************************************************/
#endregion


using UnityEngine;

namespace AutoCore.MapToolbox.Autoware
{
    class ADASGoSignal : MonoBehaviour, IADASMapGameObject
    {
        public GameObject GameObject => gameObject;
        public MonoBehaviour MonoBehaviour => this;
        public ADASMapSignal.Type signalType = ADASMapSignal.Type.BLUE;
        public ADASGoPole pole;
        public ADASGoLane linkLane;
        public CollectionADASLane CollectionLane { get; set; }
        public CollectionADASPole CollectionPole { get; set; }
        public CollectionADASSignal CollectionSignal { get; set; }
        ADASMapSignal data;
        public ADASMapSignal Signal 
        {
            set
            {
                data = value;
                if (data != null)
                {
                    CollectionSignal?.Add(data.ID, this);
                    name = data.ID.ToString();
                    signalType = data.SignalType;
                    transform.position = data.Vector.Point.Position;
                    transform.rotation = data.Vector.Rotation;
                    if (CollectionPole != null && data.Pole != null)
                    {
                        pole = CollectionPole[data.Pole.ID];
                    }
                    if (CollectionLane != null && data.LinkLane != null)
                    {
                        linkLane = CollectionLane[data.LinkLane.ID];
                    }
                }
            }
            get
            {
                if (data == null)
                {
                    data = new ADASMapSignal
                    {
                        Vector = new ADASMapVector
                        {
                            Point = new ADASMapPoint
                            {
                                Position = transform.position
                            },
                            Rotation = transform.rotation
                        },
                        SignalType = signalType
                    };
                    if (linkLane != null)
                    {
                        data.LinkLane = linkLane.Lane;
                    }
                }
                return data;
            }
        }
        internal void BuildData()
        {
            Signal = null;
            data = Signal;
        }
        private void OnDrawGizmos()
        {
            switch (signalType)
            {
                case ADASMapSignal.Type.RED:
                case ADASMapSignal.Type.RED_LEFT:
                    Gizmos.color = Color.red;
                    if (linkLane != null)
                    {
                        Gizmos.DrawLine(transform.position, linkLane.transform.position);
                    }
                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.DrawFrustum(Vector3.zero, 30, 1, 0.1f, 1);
                    break;
                case ADASMapSignal.Type.BLUE:
                case ADASMapSignal.Type.BLUE_LEFT:
                    Gizmos.color = Color.green;
                    if (linkLane != null)
                    {
                        Gizmos.DrawLine(transform.position, linkLane.transform.position);
                    }
                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.DrawFrustum(Vector3.zero, 30, 1, 0.1f, 1);
                    break;
                case ADASMapSignal.Type.YELLOW:
                case ADASMapSignal.Type.YELLOW_LEFT:
                    Gizmos.color = Color.yellow;
                    if (linkLane != null)
                    {
                        Gizmos.DrawLine(transform.position, linkLane.transform.position);
                    }
                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.DrawFrustum(Vector3.zero, 30, 1, 0.1f, 1);
                    break;
                case ADASMapSignal.Type.PEDESTRIAN_RED:
                    Gizmos.color = Color.cyan;
                    if (linkLane != null)
                    {
                        Gizmos.DrawLine(transform.position, linkLane.transform.position);
                    }
                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.DrawFrustum(Vector3.zero, 30, 1, 0.1f, 1);
                    break;
                case ADASMapSignal.Type.PEDESTRIAN_BLUE:
                    Gizmos.color = Color.blue;
                    if (linkLane != null)
                    {
                        Gizmos.DrawLine(transform.position, linkLane.transform.position);
                    }
                    Gizmos.matrix = transform.localToWorldMatrix;
                    Gizmos.DrawFrustum(Vector3.zero, 30, 1, 0.1f, 1);
                    break;
                case ADASMapSignal.Type.OTHER:
                    break;
                default:
                    break;
            }
        }
    }
}