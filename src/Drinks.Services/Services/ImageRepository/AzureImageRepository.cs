// Copyright © Anton Paar GmbH, 2019

using System;
using System.Globalization;
using System.Threading.Tasks;
using Drinks.Model;


namespace Drinks.Services.ImageRepository
{
	/// <summary>
	/// An implementation that loads images from Azure.
	/// </summary>
	public sealed class AzureImageRepository :
		IImageRepository
	{
		/// <summary>
		/// Gets the source URI for the given image.
		/// </summary>
		public async Task<Uri> GetById(ImageId id)
		{
			var fileName = id.Guid.ToString("B", CultureInfo.InvariantCulture);

			return new Uri(
				$"https://amarok.blob.core.windows.net/drinks/{fileName}.jpg"
			);
		}
	}
}
