
using Azure.Identity;
using Azure.Storage.Blobs;

string tenantId = "f0093ae3-bfa4-46e1-9b91-668278209d56";
string clientId = "409a5375-6536-483d-a0df-333fa6f11014";
string clientSecret = "4ia8Q~GFeZnMUj93Uaqh8CIlCStc92GCUHxqfbdv";


string blobURI = "https://ksday3store.blob.core.windows.net/images/veggie.jpg";
string filePath = "C:\\temp\\new.jpg";
ClientSecretCredential clientCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

BlobClient blobClient = new BlobClient(new Uri(blobURI), clientCredential);

await blobClient.DownloadToAsync(filePath);

Console.WriteLine("The blob is downloaded");

