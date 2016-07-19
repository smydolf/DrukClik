using System;
using System.ComponentModel.DataAnnotations;

namespace DrukClik.Data
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public DateTime DownloadingDateTime { get; set; }
        public TimeSpan DownloadingTimeSpan { get; set; }
        public int NewPosts { get; set; }
        public int PostsWithEmail { get; set; }
    }
}
