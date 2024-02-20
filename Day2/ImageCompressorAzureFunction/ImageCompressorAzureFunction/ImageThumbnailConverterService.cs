using SixLabors.ImageSharp.Formats.Jpeg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCompressorAzureFunction
{
    internal class ImageThumbnailConverterService
    {
        public static async Task<byte[]> GenerateThumbnailAsync(byte[] imageBytes, int width, int height)
        {
            try
            {
                using (var image = await SixLabors.ImageSharp.Image.LoadAsync(new MemoryStream(imageBytes)))
                {
                    // Resize the image to the specified width and height while preserving aspect ratio
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new Size(width, height),
                        Mode = ResizeMode.Max
                    }));

                    using (var outputStream = new MemoryStream())
                    {
                        // Save the thumbnail as a JPEG image (you can change the format as needed)
                        await image.SaveAsync(outputStream, new JpegEncoder());

                        // Convert the thumbnail to a byte array
                        return outputStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating thumbnail: {ex.Message}");
                return null;
            }
        }
    }
}
