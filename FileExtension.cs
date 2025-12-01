using System;

public abstract class FileExtension
{
    public string ExtensionName { get; private set; }
    public string Description { get; private set; }

    public FileExtension(string extension, string description)
    {
        ExtensionName = extension;
        Description = description;
    }

    public abstract void DisplayInfo();
}

public class VideoExtension : FileExtension
{
    public VideoExtension(string extension, string description)
        : base(extension, description) { }

    public override void DisplayInfo()
    {
        Console.WriteLine("[VIDEO] Extension: " + ExtensionName);
        Console.WriteLine("Description: " + Description);
    }
}

public class AudioExtension : FileExtension
{
    public AudioExtension(string extension, string description)
        : base(extension, description) { }

    public override void DisplayInfo()
    {
        Console.WriteLine("[AUDIO] Extension: " + ExtensionName);
        Console.WriteLine("Description: " + Description);
    }
}

public class ImageExtension : FileExtension
{
    public ImageExtension(string extension, string description)
        : base(extension, description) { }

    public override void DisplayInfo()
    {
        Console.WriteLine("[IMAGE] Extension: " + ExtensionName);
        Console.WriteLine("Description: " + Description);
    }
}

public class GenericExtension : FileExtension
{
    public GenericExtension(string extension, string description)
        : base(extension, description) { }

    public override void DisplayInfo()
    {
        Console.WriteLine("[FOUND] Extension: " + ExtensionName);
        Console.WriteLine("Description: " + Description);
    }
}
