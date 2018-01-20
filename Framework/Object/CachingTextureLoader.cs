using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Framework.Debug;
using Framework.Utilities;
using OpenTK.Graphics.OpenGL4;
using Zenseless.HLGL;
using Zenseless.OpenGL;

namespace Framework.Object {

	public static class CachingTextureLoader {

		private static readonly Dictionary<string, ITexture> TEXTURE_CACHE = new Dictionary<string, ITexture>();

		public static ITexture FromArray<T>(T[,] data, PixelInternalFormat internalFormat,
			PixelFormat format, PixelType type, bool invalidateCache = false) {
			throw new ToDevelopException();
		}

		public static ITexture FromBitmap(string cacheName, Func<Bitmap> bitmapCreator, bool invalidateCache = false) {
			// Cleanup old resources if were invalidating and the resource exists
			if (invalidateCache && TEXTURE_CACHE.ContainsKey(cacheName)) {
				TEXTURE_CACHE.GetOrDefault(cacheName).Dispose();
			}

			if (invalidateCache || !TEXTURE_CACHE.ContainsKey(cacheName)) {
				TEXTURE_CACHE.Add(cacheName, TextureLoader.FromBitmap(bitmapCreator.Invoke()));
			}

			return TEXTURE_CACHE.GetOrDefault(cacheName);
		}

		public static ITexture FromStream(Stream stream, bool invalidateCache = false) {
			throw new ToDevelopException();
		}

		public static ITexture FromFile(string fileName, bool invalidateCache = false) {
			throw new ToDevelopException();
		}
	}

}
