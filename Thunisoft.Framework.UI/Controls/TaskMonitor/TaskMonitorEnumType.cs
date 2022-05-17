using System.ComponentModel;

namespace Thunisoft.Framework.UI.Controls.TaskMonitor
{
    public enum TaskRunModelEnum
    {
        SINGLE = 0,
        PARALLEL = 1,
        GROUP = 2,
    }
    public enum TaskStatusEnum
    {
        Ready = 1,
        InProgress = 2,
        Completed = 3,
        Error = 4,
        Cancel = 5,
        Hangup = 6,
        OutOfControl = 7,
        ErrorCanRetry = 8,
    }
    public enum FileTypeEnum
    {
        [Description(".doc")]
        DOC = 1,
        [Description(".docx")]
        DOCX,
        [Description(".pdf")]
        PDF = 2,
        [Description(".png")]
        PNG = 3,
        [Description(".jpg")]
        JPG = 4,
        [Description(".bmp")]
        BMP = 5,
        [Description(".gif")]
        GIF = 6,
        [Description(".jpeg")]
        JPEG = 7,
        [Description(".mp3")]
        MP3 = 8,
        [Description(".mp4")]
        MP4 = 9,
        OTHER = 0,
    }
}
