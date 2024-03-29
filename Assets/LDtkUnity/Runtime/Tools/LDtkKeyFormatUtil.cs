﻿using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;

namespace LDtkUnity
{
    /// <summary>
    /// A utility class used in conjunction with <see cref="LDtkArtifactAssets"/> to get certain assets by name.
    /// </summary>
    public static class LDtkKeyFormatUtil
    {
        private static readonly StringBuilder Sb = new StringBuilder();
        
        //these dictionaries do not need to be statically cleared and can hold onto information when possible, seems safe enough to cache these dictionary entries between imports.
        //But if there's bugs, then be vigilant.
        private static readonly Dictionary<string, string> RelPathToName = new Dictionary<string, string>();
        private static readonly Dictionary<TileKeyFormat, string> KeyToAssetName = new Dictionary<TileKeyFormat, string>();

        /// <summary>
        /// Creates a formatted string usable for getting a <see cref="LDtkIntGridTile"/> by name in the importer.
        /// </summary>
        /// <param name="intGridLayerDef">
        /// The layer definition used for it's identifier.
        /// </param>
        /// <param name="def">
        /// The definition of the IntGrid Value.
        /// </param>
        /// <returns>
        /// A formatted string for getting an IntGrid Value serialized in the importer's IntGridValues.
        /// </returns>
        public static string IntGridValueFormat(LayerDefinition intGridLayerDef, IntGridValueDefinition def)
        {
            return $"{intGridLayerDef.Identifier}_{def.Value}";
        }
        internal static string IntGridValueFormat(string layerIdentifier, string intGridValue)
        {
            return $"{layerIdentifier}_{intGridValue}";
        }
        
        /// <summary>
        /// Creates a formatted string usable for getting a sprite by name in the imported <see cref="LDtkArtifactAssets"/> object.
        /// </summary>
        /// <param name="assetName">
        /// The texture's name.
        /// </param>
        /// <param name="srcRect">
        /// The source rectangle.
        /// </param>
        /// <returns>
        /// A formatted string for getting a Sprite or art tile from the importer's imported sprites.
        /// </returns>
        public static string TileKeyFormat(string assetName, Rect srcRect)
        {
            //performance critical
            TileKeyFormat format = new TileKeyFormat()
            {
                AssetName = assetName,
                SrcRect = srcRect,
            };

            if (!KeyToAssetName.ContainsKey(format))
            {
                Sb.Clear();
                Sb.Append(assetName);
                Sb.Append('_');
                Sb.Append(srcRect.x);
                Sb.Append('_');
                Sb.Append(srcRect.y);
                Sb.Append('_');
                Sb.Append(srcRect.width);
                Sb.Append('_');
                Sb.Append(srcRect.height);
                
                KeyToAssetName.Add(format, Sb.ToString());
            }

            return KeyToAssetName[format];
            
            //this was slow
            //return $"{assetName}_{srcRect.x}_{srcRect.y}_{srcRect.width}_{srcRect.height}";
        }
        
        //needed when creating the asset.
        internal static string GetCreateSpriteOrTileAssetName(Rect rect, Texture2D tex)
        {
            if (tex == null)
            {
                LDtkDebug.LogError("Tried getting sprite/tile asset name for rect but the texture was null. Returning null instead");
                return null;
            }
            
            Rect imageSliceCoord = LDtkCoordConverter.ImageSlice(rect, tex.height);
            return TileKeyFormat(tex.name, imageSliceCoord);
        }
        
        //used when getting the created assets from artifacts.
        internal static string GetGetterSpriteOrTileAssetName(Rect rect, string assetRelPath, int texHeight)
        {
            Profiler.BeginSample("GetGetterSpriteOrTileAssetName");

            Profiler.BeginSample("GetFileNameWithoutExtension");
            if (!RelPathToName.ContainsKey(assetRelPath))
            {
                RelPathToName.Add(assetRelPath, Path.GetFileNameWithoutExtension(assetRelPath));
            }
            Profiler.EndSample();

            Profiler.BeginSample("ImageSlice");
            Rect imageSliceCoord = LDtkCoordConverter.ImageSlice(rect, texHeight);
            Profiler.EndSample();
            
            Profiler.BeginSample("TileKeyFormat");
            string tileKeyFormat = TileKeyFormat(RelPathToName[assetRelPath], imageSliceCoord);
            Profiler.EndSample();
            
            Profiler.EndSample();
            return tileKeyFormat;
        }
    }
}