using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;

namespace ListView_Euroopa_riigid;

public partial class EuroopaRiigidPage : ContentPage
{
    private ObservableCollection<Euroopa> EuroopaRiigid { get; set; }
    private ListView riigidListView;
    private Button lisaButton, uuendaButton, kustutaButton, valigeLippButton;
    private Entry nimetusEntry, pealinnEntry, rahvastikEntry, infoEntry, keelEntry;
    private Image riigiLipp;
    private string selectedImagePath;
    private Euroopa valitudRiik;

    public EuroopaRiigidPage()
    {
        Title = "Euroopa Riigid";
        BackgroundColor = Color.FromArgb("#103d85");

        EuroopaRiigid = new ObservableCollection<Euroopa>
        {
            new Euroopa {Nimetus = "Eesti", Pealinn = "Tallinn", Rahvastiku_suurus = 1369285, Lipp = "eesti.png", Keel = "eesti keel"},
            new Euroopa {Nimetus = "Soome", Pealinn = "Helsinki", Rahvastiku_suurus = 5568637, Lipp = "soome.png", Keel = "soome keel"},
            new Euroopa {Nimetus = "Läti", Pealinn = "Riia", Rahvastiku_suurus = 1850000, Lipp = "lati.png", Keel = "läti keel"},
            new Euroopa {Nimetus = "Leedu", Pealinn = "Vilnius", Rahvastiku_suurus = 2800000, Lipp = "leedu.png", Keel = "leedu keel"},
            new Euroopa {Nimetus = "Rootsi", Pealinn = "Stockholm", Rahvastiku_suurus = 10500000, Lipp = "rootsi.png", Keel = "rootsi keel"},
            new Euroopa {Nimetus = "o", Pealinn = "o", Rahvastiku_suurus =0, Lipp = "pilt.png", Keel = "o"}
        };

        riigidListView = new ListView
        {
            ItemsSource = EuroopaRiigid,
            ItemTemplate = new DataTemplate(() =>
            {
                var lipp = new Image { WidthRequest = 40, HeightRequest = 40 };
                lipp.SetBinding(Image.SourceProperty, "Lipp");

                var nimi = new Label { FontSize = 18, FontAttributes = FontAttributes.Bold };
                nimi.SetBinding(Label.TextProperty, "Nimetus");

                var pealinn = new Label { FontSize = 14 };
                pealinn.SetBinding(Label.TextProperty, new Binding("Pealinn", stringFormat: "Pealinn: {0}"));

                return new ViewCell
                {
                    View = new HorizontalStackLayout
                    {
                        Padding = new Thickness(10),
                        Children = { lipp, new VerticalStackLayout { Children = { nimi, pealinn } } }
                    }
                };
            })
        };

        riigidListView.ItemTapped += (s, e) =>
        {
            if (e.Item is Euroopa riik)
            {
                valitudRiik = riik;
                nimetusEntry.Text = riik.Nimetus;
                pealinnEntry.Text = riik.Pealinn;
                rahvastikEntry.Text = riik.Rahvastiku_suurus.ToString();
                keelEntry.Text = riik.Keel;
                riigiLipp.Source = riik.Lipp;
            }
        };

        nimetusEntry = new Entry { Placeholder = "Riigi nimi" };
        pealinnEntry = new Entry { Placeholder = "Pealinn" };
        rahvastikEntry = new Entry { Placeholder = "Rahvastiku suurus", Keyboard = Keyboard.Numeric };
        keelEntry = new Entry { Placeholder = "Riigi keel" };
        riigiLipp = new Image { WidthRequest = 100, HeightRequest = 100 };

        valigeLippButton = new Button { Text = "Valige Lipp" };
        valigeLippButton.Clicked += ValigeLipp;

        lisaButton = new Button { Text = "Lisa" };
        lisaButton.Clicked += LisaRiik;

        uuendaButton = new Button { Text = "Uuenda" };
        uuendaButton.Clicked += UuendaRiik;

        kustutaButton = new Button { Text = "Kustuta" };
        kustutaButton.Clicked += KustutaRiik;

        Content = new VerticalStackLayout
        {
            Padding = new Thickness(20),
            Children =
            {
                riigidListView,
                nimetusEntry,
                pealinnEntry,
                rahvastikEntry,
                keelEntry,
                riigiLipp,
                valigeLippButton,
                new HorizontalStackLayout { Children = { lisaButton, uuendaButton, kustutaButton } }
            }
        };
    }

    private async void ValigeLipp(object sender, EventArgs e)
    {
        var fileResult = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Valige riigi lipp"
        });

        if (fileResult != null)
        {
            selectedImagePath = fileResult.FullPath;
            riigiLipp.Source = ImageSource.FromFile(selectedImagePath);
        }
    }

    private void LisaRiik(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nimetusEntry.Text) ||
            string.IsNullOrWhiteSpace(pealinnEntry.Text) ||
            !int.TryParse(rahvastikEntry.Text, out int rahvastik)) return;

        EuroopaRiigid.Add(new Euroopa
        {
            Nimetus = nimetusEntry.Text,
            Pealinn = pealinnEntry.Text,
            Rahvastiku_suurus = rahvastik,
            Keel = keelEntry.Text,
            Lipp = selectedImagePath ?? "pilt.png"
        });
    }

    private void UuendaRiik(object sender, EventArgs e)
    {
        if (valitudRiik == null) return;
        valitudRiik.Nimetus = nimetusEntry.Text;
        valitudRiik.Pealinn = pealinnEntry.Text;
        valitudRiik.Rahvastiku_suurus = int.Parse(rahvastikEntry.Text);
        valitudRiik.Keel = keelEntry.Text;
        valitudRiik.Lipp = selectedImagePath ?? valitudRiik.Lipp;

        riigidListView.ItemsSource = null;
        riigidListView.ItemsSource = EuroopaRiigid;
    }

    private void KustutaRiik(object sender, EventArgs e)
    {
        if (valitudRiik != null)
        {
            EuroopaRiigid.Remove(valitudRiik);
            valitudRiik = null;
        }
    }
}
