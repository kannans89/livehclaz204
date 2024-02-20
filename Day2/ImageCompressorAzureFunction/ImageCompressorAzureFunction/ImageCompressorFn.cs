using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

namespace ImageCompressorAzureFunction
{
    public class ImageCompressorFn
    {
        private readonly ILogger<ImageCompressorFn> _logger;

        public ImageCompressorFn(ILogger<ImageCompressorFn> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ImageCompressorFn))]
        public async Task RunAsync([BlobTrigger("mydocuments/{name}", Connection = "MyConnectionString")] Stream imageStream, string name)
        {

            try
            {
                _logger.LogInformation($"Processing image: {name}");

                // Read the uploaded image into a byte array
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await imageStream.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    // Generate the thumbnail
                    byte[] thumbnailBytes = await ImageThumbnailConverterService.GenerateThumbnailAsync(imageBytes, 100, 100);

                    string storageConnectionString = Environment.GetEnvironmentVariable("MyConnectionString");

                    // Create a CloudStorageAccount instance from the connection string
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

                    // Create a CloudBlobClient to interact with Blob storage
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference("mythumbnails");
                    CloudBlockBlob thumbnailBlob = container.GetBlockBlobReference(name);
                    thumbnailBlob.Properties.ContentType = "image/jpeg";
                    using (MemoryStream thumbnailStream = new MemoryStream(thumbnailBytes))
                    {
                        await thumbnailBlob.UploadFromStreamAsync(thumbnailStream);
                    }
                    // Write the thumbnail to the output blob
                    //  await thumbnailStream.WriteAsync(thumbnailBytes, 0, thumbnailBytes.Length);


                    _logger.LogInformation($"Thumbnail generated and uploaded: {name}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing image: {ex.Message}");
            }

        }
    }
}
