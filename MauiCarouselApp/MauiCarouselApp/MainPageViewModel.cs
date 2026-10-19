using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiCarouselApp;

public class MainPageViewModel : INotifyPropertyChanged
{
    public ObservableCollection<LanguageItem> Languages { get; set; }

    private LanguageItem _currentLanguage;
    public LanguageItem CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            _currentLanguage = value;
            OnPropertyChanged();
        }
    }

    public MainPageViewModel()
    {
        Languages = new ObservableCollection<LanguageItem>
        {
            new LanguageItem
            {
                Name = "C#",
                DescriptionEn = "Powerful object-oriented language",
                DescriptionEt = "Võimas objektorienteeritud keel",
                ImageUrl = "csharp.png",
                YearCreated = 2000,
                HelloWorldCode = "Console.WriteLine(\"Hello World\");"
            },
            new LanguageItem
            {
                Name = "Python",
                DescriptionEn = "Great for data and automation",
                DescriptionEt = "Suurepärane andmetöötluseks ja automatiseerimiseks",
                ImageUrl = "python.png",
                YearCreated = 1991,
                HelloWorldCode = "print(\"Hello World\")"
            },
            new LanguageItem
            {
                Name = "JavaScript",
                DescriptionEn = "The language of the web",
                DescriptionEt = "Veebiarenduse põhikeel",
                ImageUrl = "javascript.png",
                YearCreated = 1995,
                HelloWorldCode = "console.log(\"Hello World\");"
            },
            new LanguageItem
            {
                Name = "Java",
                DescriptionEn = "Write once, run anywhere",
                DescriptionEt = "Kirjuta kord, käivita igal pool",
                ImageUrl = "java.png",
                YearCreated = 1995,
                HelloWorldCode = "System.out.println(\"Hello World\");"
            },
            new LanguageItem
            {
                Name = "C++",
                DescriptionEn = "High-performance system language",
                DescriptionEt = "Suure jõudlusega süsteemikeel",
                ImageUrl = "cpp.png",
                YearCreated = 1985,
                HelloWorldCode = "std::cout << \"Hello World\";"
            }
        };

        CurrentLanguage = Languages[0];
        StartAutoScroll();
    }

    private void StartAutoScroll()
    {
        // Taimer vahetab kaarti iga 4 sekundi järel
        Application.Current.Dispatcher.StartTimer(TimeSpan.FromSeconds(4), () =>
        {
            if (Languages.Count == 0) return false;

            int nextIndex = Languages.IndexOf(CurrentLanguage) + 1;
            if (nextIndex >= Languages.Count)
            {
                nextIndex = 0; // Alustab algusest
            }

            CurrentLanguage = Languages[nextIndex];
            return true;
        });
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
