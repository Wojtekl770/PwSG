using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MediaPlayer;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Album> Albums { get; set; }

    public MainWindow()
    {
        Albums = new List<Album>()
        {
            new Album()
            {
                Title = "The Book of Souls",
                Author = "Iron Maiden",
                SmallImageURI = @"images/BookOfSouls_small.jpg",
                BigImageURI = @"images/BookOfSouls_big.jpg",
                Songs =
                [
                    new Song()
                    {
                        Title = "If Eternity Should Fail",
                        Rating = 5,
                        Length = new TimeSpan(0, 8, 28),
                        Authors = new List<string>()
                        {
                            "Dickinson"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Speed of Light",
                        Rating = 4,
                        Length = new TimeSpan(0, 5, 1),
                        Authors = new List<string>()
                        {
                            "Smith",
                            "Dickinson"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "The Great Unknown",
                        Rating = 3,
                        Length = new TimeSpan(0, 6, 37),
                        Authors = new List<string>()
                        {
                            "Smith",
                            "Harris"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "The Red and the Black",
                        Rating = 4,
                        Length = new TimeSpan(0, 13, 33),
                        Authors = new List<string>()
                        {
                            "Harris"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "When the River Runs Deep",
                        Rating = 5,
                        Length = new TimeSpan(0, 5, 52),
                        Authors = new List<string>()
                        {
                            "Smith",
                            "Harris"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "The Book of Souls",
                        Rating = 5,
                        Length = new TimeSpan(0, 10, 27),
                        Authors = new List<string>()
                        {
                            "Gers",
                            "Harris"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Death or Glory",
                        Rating = 2,
                        Length = new TimeSpan(0, 5, 13),
                        Authors = new List<string>()
                        {
                            "Smith",
                            "Dickinson"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Shadows of the Valley",
                        Rating = 1,
                        Length = new TimeSpan(0, 7, 32),
                        Authors = new List<string>()
                        {
                            "Gers",
                            "Harris"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Tears of a Clown",
                        Rating = 3,
                        Length = new TimeSpan(0, 4, 59),
                        Authors = new List<string>()
                        {
                            "Smith",
                            "Harris"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "The Man of Sorrows",
                        Rating = 5,
                        Length = new TimeSpan(0, 6, 28),
                        Authors = new List<string>()
                        {
                            "Murray",
                            "Harris"
                        },
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Empire of the Clouds",
                        Rating = 5,
                        Length = new TimeSpan(0, 18, 01),
                        Authors = new List<string>()
                        {
                            "Dickinson"
                        }
                    }.LoadText()
                ]
            },
            new Album()
            {
                Title = "Moonglow",
                Author = "Avantasia",
                SmallImageURI = "images/Moonglow_small.jpg",
                BigImageURI = "images/Moonglow_big.jpg",
                Songs =
                [
                    new Song()
                    {
                        Title = "Ghost in the Moon",
                        Rating = 5,
                        Length = new TimeSpan(0, 9, 51),
                        Authors = new List<string>()
                        {
                            "Sammet"
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "Book of Shallows",
                        Rating = 3,
                        Length = new TimeSpan(0, 5, 0),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Kürsch",
                            "Atkins",
                            "Lande",
                            "Petrozza"
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "Moonglow",
                        Rating = 5,
                        Length = new TimeSpan(0, 3, 56),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Night"
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "The Raven Child",
                        Rating = 4,
                        Length = new TimeSpan(0, 11, 16),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Kürsch",
                            "Lande"
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "Starlight",
                        Rating = 4,
                        Length = new TimeSpan(0, 3, 38),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Atkins"
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "Invincible",
                        Rating = 3,
                        Length = new TimeSpan(0, 3, 7),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Tate"
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "Alchemy",
                        Rating = 5,
                        Length = new TimeSpan(0, 7, 28),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Tate"
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "The Piper at the Gates of Dawn",
                        Rating = 3,
                        Length = new TimeSpan(0, 7, 20),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Atkins",
                            "Lande",
                            "Martin",
                            "Catley",
                            "Tate"
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "Lavender",
                        Rating = 4,
                        Length = new TimeSpan(0, 4, 30),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Catley",
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "Requiem for a Dream",
                        Rating = 5,
                        Length = new TimeSpan(0, 6, 8),
                        Authors = new List<string>()
                        {
                            "Sammet",
                            "Kiske",
                        }
                    }.LoadText(),

                    new Song()
                    {
                        Title = "Maniac",
                        Rating = 5,
                        Length = new TimeSpan(0, 4, 31),
                        Authors = new List<string>()
                        {
                            "Martin",
                        }
                    }.LoadText(),
                ]
            },
            new Album()
            {
                Title = "Violator",
                Author = "Depeche Mode",
                BigImageURI = @"images/Violator_big.jpg",
                SmallImageURI = @"images/Violator_small.png",
                Songs = new List<Song>()
                {
                    new Song()
                    {
                        Title = "World in My Eyes",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan"
                        },
                        Length = new TimeSpan(0, 4, 26),
                        Rating = 3,
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Sweetest Perfection",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan"
                        },
                        Length = new TimeSpan(0, 4, 43),
                        Rating = 4,
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Personal Jesus",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan"
                        },
                        Length = new TimeSpan(0, 4, 56),
                        Rating = 4,
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Halo",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan"
                        },
                        Length = new TimeSpan(0,4,30),
                        Rating = 3,
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Waiting for the Night",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan"
                        },
                        Length = new TimeSpan(0,6,7),
                        Rating = 3,
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Enjoy the Silence",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan",
                            "Fletcher"
                        },
                        Length = new TimeSpan(0,6,12),
                        Rating = 5,
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Policy of Truth",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan"
                        },
                        Length = new TimeSpan(0,4,55),
                        Rating = 5,
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Blue Dress",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan"
                        },
                        Length = new TimeSpan(0,5,41),
                        Rating = 3,
                    }.LoadText(),
                    new Song()
                    {
                        Title = "Clean",
                        Authors = new List<string>()
                        {
                            "Gore",
                            "Gahan"
                        },
                        Length = new TimeSpan(0,5,32),
                        Rating = 4,
                    }.LoadText()
                }
            }
        };
        InitializeComponent();
        DataContext = this;
    }
}
