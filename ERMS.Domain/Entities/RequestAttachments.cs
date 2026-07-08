using System;
namespace ERMS.Domain.Entities
{

    public class RequestAttachments
    {
        public int AttachmentId { get; set; }


        public int RequestId { get; set; }  //FK --> Request
        public Request Request { get; set; } 

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public int FileSize { get; set; }

        public DateTime UploadedAt { get; set; }




    }

}
	

