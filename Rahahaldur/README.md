# RahaHaldur

Lihtne rahahaldur Androidile (.NET MAUI). Rakendusega saab pidada arvet sissetulekute ja kulude üle, näha kokkuvõtet (tulud, kulud, saldo) ning sorteerida kulusid kategooriatesse lohistades.

## Funktsioonid

- Sissetulekute ja kulude lisamine, muutmine ja kustutamine (SQLite)
- Kokkuvõte: tulud, kulud, saldo
- Kulude sorteerimine kategooriatesse **drag & drop** abil (Toit, Transport, Meelelahutus, Muu)
- Kaks keelt: eesti ja inglise, vahetub kohe, ilma rakendust taaskäivitamata
- Hele ja tume teema
- Taustamuusika ja heliefektid (salvestamine, kustutamine, kategooria muutmine), mille saab seadetes välja lülitada
- Seaded (keel, teema, heli) jäävad meelde ka pärast rakenduse sulgemist

## Tehnoloogiad

- .NET MAUI (C#, XAML), Shell-navigatsioon
- MVVM muster
- SQLite: `sqlite-net-pcl` + `SQLitePCLRaw.bundle_green`
- Lokaliseerimine: `.resx` failid (`AppResources.resx` inglise, `AppResources.et.resx` eesti)
- Heli: `Plugin.Maui.Audio`
- Seaded: `Preferences`
- Kujundus: MAUI Styles, `AppThemeBinding`, StyleClass, DataTemplate

## Projekti struktuur

```
Models/                    MoneyEntry (andmetabel), Categories
ViewModels/                BaseViewModel, MainViewModel, EditViewModel, SortViewModel, SettingsViewModel
Views/                     MainPage, EditPage, SortPage, SettingsPage
Services/                  DatabaseService, SettingsService, ThemeService,
                           LocalizationService, AudioService, NavigationService
Resources/Localization/    AppResources.resx, AppResources.et.resx, TranslateExtension
Resources/Images/          ikoonid (svg)
Resources/Raw/             helifailid (wav)
Resources/Styles/          Colors.xaml, Styles.xaml
Platforms/Android/         Androidi käivitusfailid
```

ViewModelid ei viita ühelegi UI-elemendile. Nad kasutavad ainult andmeid ja käske (`ICommand`). Lehtede vahetamine ja dialoogid käivad `NavigationService` kaudu.

## Käivitamine

1. Ava `RahaHaldur.csproj` Visual Studios (vajalik MAUI workload).
2. Kui projekt ei ehitu, vaata `RahaHaldur.csproj` esimest rida: `net9.0-android` peab vastama sinu paigaldatud .NET versioonile (nt `net10.0-android`).
3. Vali Androidi emulaator ja vajuta Start.

## Drag & drop kasutamine

Sorteerimise lehel hoia sõrme (emulaatoris hiirt) kulu peal ja lohista see ühe kategooria peale.

## Heli

Helid (`coin.wav`, `click.wav`, `delete.wav`, `music.wav`) on loodud programmiliselt, seega pole autoriõiguse probleeme.
