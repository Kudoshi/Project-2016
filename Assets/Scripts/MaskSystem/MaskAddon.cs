
using UnityEngine;

[System.Serializable]
public class MaskAddon
{
    public Sprite FaceBase;
    public Sprite FaceAddon;
    public Sprite EyesAddon;
    public Sprite MouthAddon;
    public Sprite AccessoryAddon;

    public MaskAddon()
    {
    }

    public MaskAddon(Sprite faceBase, Sprite faceAddon, Sprite eyesAddon, Sprite mouthAddon, Sprite accessoryAddon)
    {
        FaceBase = faceBase;
        FaceAddon = faceAddon;
        EyesAddon = eyesAddon;
        MouthAddon = mouthAddon;
        AccessoryAddon = accessoryAddon;
    }

    public override bool Equals(object obj)
    {
        if (obj is not MaskAddon other)
            return false;

        return FaceAddon == other.FaceAddon &&
               EyesAddon == other.EyesAddon &&
               MouthAddon == other.MouthAddon &&
               AccessoryAddon == other.AccessoryAddon;
    }
}