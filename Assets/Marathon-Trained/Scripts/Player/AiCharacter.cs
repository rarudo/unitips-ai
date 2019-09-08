using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCharacter : CharacterBase {
    public GroundDetector GroundDetector => groundDetector;

    [SerializeField] private List<RayDetector> rayDetectors;

    public List<RayDetector> RayDetectors => rayDetectors;

    public int JumpCount => jumpCount;

}