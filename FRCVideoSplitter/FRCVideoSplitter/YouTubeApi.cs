using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace FRCVideoSplitter
{
    internal class VideoUploader
    {
        UserCredential credential;

        public async Task<int> SetCredentials()
        {
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(Assembly.GetExecutingAssembly().GetName().Name)
                );
            }
            return 0;
        }

        public String CreatePlaylist(String name)
        {
            if (credential == null)
                return null;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            var newPlaylist = new Playlist();
            newPlaylist.Snippet = new PlaylistSnippet();
            newPlaylist.Snippet.Title = name;
            newPlaylist.Snippet.Description = name;
            newPlaylist.Status = new PlaylistStatus();
            newPlaylist.Status.PrivacyStatus = "public";
            newPlaylist = youtubeService.Playlists.Insert(newPlaylist, "snippet,status").Execute();
            return newPlaylist.Id;
        }

        public void AddToPlaylist(String playlistId, String videoId)
        {
            if (credential == null)
                return;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = playlistId;
            newPlaylistItem.Snippet.ResourceId = new ResourceId();
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            newPlaylistItem.Snippet.ResourceId.VideoId = videoId;
            newPlaylistItem = youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").Execute();
        }

        public void Upload(String title, String description, String path)
        {
            if (credential == null)
                return;
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = title;
            video.Snippet.Description = description;
            video.Snippet.CategoryId = "28"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            video.Status = new VideoStatus();
            video.Status.PrivacyStatus = "public"; // or "private" or "public"
            var filePath = path;

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet, status", fileStream, "video/");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                videosInsertRequest.Upload();
            }
        }

        public EventHandler<long> Upload_ProgressChanged;
        public EventHandler<String> Upload_Failed;
        
        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress uploadProgress)
        {
            switch (uploadProgress.Status)
            {
                case UploadStatus.Uploading:
                    EventHandler<long> ea = Upload_ProgressChanged;
                    if (ea != null)
                        ea(this, uploadProgress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    EventHandler<String> eb = Upload_Failed;
                    if (eb != null)
                        eb(this, uploadProgress.Exception.ToString());
                    Console.WriteLine("An error prevented the upload from completing.\n{0}", uploadProgress.Exception);
                    break;
            }
        }

        public EventHandler<String> UploadCompleted;

        void videosInsertRequest_ResponseReceived(Video video)
        {
            EventHandler<String> ea = UploadCompleted;
            if (UploadCompleted != null)
            {
                ea(this, video.Id);
            }
        }
    }
}
