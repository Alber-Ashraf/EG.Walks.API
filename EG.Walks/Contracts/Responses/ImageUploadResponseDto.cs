﻿namespace EG.Walks.Contracts.Responses
{
    public class ImageUploadResponseDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string FileExtention { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
