using Grpc.Core;
using SightX2gRPC;

namespace gPRC4AspNetCore.Services
{
    public class UploaderService : Uploader.UploaderBase
    {
        private ILogger<UploaderService> _logger;
        private IConfiguration _config;


        public UploaderService(ILogger<UploaderService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }


        public override async Task<UploadFileRespone> UploadFile(IAsyncStreamReader<UploadFileRequest> requestStream, ServerCallContext context)
        {
            string fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}";
            string uploadPath = Path.Combine(_config["UploadSavePath"], $"{DateTime.Now:yyyyMMdd}");
            string uploadFilePath = Path.Combine(uploadPath, $"{fileName}.jpg");
            Directory.CreateDirectory(uploadPath);

            _logger.LogInformation($"Upload File Path: {uploadFilePath}");

            await using FileStream fs = File.Create(uploadFilePath);

            await foreach (UploadFileRequest request in requestStream.ReadAllAsync())
            {
                //if (request.Metadata != null)
                //{
                //    string uploadJsonPath = Path.Combine(uploadPath, $"{fileName}.json");

                //    _logger.LogInformation($"Upload JSon Path: {uploadJsonPath}，File Size: {request.Metadata.FileSize}");

                //    await File.WriteAllTextAsync(uploadJsonPath, request.Metadata.ToString());
                //}

                //if (request.Data != null)
                //{
                    await fs.WriteAsync(request.Data.Memory);
                //}
            }

            return new UploadFileRespone { Id = fileName };
        }
    }
}
