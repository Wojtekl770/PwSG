
using System.Drawing;
using System.IO;

namespace MediaPlayer;



public class Album
{
    public string Title { get; set; }
    public string SmallImageURI  { get; set; }
    public string BigImageURI  { get; set; }
    public List<Song> Songs { get; set; }
    public string Author { get; set; }
}

public class Song
{
    public string Title { get; set; }
    public string Text { get; set; } = "";
    public TimeSpan Length { get; set; }
    
    public List<string> Authors { get; set; }
    public int Rating { get; set; }

    public Song LoadText()
    {
        Text = File.ReadAllText($"./texts/{new string(Title.Replace(" ",""))}.txt");
        return this;
    }
}