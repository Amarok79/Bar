// Copyright © Anton Paar GmbH, 2019

using System;
using System.Globalization;
using Drinks.Model;


namespace Drinks.Services.ImageRepository
{
	/// <summary>
	/// </summary>
	public sealed class AzureImageRepository :
		IImageRepository
	{
		public Uri GetById(ImageId id)
		{
			var fileName = id.Guid.ToString("B", CultureInfo.InvariantCulture);

			return new Uri(
				$"https://amarok.blob.core.windows.net/drinks/{fileName}.jpg"
			);
		}
	}
}
