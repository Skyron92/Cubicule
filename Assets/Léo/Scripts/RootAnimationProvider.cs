using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RootAnimationProvider : MonoBehaviour
{
   [SerializeField] private List<Transform> joints;
   [SerializeField] private List<Transform> targets;
   [SerializeField] private float animationDuration;

   private void Start() {
      GrowUp();
   }

   private void GrowUp() {
      for(var i = 0; i < joints.Count; i++) MoveJoint(i);
   }

   private void MoveJoint(int index) {
      joints[index].DOMove(targets[index].position, animationDuration, true);
   }
}
