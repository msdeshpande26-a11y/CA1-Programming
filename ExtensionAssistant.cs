using System;
using System.Collections.Generic;

public class ExtensionAssistant
{
    private Dictionary<string, FileExtension> extensions;

    public ExtensionAssistant()
    {
        extensions = new Dictionary<string, FileExtension>(StringComparer.OrdinalIgnoreCase);

        // Video extensions
        AddExtension(new VideoExtension(".mp4", "MPEG-4 Video File"));
        AddExtension(new VideoExtension(".avi", "Audio Video Interleave File"));
        AddExtension(new VideoExtension(".mov", "Apple QuickTime Movie"));
        AddExtension(new VideoExtension(".mkv", "Matroska Video File"));
        AddExtension(new VideoExtension(".webm", "WebM Video File"));

        // Audio extensions
        AddExtension(new AudioExtension(".mp3", "MP3 Audio File"));
        AddExtension(new AudioExtension(".wav", "Waveform Audio File"));
        AddExtension(new AudioExtension(".flac", "Free Lossless Audio Codec File"));
        AddExtension(new AudioExtension(".aac", "Advanced Audio Coding File"));
        AddExtension(new AudioExtension(".ogg", "Ogg Vorbis Audio File"));

        // Image extensions
        AddExtension(new ImageExtension(".jpg", "JPEG Image File"));
        AddExtension(new ImageExtension(".png", "Portable Network Graphics Image"));
        AddExtension(new ImageExtension(".gif", "Graphics Interchange Format File"));
        AddExtension(new ImageExtension(".bmp", "Bitmap Image File"));
        AddExtension(new ImageExtension(".tiff", "Tagged Image File Format"));

        // Generic extensions
        AddExtension(new GenericExtension(".pdf", "Portable Document Format File"));
        AddExtension(new GenericExtension(".docx", "Microsoft Word Document"));
        AddExtension(new GenericExtension(".xlsx", "Microsoft Excel Spreadsheet"));
        AddExtension(new GenericExtension(".pptx", "Microsoft PowerPoint Presentation"));
        AddExtension(new GenericExtension(".zip", "Compressed Archive File"));
    }

    private void AddExtension(FileExtension extension)
    {
        extensions[extension.ExtensionName] = extension;
    }

    public void QueryExtension(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Please enter a valid file extension.");
            return;
        }

        if (!input.StartsWith("."))
        {
            input = "." + input;
        }

        if (extensions.TryGetValue(input, out FileExtension ext))
        {
            ext.DisplayInfo();
        }
        else
        {
            Console.WriteLine("[ERROR] Extension '" + input + "' not recognized.");
        }
    }
}
