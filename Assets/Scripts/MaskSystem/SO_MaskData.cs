
using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_MaskData", menuName = "Scriptable Objects/SO_MaskData")]
public class SO_MaskData : ScriptableObject
{
    public SerializedDictionary<MaskBaseType, Sprite> _faceBaseDict;
    public SerializedDictionary<MaskBaseType, SerializedDictionary<Sprite, ColorType>> _faceAddonDict;
    public SerializedDictionary<Sprite, ColorType> _eyesAddonDict;
    public SerializedDictionary<Sprite, ColorType> _mouthAddonDict;
    public SerializedDictionary<Sprite, ColorType> _accessoryAddonDict;
    public SerializedDictionary<ColorType, Sprite> _buttonDict;

    
    public MaskAddon GenerateMaskAddon()
    {
        List<ColorType> colorType = new List<ColorType>()
            {  ColorType.RED, ColorType.YELLOW, ColorType.GREEN, ColorType.BLUE };

        // Get colors 
        ColorType faceColor = colorType[UnityEngine.Random.Range(0, colorType.Count)];
        colorType.Remove(faceColor);
        ColorType eyesColor = colorType[UnityEngine.Random.Range(0, colorType.Count)];
        colorType.Remove(eyesColor);
        ColorType mouthColor = colorType[UnityEngine.Random.Range(0, colorType.Count)];
        colorType.Remove(mouthColor);
        ColorType accessoryColor = colorType[0];

        // Assign sprites
        MaskBaseType baseType = _faceBaseDict.ElementAt(UnityEngine.Random.Range(0, _faceBaseDict.Count)).Key;
        Sprite baseFaceSprite = _faceBaseDict[baseType];

        SerializedDictionary<Sprite, ColorType> faceAddonDict = _faceAddonDict[baseType];
        Sprite faceSprite = GetRandomAddon(faceAddonDict, faceColor);
        Sprite eyesSprite = GetRandomAddon(_eyesAddonDict, eyesColor);
        Sprite mouthSprite = GetRandomAddon(_mouthAddonDict, mouthColor);
        Sprite accessorySprite = GetRandomAddon(_accessoryAddonDict, accessoryColor);

        return new MaskAddon(baseFaceSprite, faceSprite, eyesSprite, mouthSprite, accessorySprite);
    }

    public Sprite GetRandomAddon(SerializedDictionary<Sprite, ColorType> addonType, ColorType targetColor)
    {
        var matchingSprites = addonType
            .Where(kvp => kvp.Value == targetColor)
            .Select(kvp => kvp.Key)
            .ToList();

        if (matchingSprites.Count == 0)
            return null; // or throw / fallback

        int randomIndex = UnityEngine.Random.Range(0, matchingSprites.Count);

        return matchingSprites[randomIndex];
    }

    public ColorType GetColorTypeBySprite(MaskAddonType maskAddonType,  Sprite sprite)
    {
        SerializedDictionary<Sprite, ColorType> addonDict;

        if (maskAddonType == MaskAddonType.EYES_ADDON)
        {
            addonDict = _eyesAddonDict;
            return addonDict[sprite];
        }
        else if (maskAddonType == MaskAddonType.MOUTH_ADDON)
        {
            addonDict = _mouthAddonDict;
            return addonDict[sprite];

        }
        else if (maskAddonType == MaskAddonType.ACCESSORY_ADDON)
        {
            addonDict = _accessoryAddonDict;
            return addonDict[sprite];

        }
        else
        {
            TryFindFaceSprite(sprite, out MaskBaseType maskBaseType, out ColorType colorType);
            return colorType;

        }
    }

    private bool TryFindFaceSprite(Sprite targetSprite, out MaskBaseType maskBaseType, out ColorType colorType)
    {
        var result = _faceAddonDict
            .SelectMany(outer => outer.Value,
                        (outer, inner) => new
                        {
                            MaskBaseType = outer.Key,
                            Sprite = inner.Key,
                            ColorType = inner.Value
                        })
            .FirstOrDefault(x => x.Sprite == targetSprite);

        if (result == null)
        {
            maskBaseType = default;
            colorType = default;
            return false;
        }

        maskBaseType = result.MaskBaseType;
        colorType = result.ColorType;
        return true;
    }

    public Sprite GetButtonSprite(ColorType colorType)
    {
        return _buttonDict[colorType];
    }
}


public enum ColorType
{
    RED, YELLOW, GREEN, BLUE
}

public enum MaskBaseType
{
    BASE_A, BASE_B, BASE_C, BASE_D
}

public enum MaskAddonType
{
    BASE_MASK, FACE_ADDON, EYES_ADDON, MOUTH_ADDON, ACCESSORY_ADDON
}
