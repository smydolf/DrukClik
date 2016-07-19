using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DrukClik.Credential
{
    public static class GoogleDriveAccess
    {
        public static void GetAccess()
        {
            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/drive-dotnet-quickstart.json

            string[] Scopes = { DriveService.Scope.DriveReadonly };
            string ApplicationName = "DrukClik2";

            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (file.Id == "18CpBSdWcKsDTPWKb5z2CXvKWCgdwTcP6gQFAThSJzm4") DowloadingData(service, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }

        }
      static void  DowloadingData(DriveService driveService, string fileId)
        {  
            var request = driveService.Files.Export(fileId, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            var stream = new System.IO.MemoryStream();
            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            //request.MediaDownloader.ProgressChanged +=
            //        (IDownloadProgress progress) =>
            //        {
            //            switch (progress.Status)
            //            {
            //                case DownloadStatus.Downloading:
            //                    {
            //                        Console.WriteLine(progress.BytesDownloaded);
            //                        break;
            //                    }
            //                case DownloadStatus.Completed:
            //                    {
            //                        Console.WriteLine("Download complete.");
            //                        break;
            //                    }
            //                case DownloadStatus.Failed:
            //                    {
            //                        Console.WriteLine("Download failed.");
            //                        break;
            //                    }
            //            }
            //        };
            using (FileStream file = new FileStream("SMYDAEXEL.xlsx", FileMode.Create, FileAccess.Write))
            {
                request.Download(stream);
                stream.WriteTo(file);
            }
        }
    }

}
