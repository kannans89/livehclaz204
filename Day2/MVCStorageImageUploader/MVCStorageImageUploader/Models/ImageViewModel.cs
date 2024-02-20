namespace MVCStorageImageUploader.Models
{
    public class ImageUploadViewModel
    {
        public string? ImageCaption { set; get; }
        public string? ImageDescription { set; get; }
        public IFormFile? Image { set; get; }
    }
}
